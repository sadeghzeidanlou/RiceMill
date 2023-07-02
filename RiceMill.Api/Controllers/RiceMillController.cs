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
        public Result<PaginatedList<DtoRiceMill>> Get(DtoRiceMillFilter dtoRiceMillFilter) => _riceMillQueries.GetAllAsync(dtoRiceMillFilter).Result;

        [HttpPost]
        public Result<DtoRiceMill> Post([FromBody] DtoCreateRiceMill dtoCreateRiceMill) => _riceMillCommands.CreateAsync(dtoCreateRiceMill).Result;

        public Result<DtoRiceMill> Put([FromBody] DtoUpdateRiceMill dtoUpdateRiceMill) => _riceMillCommands.UpdateAsync(dtoUpdateRiceMill).Result;

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id) => _riceMillCommands.DeleteAsync(id).Result;
    }
}