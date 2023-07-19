using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.DryerServices.Dto;

namespace RiceMill.Application.UseCases.DryerServices
{
    public interface IDryerCommands : IBaseUseCaseCommands
    {
        Result<DtoDryer> Create(DtoCreateDryer dryer);

        Result<DtoDryer> Update(DtoUpdateDryer dryer);
    }

    public class DryerCommands : IDryerCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DryerCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<DtoDryer> Create(DtoCreateDryer dryer)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Result<DtoDryer> Update(DtoUpdateDryer dryer)
        {
            throw new NotImplementedException();
        }
    }
}