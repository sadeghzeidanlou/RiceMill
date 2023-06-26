using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.DryerServices.Dto;

namespace RiceMill.Application.UseCases.DryerServices
{
    public interface IDryerCommands : IBaseUseCaseCommands
    {
        Task<Result<DtoDryer>> CreateAsync(DtoCreateDryer dryer);

        Task<Result<DtoDryer>> UpdateAsync(DtoUpdateDryer dryer);
    }

    public class DryerCommands : IDryerCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DryerCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoDryer>> CreateAsync(DtoCreateDryer dryer)
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

        public Task<Result<DtoDryer>> UpdateAsync(DtoUpdateDryer dryer)
        {
            throw new NotImplementedException();
        }
    }
}