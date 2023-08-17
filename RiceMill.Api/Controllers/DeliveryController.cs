using Microsoft.AspNetCore.Mvc;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DeliveryServices;
using RiceMill.Application.UseCases.DeliveryServices.Dto;

namespace RiceMill.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DeliveryController : BaseController
    {
        private readonly IDeliveryCommands _deliveryCommands;

        private readonly IDeliveryQueries _deliveryQueries;

        public DeliveryController(IDeliveryCommands deliveryCommands, IDeliveryQueries deliveryQueries)
        {
            _deliveryCommands = deliveryCommands;
            _deliveryQueries = deliveryQueries;
        }

        [HttpGet]
        public Result<PaginatedList<DtoDelivery>> Get([FromQuery] DtoDeliveryFilter dtoFilter) => _deliveryQueries.GetAll(dtoFilter);

        [HttpPost]
        public Result<DtoDelivery> Post([FromBody] DtoCreateDelivery dtoCreate) => _deliveryCommands.Create(dtoCreate);

        [HttpPut]
        public Result<DtoDelivery> Put([FromBody] DtoUpdateDelivery dtoUpdate) => _deliveryCommands.Update(dtoUpdate);

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id) => _deliveryCommands.Delete(id);
    }
}