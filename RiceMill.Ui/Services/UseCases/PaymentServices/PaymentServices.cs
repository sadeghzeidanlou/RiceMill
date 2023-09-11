using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.PaymentServices.Dto;
using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services.UseCases.PaymentServices
{
    internal sealed class PaymentServices : IPaymentServices
    {
        private readonly ISendRequestService _sendRequestService;
        public PaymentServices() => _sendRequestService = new SendRequestService();

        public async Task<Result<DtoPayment>> Add(DtoCreatePayment dtoCreate)
        {
            var sendRequest = new DtoSendRequest("api/v1/Payment", HttpMethod.Post);
            return await _sendRequestService.SendRequestAsync<DtoCreatePayment, Result<DtoPayment>>(dtoCreate, sendRequest);
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            var sendRequest = new DtoSendRequest($"api/v1/Payment/{id}", HttpMethod.Delete);
            return await _sendRequestService.SendRequestAsync<object, Result<bool>>(null, sendRequest);
        }

        public async Task<Result<PaginatedList<DtoPayment>>> Get(DtoPaymentFilter filter)
        {
            var sendRequest = new DtoSendRequest("api/v1/Payment", HttpMethod.Get);
            return await _sendRequestService.SendRequestAsync<DtoPaymentFilter, Result<PaginatedList<DtoPayment>>>(filter, sendRequest);
        }

        public async Task<Result<DtoPayment>> Update(DtoUpdatePayment dtoUpdate)
        {
            var sendRequest = new DtoSendRequest("api/v1/Payment", HttpMethod.Put);
            return await _sendRequestService.SendRequestAsync<DtoUpdatePayment, Result<DtoPayment>>(dtoUpdate, sendRequest);
        }
    }
}