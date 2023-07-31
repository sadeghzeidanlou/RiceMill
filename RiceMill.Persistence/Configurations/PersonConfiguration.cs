using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using Shared.Enums;

namespace RiceMill.Persistence.Configurations
{
    public sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsUnicode()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.Family)
                .IsUnicode()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.Gender)
                .HasConversion(g => (byte)g, g => (GenderEnum)g)
                .IsRequired();

            builder.Property(p => p.MobileNumber)
                .IsFixedLength()
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(p => p.HomeNumber)
                .IsFixedLength()
                .HasMaxLength(11);

            builder.Property(p => p.Address)
                .IsUnicode()
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.FatherName)
                .IsUnicode()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.IsDeleted)
                .IsRequired();

            builder.Property(p => p.CreateTime)
                .IsRequired();

            builder.Property(p => p.UpdateTime)
                .IsRequired();

            builder
                .HasQueryFilter(p => !p.IsDeleted);

            builder
                .HasOne(p => p.RiceMill)
                .WithMany(rm => rm.MemberPeople)
                .HasForeignKey(p => p.RiceMillId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}