using BackCore.BLL.Constants;
using BackCore.BLL.Enums;
using BackCore.BLL.ViewModels;
using BackCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;

namespace BackCore.API.Seed
{
    public class SeedData
    {

        private readonly ApplicationDbContext _context;

        private readonly UserManager<User> _userManager;

        private readonly RoleManager<Role> _roleManager;


        public SeedData(UserManager<User> userManager, RoleManager<Role> roleManager, ApplicationDbContext context)

        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public static void Initalize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            context.Database.EnsureCreated();

            if (!_roleManager.Roles.Any(r => r.Name == StaticRoleNames.Admins))
            {
                var role = new Role(){ Name = StaticRoleNames.Admins};
                _roleManager.CreateAsync(role).Wait();

                var AdminUser = new User()
                {
                    UserName = "Rowyda",
                    Email = "rowyda15@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    isActive = true
                };

                IdentityResult result = _userManager.CreateAsync(AdminUser, "123@Rr").Result;
                if (result.Succeeded)
                {
                    var admin = _userManager.FindByEmailAsync("rowyda15@gmail.com").Result;
                    _userManager.AddToRoleAsync(admin, StaticRoleNames.Admins).Wait();
                }

               
            }
            if (!context.Statuses.Any())
            {
                List<Status> statusList = new List<Status>()
                {
                    new Status{ Name = Enum.GetName(typeof(StatusEnum), 1) },
                    new Status{ Name = Enum.GetName(typeof(StatusEnum), 2) },
                    new Status{ Name = Enum.GetName(typeof(StatusEnum), 3) }
                };
                context.Statuses.AddRange(statusList);
            }

            if (!context.Categories.Any())
            {
                List<Category> categoryList = new List<Category>()
                {
                    new Category{ Name = "Toys" },
                    new Category{ Name = "Electronics" },
                    new Category{ Name = "Tools" }
                };
                context.Categories.AddRange(categoryList);
            }

            context.SaveChanges();
        }
    }
}
