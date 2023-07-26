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
            var entities = new List<EntityTypeEnum>();
            switch (cacheKey)
            {
                case EntityTypeEnum.Concerns:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.Payments };
                    MaintainGeneral(EntityTypeEnum.Concerns, value as Concern, e => e.Id);
                    break;

                case EntityTypeEnum.Deliveries:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.People, EntityTypeEnum.Vehicles, EntityTypeEnum.RiceThreshings };
                    MaintainGeneral(EntityTypeEnum.Deliveries, value as Delivery, e => e.Id);
                    break;

                case EntityTypeEnum.DryerHistories:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.Dryers, EntityTypeEnum.RiceThreshings, EntityTypeEnum.InputLoads };
                    MaintainGeneral(EntityTypeEnum.DryerHistories, value as DryerHistory, e => e.Id);
                    break;

                case EntityTypeEnum.Dryers:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.DryerHistories };
                    MaintainGeneral(EntityTypeEnum.Dryers, value as Dryer, e => e.Id);
                    break;

                case EntityTypeEnum.Incomes:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.RiceThreshings };
                    MaintainGeneral(EntityTypeEnum.Incomes, value as Income, e => e.Id);
                    break;

                case EntityTypeEnum.InputLoads:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.Villages, EntityTypeEnum.People,
                        EntityTypeEnum.Vehicles, EntityTypeEnum.Payments, EntityTypeEnum.DryerHistories };
                    MaintainGeneral(EntityTypeEnum.InputLoads, value as InputLoad, e => e.Id);
                    break;

                case EntityTypeEnum.Payments:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.People, EntityTypeEnum.Concerns, EntityTypeEnum.InputLoads };
                    MaintainGeneral(EntityTypeEnum.Payments, value as Payment, e => e.Id);
                    break;

                case EntityTypeEnum.People:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.Payments, EntityTypeEnum.Deliveries, EntityTypeEnum.InputLoads, EntityTypeEnum.Vehicles };
                    MaintainGeneral(EntityTypeEnum.People, value as Person, e => e.Id);
                    break;

                case EntityTypeEnum.RiceMills:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.People, EntityTypeEnum.Concerns, EntityTypeEnum.Deliveries, EntityTypeEnum.Dryers, EntityTypeEnum.Payments,
                        EntityTypeEnum.DryerHistories, EntityTypeEnum.Incomes, EntityTypeEnum.InputLoads, EntityTypeEnum.RiceThreshings, EntityTypeEnum.UserActivities, EntityTypeEnum.Vehicles, EntityTypeEnum.Villages };
                    MaintainGeneral(EntityTypeEnum.RiceMills, value as Domain.Models.RiceMill, e => e.Id);
                    break;

                case EntityTypeEnum.RiceThreshings:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.Incomes, EntityTypeEnum.Deliveries, EntityTypeEnum.DryerHistories };
                    MaintainGeneral(EntityTypeEnum.RiceThreshings, value as RiceThreshing, e => e.Id);
                    break;

                case EntityTypeEnum.UserActivities:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills };
                    MaintainGeneral(EntityTypeEnum.UserActivities, value as UserActivity, e => e.Id);
                    break;

                case EntityTypeEnum.Users:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.RiceMills, EntityTypeEnum.People, EntityTypeEnum.Concerns, EntityTypeEnum.Deliveries, EntityTypeEnum.Dryers, EntityTypeEnum.Vehicles,
                        EntityTypeEnum.DryerHistories, EntityTypeEnum.Incomes, EntityTypeEnum.InputLoads, EntityTypeEnum.Payments, EntityTypeEnum.RiceThreshings, EntityTypeEnum.UserActivities, EntityTypeEnum.Villages };
                    MaintainGeneral(EntityTypeEnum.Users, value as User, e => e.Id);
                    break;

                case EntityTypeEnum.Vehicles:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.People, EntityTypeEnum.Deliveries, EntityTypeEnum.InputLoads };
                    MaintainGeneral(EntityTypeEnum.Vehicles, value as Vehicle, e => e.Id);
                    break;

                case EntityTypeEnum.Villages:
                    entities = new List<EntityTypeEnum> { EntityTypeEnum.Users, EntityTypeEnum.RiceMills, EntityTypeEnum.InputLoads };
                    MaintainGeneral(EntityTypeEnum.Villages, value as Village, e => e.Id);
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

        private void MaintainGeneral<T>(EntityTypeEnum cacheKey, T entity, Func<T, Guid> getIdFunc)
        {
            var cacheData = Get<List<T>>(cacheKey);
            var id = getIdFunc(entity);
            var existingEntity = cacheData.FirstOrDefault(e => getIdFunc(e) == id);

            if (existingEntity == null)
                cacheData.Add(entity);
            else
                existingEntity = entity;

            Set(cacheKey, cacheData);
        }

        #region
        //private void MaintainConcerns(EntityTypeEnum cacheKey, Concern concern)
        //{
        //    var cacheData = Get<List<Concern>>(cacheKey);
        //    var concernItem = cacheData.FirstOrDefault(c => c.Id == concern.Id);
        //    if (concernItem == null)
        //        cacheData.Add(concern);
        //    else
        //        concernItem = concern;

        //    Set(cacheKey, cacheData);
        //}

        //private void MaintainDelivery(EntityTypeEnum cacheKey, Delivery delivery)
        //{
        //    var cacheData = Get<List<Delivery>>(cacheKey);
        //    var concernItem = cacheData.FirstOrDefault(c => c.Id == delivery.Id);
        //    if (concernItem == null)
        //        cacheData.Add(delivery);
        //    else
        //        concernItem = delivery;

        //    Set(cacheKey, cacheData);
        //}

        //private void MaintainDryerHistory(EntityTypeEnum cacheKey, DryerHistory dryerHistory)
        //{
        //    var cacheData = Get<List<DryerHistory>>(cacheKey);
        //    var concernItem = cacheData.FirstOrDefault(c => c.Id == dryerHistory.Id);
        //    if (concernItem == null)
        //        cacheData.Add(dryerHistory);
        //    else
        //        concernItem = dryerHistory;

        //    Set(cacheKey, cacheData);
        //}

        //private void MaintainDryer(EntityTypeEnum cacheKey, Dryer dryer)
        //{
        //    var cacheData = Get<List<Dryer>>(cacheKey);
        //    var concernItem = cacheData.FirstOrDefault(c => c.Id == dryer.Id);
        //    if (concernItem == null)
        //        cacheData.Add(dryer);
        //    else
        //        concernItem = dryer;

        //    Set(cacheKey, cacheData);
        //}

        //private void MaintainIncome(EntityTypeEnum cacheKey, Income income)
        //{
        //    var cacheData = Get<List<Income>>(cacheKey);
        //    var concernItem = cacheData.FirstOrDefault(c => c.Id == income.Id);
        //    if (concernItem == null)
        //        cacheData.Add(income);
        //    else
        //        concernItem = income;

        //    Set(cacheKey, cacheData);
        //}

        //private void MaintainInputLoad(EntityTypeEnum cacheKey, InputLoad inputLoad)
        //{
        //    var cacheData = Get<List<InputLoad>>(cacheKey);
        //    var concernItem = cacheData.FirstOrDefault(c => c.Id == inputLoad.Id);
        //    if (concernItem == null)
        //        cacheData.Add(inputLoad);
        //    else
        //        concernItem = inputLoad;

        //    Set(cacheKey, cacheData);
        //}

        //private void MaintainPayment(EntityTypeEnum cacheKey, Payment payment)
        //{
        //    var cacheData = Get<List<Payment>>(cacheKey);
        //    var concernItem = cacheData.FirstOrDefault(c => c.Id == payment.Id);
        //    if (concernItem == null)
        //        cacheData.Add(payment);
        //    else
        //        concernItem = payment;

        //    Set(cacheKey, cacheData);
        //}

        //private void MaintainPeople(EntityTypeEnum cacheKey, Person person)
        //{
        //    var cacheData = Get<List<Person>>(cacheKey);
        //    var concernItem = cacheData.FirstOrDefault(c => c.Id == person.Id);
        //    if (concernItem == null)
        //        cacheData.Add(person);
        //    else
        //        concernItem = person;

        //    Set(cacheKey, cacheData);
        //}

        //private void MaintainRiceMill(EntityTypeEnum cacheKey, Domain.Models.RiceMill riceMill)
        //{
        //    var cacheData = Get<List<Domain.Models.RiceMill>>(cacheKey);
        //    var concernItem = cacheData.FirstOrDefault(c => c.Id == riceMill.Id);
        //    if (concernItem == null)
        //        cacheData.Add(riceMill);
        //    else
        //        concernItem = riceMill;

        //    Set(cacheKey, cacheData);
        //}

        //private void MaintainRiceThreshing(EntityTypeEnum cacheKey, RiceThreshing riceThreshing)
        //{
        //    var cacheData = Get<List<RiceThreshing>>(cacheKey);
        //    var concernItem = cacheData.FirstOrDefault(c => c.Id == riceThreshing.Id);
        //    if (concernItem == null)
        //        cacheData.Add(riceThreshing);
        //    else
        //        concernItem = riceThreshing;

        //    Set(cacheKey, cacheData);
        //}

        //private void MaintainUserActivity(EntityTypeEnum cacheKey, UserActivity userActivity)
        //{
        //    var cacheData = Get<List<UserActivity>>(cacheKey);
        //    var concernItem = cacheData.FirstOrDefault(c => c.Id == userActivity.Id);
        //    if (concernItem == null)
        //        cacheData.Add(userActivity);
        //    else
        //        concernItem = userActivity;

        //    Set(cacheKey, cacheData);
        //}

        //private void MaintainUser(EntityTypeEnum cacheKey, User user)
        //{
        //    var cacheData = Get<List<User>>(cacheKey);
        //    var concernItem = cacheData.FirstOrDefault(c => c.Id == user.Id);
        //    if (concernItem == null)
        //        cacheData.Add(user);
        //    else
        //        concernItem = user;

        //    Set(cacheKey, cacheData);
        //}

        //private void MaintainVehicle(EntityTypeEnum cacheKey, Vehicle vehicle)
        //{
        //    var cacheData = Get<List<Vehicle>>(cacheKey);
        //    var concernItem = cacheData.FirstOrDefault(c => c.Id == vehicle.Id);
        //    if (concernItem == null)
        //        cacheData.Add(vehicle);
        //    else
        //        concernItem = vehicle;

        //    Set(cacheKey, cacheData);
        //}

        //private void MaintainVillage(EntityTypeEnum cacheKey, Village village)
        //{
        //    var cacheData = Get<List<Village>>(cacheKey);
        //    var concernItem = cacheData.FirstOrDefault(c => c.Id == village.Id);
        //    if (concernItem == null)
        //        cacheData.Add(village);
        //    else
        //        concernItem = village;
        //    Set(cacheKey, cacheData);
        //}
        #endregion
    }
}