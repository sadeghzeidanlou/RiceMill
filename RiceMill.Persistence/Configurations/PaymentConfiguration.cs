using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using RiceMill.Persistence.DataBaseExpression;

namespace RiceMill.Persistence.Configurations
{
    public sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .UseIdentityColumn();

            builder.Property(p => p.UnbrokenRice)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(p => p.BrokenRice)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(p => p.Flour)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(p => p.Money)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(p => p.Description)
                .IsUnicode()
                .HasMaxLength(200);

            builder.Property(p => p.PaymentTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder.Property(p => p.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(p => p.CreateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder.Property(p => p.UpdateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();
        }
    }
}