using Microsoft.AspNetCore.Mvc;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.UserServices;
using RiceMill.Application.UseCases.UserServices.Dto;

namespace RiceMill.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserCommands _userCommands;

        private readonly IUserQueries _userQueries;

        public UserController(IUserCommands userCommands, IUserQueries userQueries)
        {
            _userCommands = userCommands;
            _userQueries = userQueries;
        }

        [HttpGet]
        public Result<PaginatedList<DtoUser>> Get(DtoUserFilter dtoUserFilter) => _userQueries.GetAll(dtoUserFilter);

        [HttpPost]
        public Result<DtoUser> Post([FromBody] DtoCreateUser dtoCreateUser) => _userCommands.Create(dtoCreateUser);

        [HttpPut]
        public Result<DtoUser> Put([FromBody] DtoUpdateUser dtoUpdateUser) => _userCommands.Update(dtoUpdateUser);

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id) => _userCommands.Delete(id);
    }
}