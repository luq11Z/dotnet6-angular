using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SKINET.Business.Models.Identity;
using System.Security.Claims;

namespace SKINET.App.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser> FindUserByClaimsPrincipleWithAddressAsync(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user?.FindFirst(ClaimTypes.Email).Value;

            return await input.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<AppUser> FindByEmailFromClaimsPrinciple(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user?.FindFirst(ClaimTypes.Email).Value;

            return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
        }

        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}
