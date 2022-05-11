using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BackCore.Data;
using BackCore.BLL.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BackCore.BLL.Settings;
using Microsoft.Extensions.Options;

namespace BackCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;

        public AccountController(IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager, 
            IMapper mapper, IOptions<JWTSettings> jwtSettings)
        {  _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }



        [HttpPost,Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]UserForLoginViewModel model)
        {
            var user = _userManager.Users.FirstOrDefault(item => item.PhoneNumber == model.UserNameOrPhone || item.Email.ToLower() == model.UserNameOrPhone.ToLower()
                            || item.UserName.ToLower() == model.UserNameOrPhone.ToLower());

            if (user == null) return NotFound();

            if (!user.PhoneNumberConfirmed)
                return BadRequest("your Phone is not ativated");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    expiration = jwtSecurityToken.ValidTo,
                    userName = user.UserName

                });
            }

            return BadRequest("your email or password is incorrect");
        }


        [HttpPost,Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserForRegisterViewModel userForRegisterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);
           
            if (_userManager.Users.Any(item => item.Email == userForRegisterDto.Email))
                return BadRequest("Email Already Token");

            if (_userManager.Users.Any(item => item.PhoneNumber == userForRegisterDto.PhoneNumber))
                return BadRequest("PhoneNumber Already Token");

            User userToCreate = _mapper.Map<User>(userForRegisterDto);
            userToCreate.UserName = userForRegisterDto.Email;
            userToCreate.PhoneNumberConfirmed = true;
            userToCreate.EmailConfirmed = true;
            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);
                if (result.Succeeded)
                    return Ok(userToCreate); 

                return BadRequest(result.Errors);

            }

        


        private async Task<JwtSecurityToken> GenerateJWToken(User user)
        {
            //    var claims = new[]
            //          {
            //    new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
            //      new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            ////    new Claim(ClaimTypes.Name, user.UserName)
            // };

            var claims = new List<Claim>
                      {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
             };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            var jwtSecurityToken = new JwtSecurityToken(
             issuer: _jwtSettings.Issuer,
             audience: _jwtSettings.Audience,
             claims: claims,
             expires: DateTime.UtcNow.AddDays(2),
             signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

    }
}