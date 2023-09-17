using Microsoft.EntityFrameworkCore;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Domain.Models;
using RiceMill.Domain.Models.BaseModels;
using Shared.Enums;
using Shared.UtilityMethods;
using System.Reflection;

namespace RiceMill.Persistence
{
    public sealed class RiceMillDbContext : DbContext, IApplicationDbContext
    {
        public RiceMillDbContext(DbContextOptions<RiceMillDbContext> options) : base(options) { }

        public DbSet<Concern> Concerns { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        //public DbSet<DeliveryRiceThreshing> DeliveryRiceThreshing { get; set; }

        public DbSet<Dryer> Dryers { get; set; }

        public DbSet<DryerHistory> DryerHistories { get; set; }

        //public DbSet<DryerHistoryInputLoad> DryerHistoryInputLoad { get; set; }

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
                {EntityTypeEnum.Villages, Villages.AsNoTracking().ToList() },
                //{EntityTypeEnum.DryerHistoryInputLoads, DryerHistoryInputLoad.AsNoTracking().ToList() },
                //{EntityTypeEnum.DeliveryRiceThreshings, DeliveryRiceThreshing.AsNoTracking().ToList() }

            };
            return data;
        }

        public object GetAllData(EntityTypeEnum entityType)
        {
            return entityType switch
            {
                EntityTypeEnum.Concerns => Concerns.AsNoTracking().ToList(),
                EntityTypeEnum.Deliveries => Deliveries.AsNoTracking().ToList(),
                EntityTypeEnum.DryerHistories => DryerHistories.AsNoTracking().ToList(),
                EntityTypeEnum.Dryers => Dryers.AsNoTracking().ToList(),
                EntityTypeEnum.Incomes => Incomes.AsNoTracking().ToList(),
                EntityTypeEnum.InputLoads => InputLoads.AsNoTracking().ToList(),
                EntityTypeEnum.Payments => Payments.AsNoTracking().ToList(),
                EntityTypeEnum.People => People.AsNoTracking().ToList(),
                EntityTypeEnum.RiceMills => RiceMills.AsNoTracking().ToList(),
                EntityTypeEnum.RiceThreshings => RiceThreshings.AsNoTracking().ToList(),
                EntityTypeEnum.UserActivities => UserActivities.AsNoTracking().ToList(),
                EntityTypeEnum.Users => Users.AsNoTracking().ToList(),
                EntityTypeEnum.Vehicles => Vehicles.AsNoTracking().ToList(),
                EntityTypeEnum.Villages => Villages.AsNoTracking().ToList(),
                //EntityTypeEnum.DryerHistoryInputLoads => DryerHistoryInputLoad.AsNoTracking().ToList(),
                //EntityTypeEnum.DeliveryRiceThreshings => DeliveryRiceThreshing.AsNoTracking().ToList(),
                _ => throw new ArgumentOutOfRangeException($"{entityType}", "Entity type is not valid"),
            };
        }

        public override int SaveChanges()
        {
            DoBaseClassOperation();
            return base.SaveChanges();
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
                {
                    entity.Entity.CreateTime = currentTime;
                    var addedUsers = ChangeTracker.Entries<User>().Where(e => e.State == EntityState.Added).Select(e => e.Entity);
                    foreach (var user in addedUsers)
                        user.Password = user.Password.ToSha512();
                }
            }
        }
    }
}