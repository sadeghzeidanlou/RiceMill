using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.VehicleServices.Dto;
using RiceMill.Domain.Models;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.VehicleServices
{
    public interface IVehicleQueries
    {
        Result<PaginatedList<DtoVehicle>> GetAll(DtoVehicleFilter filter);
    }

    public sealed class VehicleQueries : IVehicleQueries
    {
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;

        public VehicleQueries(ICurrentRequestService currentRequestService, ICacheService cacheService)
        {
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
        }

        public Result<PaginatedList<DtoVehicle>> GetAll(DtoVehicleFilter filter)
        {
            var vehicles = GetFilter(filter).OrderByDescending(x => x.UpdateTime);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoVehicle>.Create(vehicles, pageNumber, pageSize);
            return Result<PaginatedList<DtoVehicle>>.Success(result);
        }

        private IQueryable<Vehicle> GetFilter(DtoVehicleFilter filter)
        {
            var vehicles = _cacheService.GetVehicles();
            if (filter == null)
                return vehicles.Where(v => false);

            if (_currentRequestService.IsNotAdmin)
            {
                if (_currentRequestService.RiceMillId.IsNullOrEmpty())
                    return vehicles.Where(rm => false);

                vehicles = vehicles.Where(rm => rm.RiceMillId.Equals(_currentRequestService.RiceMillId.Value));
            }
            if (filter.Id.IsNotNullOrEmpty())
                vehicles = vehicles.Where(v => v.Id.Equals(filter.Id.Value));

            if (filter.Ids.IsCollectionNotNullOrEmpty())
                vehicles = vehicles.Where(v => filter.Ids.Contains(v.Id));

            if (filter.RiceMillId.IsNotNullOrEmpty())
                vehicles = vehicles.Where(v => v.RiceMillId.Equals(filter.RiceMillId));

            if (filter.Plate.IsNotNullOrEmpty())
                vehicles = vehicles.Where(v => v.Plate.Contains(filter.Plate));

            if (filter.Description.IsNotNullOrEmpty())
                vehicles = vehicles.Where(v => v.Description.Contains(filter.Description));

            if (filter.VehicleType.HasValue)
                vehicles = vehicles.Where(v => v.VehicleType.Equals(filter.VehicleType.Value));

            if (filter.OwnerPersonId.IsNotNullOrEmpty())
                vehicles = vehicles.Where(v => v.OwnerPersonId.Equals(filter.OwnerPersonId));

            return vehicles;
        }
    }
}