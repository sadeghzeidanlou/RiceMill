using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.IncomeServices.Dto;

namespace RiceMill.Application.UseCases.IncomeServices
{
    public interface IIncomeCommands : IBaseUseCaseCommands
    {
        Task<Result<DtoIncome>> CreateAsync(DtoCreateIncome income);

        Task<Result<DtoIncome>> UpdateAsync(DtoUpdateIncome income);
    }

    public class IncomeCommands : IIncomeCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public IncomeCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoIncome>> CreateAsync(DtoCreateIncome income)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoIncome>> UpdateAsync(DtoUpdateIncome income)
        {
            throw new NotImplementedException();
        }
    }
}