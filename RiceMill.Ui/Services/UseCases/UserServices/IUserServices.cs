using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Ui.Common.Models;
using System.IdentityModel.Tokens.Jwt;

namespace RiceMill.Ui.Services.UseCases.UserServices
{
    public interface IUserServices
    {
        Task SetToken(DtoLogin dtoLogin);

        Task<Result<PaginatedList<DtoUser>>> GetUsers(DtoUserFilter filter);

        Task<bool> TokenIsValid();

        JwtSecurityToken ReadToken(string token);
    }
}