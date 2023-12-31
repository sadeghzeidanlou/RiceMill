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

    public sealed class DryerQueries : IDryerQueries
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
            var dryers = GetFilter(filter).OrderByDescending(x => x.UpdateTime);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoDryer>.Create(dryers, pageNumber, pageSize);
            return Result<PaginatedList<DtoDryer>>.Success(result);
        }

        private IQueryable<Dryer> GetFilter(DtoDryerFilter filter)
        {
            var dryers = _cacheService.GetDryers();
            if (filter == null)
                return dryers.Where(v => false);

            if (_currentRequestService.IsNotAdmin)
            {
                if (_currentRequestService.RiceMillId.IsNullOrEmpty())
                    return dryers.Where(rm => false);

                dryers = dryers.Where(rm => rm.RiceMillId.Equals(_currentRequestService.RiceMillId.Value));
            }
            if (filter.Id.IsNotNullOrEmpty())
                dryers = dryers.Where(d => d.Id.Equals(filter.Id.Value));

            if (filter.Ids.IsCollectionNotNullOrEmpty())
                dryers = dryers.Where(d => filter.Ids.Contains(d.Id));

            if (filter.RiceMillId.IsNotNullOrEmpty())
                dryers = dryers.Where(d => d.RiceMillId.Equals(filter.RiceMillId));

            if (filter.Title.IsNotNullOrEmpty())
                dryers = dryers.Where(d => d.Title.Contains(filter.Title));

            return dryers;
        }
    }
}