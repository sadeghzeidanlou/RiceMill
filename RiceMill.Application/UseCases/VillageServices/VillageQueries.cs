using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.VillageServices.Dto;
using RiceMill.Domain.Models;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.VillageServices
{
    public interface IVillageQueries
    {
        Result<PaginatedList<DtoVillage>> GetAll(DtoVillageFilter dtoFilter);
    }

    public class VillageQueries : IVillageQueries
    {
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;

        public VillageQueries(ICurrentRequestService currentRequestService, ICacheService cacheService)
        {
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
        }

        public Result<PaginatedList<DtoVillage>> GetAll(DtoVillageFilter filter)
        {
            var villages = GetFilter(filter);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoVillage>.Create(villages, pageNumber, pageSize);
            return Result<PaginatedList<DtoVillage>>.Success(result);
        }

        private IQueryable<Village> GetFilter(DtoVillageFilter filter)
        {
            var villages = _cacheService.GetVillages();
            if (filter == null || (_currentRequestService.IsNotAdmin && filter.RiceMillId.IsNullOrEmpty()))
                return villages.Where(c => false);

            if (filter.Id.IsNotNullOrEmpty())
                villages = villages.Where(c => c.Id.Equals(filter.Id));

            if (filter.RiceMillId.IsNotNullOrEmpty())
                villages = villages.Where(c => c.RiceMillId.Equals(filter.RiceMillId));

            if (filter.Title.IsNotNullOrEmpty())
                villages = villages.Where(c => c.Title.Contains(filter.Title));

            return villages;
        }
    }
}