using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services.UseCases.ConcernServices
{
    public class ConcernServices : IConcernServices
    {
        private readonly ISendRequestService _sendRequestService;
        public ConcernServices() => _sendRequestService = new SendRequestService();

        public async Task<Result<DtoConcern>> Add(DtoCreateConcern dtoCreate)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<PaginatedList<DtoConcern>>> Get(DtoConcernFilter filter)
        {
            var sendRequest = new DtoSendRequest("api/v1/Concern", HttpMethod.Get);
            return await _sendRequestService.SendRequestAsync<DtoConcernFilter, Result<PaginatedList<DtoConcern>>>(filter, sendRequest);
        }

        public async Task<Result<DtoConcern>> Update(DtoUpdateConcern dtoUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
