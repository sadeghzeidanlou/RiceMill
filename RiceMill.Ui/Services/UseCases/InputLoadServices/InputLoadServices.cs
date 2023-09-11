using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services.UseCases.InputLoadServices
{
    internal sealed class InputLoadServices : IInputLoadServices
    {
        private readonly ISendRequestService _sendRequestService;
        public InputLoadServices() => _sendRequestService = new SendRequestService();

        public async Task<Result<DtoInputLoad>> Add(DtoCreateInputLoad dtoCreate)
        {
            var sendRequest = new DtoSendRequest("api/v1/InputLoad", HttpMethod.Post);
            return await _sendRequestService.SendRequestAsync<DtoCreateInputLoad, Result<DtoInputLoad>>(dtoCreate, sendRequest);
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            var sendRequest = new DtoSendRequest($"api/v1/InputLoad/{id}", HttpMethod.Delete);
            return await _sendRequestService.SendRequestAsync<object, Result<bool>>(null, sendRequest);
        }

        public async Task<Result<PaginatedList<DtoInputLoad>>> Get(DtoInputLoadFilter filter)
        {
            var sendRequest = new DtoSendRequest("api/v1/InputLoad", HttpMethod.Get);
            return await _sendRequestService.SendRequestAsync<DtoInputLoadFilter, Result<PaginatedList<DtoInputLoad>>>(filter, sendRequest);
        }

        public async Task<Result<DtoInputLoad>> Update(DtoUpdateInputLoad dtoUpdate)
        {
            var sendRequest = new DtoSendRequest("api/v1/InputLoad", HttpMethod.Put);
            return await _sendRequestService.SendRequestAsync<DtoUpdateInputLoad, Result<DtoInputLoad>>(dtoUpdate, sendRequest);
        }
    }
}