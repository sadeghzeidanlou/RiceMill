using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;

namespace RiceMill.Persistence.Configurations
{
    public sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.UnbrokenRice)
                .IsRequired();

            builder.Property(p => p.BrokenRice)
                .IsRequired();

            builder.Property(p => p.Flour)
                .IsRequired();

            builder.Property(p => p.Money)
                .IsRequired();

            builder.Property(p => p.Description)
                .IsUnicode()
                .HasMaxLength(200);

            builder.Property(p => p.PaymentTime)
                .IsRequired();

            builder.Property(p => p.IsDeleted)
                .IsRequired();

            builder.Property(p => p.CreateTime)
                .IsRequired();

            builder.Property(p => p.UpdateTime)
                .IsRequired();

            builder
                .HasOne(p => p.PaidPerson)
                .WithMany(p => p.Payments)
                .HasForeignKey(p => p.PaidPersonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(p => p.RiceMill)
                .WithMany(p => p.Payments)
                .HasForeignKey(p => p.RiceMillId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(p => p.User)
                .WithMany(p => p.Payments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}