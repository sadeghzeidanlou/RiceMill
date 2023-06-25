﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;

namespace RiceMill.Persistence.Configurations
{
    public sealed class RiceThreshingConfiguration : IEntityTypeConfiguration<RiceThreshing>
    {
        public void Configure(EntityTypeBuilder<RiceThreshing> builder)
        {
            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Id)
                .ValueGeneratedOnAdd();

            builder.Property(rt => rt.RiceThreshingStart)
                .IsRequired();

            builder.Property(rt => rt.RiceThreshingEnd)
                .IsRequired();

            builder.Property(rt => rt.UnbrokenRice)
                .IsRequired();

            builder.Property(rt => rt.BrokenRice)
                .IsRequired();

            builder.Property(rt => rt.ChickenRice)
                .IsRequired();

            builder.Property(rt => rt.Flour)
                .IsRequired();

            builder.Property(rt => rt.Description)
                .IsUnicode()
                .HasMaxLength(200);

            builder.Property(rt => rt.IsDelivered)
                .IsRequired();

            builder.Property(rt => rt.IsDeleted)
                .IsRequired();

            builder.Property(rt => rt.CreateTime)
                .IsRequired();

            builder.Property(rt => rt.UpdateTime)
                .IsRequired();

            builder
                .HasOne(rt => rt.User)
                .WithMany(u => u.RiceThreshings)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}