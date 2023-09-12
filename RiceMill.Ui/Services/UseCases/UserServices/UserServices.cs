using RiceMill.Application.Common.Models.Resource;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Common.Models;
using Shared.ExtensionMethods;
using System.IdentityModel.Tokens.Jwt;

namespace RiceMill.Ui.Services.UseCases.UserServices
{
    internal class UserServices : IUserServices
    {
        private readonly ISendRequestService _sendRequestService;
        public UserServices() => _sendRequestService = new SendRequestService();

        public async Task SetToken(DtoLogin dtoLogin)
        {
            var tokenIsValid = await TokenIsValid();
            if (tokenIsValid)
                return;

            var sendRequest = new DtoSendRequest("api/v1/User/GenerateToken", HttpMethod.Post);
            var userToken = await _sendRequestService.SendRequestAsync<DtoLogin, Result<DtoTokenInfo>>(dtoLogin, sendRequest);
            ApplicationStaticContext.Token = userToken.Data.Token;
            await SecureStorage.Default.SetAsync(SharedResource.TokenKey, ApplicationStaticContext.Token);
            await SecureStorage.SetAsync(SharedResource.TokenKey, ApplicationStaticContext.Token);
        }

        public async Task<Result<PaginatedList<DtoUser>>> GetUsers(DtoUserFilter filter)
        {
            var sendRequest = new DtoSendRequest("api/v1/User", HttpMethod.Get);
            return await _sendRequestService.SendRequestAsync<DtoUserFilter, Result<PaginatedList<DtoUser>>>(filter, sendRequest);
        }

        public async Task<bool> TokenIsValid()
        {
            string tokenValueDefault = await SecureStorage.Default.GetAsync(SharedResource.TokenKey);
            string tokenValue = await SecureStorage.GetAsync(SharedResource.TokenKey);
            if (tokenValue.IsNullOrEmpty() && tokenValueDefault.IsNullOrEmpty())
                return false;

            ApplicationStaticContext.Token = tokenValueDefault.IsNullOrEmpty() ? tokenValue : tokenValueDefault;
            var tokenDetail = ReadToken(ApplicationStaticContext.Token);
            return tokenDetail?.ValidTo >= DateTime.UtcNow;
        }

        public JwtSecurityToken ReadToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(token);
        }
    }
}