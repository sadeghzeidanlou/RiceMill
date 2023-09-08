using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.VehicleServices.Dto;
using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services.UseCases.VehicleServices
{
    internal sealed class VehicleServices : IVehicleServices
    {
        private readonly ISendRequestService _sendRequestService;
        public VehicleServices() => _sendRequestService = new SendRequestService();

        public async Task<Result<DtoVehicle>> Add(DtoCreateVehicle dtoCreate)
        {
            var sendRequest = new DtoSendRequest("api/v1/Vehicle", HttpMethod.Post);
            return await _sendRequestService.SendRequestAsync<DtoCreateVehicle, Result<DtoVehicle>>(dtoCreate, sendRequest);
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            var sendRequest = new DtoSendRequest($"api/v1/Vehicle/{id}", HttpMethod.Delete);
            return await _sendRequestService.SendRequestAsync<object, Result<bool>>(null, sendRequest);
        }

        public async Task<Result<PaginatedList<DtoVehicle>>> Get(DtoVehicleFilter filter)
        {
            var sendRequest = new DtoSendRequest("api/v1/Vehicle", HttpMethod.Get);
            return await _sendRequestService.SendRequestAsync<DtoVehicleFilter, Result<PaginatedList<DtoVehicle>>>(filter, sendRequest);
        }

        public async Task<Result<DtoVehicle>> Update(DtoUpdateVehicle dtoUpdate)
        {
            var sendRequest = new DtoSendRequest("api/v1/Vehicle", HttpMethod.Put);
            return await _sendRequestService.SendRequestAsync<DtoUpdateVehicle, Result<DtoVehicle>>(dtoUpdate, sendRequest);
        }
    }
}