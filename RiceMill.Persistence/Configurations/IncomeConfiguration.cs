using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using RiceMill.Persistence.DataBaseExpression;

namespace RiceMill.Persistence.Configurations
{
    public sealed class IncomeConfiguration : IEntityTypeConfiguration<Income>
    {
        public void Configure(EntityTypeBuilder<Income> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .UseIdentityColumn();

            builder.Property(i => i.Description)
                .IsUnicode()
                .HasMaxLength(200);

            builder.Property(i => i.UnbrokenRice)
               .HasDefaultValue(0)
               .IsRequired();

            builder.Property(i => i.BrokenRice)
               .HasDefaultValue(0)
               .IsRequired();

            builder.Property(i => i.Flour)
               .HasDefaultValue(0)
               .IsRequired();

            builder.Property(i => i.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(i => i.IncomeTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder.Property(i => i.CreateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder.Property(i => i.UpdateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();
        }
    }
}