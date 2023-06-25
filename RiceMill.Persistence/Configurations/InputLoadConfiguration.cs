using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using Shared.Enums;

namespace RiceMill.Persistence.Configurations
{
    public sealed class InputLoadConfiguration : IEntityTypeConfiguration<InputLoad>
    {
        public void Configure(EntityTypeBuilder<InputLoad> builder)
        {
            builder.HasKey(il => il.Id);

            builder.Property(il => il.Id)
                .ValueGeneratedOnAdd();

            builder.Property(il => il.NumberOfBags)
                .IsRequired();

            builder.Property(il => il.Description)
                .IsUnicode()
                .HasMaxLength(200);

            builder.Property(il => il.IsInDryer)
                .IsRequired();

            builder.Property(il => il.ReceiveTime)
                .IsRequired();

            builder.Property(il => il.NoticesType)
                .HasConversion(n => n.ToString(), n => (NoticesTypeEnum)Enum.Parse(typeof(NoticesTypeEnum), n))
                .IsRequired();

            builder.Property(il => il.IsDeleted)
                .IsRequired();

            builder.Property(il => il.CreateTime)
                .IsRequired();

            builder.Property(il => il.UpdateTime)
                .IsRequired();

            builder
                .HasOne(il => il.CarrierPerson)
                .WithMany(p => p.CarrierInputLoads)
                .HasForeignKey(il => il.CarrierPersonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(il => il.DelivererPerson)
                .WithMany(p => p.DelivererInputLoads)
                .HasForeignKey(il => il.DelivererPersonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(il => il.OwnerPerson)
                .WithMany(p => p.OwnedInputLoads)
                .HasForeignKey(il => il.OwnerPersonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(il => il.ReceiverPerson)
                .WithMany(p => p.ReceiverInputLoads)
                .HasForeignKey(il => il.ReceiverPersonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(il => il.RiceMill)
                .WithMany(rm => rm.InputLoads)
                .HasForeignKey(il => il.RiceMillId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(il => il.User)
                .WithMany(u => u.InputLoads)
                .HasForeignKey(il => il.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(il => il.Vehicle)
                .WithMany(v => v.InputLoads)
                .HasForeignKey(il => il.VehicleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(il => il.Village)
                .WithMany(v => v.InputLoads)
                .HasForeignKey(il => il.VillageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(il => il.Payment)
                .WithOne(p => p.InputLoad)
                .HasForeignKey<Payment>(p => p.InputLoadId);
        }
    }
}