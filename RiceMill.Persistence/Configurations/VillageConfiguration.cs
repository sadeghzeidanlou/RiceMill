using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;

namespace RiceMill.Persistence.Configurations
{
    public sealed class VillageConfiguration : IEntityTypeConfiguration<Village>
    {
        public void Configure(EntityTypeBuilder<Village> builder)
        {
            //builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .ValueGeneratedOnAdd();

            builder.Property(v => v.Title)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder.Property(v => v.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(v => v.CreateTime)
                .IsRequired();

            builder.Property(v => v.UpdateTime)
                .IsRequired();

            builder
                .HasQueryFilter(v => !v.IsDeleted);

            builder
                .HasOne(v => v.User)
                .WithMany(u => u.Villages)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}