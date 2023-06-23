using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using RiceMill.Persistence.DataBaseExpression;
using Shared.Enums;

namespace RiceMill.Persistence.Configurations
{
    public sealed class DryerHistoryConfiguration : IEntityTypeConfiguration<DryerHistory>
    {
        public void Configure(EntityTypeBuilder<DryerHistory> builder)
        {
            builder.HasKey(dh => dh.Id);

            builder.Property(dh => dh.Id)
                .UseIdentityColumn();

            builder.Property(dh => dh.Operation)
                .HasConversion(o => o.ToString(), o => (DryerOperationEnum)Enum.Parse(typeof(DryerOperationEnum), o))
                .IsRequired();

            builder.Property(dh => dh.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(dh => dh.StartTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder.Property(dh => dh.CreateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder.Property(dh => dh.UpdateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();
        }
    }
}