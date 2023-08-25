using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services.UseCases.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly ISendRequestService _sendRequestService;

        public UserServices(ISendRequestService sendRequestService) => _sendRequestService = sendRequestService;

        public async Task<Result<DtoTokenInfo>> GetToken(DtoLogin dtoLogin, DtoSendRequest sendRequest = null)
        {
            sendRequest ??= new DtoSendRequest("api/v1/User/GenerateToken", HttpMethod.Post);
            return await _sendRequestService.SendRequestAsync<DtoLogin, Result<DtoTokenInfo>>(dtoLogin, sendRequest);
        }
    }
}