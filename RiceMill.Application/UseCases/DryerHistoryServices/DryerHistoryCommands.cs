using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.DryerHistoryServices.Dto;

namespace RiceMill.Application.UseCases.DryerHistoryServices
{
    public interface IDryerHistoryCommands : IBaseUseCaseCommands
    {
        Result<DtoDryerHistory> Create(DtoCreateDryerHistory dryerHistory);

        Result<DtoDryerHistory> Update(DtoUpdateDryerHistory dryerHistory);
    }

    public class DryerHistoryCommands : IDryerHistoryCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DryerHistoryCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<DtoDryerHistory> Create(DtoCreateDryerHistory dryerHistory)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Result<DtoDryerHistory> Update(DtoUpdateDryerHistory dryerHistory)
        {
            throw new NotImplementedException();
        }
    }
}