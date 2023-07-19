using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.VehicleServices.Dto;

namespace RiceMill.Application.UseCases.VehicleServices
{
    public interface IVehicleQueries
    {
        Result<PaginatedList<DtoVehicle>> GetAll();
    }

    public class VehicleQueries : IVehicleQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public VehicleQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<PaginatedList<DtoVehicle>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}