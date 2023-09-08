using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.VehicleServices.Dto;

namespace RiceMill.Ui.Services.UseCases.VehicleServices
{
    internal interface IVehicleServices
    {
        Task<Result<PaginatedList<DtoVehicle>>> Get(DtoVehicleFilter filter);

        Task<Result<DtoVehicle>> Update(DtoUpdateVehicle dtoUpdate);

        Task<Result<DtoVehicle>> Add(DtoCreateVehicle dtoCreate);

        Task<Result<bool>> Delete(Guid id);
    }
}