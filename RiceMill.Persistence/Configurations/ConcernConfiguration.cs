using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using RiceMill.Persistence.DataBaseExpression;

namespace RiceMill.Persistence.Configurations
{
    public sealed class ConcernConfiguration : IEntityTypeConfiguration<Concern>
    {
        public void Configure(EntityTypeBuilder<Concern> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Title)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(c => c.CreateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder.Property(c => c.UpdateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();
        }
    }
}