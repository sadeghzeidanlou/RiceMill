using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DryerServices.Dto;
using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services.UseCases.DryerServices
{
    internal class DryerServices : IDryerServices
    {
        private readonly ISendRequestService _sendRequestService;
        public DryerServices() => _sendRequestService = new SendRequestService();

        public async Task<Result<DtoDryer>> Add(DtoCreateDryer dtoCreate)
        {
            var sendRequest = new DtoSendRequest("api/v1/Dryer", HttpMethod.Post);
            return await _sendRequestService.SendRequestAsync<DtoCreateDryer, Result<DtoDryer>>(dtoCreate, sendRequest);
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            var sendRequest = new DtoSendRequest($"api/v1/Dryer/{id}", HttpMethod.Delete);
            return await _sendRequestService.SendRequestAsync<object, Result<bool>>(null, sendRequest);
        }

        public async Task<Result<PaginatedList<DtoDryer>>> Get(DtoDryerFilter filter)
        {
            var sendRequest = new DtoSendRequest("api/v1/Dryer", HttpMethod.Get);
            return await _sendRequestService.SendRequestAsync<DtoDryerFilter, Result<PaginatedList<DtoDryer>>>(filter, sendRequest);
        }

        public async Task<Result<DtoDryer>> Update(DtoUpdateDryer dtoUpdate)
        {
            var sendRequest = new DtoSendRequest("api/v1/Dryer", HttpMethod.Put);
            return await _sendRequestService.SendRequestAsync<DtoUpdateDryer, Result<DtoDryer>>(dtoUpdate, sendRequest);
        }
    }
}