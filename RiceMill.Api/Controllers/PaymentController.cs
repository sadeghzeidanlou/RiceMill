using Microsoft.AspNetCore.Mvc;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.PaymentServices;
using RiceMill.Application.UseCases.PaymentServices.Dto;

namespace RiceMill.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public sealed class PaymentController : BaseController
    {
        private readonly IPaymentCommands _paymentCommands;

        private readonly IPaymentQueries _paymentQueries;

        public PaymentController(IPaymentCommands paymentCommands, IPaymentQueries paymentQueries)
        {
            _paymentCommands = paymentCommands;
            _paymentQueries = paymentQueries;
        }

        [HttpGet]
        public Result<PaginatedList<DtoPayment>> Get([FromQuery] DtoPaymentFilter dtoFilter) => _paymentQueries.GetAll(dtoFilter);

        [HttpPost]
        public Result<DtoPayment> Post([FromBody] DtoCreatePayment dtoCreate) => _paymentCommands.Create(dtoCreate);

        [HttpPut]
        public Result<DtoPayment> Put([FromBody] DtoUpdatePayment dtoUpdate) => _paymentCommands.Update(dtoUpdate);

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id) => _paymentCommands.Delete(id);
    }
}
