using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.VillageServices.Dto;

namespace RiceMill.Application.UseCases.VillageServices
{
    public interface IVillageQueries
    {
        Result<PaginatedList<DtoVillage>> GetAll();
    }

    public class VillageQueries : IVillageQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public VillageQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<PaginatedList<DtoVillage>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}