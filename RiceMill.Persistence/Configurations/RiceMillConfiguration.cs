using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RiceMill.Persistence.Configurations
{
    public sealed class RiceMillConfiguration : IEntityTypeConfiguration<Domain.Models.RiceMill>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.RiceMill> builder)
        {
            builder.Property(rm => rm.Id)
                .ValueGeneratedOnAdd();

            builder.Property(rm => rm.Title)
                .IsUnicode()
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(rm => rm.Address)
                .IsUnicode()
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(rm => rm.Wage)
                .IsRequired();

            builder.Property(rm => rm.Phone)
                .IsFixedLength()
                .HasMaxLength(11);

            builder.Property(rm => rm.PostalCode)
                .IsFixedLength()
                .HasMaxLength(10);

            builder.Property(rm => rm.Description)
                .IsUnicode()
                .HasMaxLength(200);

            builder.Property(rm => rm.IsDeleted)
                .IsRequired();

            builder.Property(rm => rm.CreateTime)
                .IsRequired();

            builder.Property(rm => rm.UpdateTime)
                .IsRequired();

            builder.Property(rm => rm.OwnerPersonId)
                .IsRequired(false);

            builder
                .HasQueryFilter(rm => !rm.IsDeleted);

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