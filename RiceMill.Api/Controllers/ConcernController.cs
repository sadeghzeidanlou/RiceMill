using Microsoft.AspNetCore.Mvc;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.ConcernServices;
using RiceMill.Application.UseCases.ConcernServices.Dto;

namespace RiceMill.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public sealed class ConcernController : BaseController
    {
        private readonly IConcernCommands _concernCommands;

        private readonly IConcernQueries _concernQueries;

        public ConcernController(IConcernCommands concernCommands, IConcernQueries concernQueries)
        {
            _concernCommands = concernCommands;
            _concernQueries = concernQueries;
        }

        /// <summary>
        /// Get all active concerns based on filter that passed
        /// </summary>
        /// <param name="dtoFilter">filter for retrieve data</param>
        /// <returns></returns>
        [HttpGet]
        public Result<PaginatedList<DtoConcern>> Get([FromQuery] DtoConcernFilter dtoFilter) => _concernQueries.GetAll(dtoFilter);

        [HttpPost]
        public Result<DtoConcern> Post([FromBody] DtoCreateConcern dtoCreate) => _concernCommands.Create(dtoCreate);

        [HttpPut]
        public Result<DtoConcern> Put([FromBody] DtoUpdateConcern dtoUpdate) => _concernCommands.Update(dtoUpdate);

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id) => _concernCommands.Delete(id);
    }
}