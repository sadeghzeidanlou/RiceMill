using Microsoft.AspNetCore.Mvc;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DryerServices;
using RiceMill.Application.UseCases.DryerServices.Dto;

namespace RiceMill.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public sealed class DryerController : BaseController
    {
        private readonly IDryerCommands _dryerCommands;

        private readonly IDryerQueries _dryerQueries;

        public DryerController(IDryerCommands dryerCommands, IDryerQueries dryerQueries)
        {
            _dryerCommands = dryerCommands;
            _dryerQueries = dryerQueries;
        }

        [HttpGet]
        public Result<PaginatedList<DtoDryer>> Get([FromQuery] DtoDryerFilter dtoFilter) => _dryerQueries.GetAll(dtoFilter);

        [HttpPost]
        public Result<DtoDryer> Post([FromBody] DtoCreateDryer dtoCreate) => _dryerCommands.Create(dtoCreate);

        [HttpPut]
        public Result<DtoDryer> Put([FromBody] DtoUpdateDryer dtoUpdate) => _dryerCommands.Update(dtoUpdate);

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id) => _dryerCommands.Delete(id);
    }
}
