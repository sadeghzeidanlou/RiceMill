using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using RiceMill.Persistence.DataBaseExpression;
using Shared.Enums;

namespace RiceMill.Persistence.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .UseIdentityColumn();

            builder.Property(u => u.Username)
                .HasMaxLength(30)
                .IsUnicode()
                .IsRequired();

            builder.Property(u => u.Password)
                .HasMaxLength(128)
                .IsUnicode()
                .IsRequired();

            builder.Property(u => u.Role)
                .HasConversion(r => r.ToString(), r => (RoleEnum)Enum.Parse(typeof(RoleEnum), r))
                .IsRequired();

            builder.Property(u => u.IsActive)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(u => u.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(u => u.CreateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder.Property(u => u.UpdateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();
        }
    }
}