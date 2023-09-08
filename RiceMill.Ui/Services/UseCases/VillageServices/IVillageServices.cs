using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.VillageServices.Dto;

namespace RiceMill.Ui.Services.UseCases.VillageServices
{
    public interface IVillageServices
    {
        Task<Result<PaginatedList<DtoVillage>>> Get(DtoVillageFilter filter);

        Task<Result<DtoVillage>> Update(DtoUpdateVillage dtoUpdate);

        Task<Result<DtoVillage>> Add(DtoCreateVillage dtoCreate);

        Task<Result<bool>> Delete(Guid id);
    }
}