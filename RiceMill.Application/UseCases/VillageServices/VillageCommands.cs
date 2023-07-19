using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.VillageServices.Dto;

namespace RiceMill.Application.UseCases.VillageServices
{
    public interface IVillageCommands : IBaseUseCaseCommands
    {
        Result<DtoVillage> Create(DtoCreateVillage village);

        Result<DtoVillage> Update(DtoUpdateVillage village);
    }

    public class VillageCommands : IVillageCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public VillageCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<DtoVillage> Create(DtoCreateVillage village)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Result<DtoVillage> Update(DtoUpdateVillage village)
        {
            throw new NotImplementedException();
        }
    }
}