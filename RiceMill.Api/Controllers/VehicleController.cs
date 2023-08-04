using Microsoft.AspNetCore.Mvc;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.VehicleServices;
using RiceMill.Application.UseCases.VehicleServices.Dto;

namespace RiceMill.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VehicleController : BaseController
    {
        private readonly IVehicleCommands _vehicleCommands;

        private readonly IVehicleQueries _vehicleQueries;

        public VehicleController(IVehicleCommands vehicleCommands, IVehicleQueries vehicleQueries)
        {
            _vehicleCommands = vehicleCommands;
            _vehicleQueries = vehicleQueries;
        }

        [HttpGet]
        public Result<PaginatedList<DtoVehicle>> Get([FromQuery] DtoVehicleFilter dtoFilter) => _vehicleQueries.GetAll(dtoFilter);

        [HttpPost]
        public Result<DtoVehicle> Post([FromBody] DtoCreateVehicle dtoCreate) => _vehicleCommands.Create(dtoCreate);

        [HttpPut]
        public Result<DtoVehicle> Put([FromBody] DtoUpdateVehicle dtoUpdate) => _vehicleCommands.Update(dtoUpdate);

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id) => _vehicleCommands.Delete(id);
    }
}
