using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;

namespace RiceMill.Persistence.Configurations
{
    public sealed class DeliveryRiceThreshingConfiguration : IEntityTypeConfiguration<DeliveryRiceThreshing>
    {
        public void Configure(EntityTypeBuilder<DeliveryRiceThreshing> builder)
        {
            builder.Property(drt => drt.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasIndex(drt => new { drt.DeliveryId, drt.RiceThreshingId }).IsUnique();

            builder
                .HasQueryFilter(drt => !drt.IsDeleted);

            builder
                .HasOne(drt => drt.Delivery)
                .WithMany(d => d.DeliveryRiceThreshings)
                .HasForeignKey(drt => drt.DeliveryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(drt => drt.RiceThreshing)
                .WithMany(d => d.DeliveryRiceThreshings)
                .HasForeignKey(drt => drt.RiceThreshingId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}