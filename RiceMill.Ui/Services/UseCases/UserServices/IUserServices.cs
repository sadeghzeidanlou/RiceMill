using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Ui.Common.Models;

namespace RiceMill.Ui.Services.UseCases.UserServices
{
    public interface IUserServices
    {
        Task<Result<DtoTokenInfo>> GetToken(DtoLogin dtoLogin, DtoSendRequest sendRequest = null);
    }
}