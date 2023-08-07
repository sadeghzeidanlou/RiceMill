﻿using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DryerServices.Dto;
using RiceMill.Domain.Models;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.DryerServices
{
    public interface IDryerQueries
    {
        Result<PaginatedList<DtoDryer>> GetAll(DtoDryerFilter filter);
    }

    public class DryerQueries : IDryerQueries
    {
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;

        public DryerQueries(ICurrentRequestService currentRequestService, ICacheService cacheService)
        {
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
        }

        public Result<PaginatedList<DtoDryer>> GetAll(DtoDryerFilter filter)
        {
            var dryers = GetFilter(filter);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoDryer>.Create(dryers, pageNumber, pageSize);
            return Result<PaginatedList<DtoDryer>>.Success(result);
        }

        private IQueryable<Dryer> GetFilter(DtoDryerFilter filter)
        {
            var dryers = _cacheService.GetDryers();
            if (filter == null || (_currentRequestService.IsNotAdmin && filter.RiceMillId.IsNullOrEmpty()))
                return dryers.Where(c => false);

            if (filter.Id.IsNotNullOrEmpty())
                dryers = dryers.Where(c => c.Id.Equals(filter.Id));

            if (filter.RiceMillId.IsNotNullOrEmpty())
                dryers = dryers.Where(c => c.RiceMillId.Equals(filter.RiceMillId));

            if (filter.Title.IsNotNullOrEmpty())
                dryers = dryers.Where(c => c.Title.Contains(filter.Title));

            return dryers;
        }
    }
}