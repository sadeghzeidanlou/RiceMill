using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using Shared.Enums;

namespace RiceMill.Persistence.Configurations
{
    public sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .ValueGeneratedOnAdd();

            builder.Property(v => v.Title)
                .HasMaxLength(30)
                .IsUnicode()
                .IsRequired();

            builder.Property(v => v.Plate)
                .HasMaxLength(8)
                .IsUnicode()
                .IsRequired();

            builder.Property(v => v.Description)
                .HasMaxLength(200)
                .IsUnicode();

            builder.Property(v => v.VehicleType)
                .HasConversion(vt => vt.ToString(), vt => (VehicleTypeEnum)Enum.Parse(typeof(VehicleTypeEnum), vt))
                .IsRequired();

            builder.Property(v => v.IsDeleted)
                .IsRequired();

            builder.Property(v => v.CreateTime)
                .IsRequired();

            builder.Property(v => v.UpdateTime)
                .IsRequired();

            builder
                .HasOne(v => v.RiceMill)
                .WithMany(rm => rm.Vehicles)
                .HasForeignKey(v => v.RiceMillId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(v => v.User)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}