using RiceMill.Domain.Models;
using Shared.Enums;

namespace RiceMill.Application.Common.Interfaces
{
    public interface ICacheService
    {
        IQueryable<Concern> GetConcerns();
      
        IQueryable<Delivery> GetDeliveries();
        
        IQueryable<DryerHistory> GetDryerHistories();
        
        IQueryable<Dryer> GetDryers();
        
        IQueryable<Income> GetIncomes();
        
        IQueryable<InputLoad> GetInputLoads();
        
        IQueryable<Payment> GetPayments();
        
        IQueryable<Person> GetPeople();
        
        IQueryable<Domain.Models.RiceMill> GetRiceMills();
        
        IQueryable<RiceThreshing> GetRiceThreshings();
        
        IQueryable<UserActivity> GetUserActivities();
        
        IQueryable<User> GetUsers();
        
        IQueryable<Vehicle> GetVehicles();
        
        IQueryable<Village> GetVillages();

        void Set<T>(EntityTypeEnum key, T value);

        void Maintain<T>(EntityTypeEnum key, T value);

        void LoadCache(List<EntityTypeEnum> entityTypes);
    }
}