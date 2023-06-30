using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using Shared.Enums;

namespace RiceMill.Persistence.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd();

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
                .IsRequired();

            builder.Property(u => u.IsDeleted)
                .IsRequired();

            builder.Property(u => u.CreateTime)
                .IsRequired();

            builder.Property(u => u.UpdateTime)
                .IsRequired();

            builder.Property(u => u.UserPersonId)
                .IsRequired(false);

            builder
                .HasQueryFilter(u => !u.IsDeleted);

            builder
                .HasOne(u => u.ParentUser)
                .WithOne()
                .HasForeignKey<User>(u => u.ParentUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}