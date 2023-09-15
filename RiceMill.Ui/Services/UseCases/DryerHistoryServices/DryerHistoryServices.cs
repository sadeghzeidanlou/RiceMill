using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DryerHistoryServices.Dto;
using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services.UseCases.DryerHistoryServices
{
    internal sealed class DryerHistoryServices : IDryerHistoryServices
    {
        private readonly ISendRequestService _sendRequestService;
        public DryerHistoryServices() => _sendRequestService = new SendRequestService();

        public async Task<Result<DtoDryerHistory>> Add(DtoCreateDryerHistory dtoCreate)
        {
            var sendRequest = new DtoSendRequest("api/v1/DryerHistory", HttpMethod.Post);
            return await _sendRequestService.SendRequestAsync<DtoCreateDryerHistory, Result<DtoDryerHistory>>(dtoCreate, sendRequest);
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            var sendRequest = new DtoSendRequest($"api/v1/DryerHistory/{id}", HttpMethod.Delete);
            return await _sendRequestService.SendRequestAsync<object, Result<bool>>(null, sendRequest);
        }

        public async Task<Result<PaginatedList<DtoDryerHistory>>> Get(DtoDryerHistoryFilter filter)
        {
            var sendRequest = new DtoSendRequest("api/v1/DryerHistory", HttpMethod.Get);
            return await _sendRequestService.SendRequestAsync<DtoDryerHistoryFilter, Result<PaginatedList<DtoDryerHistory>>>(filter, sendRequest);
        }

        public async Task<Result<DtoDryerHistory>> Update(DtoUpdateDryerHistory dtoUpdate)
        {
            var sendRequest = new DtoSendRequest("api/v1/DryerHistory", HttpMethod.Put);
            return await _sendRequestService.SendRequestAsync<DtoUpdateDryerHistory, Result<DtoDryerHistory>>(dtoUpdate, sendRequest);
        }
    }
}