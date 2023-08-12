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
            var dryers = _cacheService.GetDryerHistories();
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