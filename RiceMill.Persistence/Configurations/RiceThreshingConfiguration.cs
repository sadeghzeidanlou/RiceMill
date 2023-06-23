using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using RiceMill.Persistence.DataBaseExpression;

namespace RiceMill.Persistence.Configurations
{
    public sealed class RiceThreshingConfiguration : IEntityTypeConfiguration<RiceThreshing>
    {
        public void Configure(EntityTypeBuilder<RiceThreshing> builder)
        {
            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Id)
                .UseIdentityColumn();

            builder.Property(rt => rt.RiceThreshingStart)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder.Property(rt => rt.RiceThreshingEnd)
                .IsRequired();

            builder.Property(rt => rt.UnbrokenRice)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(rt => rt.BrokenRice)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(rt => rt.ChickenRice)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(rt => rt.Flour)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(rt => rt.Description)
                .IsUnicode()
                .HasMaxLength(200);

            builder.Property(rt => rt.IsDelivered)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(rt => rt.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(rt => rt.CreateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder.Property(rt => rt.UpdateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();
        }
    }
}