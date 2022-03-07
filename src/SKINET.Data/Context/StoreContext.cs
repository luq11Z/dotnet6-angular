using Microsoft.EntityFrameworkCore;
using SKINET.Business.Models;

namespace SKINET.Data.Context
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Product> Products { get; set; }
    }
}
