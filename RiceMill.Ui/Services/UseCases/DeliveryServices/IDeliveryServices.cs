using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DeliveryServices.Dto;

namespace RiceMill.Ui.Services.UseCases.DeliveryServices
{
    internal interface IDeliveryServices
    {
        Task<Result<PaginatedList<DtoDelivery>>> Get(DtoDeliveryFilter filter);

        Task<Result<DtoDelivery>> Update(DtoUpdateDelivery dtoUpdate);

        Task<Result<DtoDelivery>> Add(DtoCreateDelivery dtoCreate);

        Task<Result<bool>> Delete(Guid id);
    }
}