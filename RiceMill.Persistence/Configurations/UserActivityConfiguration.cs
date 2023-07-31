using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using Shared.Enums;

namespace RiceMill.Persistence.Configurations
{
    public sealed class UserActivityConfiguration : IEntityTypeConfiguration<UserActivity>
    {
        public void Configure(EntityTypeBuilder<UserActivity> builder)
        {
            builder.Property(ua => ua.Id)
                .ValueGeneratedOnAdd();

            builder.Property(ua => ua.Ip)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(ua => ua.UserActivityType)
                .HasConversion(uat => (byte)uat, uat => (UserActivityTypeEnum)uat)
                .IsRequired();

            builder.Property(ua => ua.EntityType)
                .HasConversion(et => (byte)et, et => (EntityTypeEnum)et)
                .IsRequired();

            builder.Property(ua => ua.ApplicationId)
                .HasConversion(a => (byte)a, a => (ApplicationIdEnum)a)
                .IsRequired();

            builder.Property(ua => ua.BeforeEdit)
                .HasMaxLength(3000)
                .IsUnicode();

            builder.Property(ua => ua.AfterEdit)
                .HasMaxLength(3000)
                .IsUnicode();

            builder.Property(u => u.RiceMillId)
                .IsRequired(false);

            builder.Property(ua => ua.IsDeleted)
                .IsRequired();

            builder.Property(ua => ua.CreateTime)
                .IsRequired();

            builder.Property(ua => ua.UpdateTime)
                .IsRequired();

            builder
                .HasQueryFilter(ua => !ua.IsDeleted);

            builder
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserActivities)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}