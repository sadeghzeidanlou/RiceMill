using Microsoft.EntityFrameworkCore;
using RiceMill.Domain.Models;
using Shared.Enums;

namespace RiceMill.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Concern> Concerns { get; }
        DbSet<Delivery> Deliveries { get; }
        DbSet<Dryer> Dryers { get; }
        DbSet<DryerHistory> DryerHistories { get; }
        DbSet<Income> Incomes { get; }
        DbSet<InputLoad> InputLoads { get; }
        DbSet<Payment> Payments { get; }
        DbSet<Person> People { get; }
        DbSet<Domain.Models.RiceMill> RiceMills { get; }
        DbSet<RiceThreshing> RiceThreshings { get; }
        DbSet<User> Users { get; }
        DbSet<UserActivity> UserActivities { get; }
        DbSet<Vehicle> Vehicles { get; }
        DbSet<Village> Villages { get; }

        Dictionary<EntityTypeEnum, object> GetAllData();
        object GetAllData(EntityTypeEnum entityType);
        int SaveChanges();
    }
}