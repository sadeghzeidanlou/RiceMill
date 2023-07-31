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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<EventBaseModel>();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public Dictionary<EntityTypeEnum, object> GetAllData()
        {
            var data = new Dictionary<EntityTypeEnum, object>
            {
                {EntityTypeEnum.Concerns, Concerns.AsNoTracking().ToList() },
                {EntityTypeEnum.Deliveries, Deliveries.AsNoTracking().ToList() },
                {EntityTypeEnum.DryerHistories, DryerHistories.AsNoTracking().ToList() },
                {EntityTypeEnum.Dryers, Dryers.AsNoTracking().ToList() },
                {EntityTypeEnum.Incomes, Incomes.AsNoTracking().ToList() },
                {EntityTypeEnum.InputLoads, InputLoads.AsNoTracking().ToList() },
                {EntityTypeEnum.Payments, Payments.AsNoTracking().ToList() },
                {EntityTypeEnum.People, People.AsNoTracking().ToList() },
                {EntityTypeEnum.RiceMills, RiceMills.AsNoTracking().ToList() },
                {EntityTypeEnum.RiceThreshings, RiceThreshings.AsNoTracking().ToList() },
                {EntityTypeEnum.UserActivities, UserActivities.AsNoTracking().ToList() },
                {EntityTypeEnum.Users, Users.AsNoTracking().ToList() },
                {EntityTypeEnum.Vehicles, Vehicles.AsNoTracking().ToList() },
                {EntityTypeEnum.Villages, Villages.AsNoTracking().ToList() }
            };
            return data;
        }

        public object GetAllData(EntityTypeEnum entityType)
        {
            switch (entityType)
            {
                case EntityTypeEnum.Concerns:
                    return Concerns.AsNoTracking().ToList();

                case EntityTypeEnum.Deliveries:
                    return Deliveries.AsNoTracking().ToList();

                case EntityTypeEnum.DryerHistories:
                    return DryerHistories.AsNoTracking().ToList();

                case EntityTypeEnum.Dryers:
                    return Dryers.AsNoTracking().ToList();

                case EntityTypeEnum.Incomes:
                    return Incomes.AsNoTracking().ToList();

                case EntityTypeEnum.InputLoads:
                    return InputLoads.AsNoTracking().ToList();

                case EntityTypeEnum.Payments:
                    return Payments.AsNoTracking().ToList();

                case EntityTypeEnum.People:
                    return People.AsNoTracking().ToList();

                case EntityTypeEnum.RiceMills:
                    return RiceMills.AsNoTracking().ToList();

                case EntityTypeEnum.RiceThreshings:
                    return RiceThreshings.AsNoTracking().ToList();

                case EntityTypeEnum.UserActivities:
                    return UserActivities.AsNoTracking().ToList();

                case EntityTypeEnum.Users:
                    return Users.AsNoTracking().ToList();

                case EntityTypeEnum.Vehicles:
                    return Vehicles.AsNoTracking().ToList();

                case EntityTypeEnum.Villages:
                    return Villages.AsNoTracking().ToList();

                default:
                    throw new ArgumentOutOfRangeException($"{entityType}", "Entity type is not valid");
            }
        }

        public override int SaveChanges()
        {
            DoBaseClassOperation();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            DoBaseClassOperation();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void DoBaseClassOperation()
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
        }

        private void ApplyPasswordHashing()
        {
            var entitiesWithPassword = ChangeTracker.Entries<User>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity);

            foreach (var user in entitiesWithPassword)
            {
                if (user.Password.Length < 100)
                    user.Password = user.Password.ToSha512();
            }
        }
    }
}