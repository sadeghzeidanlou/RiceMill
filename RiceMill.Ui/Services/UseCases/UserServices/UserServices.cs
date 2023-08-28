using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services.UseCases.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly ISendRequestService _sendRequestService;

        public UserServices(ISendRequestService sendRequestService) => _sendRequestService = sendRequestService;

        public async Task SetToken(DtoLogin dtoLogin)
        {
            var sendRequest = new DtoSendRequest("api/v1/User/GenerateToken", HttpMethod.Post);
            var userToken = await _sendRequestService.SendRequestAsync<DtoLogin, Result<DtoTokenInfo>>(dtoLogin, sendRequest);
            ApplicationStaticContext.Token = userToken.Data.Token;
        }

        public async Task<Result<PaginatedList<DtoUser>>> GetUsers(DtoUserFilter filter)
        {
            var sendRequest = new DtoSendRequest("api/v1/User", HttpMethod.Get);
            return await _sendRequestService.SendRequestAsync<DtoUserFilter, Result<PaginatedList<DtoUser>>>(filter, sendRequest);
        }
    }
}