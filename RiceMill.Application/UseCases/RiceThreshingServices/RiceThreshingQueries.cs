using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.RiceThreshingServices.Dto;
using RiceMill.Domain.Models;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.RiceThreshingServices
{
    public interface IRiceThreshingQueries
    {
        Result<PaginatedList<DtoRiceThreshing>> GetAll(DtoRiceThreshingFilter filter);
    }

    public sealed class RiceThreshingQueries : IRiceThreshingQueries
    {
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;

        public RiceThreshingQueries(ICurrentRequestService currentRequestService, ICacheService cacheService)
        {
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
        }

        public Result<PaginatedList<DtoRiceThreshing>> GetAll(DtoRiceThreshingFilter filter)
        {
            var dryers = GetFilter(filter).OrderByDescending(x => x.UpdateTime);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoRiceThreshing>.Create(dryers, pageNumber, pageSize);
            return Result<PaginatedList<DtoRiceThreshing>>.Success(result);
        }

        private IQueryable<RiceThreshing> GetFilter(DtoRiceThreshingFilter filter)
        {
            var riceThreshings = _cacheService.GetRiceThreshings();
            if (filter == null || (_currentRequestService.IsNotAdmin && filter.RiceMillId.IsNullOrEmpty()))
                return riceThreshings.Where(rt => false);

            if (filter.Id.IsNotNullOrEmpty())
                riceThreshings = riceThreshings.Where(rt => rt.Id.Equals(filter.Id.Value));

            if (filter.Ids.IsCollectionNotNullOrEmpty())
                riceThreshings = riceThreshings.Where(rt => filter.Ids.Contains(rt.Id));

            if (filter.RiceMillId.IsNotNullOrEmpty())
                riceThreshings = riceThreshings.Where(rt => rt.RiceMillId.Equals(filter.RiceMillId));

            if (filter.StartTimeLower.HasValue)
                riceThreshings = riceThreshings.Where(rt => rt.StartTime < filter.StartTimeLower.Value);

            if (filter.StartTime.HasValue)
                riceThreshings = riceThreshings.Where(rt => rt.StartTime.Equals(filter.StartTime.Value));

            if (filter.StartTimeGreater.HasValue)
                riceThreshings = riceThreshings.Where(rt => rt.StartTime > filter.StartTimeGreater.Value);

            if (filter.EndTimeLower.HasValue)
                riceThreshings = riceThreshings.Where(rt => rt.EndTime < filter.EndTimeLower.Value);

            if (filter.EndTime.HasValue)
                riceThreshings = riceThreshings.Where(rt => rt.EndTime.Equals(filter.EndTime.Value));

            if (filter.EndTimeGreater.HasValue)
                riceThreshings = riceThreshings.Where(rt => rt.EndTime > filter.EndTimeGreater.Value);

            if (filter.UnbrokenRiceLower.HasValue)
                riceThreshings = riceThreshings.Where(p => p.UnbrokenRice < filter.UnbrokenRiceLower.Value);

            if (filter.UnbrokenRice.HasValue)
                riceThreshings = riceThreshings.Where(p => p.UnbrokenRice == filter.UnbrokenRice.Value);

            if (filter.UnbrokenRiceGreater.HasValue)
                riceThreshings = riceThreshings.Where(p => p.UnbrokenRice > filter.UnbrokenRiceGreater.Value);

            if (filter.BrokenRiceLower.HasValue)
                riceThreshings = riceThreshings.Where(p => p.BrokenRice < filter.BrokenRiceLower.Value);

            if (filter.BrokenRice.HasValue)
                riceThreshings = riceThreshings.Where(p => p.BrokenRice == filter.BrokenRice.Value);

            if (filter.BrokenRiceGreater.HasValue)
                riceThreshings = riceThreshings.Where(p => p.BrokenRice > filter.BrokenRiceGreater.Value);

            if (filter.ChickenRiceLower.HasValue)
                riceThreshings = riceThreshings.Where(p => p.ChickenRice < filter.ChickenRiceLower.Value);

            if (filter.ChickenRice.HasValue)
                riceThreshings = riceThreshings.Where(p => p.ChickenRice == filter.ChickenRice.Value);

            if (filter.ChickenRiceGreater.HasValue)
                riceThreshings = riceThreshings.Where(p => p.ChickenRice > filter.ChickenRiceGreater.Value);

            if (filter.FlourLower.HasValue)
                riceThreshings = riceThreshings.Where(p => p.Flour < filter.FlourLower.Value);

            if (filter.Flour.HasValue)
                riceThreshings = riceThreshings.Where(p => p.Flour == filter.Flour.Value);

            if (filter.FlourGreater.HasValue)
                riceThreshings = riceThreshings.Where(p => p.Flour > filter.FlourGreater.Value);

            if (filter.Description.IsNotNullOrEmpty())
                riceThreshings = riceThreshings.Where(p => p.Description.Contains(filter.Description));

            if (filter.IsDelivered.HasValue)
                riceThreshings = riceThreshings.Where(rt => rt.IsDelivered.Equals(filter.IsDelivered.Value));

            if (filter.IncomeId.IsNotNullOrEmpty())
                riceThreshings = riceThreshings.Where(rt => rt.IncomeId.Equals(filter.IncomeId.Value));

            return riceThreshings;
        }
    }
}