using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.UserServices.Dto;
using System.IdentityModel.Tokens.Jwt;

namespace RiceMill.Ui.Services.UseCases.UserServices
{
    internal interface IUserServices
    {
        Task SetToken(DtoLogin dtoLogin);

        Task<Result<PaginatedList<DtoUser>>> GetUsers(DtoUserFilter filter);

        Task<bool> TokenIsValid();

        JwtSecurityToken ReadToken(string token);
    }
}