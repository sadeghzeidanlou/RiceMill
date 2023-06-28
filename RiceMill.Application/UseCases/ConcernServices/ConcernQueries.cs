using Mapster;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Domain.Models;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.ConcernServices
{
    public interface IConcernQueries
    {
        Task<Result<PaginatedList<DtoConcern>>> GetAllAsync(DtoConcernFilter filter);

        Task<Result<int>> GetCountAsync(DtoConcernFilter filter);
    }

    public class ConcernQueries : IConcernQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        private readonly ICurrentRequestService _currentRequestService;

        public ConcernQueries(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
        }

        public Task<Result<PaginatedList<DtoConcern>>> GetAllAsync(DtoConcernFilter filter)
        {
            var concerns = _applicationDbContext.Concerns.Where(c => c.RiceMillId == _currentRequestService.RiceMillId).AsQueryable();
            ApplyFilter(concerns, filter);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var tempResult = PaginatedList<Concern>.CreateAsync(concerns, pageNumber, pageSize).Result;
            var result = new PaginatedList<DtoConcern>(tempResult.Items.Adapt<IReadOnlyCollection<DtoConcern>>(), tempResult.TotalCount, pageNumber, pageSize);
            return Task.FromResult(Result<PaginatedList<DtoConcern>>.Success(result));
        }

        public Task<Result<int>> GetCountAsync(DtoConcernFilter filter)
        {
            var concerns = _applicationDbContext.Concerns.Where(c => c.RiceMillId == _currentRequestService.RiceMillId).AsQueryable();
            ApplyFilter(concerns, filter);
            return Task.FromResult(Result<int>.Success(concerns.Count()));
        }

        private static IQueryable<Concern> ApplyFilter(IQueryable<Concern> concerns, DtoConcernFilter filter)
        {
            if (filter != null && filter.Title.IsNotNullOrEmpty())
                concerns = concerns.Where(c => c.Title.Contains(filter.Title));

            return concerns;
        }
    }
}