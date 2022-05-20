using Microsoft.AspNetCore.Identity;
using SKINET.Business.Models;
using SKINET.Business.Models.Identity;
using System.Text.Json;

namespace SKINET.Data.SeedData
{
    public class AppIdentityContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var rolesData = File.ReadAllText("../SKINET.DATA/SeedData/roles.json");
                var usersData = File.ReadAllText("../SKINET.DATA/SeedData/users.json");

                var roles = JsonSerializer.Deserialize<List<AppRole>>(rolesData);
                var users = JsonSerializer.Deserialize<List<AppUser>>(usersData);

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                    await userManager.AddToRoleAsync(user, "Member");

                    if (user.Email == "admin@test.com")
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                }
            }
        }
    }
}
