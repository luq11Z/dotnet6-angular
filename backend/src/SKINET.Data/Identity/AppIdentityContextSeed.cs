using Microsoft.AspNetCore.Identity;
using SKINET.Business.Models.Identity;

namespace SKINET.Data.Identity
{
    public static class AppIdentityContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    Address = new Address
                    {
                        FirstName = "Bob",
                        LastName = "Bobbbby",
                        Street = "10 The Street",
                        City = "New York",
                        State = "NY",
                        Zipcode = "92131"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
