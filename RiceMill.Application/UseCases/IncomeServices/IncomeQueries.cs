using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.IncomeServices.Dto;

namespace RiceMill.Application.UseCases.IncomeServices
{
    public interface IIncomeQueries
    {
        Result<PaginatedList<DtoIncome>> GetAll();
    }

    public class IncomeQueries : IIncomeQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public IncomeQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<PaginatedList<DtoIncome>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}