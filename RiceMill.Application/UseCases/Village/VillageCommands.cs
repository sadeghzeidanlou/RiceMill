using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.Base;
using RiceMill.Application.UseCases.Village.Dto;

namespace RiceMill.Application.UseCases.Village
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

        public Task<Result<int>> DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoVillage>> UpdateAsync(DtoUpdateVillage village)
        {
            throw new NotImplementedException();
        }
    }
}