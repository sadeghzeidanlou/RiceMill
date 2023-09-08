using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services.UseCases.PersonServices
{
    internal sealed class PersonServices : IPersonServices
    {
        private readonly ISendRequestService _sendRequestService;
        public PersonServices() => _sendRequestService = new SendRequestService();

        public async Task<Result<DtoPerson>> Add(DtoCreatePerson dtoCreate)
        {
            var sendRequest = new DtoSendRequest("api/v1/Person", HttpMethod.Post);
            return await _sendRequestService.SendRequestAsync<DtoCreatePerson, Result<DtoPerson>>(dtoCreate, sendRequest);
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            var sendRequest = new DtoSendRequest($"api/v1/Person/{id}", HttpMethod.Delete);
            return await _sendRequestService.SendRequestAsync<object, Result<bool>>(null, sendRequest);
        }

        public async Task<Result<PaginatedList<DtoPerson>>> Get(DtoPersonFilter filter)
        {
            var sendRequest = new DtoSendRequest("api/v1/Person", HttpMethod.Get);
            return await _sendRequestService.SendRequestAsync<DtoPersonFilter, Result<PaginatedList<DtoPerson>>>(filter, sendRequest);
        }

        public async Task<Result<DtoPerson>> Update(DtoUpdatePerson dtoUpdate)
        {
            var sendRequest = new DtoSendRequest("api/v1/Person", HttpMethod.Put);
            return await _sendRequestService.SendRequestAsync<DtoUpdatePerson, Result<DtoPerson>>(dtoUpdate, sendRequest);
        }
    }
}