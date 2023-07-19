using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.IncomeServices.Dto;

namespace RiceMill.Application.UseCases.IncomeServices
{
    public interface IIncomeCommands : IBaseUseCaseCommands
    {
        Result<DtoIncome> Create(DtoCreateIncome income);

        Result<DtoIncome> Update(DtoUpdateIncome income);
    }

    public class IncomeCommands : IIncomeCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public IncomeCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<DtoIncome> Create(DtoCreateIncome income)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Result<DtoIncome> Update(DtoUpdateIncome income)
        {
            throw new NotImplementedException();
        }
    }
}