using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.IncomeServices.Dto;

namespace RiceMill.Application.UseCases.IncomeServices
{
    public interface IIncomeQueries
    {
        Task<Result<int>> GetCountAsync();

        Task<Result<List<DtoIncome>>> GetAllAsync();
    }

    public class IncomeQueries : IIncomeQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public IncomeQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<List<DtoIncome>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}