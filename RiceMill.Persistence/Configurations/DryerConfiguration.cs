using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;

namespace RiceMill.Persistence.Configurations
{
    public sealed class DryerConfiguration : IEntityTypeConfiguration<Dryer>
    {
        public void Configure(EntityTypeBuilder<Dryer> builder)
        {
            builder.Property(d => d.Id)
                .ValueGeneratedOnAdd();

            builder.Property(d => d.Title)
                .HasMaxLength(30)
                .IsUnicode()
                .IsRequired();

            builder.Property(d => d.IsDeleted)
                .IsRequired();

            builder.Property(d => d.CreateTime)
                .IsRequired();

            builder.Property(d => d.UpdateTime)
                .IsRequired();

            builder
                .HasQueryFilter(d => !d.IsDeleted);

            builder
                .HasOne(d => d.RiceMill)
                .WithMany(rm => rm.Dryers)
                .HasForeignKey(d => d.RiceMillId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(d => d.User)
                .WithMany(u => u.Dryers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}