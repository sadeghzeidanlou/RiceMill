using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;

namespace RiceMill.Persistence.Configurations
{
    public sealed class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .ValueGeneratedOnAdd();

            builder.Property(d => d.UnbrokenRice)
                .IsRequired();

            builder.Property(d => d.BrokenRice)
                .IsRequired();

            builder.Property(d => d.ChickenRice)
                .IsRequired();

            builder.Property(d => d.Flour)
                .IsRequired();

            builder.Property(d => d.Description)
                .IsUnicode()
                .HasMaxLength(200);

            builder.Property(d => d.IsDeleted)
                .IsRequired();

            builder.Property(d => d.DeliveryTime)
                .IsRequired();

            builder.Property(d => d.CreateTime)
                .IsRequired();

            builder.Property(d => d.UpdateTime)
                .IsRequired();

            builder
                .HasOne(d => d.CarrierPerson)
                .WithMany(p => p.CarrierDeliveries)
                .HasForeignKey(d => d.CarrierPersonId);

            builder
                .HasOne(d => d.DelivererPerson)
                .WithMany(p => p.DelivererDeliveries)
                .HasForeignKey(d => d.DelivererPersonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(d => d.ReceiverPerson)
                .WithMany(p => p.ReceiverDeliveries)
                .HasForeignKey(d => d.ReceiverPersonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(d => d.RiceMill)
                .WithMany(rm => rm.Deliveries)
                .HasForeignKey(d => d.RiceMillId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder
                .HasOne(d => d.User)
                .WithMany(u => u.Deliveries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(d => d.Vehicle)
                .WithMany(v => v.Deliveries)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}