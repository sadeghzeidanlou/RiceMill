﻿using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.UserActivityServices.Dto;
using RiceMill.Domain.Models;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.UserActivityServices
{
    public interface IUserActivityQueries
    {
        Result<PaginatedList<DtoUserActivity>> GetAll(DtoUserActivityFilter filter);
    }

    public sealed class UserActivityQueries : IUserActivityQueries
    {
        private readonly ICacheService _cacheService;
        private readonly ICurrentRequestService _currentRequestService;

        public UserActivityQueries(ICacheService cacheService, ICurrentRequestService currentRequestService)
        {
            _cacheService = cacheService;
            _currentRequestService = currentRequestService;
        }

        public Result<PaginatedList<DtoUserActivity>> GetAll(DtoUserActivityFilter filter)
        {
            if (_currentRequestService.HasNotAccessToRiceMills)
                return Result<PaginatedList<DtoUserActivity>>.Forbidden();

            var userActivities = GetFilter(filter).OrderByDescending(x => x.UpdateTime);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoUserActivity>.Create(userActivities, pageNumber, pageSize);
            return Result<PaginatedList<DtoUserActivity>>.Success(result);
        }

        private IQueryable<UserActivity> GetFilter(DtoUserActivityFilter filter)
        {
            var userActivities = _cacheService.GetUserActivities();
            if (filter == null)
                return userActivities.Where(v => false);

            if (_currentRequestService.IsNotAdmin)
            {
                if (_currentRequestService.RiceMillId.IsNullOrEmpty())
                    return userActivities.Where(rm => false);

                userActivities = userActivities.Where(rm => rm.RiceMillId.Equals(_currentRequestService.RiceMillId.Value));
            }
            if (filter.RiceMillId.IsNotNullOrEmpty())
                userActivities = userActivities.Where(u => u.RiceMillId.Equals(filter.RiceMillId.Value));

            if (filter.Ip.IsNotNullOrEmpty())
                userActivities = userActivities.Where(u => u.Ip.Contains(filter.Ip));

            if (!filter.UserActivityType.HasValue)
                userActivities = userActivities.Where(u => u.UserActivityType.Equals(filter.UserActivityType));

            if (!filter.EntityType.HasValue)
                userActivities = userActivities.Where(u => u.EntityType.Equals(filter.EntityType));

            if (!filter.ApplicationId.HasValue)
                userActivities = userActivities.Where(u => u.ApplicationId.Equals(filter.ApplicationId));

            if (filter.BeforeEdit.IsNotNullOrEmpty())
                userActivities = userActivities.Where(u => u.BeforeEdit.Contains(filter.BeforeEdit));

            if (filter.AfterEdit.IsNotNullOrEmpty())
                userActivities = userActivities.Where(u => u.AfterEdit.Contains(filter.AfterEdit));

            return userActivities;
        }
    }
}