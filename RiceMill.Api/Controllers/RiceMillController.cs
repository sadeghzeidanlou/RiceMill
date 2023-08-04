using Microsoft.AspNetCore.Mvc;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.RiceMillServices;
using RiceMill.Application.UseCases.RiceMillServices.Dto;

namespace RiceMill.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RiceMillController : BaseController
    {
        private readonly IRiceMillCommands _riceMillCommands;

        private readonly IRiceMillQueries _riceMillQueries;

        public RiceMillController(IRiceMillCommands riceMillCommands, IRiceMillQueries riceMillQueries)
        {
            _riceMillCommands = riceMillCommands;
            _riceMillQueries = riceMillQueries;
        }

        [HttpGet]
        public Result<PaginatedList<DtoRiceMill>> Get([FromQuery] DtoRiceMillFilter dtoFilter) => _riceMillQueries.GetAll(dtoFilter);

        [HttpPost]
        public Result<DtoRiceMill> Post([FromBody] DtoCreateRiceMill dtoCreate) => _riceMillCommands.Create(dtoCreate);

        [HttpPut]
        public Result<DtoRiceMill> Put([FromBody] DtoUpdateRiceMill dtoUpdate) => _riceMillCommands.Update(dtoUpdate);

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id) => _riceMillCommands.Delete(id);
    }
}