using Microsoft.AspNetCore.Mvc;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DryerHistoryServices;
using RiceMill.Application.UseCases.DryerHistoryServices.Dto;

namespace RiceMill.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public sealed class DryerHistoryController : BaseController
    {
        private readonly IDryerHistoryCommands _dryerHistoryCommands;

        private readonly IDryerHistoryQueries _dryerHistoryQueries;

        public DryerHistoryController(IDryerHistoryCommands dryerHistoryCommands, IDryerHistoryQueries dryerHistoryQueries)
        {
            _dryerHistoryCommands = dryerHistoryCommands;
            _dryerHistoryQueries = dryerHistoryQueries;
        }

        [HttpGet]
        public Result<PaginatedList<DtoDryerHistory>> Get([FromQuery] DtoDryerHistoryFilter dtoFilter) => _dryerHistoryQueries.GetAll(dtoFilter);

        [HttpPost]
        public Result<DtoDryerHistory> Post([FromBody] DtoCreateDryerHistory dtoCreate) => _dryerHistoryCommands.Create(dtoCreate);

        [HttpPut]
        public Result<DtoDryerHistory> Put([FromBody] DtoUpdateDryerHistory dtoUpdate) => _dryerHistoryCommands.Update(dtoUpdate);

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id) => _dryerHistoryCommands.Delete(id);
    }
}