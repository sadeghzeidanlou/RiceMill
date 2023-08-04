using Microsoft.AspNetCore.Mvc;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.VillageServices;
using RiceMill.Application.UseCases.VillageServices.Dto;

namespace RiceMill.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VillageController : BaseController
    {
        private readonly IVillageCommands _villageCommands;

        private readonly IVillageQueries _villageQueries;

        public VillageController(IVillageCommands villageCommands, IVillageQueries villageQueries)
        {
            _villageCommands = villageCommands;
            _villageQueries = villageQueries;
        }

        [HttpGet]
        public Result<PaginatedList<DtoVillage>> Get([FromQuery] DtoVillageFilter dtoFilter) => _villageQueries.GetAll(dtoFilter);

        [HttpPost]
        public Result<DtoVillage> Post([FromBody] DtoCreateVillage dtoCreate) => _villageCommands.Create(dtoCreate);

        [HttpPut]
        public Result<DtoVillage> Put([FromBody] DtoUpdateVillage dtoUpdate) => _villageCommands.Update(dtoUpdate);

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id) => _villageCommands.Delete(id);
    }
}
