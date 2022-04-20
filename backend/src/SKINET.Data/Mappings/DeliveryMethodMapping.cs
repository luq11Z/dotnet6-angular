using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SKINET.Business.Models.OrderAggregate;

namespace SKINET.Data.Mappings
{
    public class DeliveryMethodMapping : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Price)
                .HasColumnType("decimal(18,2)");

            builder.ToTable("DeliveryMethods");
        }
    }
}
