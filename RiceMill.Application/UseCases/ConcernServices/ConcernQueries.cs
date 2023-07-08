using Microsoft.EntityFrameworkCore;
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

        public async Task<Result<PaginatedList<DtoConcern>>> GetAllAsync(DtoConcernFilter filter)
        {
            var concerns = GetFilter(filter);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoConcern>.CreateAsync(concerns, pageNumber, pageSize).Result;
            return await Task.FromResult(Result<PaginatedList<DtoConcern>>.Success(result));
        }

        private IQueryable<Concern> GetFilter(DtoConcernFilter filter)
        {
            var concerns = _applicationDbContext.Concerns.AsNoTracking().AsQueryable();
            if (filter == null || (_currentRequestService.IsNotAdmin && filter.RiceMillId.IsNullOrEmpty()))
                return concerns.Where(c => false);

            if (filter.RiceMillId.IsNotNullOrEmpty())
                concerns = concerns.Where(c => c.RiceMillId.Equals(filter.RiceMillId));

            if (filter.Title.IsNotNullOrEmpty())
                concerns = concerns.Where(c => c.Title.Contains(filter.Title));

            return concerns;
        }
    }
}