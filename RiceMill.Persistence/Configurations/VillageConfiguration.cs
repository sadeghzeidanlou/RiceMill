using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using RiceMill.Persistence.DataBaseExpression;

namespace RiceMill.Persistence.Configurations
{
    public sealed class VillageConfiguration : IEntityTypeConfiguration<Village>
    {
        public void Configure(EntityTypeBuilder<Village> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .UseIdentityColumn();

            builder.Property(v => v.Title)
                .HasMaxLength(50)
                .IsUnicode()
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