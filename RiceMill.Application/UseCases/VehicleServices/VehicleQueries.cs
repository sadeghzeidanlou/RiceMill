﻿using RiceMill.Application.Common.Interfaces;
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

    public class VehicleQueries : IVehicleQueries
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
            var vehicles = GetFilter(filter);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoVehicle>.Create(vehicles, pageNumber, pageSize);
            return Result<PaginatedList<DtoVehicle>>.Success(result);
        }

        private IQueryable<Vehicle> GetFilter(DtoVehicleFilter filter)
        {
            var vehicles = _cacheService.GetVehicles();
            if (filter == null || (_currentRequestService.IsNotAdmin && filter.RiceMillId.IsNullOrEmpty()))
                return vehicles.Where(v => false);

            if (filter.Id.IsNotNullOrEmpty())
                vehicles = vehicles.Where(v => v.Id.Equals(filter.Id));

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