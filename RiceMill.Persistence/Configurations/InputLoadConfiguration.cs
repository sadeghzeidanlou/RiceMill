﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using RiceMill.Persistence.DataBaseExpression;
using Shared.Enums;

namespace RiceMill.Persistence.Configurations
{
    public sealed class InputLoadConfiguration : IEntityTypeConfiguration<InputLoad>
    {
        public void Configure(EntityTypeBuilder<InputLoad> builder)
        {
            builder.HasKey(il => il.Id);

            builder.Property(il => il.Id)
                .ValueGeneratedOnAdd();

            builder.Property(il => il.NumberOfBags)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(il => il.Description)
                .IsUnicode()
                .HasMaxLength(200);

            builder.Property(il => il.IsInDryer)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(il => il.ReceiveTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder.Property(il => il.NoticesType)
                .HasConversion(n => n.ToString(), n => (NoticesTypeEnum)Enum.Parse(typeof(NoticesTypeEnum), n))
                .IsRequired();

            builder.Property(il => il.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(il => il.CreateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder.Property(il => il.UpdateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder
                .HasOne(il => il.CarrierPerson)
                .WithMany(p => p.CarrierInputLoads)
                .HasForeignKey(il => il.CarrierPersonId);

            builder
                .HasOne(il => il.DelivererPerson)
                .WithMany(p => p.DelivererInputLoads)
                .HasForeignKey(il => il.DelivererPersonId);

            builder
                .HasOne(il => il.OwnerPerson)
                .WithMany(p => p.OwnedInputLoads)
                .HasForeignKey(il => il.OwnerPersonId);

            builder
              .HasOne(il => il.Payment)
              .WithOne(p => p.InputLoad)
              .HasForeignKey<Payment>(p => p.InputLoadId);
        }
    }
}