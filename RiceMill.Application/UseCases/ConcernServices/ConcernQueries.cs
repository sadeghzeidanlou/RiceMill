using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Domain.Models;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.ConcernServices
{
    public interface IConcernQueries
    {
        Result<PaginatedList<DtoConcern>> GetAll(DtoConcernFilter filter);
    }

    public sealed class ConcernQueries : IConcernQueries
    {
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;

        public ConcernQueries(ICurrentRequestService currentRequestService, ICacheService cacheService)
        {
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
        }

        public Result<PaginatedList<DtoConcern>> GetAll(DtoConcernFilter filter)
        {
            var concerns = GetFilter(filter).OrderByDescending(x=>x.UpdateTime);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoConcern>.Create(concerns, pageNumber, pageSize);
            return Result<PaginatedList<DtoConcern>>.Success(result);
        }

        private IQueryable<Concern> GetFilter(DtoConcernFilter filter)
        {
            var concerns = _cacheService.GetConcerns();
            if (filter == null)
                return concerns.Where(v => false);

            if (_currentRequestService.IsNotAdmin)
            {
                if (_currentRequestService.RiceMillId.IsNullOrEmpty())
                    return concerns.Where(rm => false);

                concerns = concerns.Where(rm => rm.RiceMillId.Equals(_currentRequestService.RiceMillId.Value));
            }
            if (filter.Id.IsNotNullOrEmpty())
                concerns = concerns.Where(c => c.Id.Equals(filter.Id.Value));

            if (filter.Ids.IsCollectionNotNullOrEmpty())
                concerns = concerns.Where(c => filter.Ids.Contains(c.Id));

            if (filter.RiceMillId.IsNotNullOrEmpty())
                concerns = concerns.Where(c => c.RiceMillId.Equals(filter.RiceMillId));

            if (filter.Title.IsNotNullOrEmpty())
                concerns = concerns.Where(c => c.Title.Contains(filter.Title));

            return concerns;
        }
    }
}