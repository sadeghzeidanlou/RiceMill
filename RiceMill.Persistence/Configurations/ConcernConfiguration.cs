using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;

namespace RiceMill.Persistence.Configurations
{
    internal class ConcernConfiguration : IEntityTypeConfiguration<Concern>
    {
        public void Configure(EntityTypeBuilder<Concern> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(c => c.CreateTime)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UpdateTime)
                .HasDefaultValueSql("GETDATE()");

            builder
                .HasMany(c => c.Payment)
                .WithOne(c => c.Concern);

            builder
                .HasOne(c => c.User)
                .WithMany(c => c.Concerns);

            builder
                .HasOne(c => c.RiceMill)
                .WithMany(c => c.Concerns);
        }
    }
}