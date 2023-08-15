using Microsoft.AspNetCore.Mvc;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.RiceThreshingServices;
using RiceMill.Application.UseCases.RiceThreshingServices.Dto;

namespace RiceMill.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RiceThreshingController : BaseController
    {
        private readonly IRiceThreshingCommands _riceThreshingCommands;

        private readonly IRiceThreshingQueries _riceThreshingQueries;

        public RiceThreshingController(IRiceThreshingCommands riceThreshingCommands, IRiceThreshingQueries riceThreshingQueries)
        {
            _riceThreshingCommands = riceThreshingCommands;
            _riceThreshingQueries = riceThreshingQueries;
        }

        [HttpGet]
        public Result<PaginatedList<DtoRiceThreshing>> Get([FromQuery] DtoRiceThreshingFilter dtoFilter) => _riceThreshingQueries.GetAll(dtoFilter);

        [HttpPost]
        public Result<DtoRiceThreshing> Post([FromBody] DtoCreateRiceThreshing dtoCreate) => _riceThreshingCommands.Create(dtoCreate);

        [HttpPut]
        public Result<DtoRiceThreshing> Put([FromBody] DtoUpdateRiceThreshing dtoUpdate) => _riceThreshingCommands.Update(dtoUpdate);

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id) => _riceThreshingCommands.Delete(id);
    }
}