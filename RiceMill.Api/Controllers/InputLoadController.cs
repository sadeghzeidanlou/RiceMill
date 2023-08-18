using Microsoft.AspNetCore.Mvc;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.InputLoadServices;
using RiceMill.Application.UseCases.InputLoadServices.Dto;

namespace RiceMill.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public sealed class InputLoadController : BaseController
    {
        private readonly IInputLoadCommands _inputLoadCommands;

        private readonly IInputLoadQueries _inputLoadQueries;

        public InputLoadController(IInputLoadCommands inputLoadCommands, IInputLoadQueries inputLoadQueries)
        {
            _inputLoadCommands = inputLoadCommands;
            _inputLoadQueries = inputLoadQueries;
        }

        [HttpGet]
        public Result<PaginatedList<DtoInputLoad>> Get([FromQuery] DtoInputLoadFilter dtoFilter) => _inputLoadQueries.GetAll(dtoFilter);

        [HttpPost]
        public Result<DtoInputLoad> Post([FromBody] DtoCreateInputLoad dtoCreate) => _inputLoadCommands.Create(dtoCreate);

        [HttpPut]
        public Result<DtoInputLoad> Put([FromBody] DtoUpdateInputLoad dtoUpdate) => _inputLoadCommands.Update(dtoUpdate);

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id) => _inputLoadCommands.Delete(id);
    }
}