using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;
using RiceMill.Persistence.DataBaseExpression;
using Shared.Enums;

namespace RiceMill.Persistence.Configurations
{
    public sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .UseIdentityColumn();

            builder.Property(p => p.Name)
                .IsUnicode()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.Family)
                .IsUnicode()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.Gender)
                .HasConversion(g => g.ToString(), g => (GenderEnum)Enum.Parse(typeof(GenderEnum), g))
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
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(p => p.CreateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder.Property(p => p.UpdateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();
        }
    }
}