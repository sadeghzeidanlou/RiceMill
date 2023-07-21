﻿using Microsoft.EntityFrameworkCore;
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
                {EntityTypeEnum.Concerns, Concerns.IgnoreQueryFilters().ToList() },
                {EntityTypeEnum.Deliveries, Deliveries.IgnoreQueryFilters().ToList() },
                {EntityTypeEnum.DryerHistories, DryerHistories.IgnoreQueryFilters().ToList() },
                {EntityTypeEnum.Dryers, Dryers.IgnoreQueryFilters().ToList() },
                {EntityTypeEnum.Incomes, Incomes.IgnoreQueryFilters().ToList() },
                {EntityTypeEnum.InputLoads, InputLoads.IgnoreQueryFilters().ToList() },
                {EntityTypeEnum.Payments, Payments.IgnoreQueryFilters().ToList() },
                {EntityTypeEnum.People, People.IgnoreQueryFilters().ToList() },
                {EntityTypeEnum.RiceMills, RiceMills.IgnoreQueryFilters().ToList() },
                {EntityTypeEnum.RiceThreshings, RiceThreshings.IgnoreQueryFilters().ToList() },
                {EntityTypeEnum.UserActivities, UserActivities.IgnoreQueryFilters().ToList() },
                {EntityTypeEnum.Users, Users.IgnoreQueryFilters().ToList() },
                {EntityTypeEnum.Vehicles, Vehicles.IgnoreQueryFilters().ToList() },
                {EntityTypeEnum.Villages, Villages.IgnoreQueryFilters().ToList() }
            };
            return data;
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
                user.Password = user.Password.ToSha512();
        }
    }
}