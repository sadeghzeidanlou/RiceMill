using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.VillageServices.Dto;

namespace RiceMill.Application.UseCases.VillageServices
{
    public interface IVillageCommands : IBaseUseCaseCommands
    {
        Task<Result<DtoVillage>> CreateAsync(DtoCreateVillage village);

        Task<Result<DtoVillage>> UpdateAsync(DtoUpdateVillage village);
    }

    public class VillageCommands : IVillageCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public VillageCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoVillage>> CreateAsync(DtoCreateVillage village)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoVillage>> UpdateAsync(DtoUpdateVillage village)
        {
            throw new NotImplementedException();
        }
    }
}