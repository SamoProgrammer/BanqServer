using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Banq.Authentication
{
    public class AdminsDbInitializer
    {

        public static async Task Initialize(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Adding Admin Role
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            var adminUser = new ApplicationUser
            {
                UserName = "Samo@admin",
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = "samo092813463@gmail.com"
            };

            // Adding Admin User admin@admin.com
            if (await userManager.FindByEmailAsync(adminUser.Email) == null)
            {

                await userManager.CreateAsync(adminUser, "Samo@0928134636");
                await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
            }

            // Repeat for other default roles and users
        }

    }
}