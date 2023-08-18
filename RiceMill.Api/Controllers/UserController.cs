using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiceMill.Api.Services.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.UserServices;
using RiceMill.Application.UseCases.UserServices.Dto;

namespace RiceMill.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public sealed class UserController : BaseController
    {
        private readonly IUserCommands _userCommands;

        private readonly IUserQueries _userQueries;

        private readonly IJwtService _jwtService;

        public UserController(IUserCommands userCommands, IUserQueries userQueries, IJwtService jwtService)
        {
            _userCommands = userCommands;
            _userQueries = userQueries;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("GenerateToken")]
        public Result<DtoTokenInfo> GenerateToken(DtoLogin dtoLogin)
        {
            var userInfo = _userQueries.Login(dtoLogin);
            var result = new Result<DtoTokenInfo>
            {
                Errors = userInfo.Errors,
                HttpStatusCode = userInfo.HttpStatusCode,
                IsSucceeded = userInfo.IsSucceeded
            };
            if (!userInfo.IsSucceeded)
                return result;

            result.Data = new DtoTokenInfo { Token = _jwtService.GenerateToken(userInfo.Data.Id) };
            return result;
        }

        [HttpGet]
        public Result<PaginatedList<DtoUser>> Get([FromQuery] DtoUserFilter dtoFilter) => _userQueries.GetAll(dtoFilter);

        [HttpPost]
        public Result<DtoUser> Post([FromBody] DtoCreateUser dtoCreate) => _userCommands.Create(dtoCreate);

        [HttpPut]
        public Result<DtoUser> Put([FromBody] DtoUpdateUser dtoUpdate) => _userCommands.Update(dtoUpdate);

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id) => _userCommands.Delete(id);
    }
}