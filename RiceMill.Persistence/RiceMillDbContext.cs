﻿using Microsoft.EntityFrameworkCore;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Domain.Models;
using RiceMill.Domain.Models.BaseModels;
using System.Reflection;

namespace RiceMill.Persistence
{
    public class RiceMillDbContext : DbContext, IApplicationDbContext
    {
        public RiceMillDbContext(DbContextOptions<RiceMillDbContext> options) : base(options)
        {
        }

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
            var entities = ChangeTracker.Entries<EventBaseModel>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            var currentTime = DateTime.Now;
            foreach (var entity in entities)
            {
                entity.Entity.UpdateTime = currentTime;
                if (entity.State == EntityState.Added)
                    entity.Entity.CreateTime = currentTime;

                if (entity.State == EntityState.Deleted)
                {
                    entity.Entity.DeleteTime = currentTime;
                    entity.Entity.IsDeleted = true;
                    entity.State = EntityState.Modified;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventBaseModel>()
                .HasQueryFilter(ebm => !ebm.IsDeleted);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}