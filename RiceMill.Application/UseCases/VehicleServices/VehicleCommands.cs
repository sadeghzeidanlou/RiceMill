using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.VehicleServices.Dto;

namespace RiceMill.Application.UseCases.VehicleServices
{
    public interface IVehicleCommands : IBaseUseCaseCommands
    {
        Result<DtoVehicle> Create(DtoCreateVehicle vehicle);

        Result<DtoVehicle> Update(DtoUpdateVehicle vehicle);
    }

    public class VehicleCommands : IVehicleCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public VehicleCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<DtoVehicle> Create(DtoCreateVehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Result<DtoVehicle> Update(DtoUpdateVehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}