using Microsoft.AspNetCore.Mvc;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.ConcernServices;
using RiceMill.Application.UseCases.ConcernServices.Dto;

namespace RiceMill.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ConcernController : BaseController
    {
        private readonly IConcernCommands _concernCommands;

        private readonly IConcernQueries _concernQueries;

        public ConcernController(IConcernCommands concernCommands, IConcernQueries concernQueries)
        {
            _concernCommands = concernCommands;
            _concernQueries = concernQueries;
        }

        [HttpGet]
        public Result<PaginatedList<DtoConcern>> Get([FromQuery] DtoConcernFilter filter)
        {
            var res = _concernQueries.GetAllAsync(filter).Result;
            return res;
        }

        [HttpPost]
        public Result<DtoConcern> Post([FromBody] DtoCreateConcern dtoCreateConcern)
        {
            var result = _concernCommands.CreateAsync(dtoCreateConcern).Result;
            return result;
        }

        [HttpPut]
        public Result<DtoConcern> Put([FromBody] DtoUpdateConcern dtoUpdateConcern)
        {
            var result = _concernCommands.UpdateAsync(dtoUpdateConcern).Result;
            return result;
        }

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id)
        {
            var result = _concernCommands.DeleteAsync(id).Result;
            return result;
        }
    }
}