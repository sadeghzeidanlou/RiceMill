using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.RiceMillServices.Dto;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.RiceMillServices
{
    public interface IRiceMillQueries
    {
        Result<PaginatedList<DtoRiceMill>> GetAll(DtoRiceMillFilter riceMillFilter);
    }

    public sealed class RiceMillQueries : IRiceMillQueries
    {
        private readonly ICacheService _cacheService;
        private readonly ICurrentRequestService _currentRequestService;

        public RiceMillQueries(ICacheService cacheService, ICurrentRequestService currentRequestService)
        {
            _cacheService = cacheService;
            _currentRequestService = currentRequestService;
        }

        public Result<PaginatedList<DtoRiceMill>> GetAll(DtoRiceMillFilter filter)
        {
            if (_currentRequestService.HasNotAccessToRiceMills)
                return Result<PaginatedList<DtoRiceMill>>.Forbidden();

            var riceMilles = GetFilter(filter).OrderByDescending(x => x.UpdateTime);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoRiceMill>.Create(riceMilles, pageNumber, pageSize);
            return Result<PaginatedList<DtoRiceMill>>.Success(result);
        }

        private IQueryable<Domain.Models.RiceMill> GetFilter(DtoRiceMillFilter filter)
        {
            var riceMilles = _cacheService.GetRiceMills();
            if (filter == null || (_currentRequestService.IsNotAdmin && filter.Id.IsNullOrEmpty()))
                return riceMilles.Where(rm => false);

            if (filter.Id.IsNotNullOrEmpty())
                riceMilles = riceMilles.Where(rm => rm.Id.Equals(filter.Id.Value));

            if (filter.Ids.IsCollectionNotNullOrEmpty())
                riceMilles = riceMilles.Where(rm => filter.Ids.Contains(rm.Id));

            if (filter.Title.IsNotNullOrEmpty())
                riceMilles = riceMilles.Where(rm => rm.Title.Contains(filter.Title));

            if (filter.Address.IsNotNullOrEmpty())
                riceMilles = riceMilles.Where(rm => rm.Address.Contains(filter.Address));

            if (filter.Wage.HasValue)
                riceMilles = riceMilles.Where(rm => rm.Wage == filter.Wage.Value);

            if (filter.WageGreeterThan.HasValue)
                riceMilles = riceMilles.Where(rm => rm.Wage > filter.WageGreeterThan.Value);

            if (filter.WageLowerThan.HasValue)
                riceMilles = riceMilles.Where(rm => rm.Wage < filter.WageLowerThan.Value);

            if (filter.Phone.IsNotNullOrEmpty())
                riceMilles = riceMilles.Where(rm => rm.Phone.Contains(filter.Phone));

            if (filter.PostalCode.IsNotNullOrEmpty())
                riceMilles = riceMilles.Where(rm => rm.PostalCode.Contains(filter.PostalCode));

            if (filter.Description.IsNotNullOrEmpty())
                riceMilles = riceMilles.Where(rm => rm.Description.Contains(filter.Description));

            if (filter.OwnerPersonId.IsNotNullOrEmpty())
                riceMilles = riceMilles.Where(rm => rm.OwnerPersonId == filter.OwnerPersonId.Value);

            return riceMilles;
        }
    }
}