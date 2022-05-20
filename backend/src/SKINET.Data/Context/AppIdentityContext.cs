using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SKINET.Business.Models;
using SKINET.Business.Models.Identity;

namespace SKINET.Data.Context
{
    public class AppIdentityContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
