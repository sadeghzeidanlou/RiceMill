using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using RiceMill.Persistence.DataBaseExpression;

namespace RiceMill.Persistence.Configurations
{
    public sealed class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .UseIdentityColumn();

            builder.Property(d => d.UnbrokenRice)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(d => d.BrokenRice)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(d => d.ChickenRice)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(d => d.Flour)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(d => d.Description)
                .IsUnicode()
                .HasMaxLength(200);

            builder.Property(d => d.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(d => d.DeliveryTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder.Property(d => d.CreateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder.Property(d => d.UpdateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();
        }
    }
}