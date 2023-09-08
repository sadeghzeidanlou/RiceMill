using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.VillageServices.Dto;
using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services.UseCases.VillageServices
{
    internal sealed class VillageServices : IVillageServices
    {
        private readonly ISendRequestService _sendRequestService;
        public VillageServices() => _sendRequestService = new SendRequestService();

        public async Task<Result<DtoVillage>> Add(DtoCreateVillage dtoCreate)
        {
            var sendRequest = new DtoSendRequest("api/v1/Village", HttpMethod.Post);
            return await _sendRequestService.SendRequestAsync<DtoCreateVillage, Result<DtoVillage>>(dtoCreate, sendRequest);
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            var sendRequest = new DtoSendRequest($"api/v1/Village/{id}", HttpMethod.Delete);
            return await _sendRequestService.SendRequestAsync<object, Result<bool>>(null, sendRequest);
        }

        public async Task<Result<PaginatedList<DtoVillage>>> Get(DtoVillageFilter filter)
        {
            var sendRequest = new DtoSendRequest("api/v1/Village", HttpMethod.Get);
            return await _sendRequestService.SendRequestAsync<DtoVillageFilter, Result<PaginatedList<DtoVillage>>>(filter, sendRequest);
        }

        public async Task<Result<DtoVillage>> Update(DtoUpdateVillage dtoUpdate)
        {
            var sendRequest = new DtoSendRequest("api/v1/Village", HttpMethod.Put);
            return await _sendRequestService.SendRequestAsync<DtoUpdateVillage, Result<DtoVillage>>(dtoUpdate, sendRequest);
        }
    }
}