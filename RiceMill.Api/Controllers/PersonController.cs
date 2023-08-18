using Microsoft.AspNetCore.Mvc;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.PersonServices;
using RiceMill.Application.UseCases.PersonServices.Dto;

namespace RiceMill.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public sealed class PersonController : BaseController
    {
        private readonly IPersonCommands _personCommands;

        private readonly IPersonQueries _personQueries;

        public PersonController(IPersonCommands personCommands, IPersonQueries personQueries)
        {
            _personCommands = personCommands;
            _personQueries = personQueries;
        }

        [HttpGet]
        public Result<PaginatedList<DtoPerson>> Get([FromQuery] DtoPersonFilter dtoFilter) => _personQueries.GetAll(dtoFilter);

        [HttpPost]
        public Result<DtoPerson> Post([FromBody] DtoCreatePerson dtoCreate) => _personCommands.Create(dtoCreate);

        [HttpPut]
        public Result<DtoPerson> Put([FromBody] DtoUpdatePerson dtoUpdate) => _personCommands.Update(dtoUpdate);

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id) => _personCommands.Delete(id);
    }
}