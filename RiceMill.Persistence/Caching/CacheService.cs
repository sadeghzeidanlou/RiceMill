﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Domain.Models;
using Shared.Enums;

namespace RiceMill.Persistence.Caching
{
    public sealed class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly IServiceScopeFactory _scopeFactory;

        public CacheService(IMemoryCache cache, IServiceScopeFactory scopeFactory)
        {
            _cache = cache;
            _scopeFactory = scopeFactory;
        }

        public IQueryable<Concern> GetConcerns() => Get<List<Concern>>(EntityTypeEnum.Concerns).AsQueryable();

        public IQueryable<Delivery> GetDeliveries() => Get<List<Delivery>>(EntityTypeEnum.Deliveries).AsQueryable();

        public IQueryable<DryerHistory> GetDryerHistories() => Get<List<DryerHistory>>(EntityTypeEnum.DryerHistories).AsQueryable();

        public IQueryable<Dryer> GetDryers() => Get<List<Dryer>>(EntityTypeEnum.Dryers).AsQueryable();

        public IQueryable<Income> GetIncomes() => Get<List<Income>>(EntityTypeEnum.Incomes).AsQueryable();

        public IQueryable<InputLoad> GetInputLoads() => Get<List<InputLoad>>(EntityTypeEnum.InputLoads).AsQueryable();

        public IQueryable<Payment> GetPayments() => Get<List<Payment>>(EntityTypeEnum.Payments).AsQueryable();

        public IQueryable<Person> GetPeople() => Get<List<Person>>(EntityTypeEnum.People).AsQueryable();

        public IQueryable<Domain.Models.RiceMill> GetRiceMills() => Get<List<Domain.Models.RiceMill>>(EntityTypeEnum.RiceMills).AsQueryable();

        public IQueryable<RiceThreshing> GetRiceThreshings() => Get<List<RiceThreshing>>(EntityTypeEnum.RiceThreshings).AsQueryable();

        public IQueryable<UserActivity> GetUserActivities() => Get<List<UserActivity>>(EntityTypeEnum.UserActivities).AsQueryable();

        public IQueryable<User> GetUsers() => Get<List<User>>(EntityTypeEnum.Users).AsQueryable();

        public IQueryable<Vehicle> GetVehicles() => Get<List<Vehicle>>(EntityTypeEnum.Vehicles).AsQueryable();

        public IQueryable<Village> GetVillages() => Get<List<Village>>(EntityTypeEnum.Villages).AsQueryable()   ;

        public void Set<T>(EntityTypeEnum key, T value) => _cache.Set(key.ToString(), value, DateTimeOffset.MaxValue);

        public void Maintain<T>(EntityTypeEnum cacheKey, T value)
        {
            var entities = new List<EntityTypeEnum>();
            switch (cacheKey)
            {
                case EntityTypeEnum.Concerns:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.Payments };
                    MaintainGeneral(EntityTypeEnum.Concerns, value as Concern, e => e.Id, e => e.IsDeleted);
                    break;

                case EntityTypeEnum.Deliveries:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.People, EntityTypeEnum.Vehicles, EntityTypeEnum.RiceThreshings };
                    MaintainGeneral(EntityTypeEnum.Deliveries, value as Delivery, e => e.Id, e => e.IsDeleted);
                    break;

                case EntityTypeEnum.DryerHistories:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.Dryers, EntityTypeEnum.RiceThreshings, EntityTypeEnum.InputLoads };
                    MaintainGeneral(EntityTypeEnum.DryerHistories, value as DryerHistory, e => e.Id, e => e.IsDeleted);
                    break;

                case EntityTypeEnum.Dryers:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.DryerHistories };
                    MaintainGeneral(EntityTypeEnum.Dryers, value as Dryer, e => e.Id, e => e.IsDeleted);
                    break;

                case EntityTypeEnum.Incomes:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.RiceThreshings };
                    MaintainGeneral(EntityTypeEnum.Incomes, value as Income, e => e.Id, e => e.IsDeleted);
                    break;

                case EntityTypeEnum.InputLoads:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.Villages, EntityTypeEnum.People,
                        EntityTypeEnum.Vehicles, EntityTypeEnum.Payments, EntityTypeEnum.DryerHistories };
                    MaintainGeneral(EntityTypeEnum.InputLoads, value as InputLoad, e => e.Id, e => e.IsDeleted);
                    break;

                case EntityTypeEnum.Payments:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.People, EntityTypeEnum.Concerns, EntityTypeEnum.InputLoads };
                    MaintainGeneral(EntityTypeEnum.Payments, value as Payment, e => e.Id, e => e.IsDeleted);
                    break;

                case EntityTypeEnum.People:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.Payments, EntityTypeEnum.Deliveries, EntityTypeEnum.InputLoads, EntityTypeEnum.Vehicles };
                    MaintainGeneral(EntityTypeEnum.People, value as Person, e => e.Id, e => e.IsDeleted);
                    break;

                case EntityTypeEnum.RiceMills:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.People, EntityTypeEnum.Concerns, EntityTypeEnum.Deliveries, EntityTypeEnum.Dryers, EntityTypeEnum.Payments,
                        EntityTypeEnum.DryerHistories, EntityTypeEnum.Incomes, EntityTypeEnum.InputLoads, EntityTypeEnum.RiceThreshings, EntityTypeEnum.UserActivities, EntityTypeEnum.Vehicles, EntityTypeEnum.Villages };
                    MaintainGeneral(EntityTypeEnum.RiceMills, value as Domain.Models.RiceMill, e => e.Id, e => e.IsDeleted);
                    break;

                case EntityTypeEnum.RiceThreshings:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.Incomes, EntityTypeEnum.Deliveries, EntityTypeEnum.DryerHistories };
                    MaintainGeneral(EntityTypeEnum.RiceThreshings, value as RiceThreshing, e => e.Id, e => e.IsDeleted);
                    break;

                case EntityTypeEnum.UserActivities:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills };
                    MaintainGeneral(EntityTypeEnum.UserActivities, value as UserActivity, e => e.Id, e => true);
                    break;

                case EntityTypeEnum.Users:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.RiceMills, EntityTypeEnum.People, EntityTypeEnum.Concerns, EntityTypeEnum.Deliveries, EntityTypeEnum.Dryers, EntityTypeEnum.Vehicles,
                        EntityTypeEnum.DryerHistories, EntityTypeEnum.Incomes, EntityTypeEnum.InputLoads, EntityTypeEnum.Payments, EntityTypeEnum.RiceThreshings, EntityTypeEnum.UserActivities, EntityTypeEnum.Villages };
                    MaintainGeneral(EntityTypeEnum.Users, value as User, e => e.Id, e => e.IsDeleted);
                    break;

                case EntityTypeEnum.Vehicles:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.People, EntityTypeEnum.Deliveries, EntityTypeEnum.InputLoads };
                    MaintainGeneral(EntityTypeEnum.Vehicles, value as Vehicle, e => e.Id, e => e.IsDeleted);
                    break;

                case EntityTypeEnum.Villages:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.InputLoads };
                    MaintainGeneral(EntityTypeEnum.Villages, value as Village, e => e.Id, e => e.IsDeleted);
                    break;
            }
            LoadCache(entities);
        }

        public void LoadCache(List<EntityTypeEnum> entityTypes)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            entityTypes.ForEach(e => Set(e, dbContext.GetAllData(e)));
        }

        private void MaintainGeneral<T>(EntityTypeEnum cacheKey, T entity, Func<T, Guid> getIdFunc, Func<T, bool> isDeletedFunc)
        {
            var cacheData = Get<List<T>>(cacheKey);
            var id = getIdFunc(entity);
            cacheData.RemoveAll(e => getIdFunc(e) == id);
            if (!isDeletedFunc(entity))
                cacheData.Add(entity);

            Set(cacheKey, cacheData);
        }
        private T Get<T>(EntityTypeEnum key) => _cache.Get<T>(key.ToString());
    }
}