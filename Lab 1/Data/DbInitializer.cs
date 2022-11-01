using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab_1.Models;
using System.Configuration;

namespace Lab_1.Data
{
    public static class DbInitializer
    {
        public static async Task<int> SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
            // create the database if it doesn't exist
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            
            // Check if roles already exist and exit if there are
            if (roleManager.Roles.Count() > 0)
                return 1;  // should log an error message here

            // Seed roles
            int result = await SeedRoles(roleManager);
            if (result != 0)
                return 2;  // should log an error message here
            
            // Check if users already exist and exit if there are
            if (userManager.Users.Count() > 0)
                return 3;  // should log an error message here

            // Seed users
            result = await SeedUsers(userManager);
            if (result != 0)
                return 4;  // should log an error message here
            
            return 0;
        }

        private static async Task<int> SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Create Admin Role
            var result = await roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!result.Succeeded)
                return 1;  // should log an error message here

            result = await roleManager.CreateAsync(new IdentityRole("Manager"));
            if (!result.Succeeded)
                return 2;  // should log an error message here

            // Create Member Role
            result = await roleManager.CreateAsync(new IdentityRole("Player"));
            if (!result.Succeeded)
                return 3;  // should log an error message here

            result = await roleManager.CreateAsync(new IdentityRole("Referee"));
            if (!result.Succeeded)
                return 4;  // should log an error message here

            return 0;
        }

        private static async Task<int> SeedUsers(UserManager<ApplicationUser> userManager)
        {
            var URI = Environment.GetEnvironmentVariable("VaultUri");
            var client = new SecretClient(new Uri(URI), new DefaultAzureCredential());
            var adminPass = client.GetSecret("Admin-Password").Value.Value;
            // Create Admin User
            var adminUser = new ApplicationUser
            {
                UserName = "admin@mohawkcollege.ca",
                Email = "admin@mohawkcollege.ca",
                FirstName = "Admin",
                LastName = "Admin",
                EmailConfirmed = true
            };
            
            var result = await userManager.CreateAsync(adminUser, adminPass);
            if (!result.Succeeded)
                return 1;  // should log an error message here

            // Assign user to Admin role
            result = await userManager.AddToRoleAsync(adminUser, "Admin");
            if (!result.Succeeded)
                return 2;  // should log an error message here

            return 0;
        }
    }
}
