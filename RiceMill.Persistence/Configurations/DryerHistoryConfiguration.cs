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
            builder.HasKey(dh => dh.Id);

            builder.Property(dh => dh.Id)
                .ValueGeneratedOnAdd();

            builder.Property(dh => dh.Operation)
                .HasConversion(o => o.ToString(), o => (DryerOperationEnum)Enum.Parse(typeof(DryerOperationEnum), o))
                .IsRequired();

            builder.Property(dh => dh.IsDeleted)
                .IsRequired();

            builder.Property(dh => dh.StartTime)
                .IsRequired();

            builder.Property(dh => dh.CreateTime)
                .IsRequired();

            builder.Property(dh => dh.UpdateTime)
                .IsRequired();

            builder
                .HasOne(dh => dh.RiceThreshing)
                .WithMany(rt => rt.DryerHistories)
                .HasForeignKey(dh => dh.RiceThreshingId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(dh => dh.User)
                .WithMany(u => u.DryerHistories)
                .HasForeignKey(dh => dh.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}