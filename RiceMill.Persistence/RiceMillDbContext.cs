using Microsoft.EntityFrameworkCore;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Domain.Models;
using RiceMill.Domain.Models.BaseModels;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Reflection;

namespace RiceMill.Persistence
{
    public class RiceMillDbContext : DbContext, IApplicationDbContext
    {
        public RiceMillDbContext(DbContextOptions<RiceMillDbContext> options) : base(options) { }

        public DbSet<Concern> Concerns { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<Dryer> Dryers { get; set; }

        public DbSet<DryerHistory> DryerHistories { get; set; }

        public DbSet<Income> Incomes { get; set; }

        public DbSet<InputLoad> InputLoads { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Domain.Models.RiceMill> RiceMills { get; set; }

        public DbSet<RiceThreshing> RiceThreshings { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserActivity> UserActivities { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Village> Villages { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<EventBaseModel>()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted);

            var currentTime = DateTime.Now;
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Deleted)
                {
                    entity.Entity.DeleteTime = currentTime;
                    entity.Entity.IsDeleted = true;
                    entity.State = EntityState.Modified;
                    continue;
                }
                entity.Entity.UpdateTime = currentTime;
                if (entity.State == EntityState.Added)
                    entity.Entity.CreateTime = currentTime;
            }
            ApplyPasswordHashing();
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<EventBaseModel>();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        private void ApplyPasswordHashing()
        {
            var entitiesWithPassword = ChangeTracker.Entries<User>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity);

            foreach (var user in entitiesWithPassword)
                user.Password = user.Password.ToSha512();
        }

        public Dictionary<string, object> GetAllData()
        {
            var data = new Dictionary<string, object>
            {
                {nameof(EntityTypeEnum.Concerns), Concerns.IgnoreQueryFilters().ToList() },
                {nameof(EntityTypeEnum.Deliveries), Deliveries.IgnoreQueryFilters().ToList() },
                {nameof(EntityTypeEnum.DryerHistories), DryerHistories.IgnoreQueryFilters().ToList() },
                {nameof(EntityTypeEnum.Dryers), Dryers.IgnoreQueryFilters().ToList() },
                {nameof(EntityTypeEnum.Incomes),Incomes.IgnoreQueryFilters().ToList() },
                {nameof(EntityTypeEnum.InputLoads), InputLoads.IgnoreQueryFilters().ToList() },
                {nameof(EntityTypeEnum.Payments),Payments.IgnoreQueryFilters().ToList() },
                {nameof(EntityTypeEnum.People),People.IgnoreQueryFilters().ToList() },
                {nameof(EntityTypeEnum.RiceMills),RiceMills.IgnoreQueryFilters().ToList() },
                {nameof(EntityTypeEnum.RiceThreshings),RiceThreshings.IgnoreQueryFilters().ToList() },
                {nameof(EntityTypeEnum.UserActivities),UserActivities.IgnoreQueryFilters().ToList() },
                {nameof(EntityTypeEnum.Users),Users.IgnoreQueryFilters().ToList() },
                {nameof(EntityTypeEnum.Vehicles),Vehicles.IgnoreQueryFilters().ToList() },
                {nameof(EntityTypeEnum.Villages),Villages.IgnoreQueryFilters().ToList() }
            };
            return data;
        }
    }
}