using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DryerServices.Dto;

namespace RiceMill.Ui.Services.UseCases.DryerServices
{
    internal interface IDryerServices
    {
        Task<Result<PaginatedList<DtoDryer>>> Get(DtoDryerFilter filter);

        Task<Result<DtoDryer>> Update(DtoUpdateDryer dtoUpdate);

        Task<Result<DtoDryer>> Add(DtoCreateDryer dtoCreate);

        Task<Result<bool>> Delete(Guid id);
    }
}