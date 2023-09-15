using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.IncomeServices.Dto;
using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services.UseCases.IncomeServices
{
    internal sealed class IncomeServices : IIncomeServices
    {
        private readonly ISendRequestService _sendRequestService;
        public IncomeServices() => _sendRequestService = new SendRequestService();

        public async Task<Result<DtoIncome>> Add(DtoCreateIncome dtoCreate)
        {
            var sendRequest = new DtoSendRequest("api/v1/Income", HttpMethod.Post);
            return await _sendRequestService.SendRequestAsync<DtoCreateIncome, Result<DtoIncome>>(dtoCreate, sendRequest);
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            var sendRequest = new DtoSendRequest($"api/v1/Income/{id}", HttpMethod.Delete);
            return await _sendRequestService.SendRequestAsync<object, Result<bool>>(null, sendRequest);
        }

        public async Task<Result<PaginatedList<DtoIncome>>> Get(DtoIncomeFilter filter)
        {
            var sendRequest = new DtoSendRequest("api/v1/Income", HttpMethod.Get);
            return await _sendRequestService.SendRequestAsync<DtoIncomeFilter, Result<PaginatedList<DtoIncome>>>(filter, sendRequest);
        }

        public async Task<Result<DtoIncome>> Update(DtoUpdateIncome dtoUpdate)
        {
            var sendRequest = new DtoSendRequest("api/v1/Income", HttpMethod.Put);
            return await _sendRequestService.SendRequestAsync<DtoUpdateIncome, Result<DtoIncome>>(dtoUpdate, sendRequest);
        }
    }
}