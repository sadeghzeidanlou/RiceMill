using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Persistence.DataBaseExpression;

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
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(r => r.CreateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder.Property(r => r.UpdateTime)
                .HasDefaultValueSql(SqlExpressions.CurrentDateTime)
                .IsRequired();

            builder
                .HasOne(rm => rm.OwnerPerson)
                .WithMany(op => op.OwnedRiceMills)
                .HasForeignKey(rm => rm.OwnerPersonId);
        }
    }
}