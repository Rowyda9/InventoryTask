using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackCore.Data;
using BackCore.BLL.ViewModels;
using Microsoft.EntityFrameworkCore;
using BackCore.BLL.Constants;
using BackCore.Utilities.Paging;

namespace BackCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly IMapper _mapper;
        private RoleManager<IdentityRole> _roleManager;
       
        public UserController(IConfiguration config, UserManager<User> userManager,
            RoleManager<IdentityRole> roleMgr,
            SignInManager<User> signInManager, IMapper mapper)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleMgr;
        }

        [Authorize(Roles = StaticRoleNames.Admins)]
        [Route("GetAll")]
        [HttpPost]
        public async Task<IActionResult> GetAll(PaginatedItemsViewModel pagingparametermodel)
        {
            var pagedResult = new PagedResult<UserForDetailedViewModel>();

            pagingparametermodel.PageNumber = (pagingparametermodel.PageNumber == 0) ? 1 : pagingparametermodel.PageNumber;
            pagingparametermodel.PageSize = (pagingparametermodel.PageSize == 0) ? 20 : pagingparametermodel.PageSize;

            var users = await _userManager.Users.OrderByDescending(a => a.Created).ToListAsync();

            if (!String.IsNullOrEmpty(pagingparametermodel.SearchBy))
            {
                users = users.Where(a => a.UserName.ToLower().Contains(pagingparametermodel.SearchBy)
                               || (!String.IsNullOrEmpty(a.FirstName) && a.FirstName.ToLower().Contains(pagingparametermodel.SearchBy)


                    )).ToList();
            }
            var source = _mapper.Map<List<UserForDetailedViewModel>>(users);
            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = pagingparametermodel.PageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = pagingparametermodel.PageSize;
            pagedResult.TotalCount = source.Count();//
            pagedResult.Result = source.ToList().Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            return Ok(pagedResult);

        }


        [Route("UserInfo/{userName}")]
        public IActionResult UserInfo(string userName)
        {
            var user = _userManager.Users.FirstOrDefault(item => item.PhoneNumber == userName
                                    || item.UserName == userName || item.Email == userName);
            if (user == null)
                return NotFound();

            return Ok(_mapper.Map<UserForDetailedViewModel>(user));

        }


        [Authorize(Roles = StaticRoleNames.Admins)]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(await _roleManager.Roles.ToListAsync());
        }

       
        [Authorize(Roles = StaticRoleNames.Admins)]
        [HttpPost, Route("RoleManagement")]

        public async Task<IActionResult> Role(UserRoleViewModels roleViewModels)
        {
            if (!String.IsNullOrEmpty(roleViewModels.RoleName) && roleViewModels.isAdd)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(roleViewModels.RoleName));
                if (result.Succeeded)
                    return Ok();

                return BadRequest(result.Errors);
            }
            else
            {
                IdentityRole role = await _roleManager.FindByIdAsync(roleViewModels.RoleId);
                if (role != null)
                {
                    IdentityResult result = await _roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                        return Ok();
                    else
                        return BadRequest(result.Errors);
                }
                else
                    return NotFound();

            }

        }

        [Authorize(Roles = StaticRoleNames.Admins)]
        [Route("UserRoleManagement")]
        [HttpPost]
        public async Task<IActionResult> UserRole(UserRoleViewModels userRole)
        {
            IdentityRole role = await _roleManager.FindByNameAsync(userRole.RoleName);

            User user = await _userManager.FindByNameAsync(userRole.UserId);
            if (role == null || user == null) return NotFound();


            if (userRole.isAdd)
            {

                var result = await _userManager.AddToRoleAsync(user, role.Name);
                if (!result.Succeeded) return Ok();

                return BadRequest(result.Errors);
            }
            else
            {
                var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                if (!result.Succeeded) return Ok();

                return BadRequest(result.Errors);
            }
        }

       
        [Authorize(Roles = StaticRoleNames.Admins)]
        [Route("GetUsersInRole")]
        [HttpGet]
        public async Task<IActionResult> GetUsersInRole(string RoleName)
        {

            IdentityRole role = await _roleManager.FindByNameAsync(RoleName);

            if (role == null) return NotFound();

            var users = await _userManager.GetUsersInRoleAsync(role.Name);

            return Ok(_mapper.Map<List<UserForDetailedViewModel>>(users));
        }

       
        [Authorize(Roles = StaticRoleNames.Admins)]
        [HttpGet, Route("RemoveUser/{UserID}")]
        public async Task<IActionResult> RemoveUsers(string UserID)
        {

            var user = await _userManager.FindByIdAsync(UserID);
            if (user == null)
                return NotFound();
            return Ok(await _userManager.DeleteAsync(user));

        }


    }
}