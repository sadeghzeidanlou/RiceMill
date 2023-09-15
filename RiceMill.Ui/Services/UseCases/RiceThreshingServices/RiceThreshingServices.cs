using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.RiceThreshingServices.Dto;
using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services.UseCases.RiceThreshingServices
{
    internal sealed class RiceThreshingServices : IRiceThreshingServices
    {
        private readonly ISendRequestService _sendRequestService;
        public RiceThreshingServices() => _sendRequestService = new SendRequestService();

        public async Task<Result<DtoRiceThreshing>> Add(DtoCreateRiceThreshing dtoCreate)
        {
            var sendRequest = new DtoSendRequest("api/v1/RiceThreshing", HttpMethod.Post);
            return await _sendRequestService.SendRequestAsync<DtoCreateRiceThreshing, Result<DtoRiceThreshing>>(dtoCreate, sendRequest);
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            var sendRequest = new DtoSendRequest($"api/v1/RiceThreshing/{id}", HttpMethod.Delete);
            return await _sendRequestService.SendRequestAsync<object, Result<bool>>(null, sendRequest);
        }

        public async Task<Result<PaginatedList<DtoRiceThreshing>>> Get(DtoRiceThreshingFilter filter)
        {
            var sendRequest = new DtoSendRequest("api/v1/RiceThreshing", HttpMethod.Get);
            return await _sendRequestService.SendRequestAsync<DtoRiceThreshingFilter, Result<PaginatedList<DtoRiceThreshing>>>(filter, sendRequest);
        }

        public async Task<Result<DtoRiceThreshing>> Update(DtoUpdateRiceThreshing dtoUpdate)
        {
            var sendRequest = new DtoSendRequest("api/v1/RiceThreshing", HttpMethod.Put);
            return await _sendRequestService.SendRequestAsync<DtoUpdateRiceThreshing, Result<DtoRiceThreshing>>(dtoUpdate, sendRequest);
        }
    }
}