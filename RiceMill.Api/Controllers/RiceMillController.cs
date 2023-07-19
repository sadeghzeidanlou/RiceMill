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
        public Result<PaginatedList<DtoRiceMill>> Get([FromQuery] DtoRiceMillFilter dtoRiceMillFilter) => _riceMillQueries.GetAll(dtoRiceMillFilter);

        [HttpPost]
        public Result<DtoRiceMill> Post([FromBody] DtoCreateRiceMill dtoCreateRiceMill) => _riceMillCommands.Create(dtoCreateRiceMill);

        [HttpPut]
        public Result<DtoRiceMill> Put([FromBody] DtoUpdateRiceMill dtoUpdateRiceMill) => _riceMillCommands.Update(dtoUpdateRiceMill);

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id) => _riceMillCommands.Delete(id);
    }
}