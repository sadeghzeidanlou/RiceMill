using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services.UseCases.ConcernServices
{
    internal sealed class ConcernServices : IConcernServices
    {
        private readonly ISendRequestService _sendRequestService;
        public ConcernServices() => _sendRequestService = new SendRequestService();

        public async Task<Result<DtoConcern>> Add(DtoCreateConcern dtoCreate)
        {
            var sendRequest = new DtoSendRequest("api/v1/Concern", HttpMethod.Post);
            return await _sendRequestService.SendRequestAsync<DtoCreateConcern, Result<DtoConcern>>(dtoCreate, sendRequest);
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            var sendRequest = new DtoSendRequest($"api/v1/Concern/{id}", HttpMethod.Delete);
            return await _sendRequestService.SendRequestAsync<object, Result<bool>>(null, sendRequest);
        }

        public async Task<Result<PaginatedList<DtoConcern>>> Get(DtoConcernFilter filter)
        {
            var sendRequest = new DtoSendRequest("api/v1/Concern", HttpMethod.Get);
            return await _sendRequestService.SendRequestAsync<DtoConcernFilter, Result<PaginatedList<DtoConcern>>>(filter, sendRequest);
        }

        public async Task<Result<DtoConcern>> Update(DtoUpdateConcern dtoUpdate)
        {
            var sendRequest = new DtoSendRequest("api/v1/Concern", HttpMethod.Put);
            return await _sendRequestService.SendRequestAsync<DtoUpdateConcern, Result<DtoConcern>>(dtoUpdate, sendRequest);
        }
    }
}