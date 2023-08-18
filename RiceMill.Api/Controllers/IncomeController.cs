using Microsoft.AspNetCore.Mvc;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.IncomeServices;
using RiceMill.Application.UseCases.IncomeServices.Dto;

namespace RiceMill.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public sealed class IncomeController : BaseController
    {
        private readonly IIncomeCommands _incomeCommands;

        private readonly IIncomeQueries _incomeQueries;

        public IncomeController(IIncomeCommands incomeCommands, IIncomeQueries incomeQueries)
        {
            _incomeCommands = incomeCommands;
            _incomeQueries = incomeQueries;
        }

        [HttpGet]
        public Result<PaginatedList<DtoIncome>> Get([FromQuery] DtoIncomeFilter dtoFilter) => _incomeQueries.GetAll(dtoFilter);

        [HttpPost]
        public Result<DtoIncome> Post([FromBody] DtoCreateIncome dtoCreate) => _incomeCommands.Create(dtoCreate);

        [HttpPut]
        public Result<DtoIncome> Put([FromBody] DtoUpdateIncome dtoUpdate) => _incomeCommands.Update(dtoUpdate);

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id) => _incomeCommands.Delete(id);
    }
}