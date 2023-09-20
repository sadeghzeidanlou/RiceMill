using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.IncomeServices.Dto;
using RiceMill.Domain.Models;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.IncomeServices
{
    public interface IIncomeQueries
    {
        Result<PaginatedList<DtoIncome>> GetAll(DtoIncomeFilter filter);
    }

    public sealed class IncomeQueries : IIncomeQueries
    {
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;

        public IncomeQueries(ICurrentRequestService currentRequestService, ICacheService cacheService)
        {
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
        }

        public Result<PaginatedList<DtoIncome>> GetAll(DtoIncomeFilter filter)
        {
            var dryers = GetFilter(filter).OrderByDescending(x => x.UpdateTime);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoIncome>.Create(dryers, pageNumber, pageSize);
            return Result<PaginatedList<DtoIncome>>.Success(result);
        }

        private IQueryable<Income> GetFilter(DtoIncomeFilter filter)
        {
            var incomes = _cacheService.GetIncomes();
            if (filter == null)
                return incomes.Where(v => false);

            if (_currentRequestService.IsNotAdmin)
            {
                if (_currentRequestService.RiceMillId.IsNullOrEmpty())
                    return incomes.Where(rm => false);

                incomes = incomes.Where(rm => rm.RiceMillId.Equals(_currentRequestService.RiceMillId.Value));
            }
            if (filter.Id.IsNotNullOrEmpty())
                incomes = incomes.Where(rt => rt.Id.Equals(filter.Id.Value));

            if (filter.Ids.IsCollectionNotNullOrEmpty())
                incomes = incomes.Where(rt => filter.Ids.Contains(rt.Id));

            if (filter.RiceMillId.IsNotNullOrEmpty())
                incomes = incomes.Where(rt => rt.RiceMillId.Equals(filter.RiceMillId));

            if (filter.IncomeTimeLower.HasValue)
                incomes = incomes.Where(rt => rt.IncomeTime < filter.IncomeTimeLower.Value);

            if (filter.IncomeTime.HasValue)
                incomes = incomes.Where(rt => rt.IncomeTime.Equals(filter.IncomeTime.Value));

            if (filter.IncomeTimeGreater.HasValue)
                incomes = incomes.Where(rt => rt.IncomeTime > filter.IncomeTimeGreater.Value);

            if (filter.UnbrokenRiceLower.HasValue)
                incomes = incomes.Where(p => p.UnbrokenRice < filter.UnbrokenRiceLower.Value);

            if (filter.UnbrokenRice.HasValue)
                incomes = incomes.Where(p => p.UnbrokenRice == filter.UnbrokenRice.Value);

            if (filter.UnbrokenRiceGreater.HasValue)
                incomes = incomes.Where(p => p.UnbrokenRice > filter.UnbrokenRiceGreater.Value);

            if (filter.BrokenRiceLower.HasValue)
                incomes = incomes.Where(p => p.BrokenRice < filter.BrokenRiceLower.Value);

            if (filter.BrokenRice.HasValue)
                incomes = incomes.Where(p => p.BrokenRice == filter.BrokenRice.Value);

            if (filter.BrokenRiceGreater.HasValue)
                incomes = incomes.Where(p => p.BrokenRice > filter.BrokenRiceGreater.Value);

            if (filter.FlourLower.HasValue)
                incomes = incomes.Where(p => p.Flour < filter.FlourLower.Value);

            if (filter.Flour.HasValue)
                incomes = incomes.Where(p => p.Flour == filter.Flour.Value);

            if (filter.FlourGreater.HasValue)
                incomes = incomes.Where(p => p.Flour > filter.FlourGreater.Value);

            if (filter.Description.IsNotNullOrEmpty())
                incomes = incomes.Where(p => p.Description.Contains(filter.Description));

            return incomes;
        }
    }
}