using System;
using Backend_Project_Amado.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Backend_Project_Amado.Data;

namespace P335_BackEnd.Helper
{
    public static class DataSeed
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {

            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string[] roles = new[] { "Admin", "Moderator" };

                foreach (string role in roles)
                {
                    var exists = await roleManager.RoleExistsAsync(role);
                    if (exists) continue;
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                var user = new AppUser
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                };

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var existingUser = await userManager.FindByNameAsync("admin@gmail.com");
                if (existingUser is not null) return;

                await userManager.CreateAsync(user, "admin12345");
                await userManager.AddToRoleAsync(user, roles[0]);

                return;
            }
        }
    }
}