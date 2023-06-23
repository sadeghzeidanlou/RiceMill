using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using RiceMill.Persistence.DataBaseExpression;
using Shared.Enums;

namespace RiceMill.Persistence.Configurations
{
    public sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .UseIdentityColumn();

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
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(v => v.CreateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder.Property(v => v.UpdateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();
        }
    }
}