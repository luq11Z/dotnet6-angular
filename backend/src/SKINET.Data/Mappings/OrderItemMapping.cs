using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SKINET.Business.Models.OrderAggregate;

namespace SKINET.Data.Config
{
    public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.OwnsOne(i => i.ItemOrdered, io => { io.WithOwner(); });

            builder.Property(i => i.Price)
                .HasColumnType("decimal(18,2)");

            builder.ToTable("OrderItems");
        }
    }
}
