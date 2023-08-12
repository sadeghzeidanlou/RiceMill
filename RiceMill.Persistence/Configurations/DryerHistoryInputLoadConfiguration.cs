using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiceMill.Domain.Models;

namespace RiceMill.Persistence.Configurations
{
    public sealed class DryerHistoryInputLoadConfiguration : IEntityTypeConfiguration<DryerHistoryInputLoad>
    {
        public void Configure(EntityTypeBuilder<DryerHistoryInputLoad> builder)
        {
            builder.Property(dil => dil.Id)
                .ValueGeneratedOnAdd();
            
            builder
                .HasIndex(dil => new { dil.DryerHistoryId, dil.InputLoadId }).IsUnique();

            builder
               .HasQueryFilter(dil => !dil.IsDeleted);

            builder
                .HasOne(dil => dil.DryerHistory)
                .WithMany(d => d.DryerHistoryInputLoads)
                .HasForeignKey(dil => dil.DryerHistoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(dil => dil.InputLoad)
                .WithMany(d => d.DryerHistoryInputLoads)
                .HasForeignKey(dil => dil.InputLoadId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}