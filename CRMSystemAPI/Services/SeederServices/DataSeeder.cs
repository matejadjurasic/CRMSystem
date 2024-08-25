using CRMSystemAPI.Data;
using CRMSystemAPI.Models.DatabaseModels;
using CRMSystemAPI.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace CRMSystemAPI.Services.SeederServices
{
    public class DataSeeder : IDataSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public DataSeeder( UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedDataAsync()
        {
            await SeedRolesAsync();
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var role = new Role
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };
                await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync("Client"))
            {
                var role = new Role
                {
                    Name = "Client",
                    NormalizedName = "CLIENT"
                };
                await _roleManager.CreateAsync(role);
            }
        }

        private async Task SeedUsersAsync()
        {
            if (await _userManager.FindByEmailAsync("admin@email.com") == null)
            {
                var user = new User
                {
                    Email = "admin@email.com",
                    UserName = "admin",
                    Name = "Admin",
                    NormalizedEmail = "ADMIN@EMAIL.COM",
                    NormalizedUserName = "ADMIN",
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, "Admin123");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
