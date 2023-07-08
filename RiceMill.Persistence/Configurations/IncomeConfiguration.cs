using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;

namespace RiceMill.Persistence.Configurations
{
    public sealed class IncomeConfiguration : IEntityTypeConfiguration<Income>
    {
        public void Configure(EntityTypeBuilder<Income> builder)
        {
            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.Description)
                .IsUnicode()
                .HasMaxLength(200);

            builder.Property(i => i.UnbrokenRice)
                .IsRequired();

            builder.Property(i => i.BrokenRice)
                .IsRequired();

            builder.Property(i => i.Flour)
                .IsRequired();

            builder.Property(i => i.IsDeleted)
                .IsRequired();

            builder.Property(i => i.IncomeTime)
                .IsRequired();

            builder.Property(i => i.CreateTime)
                .IsRequired();

            builder.Property(i => i.UpdateTime)
                .IsRequired();

            builder
                .HasQueryFilter(i => !i.IsDeleted);

            builder
                .HasOne(i => i.RiceThreshing)
                .WithOne(rt => rt.Income)
                .HasForeignKey<RiceThreshing>(il => il.IncomeId);

            builder
                .HasOne(i => i.RiceMill)
                .WithMany(rm => rm.Incomes)
                .HasForeignKey(i => i.RiceMillId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(i => i.User)
                .WithMany(u => u.Incomes)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}