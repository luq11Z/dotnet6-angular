using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SKINET.Business.Models.OrderAggregate;

namespace SKINET.Data.Config
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.OwnsOne(o => o.ShipToAddress, a =>
            {
                a.WithOwner();
            });

            builder.Property(s => s.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (OrderStatus) Enum.Parse(typeof(OrderStatus), o.ToString())
                );

            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Orders");
        }
    }
}
