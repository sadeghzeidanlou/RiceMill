using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using RiceMill.Persistence.DataBaseExpression;

namespace RiceMill.Persistence.Configurations
{
    public sealed class DryerConfiguration : IEntityTypeConfiguration<Dryer>
    {
        public void Configure(EntityTypeBuilder<Dryer> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d=>d.Id)
                .ValueGeneratedOnAdd();

            builder.Property(d => d.Title)
                .HasMaxLength(30)
                .IsUnicode()
                .IsRequired();

            builder.Property(d => d.IsDeleted)
                .HasDefaultValue(false)
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