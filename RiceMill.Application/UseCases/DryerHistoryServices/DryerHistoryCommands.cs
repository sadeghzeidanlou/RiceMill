using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.DryerHistoryServices.Dto;

namespace RiceMill.Application.UseCases.DryerHistoryServices
{
    public interface IDryerHistoryCommands : IBaseUseCaseCommands
    {
        Task<Result<DtoDryerHistory>> CreateAsync(DtoCreateDryerHistory dryerHistory);

        Task<Result<DtoDryerHistory>> UpdateAsync(DtoUpdateDryerHistory dryerHistory);
    }

    public class DryerHistoryCommands : IDryerHistoryCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DryerHistoryCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoDryerHistory>> CreateAsync(DtoCreateDryerHistory dryerHistory)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteAsync(Guid id, Guid riceMillId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoDryerHistory>> UpdateAsync(DtoUpdateDryerHistory dryerHistory)
        {
            throw new NotImplementedException();
        }
    }
}