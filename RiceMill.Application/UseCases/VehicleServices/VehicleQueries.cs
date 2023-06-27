using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.VehicleServices.Dto;

namespace RiceMill.Application.UseCases.VehicleServices
{
    public interface IVehicleQueries
    {
        Task<Result<int>> GetCountAsync();

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

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}