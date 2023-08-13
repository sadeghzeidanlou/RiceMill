using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DryerHistoryServices.Dto;
using RiceMill.Domain.Models;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.DryerHistoryServices
{
    public interface IDryerHistoryQueries
    {
        Result<PaginatedList<DtoDryerHistory>> GetAll(DtoDryerHistoryFilter filter);
    }

    public class DryerHistoryQueries : IDryerHistoryQueries
    {
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;

        public DryerHistoryQueries(ICurrentRequestService currentRequestService, ICacheService cacheService)
        {
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
        }

        public Result<PaginatedList<DtoDryerHistory>> GetAll(DtoDryerHistoryFilter filter)
        {
            var dryers = GetFilter(filter);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoDryerHistory>.Create(dryers, pageNumber, pageSize);
            return Result<PaginatedList<DtoDryerHistory>>.Success(result);
        }

        private IQueryable<DryerHistory> GetFilter(DtoDryerHistoryFilter filter)
        {
            var dryerHistories = _cacheService.GetDryerHistories();
            if (filter == null || (_currentRequestService.IsNotAdmin && filter.RiceMillId.IsNullOrEmpty()))
                return dryerHistories.Where(dh => false);

            if (filter.Id.IsNotNullOrEmpty())
                dryerHistories = dryerHistories.Where(dh => dh.Id.Equals(filter.Id.Value));

            if (filter.Ids.IsCollectionNotNullOrEmpty())
                dryerHistories = dryerHistories.Where(dh => filter.Ids.Contains(dh.Id));

            if (filter.RiceMillId.IsNotNullOrEmpty())
                dryerHistories = dryerHistories.Where(dh => dh.RiceMillId.Equals(filter.RiceMillId));

            if (filter.Operation.HasValue)
                dryerHistories = dryerHistories.Where(dh => dh.Operation.Equals(filter.Operation.Value));

            if (filter.StartTimeLower.HasValue)
                dryerHistories = dryerHistories.Where(dh => dh.StartTime < filter.StartTimeLower.Value);

            if (filter.StartTime.HasValue)
                dryerHistories = dryerHistories.Where(dh => dh.StartTime.Equals(filter.StartTime.Value));

            if (filter.StartTimeGreater.HasValue)
                dryerHistories = dryerHistories.Where(dh => dh.StartTime > filter.StartTimeGreater.Value);

            if (filter.StopTimeLower.HasValue)
                dryerHistories = dryerHistories.Where(dh => dh.StopTime < filter.StopTimeLower.Value);

            if (filter.StopTime.HasValue)
                dryerHistories = dryerHistories.Where(dh => dh.StopTime.Equals(filter.StopTime.Value));

            if (filter.StopTimeGreater.HasValue)
                dryerHistories = dryerHistories.Where(dh => dh.StopTime > filter.StopTimeGreater.Value);

            if (filter.DryerId.IsNotNullOrEmpty())
                dryerHistories = dryerHistories.Where(dh => dh.DryerId.Equals(filter.DryerId.Value));

            if (filter.RiceThreshingId.IsNotNullOrEmpty())
                dryerHistories = dryerHistories.Where(dh => dh.RiceThreshingId.Equals(filter.RiceThreshingId.Value));

            return dryerHistories;
        }
    }
}