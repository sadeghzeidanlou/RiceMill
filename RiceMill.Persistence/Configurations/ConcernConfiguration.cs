using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;

namespace RiceMill.Persistence.Configurations
{
    public sealed class ConcernConfiguration : IEntityTypeConfiguration<Concern>
    {
        public void Configure(EntityTypeBuilder<Concern> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Title)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder.Property(c => c.IsDeleted)
                .IsRequired();

            builder.Property(c => c.CreateTime)
                .IsRequired();

            builder.Property(c => c.UpdateTime)
                .IsRequired();

            builder
                .HasOne(c => c.User)
                .WithMany(u => u.Concerns)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}