using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.Base;
using RiceMill.Application.UseCases.Vehicle.Dto;

namespace RiceMill.Application.UseCases.Vehicle
{
    public interface IVehicleQueries : IBaseUseCaseQueries
    {
        Task<Result<DtoVehicle>> GetAsync(int id);

        Task<Result<List<DtoVehicle>>> GetAllAsync();
    }

    public class VehicleQueries : IVehicleQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public VehicleQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<List<DtoVehicle>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoVehicle>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}