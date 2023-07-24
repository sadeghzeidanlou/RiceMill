using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Domain.Models;
using Shared.Enums;

namespace RiceMill.Persistence.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly IServiceScopeFactory _scopeFactory;

        public CacheService(IMemoryCache cache, IServiceScopeFactory scopeFactory)
        {
            _cache = cache;
            _scopeFactory = scopeFactory;
        }

        public T Get<T>(EntityTypeEnum key) => _cache.Get<T>(key.ToString());

        public void Set<T>(EntityTypeEnum key, T value) => _cache.Set(key.ToString(), value, DateTimeOffset.MaxValue);

        public void Maintain<T>(EntityTypeEnum cacheKey, T value)
        {
            switch (cacheKey)
            {
                case EntityTypeEnum.Concerns:
                    MaintainConcerns(cacheKey, value as Concern);
                    LoadCache(new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.Payments });
                    break;

                case EntityTypeEnum.Deliveries:
                    MaintainDelivery(cacheKey, value as Delivery);
                    LoadCache(new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.People, EntityTypeEnum.Vehicles, EntityTypeEnum.RiceThreshings });
                    break;

                case EntityTypeEnum.DryerHistories:
                    MaintainDryerHistory(cacheKey, value as DryerHistory);
                    LoadCache(new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.Dryers, EntityTypeEnum.RiceThreshings, EntityTypeEnum.InputLoads });
                    break;

                case EntityTypeEnum.Dryers:
                    MaintainDryer(cacheKey, value as Dryer);
                    LoadCache(new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.DryerHistories });
                    break;

                case EntityTypeEnum.Incomes:
                    MaintainIncome(cacheKey, value as Income);
                    LoadCache(new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.RiceThreshings });
                    break;

                case EntityTypeEnum.InputLoads:
                    MaintainInputLoad(cacheKey, value as InputLoad);
                    LoadCache(new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.Villages, EntityTypeEnum.People, EntityTypeEnum.Vehicles,
                        EntityTypeEnum.Payments, EntityTypeEnum.DryerHistories });
                    break;

                case EntityTypeEnum.Payments:
                    MaintainPayment(cacheKey, value as Payment);
                    LoadCache(new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.People, EntityTypeEnum.Concerns, EntityTypeEnum.InputLoads });
                    break;

                case EntityTypeEnum.People:
                    MaintainPeople(cacheKey, value as Person);
                    LoadCache(new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.Payments, EntityTypeEnum.Deliveries, EntityTypeEnum.InputLoads,
                        EntityTypeEnum.Vehicles });
                    break;

                case EntityTypeEnum.RiceMills:
                    MaintainRiceMill(cacheKey, value as Domain.Models.RiceMill);
                    LoadCache(new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.People, EntityTypeEnum.Concerns, EntityTypeEnum.Deliveries, EntityTypeEnum.Dryers,
                        EntityTypeEnum.DryerHistories, EntityTypeEnum.Incomes, EntityTypeEnum.InputLoads, EntityTypeEnum.Payments, EntityTypeEnum.RiceThreshings,
                        EntityTypeEnum.UserActivities, EntityTypeEnum.Vehicles, EntityTypeEnum.Villages });
                    break;

                case EntityTypeEnum.RiceThreshings:
                    MaintainRiceThreshing(cacheKey, value as RiceThreshing);
                    LoadCache(new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.Incomes, EntityTypeEnum.Deliveries, EntityTypeEnum.DryerHistories });
                    break;

                case EntityTypeEnum.UserActivities:
                    MaintainUserActivity(cacheKey, value as UserActivity);
                    LoadCache(new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills });
                    break;

                case EntityTypeEnum.Users:
                    MaintainUser(cacheKey, value as User);
                    LoadCache(new List<EntityTypeEnum> { EntityTypeEnum.RiceMills, EntityTypeEnum.People, EntityTypeEnum.Concerns, EntityTypeEnum.Deliveries, EntityTypeEnum.Dryers,
                        EntityTypeEnum.DryerHistories, EntityTypeEnum.Incomes, EntityTypeEnum.InputLoads, EntityTypeEnum.Payments, EntityTypeEnum.RiceThreshings,
                        EntityTypeEnum.UserActivities, EntityTypeEnum.Vehicles, EntityTypeEnum.Villages });
                    break;

                case EntityTypeEnum.Vehicles:
                    MaintainVehicle(cacheKey, value as Vehicle);
                    LoadCache(new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.People, EntityTypeEnum.Deliveries, EntityTypeEnum.InputLoads });
                    break;

                case EntityTypeEnum.Villages:
                    MaintainVillage(cacheKey, value as Village);
                    LoadCache(new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.InputLoads });
                    break;
            }
        }

        public void LoadCache(List<EntityTypeEnum> entityTypes)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            entityTypes.ForEach(e => Set(e, dbContext.GetAllData(e)));
        }

        private void MaintainConcerns(EntityTypeEnum cacheKey, Concern concern)
        {
            var cacheData = Get<List<Concern>>(cacheKey);
            var concernItem = cacheData.FirstOrDefault(c => c.Id == concern.Id);
            if (concernItem == null)
                cacheData.Add(concern);
            else
                concernItem = concern;

            Set(cacheKey, cacheData);
        }

        private void MaintainDelivery(EntityTypeEnum cacheKey, Delivery delivery)
        {
            var cacheData = Get<List<Delivery>>(cacheKey);
            var concernItem = cacheData.FirstOrDefault(c => c.Id == delivery.Id);
            if (concernItem == null)
                cacheData.Add(delivery);
            else
                concernItem = delivery;

            Set(cacheKey, cacheData);
        }

        private void MaintainDryerHistory(EntityTypeEnum cacheKey, DryerHistory dryerHistory)
        {
            var cacheData = Get<List<DryerHistory>>(cacheKey);
            var concernItem = cacheData.FirstOrDefault(c => c.Id == dryerHistory.Id);
            if (concernItem == null)
                cacheData.Add(dryerHistory);
            else
                concernItem = dryerHistory;

            Set(cacheKey, cacheData);
        }

        private void MaintainDryer(EntityTypeEnum cacheKey, Dryer dryer)
        {
            var cacheData = Get<List<Dryer>>(cacheKey);
            var concernItem = cacheData.FirstOrDefault(c => c.Id == dryer.Id);
            if (concernItem == null)
                cacheData.Add(dryer);
            else
                concernItem = dryer;

            Set(cacheKey, cacheData);
        }

        private void MaintainIncome(EntityTypeEnum cacheKey, Income income)
        {
            var cacheData = Get<List<Income>>(cacheKey);
            var concernItem = cacheData.FirstOrDefault(c => c.Id == income.Id);
            if (concernItem == null)
                cacheData.Add(income);
            else
                concernItem = income;

            Set(cacheKey, cacheData);
        }

        private void MaintainInputLoad(EntityTypeEnum cacheKey, InputLoad inputLoad)
        {
            var cacheData = Get<List<InputLoad>>(cacheKey);
            var concernItem = cacheData.FirstOrDefault(c => c.Id == inputLoad.Id);
            if (concernItem == null)
                cacheData.Add(inputLoad);
            else
                concernItem = inputLoad;

            Set(cacheKey, cacheData);
        }

        private void MaintainPayment(EntityTypeEnum cacheKey, Payment payment)
        {
            var cacheData = Get<List<Payment>>(cacheKey);
            var concernItem = cacheData.FirstOrDefault(c => c.Id == payment.Id);
            if (concernItem == null)
                cacheData.Add(payment);
            else
                concernItem = payment;

            Set(cacheKey, cacheData);
        }

        private void MaintainPeople(EntityTypeEnum cacheKey, Person person)
        {
            var cacheData = Get<List<Person>>(cacheKey);
            var concernItem = cacheData.FirstOrDefault(c => c.Id == person.Id);
            if (concernItem == null)
                cacheData.Add(person);
            else
                concernItem = person;

            Set(cacheKey, cacheData);
        }

        private void MaintainRiceMill(EntityTypeEnum cacheKey, Domain.Models.RiceMill riceMill)
        {
            var cacheData = Get<List<Domain.Models.RiceMill>>(cacheKey);
            var concernItem = cacheData.FirstOrDefault(c => c.Id == riceMill.Id);
            if (concernItem == null)
                cacheData.Add(riceMill);
            else
                concernItem = riceMill;

            Set(cacheKey, cacheData);
        }

        private void MaintainRiceThreshing(EntityTypeEnum cacheKey, RiceThreshing riceThreshing)
        {
            var cacheData = Get<List<RiceThreshing>>(cacheKey);
            var concernItem = cacheData.FirstOrDefault(c => c.Id == riceThreshing.Id);
            if (concernItem == null)
                cacheData.Add(riceThreshing);
            else
                concernItem = riceThreshing;

            Set(cacheKey, cacheData);
        }

        private void MaintainUserActivity(EntityTypeEnum cacheKey, UserActivity userActivity)
        {
            var cacheData = Get<List<UserActivity>>(cacheKey);
            var concernItem = cacheData.FirstOrDefault(c => c.Id == userActivity.Id);
            if (concernItem == null)
                cacheData.Add(userActivity);
            else
                concernItem = userActivity;

            Set(cacheKey, cacheData);
        }

        private void MaintainUser(EntityTypeEnum cacheKey, User user)
        {
            var cacheData = Get<List<User>>(cacheKey);
            var concernItem = cacheData.FirstOrDefault(c => c.Id == user.Id);
            if (concernItem == null)
                cacheData.Add(user);
            else
                concernItem = user;

            Set(cacheKey, cacheData);
        }

        private void MaintainVehicle(EntityTypeEnum cacheKey, Vehicle vehicle)
        {
            var cacheData = Get<List<Vehicle>>(cacheKey);
            var concernItem = cacheData.FirstOrDefault(c => c.Id == vehicle.Id);
            if (concernItem == null)
                cacheData.Add(vehicle);
            else
                concernItem = vehicle;

            Set(cacheKey, cacheData);
        }

        private void MaintainVillage(EntityTypeEnum cacheKey, Village village)
        {
            var cacheData = Get<List<Village>>(cacheKey);
            var concernItem = cacheData.FirstOrDefault(c => c.Id == village.Id);
            if (concernItem == null)
                cacheData.Add(village);
            else
                concernItem = village;
            Set(cacheKey, cacheData);
        }
    }
}