using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using Shared.Enums;

namespace RiceMill.Persistence.Configurations
{
    public sealed class DryerHistoryConfiguration : IEntityTypeConfiguration<DryerHistory>
    {
        public void Configure(EntityTypeBuilder<DryerHistory> builder)
        {
            builder.Property(dh => dh.Id)
                .ValueGeneratedOnAdd();

            builder.Property(dh => dh.Operation)
                .HasConversion(o => (byte)o, o => (DryerOperationEnum)o)
                .IsRequired();

            builder.Property(dh => dh.IsDeleted)
                .IsRequired();

            builder.Property(dh => dh.StartTime)
                .IsRequired();

            builder.Property(dh => dh.EndTime)
                .IsRequired(false);

            builder.Property(dh => dh.CreateTime)
                .IsRequired();

            builder.Property(dh => dh.UpdateTime)
                .IsRequired();

            builder
                .HasQueryFilter(dh => !dh.IsDeleted);

            builder
                .HasOne(dh => dh.InputLoad)
                .WithOne(rt => rt.DryerHistory)
                .HasForeignKey<DryerHistory>(dh => dh.InputLoadId);

            builder
                .HasOne(dh => dh.User)
                .WithMany(u => u.DryerHistories)
                .HasForeignKey(dh => dh.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}