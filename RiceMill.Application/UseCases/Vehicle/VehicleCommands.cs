using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.Base;
using RiceMill.Application.UseCases.Vehicle.Dto;

namespace RiceMill.Application.UseCases.Vehicle
{
    public interface IVehicleCommands : IBaseUseCaseCommands
    {
        Task<Result<DtoVehicle>> CreateAsync(DtoCreateVehicle vehicle);

        Task<Result<DtoVehicle>> UpdateAsync(DtoUpdateVehicle vehicle);
    }

    public class VehicleCommands : IVehicleCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public VehicleCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoVehicle>> CreateAsync(DtoCreateVehicle vehicle)
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

        public Task<Result<DtoVehicle>> UpdateAsync(DtoUpdateVehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}