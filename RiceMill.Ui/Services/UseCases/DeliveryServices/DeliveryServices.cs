using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DeliveryServices.Dto;
using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services.UseCases.DeliveryServices
{
    internal sealed class DeliveryServices : IDeliveryServices
    {
        private readonly ISendRequestService _sendRequestService;
        public DeliveryServices() => _sendRequestService = new SendRequestService();

        public async Task<Result<DtoDelivery>> Add(DtoCreateDelivery dtoCreate)
        {
            var sendRequest = new DtoSendRequest("api/v1/Delivery", HttpMethod.Post);
            return await _sendRequestService.SendRequestAsync<DtoCreateDelivery, Result<DtoDelivery>>(dtoCreate, sendRequest);
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            var sendRequest = new DtoSendRequest($"api/v1/Delivery/{id}", HttpMethod.Delete);
            return await _sendRequestService.SendRequestAsync<object, Result<bool>>(null, sendRequest);
        }

        public async Task<Result<PaginatedList<DtoDelivery>>> Get(DtoDeliveryFilter filter)
        {
            var sendRequest = new DtoSendRequest("api/v1/Delivery", HttpMethod.Get);
            return await _sendRequestService.SendRequestAsync<DtoDeliveryFilter, Result<PaginatedList<DtoDelivery>>>(filter, sendRequest);
        }

        public async Task<Result<DtoDelivery>> Update(DtoUpdateDelivery dtoUpdate)
        {
            var sendRequest = new DtoSendRequest("api/v1/Delivery", HttpMethod.Put);
            return await _sendRequestService.SendRequestAsync<DtoUpdateDelivery, Result<DtoDelivery>>(dtoUpdate, sendRequest);
        }
    }
}