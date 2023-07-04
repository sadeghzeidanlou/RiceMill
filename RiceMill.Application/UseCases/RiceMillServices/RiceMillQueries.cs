using Mapster;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.RiceMillServices.Dto;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.RiceMillServices
{
    public interface IRiceMillQueries
    {
        Task<Result<PaginatedList<DtoRiceMill>>> GetAllAsync(DtoRiceMillFilter riceMillFilter);
    }

    public class RiceMillQueries : IRiceMillQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;

        public RiceMillQueries(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
        }

        public Task<Result<PaginatedList<DtoRiceMill>>> GetAllAsync(DtoRiceMillFilter filter)
        {
            if (!_currentRequestService.HasAccessToRiceMills)
                return Task.FromResult(Result<PaginatedList<DtoRiceMill>>.Failure(new Error(Common.Models.Enums.ResultStatusEnum.Forbidden), System.Net.HttpStatusCode.Forbidden));

            var riceMilles = _applicationDbContext.RiceMills.AsQueryable();
            riceMilles = GetFilter(riceMilles, filter);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var tempResult = PaginatedList<Domain.Models.RiceMill>.CreateAsync(riceMilles, pageNumber, pageSize).Result;
            var result = new PaginatedList<DtoRiceMill>(tempResult.Items.Adapt<List<DtoRiceMill>>(), tempResult.TotalCount, pageNumber, pageSize);
            return Task.FromResult(Result<PaginatedList<DtoRiceMill>>.Success(result));
        }

        private IQueryable<Domain.Models.RiceMill> GetFilter(IQueryable<Domain.Models.RiceMill> riceMill, DtoRiceMillFilter filter)
        {
            if (_currentRequestService.IsManager)
                riceMill = riceMill.Where(rm => rm.Id == _currentRequestService.RiceMillId);

            if (filter == null)
                return riceMill;

            if (filter.Title.IsNotNullOrEmpty())
                riceMill = riceMill.Where(c => c.Title.Contains(filter.Title));

            if (filter.Address.IsNotNullOrEmpty())
                riceMill = riceMill.Where(c => c.Address.Contains(filter.Address));

            if (filter.Wage.HasValue)
                riceMill = riceMill.Where(c => c.Wage == filter.Wage.Value);

            if (filter.WageGreeterThan.HasValue)
                riceMill = riceMill.Where(c => c.Wage > filter.WageGreeterThan.Value);

            if (filter.WageLowerThan.HasValue)
                riceMill = riceMill.Where(c => c.Wage < filter.WageLowerThan.Value);

            if (filter.Phone.IsNotNullOrEmpty())
                riceMill = riceMill.Where(c => c.Phone.Contains(filter.Phone));

            if (filter.PostalCode.IsNotNullOrEmpty())
                riceMill = riceMill.Where(c => c.PostalCode.Contains(filter.PostalCode));

            if (filter.Description.IsNotNullOrEmpty())
                riceMill = riceMill.Where(c => c.Description.Contains(filter.Description));

            return riceMill;
        }
    }
}