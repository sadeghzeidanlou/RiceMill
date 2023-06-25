using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RiceMill.Persistence.Configurations
{
    public sealed class RiceMillConfiguration : IEntityTypeConfiguration<Domain.Models.RiceMill>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.RiceMill> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .ValueGeneratedOnAdd();

            builder.Property(r => r.Title)
                .IsUnicode()
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(r => r.Address)
                .IsUnicode()
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(r => r.Wage)
                .IsRequired();

            builder.Property(r => r.Phone)
                .IsFixedLength()
                .HasMaxLength(11);

            builder.Property(r => r.PostalCode)
                .IsFixedLength()
                .HasMaxLength(10);

            builder.Property(r => r.Description)
                .IsUnicode()
                .HasMaxLength(200);

            builder.Property(r => r.IsDeleted)
                .IsRequired();

            builder.Property(r => r.CreateTime)
                .IsRequired();

            builder.Property(r => r.UpdateTime)
                .IsRequired();

            builder
                .HasOne(rm => rm.OwnerPerson)
                .WithMany(op => op.OwnedRiceMills)
                .HasForeignKey(rm => rm.OwnerPersonId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(rm => rm.Users)
                .WithOne(u => u.RiceMill)
                .HasForeignKey(u => u.RiceMillId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}