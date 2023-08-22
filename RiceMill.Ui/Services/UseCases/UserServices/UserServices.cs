using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services.UseCases.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly ISendRequestService _sendRequestService;

        public UserServices(ISendRequestService sendRequestService) => _sendRequestService = sendRequestService;

        public Result<DtoTokenInfo> GetToken(DtoLogin dtoLogin, DtoSendRequest sendRequest = null)
        {
            sendRequest ??= new DtoSendRequest { HttpMethod = HttpMethod.Post, MethodName = "api/v1/User/GenerateToken" };
            return _sendRequestService.SendRequest<DtoLogin, Result<DtoTokenInfo>>(dtoLogin, sendRequest);
        }
    }
}