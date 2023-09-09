using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.RiceMillServices.Dto;
using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services.UseCases.RiceMillServices
{
    internal class RiceMillServices : IRiceMillServices
    {
        private readonly ISendRequestService _sendRequestService;
        public RiceMillServices() => _sendRequestService = new SendRequestService();

        public async Task<Result<DtoRiceMill>> Add(DtoCreateRiceMill dtoCreate)
        {
            var sendRequest = new DtoSendRequest("api/v1/RiceMill", HttpMethod.Post);
            return await _sendRequestService.SendRequestAsync<DtoCreateRiceMill, Result<DtoRiceMill>>(dtoCreate, sendRequest);
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            var sendRequest = new DtoSendRequest($"api/v1/RiceMill/{id}", HttpMethod.Delete);
            return await _sendRequestService.SendRequestAsync<object, Result<bool>>(null, sendRequest);
        }

        public async Task<Result<PaginatedList<DtoRiceMill>>> Get(DtoRiceMillFilter filter)
        {
            var sendRequest = new DtoSendRequest("api/v1/RiceMill", HttpMethod.Get);
            return await _sendRequestService.SendRequestAsync<DtoRiceMillFilter, Result<PaginatedList<DtoRiceMill>>>(filter, sendRequest);
        }

        public async Task<Result<DtoRiceMill>> Update(DtoUpdateRiceMill dtoUpdate)
        {
            var sendRequest = new DtoSendRequest("api/v1/RiceMill", HttpMethod.Put);
            return await _sendRequestService.SendRequestAsync<DtoUpdateRiceMill, Result<DtoRiceMill>>(dtoUpdate, sendRequest);
        }
    }
}