using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DeliveryServices.Dto;
using RiceMill.Domain.Models;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.DeliveryServices
{
    public interface IDeliveryQueries
    {
        Result<PaginatedList<DtoDelivery>> GetAll(DtoDeliveryFilter filter);
    }

    public sealed class DeliveryQueries : IDeliveryQueries
    {
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;

        public DeliveryQueries(ICurrentRequestService currentRequestService, ICacheService cacheService)
        {
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
        }

        public Result<PaginatedList<DtoDelivery>> GetAll(DtoDeliveryFilter filter)
        {
            var dryers = GetFilter(filter);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoDelivery>.Create(dryers, pageNumber, pageSize);
            return Result<PaginatedList<DtoDelivery>>.Success(result);
        }

        private IQueryable<Delivery> GetFilter(DtoDeliveryFilter filter)
        {
            var deliveries = _cacheService.GetDeliveries();
            if (filter == null || (_currentRequestService.IsNotAdmin && filter.RiceMillId.IsNullOrEmpty()))
                return deliveries.Where(dh => false);

            if (filter.Id.IsNotNullOrEmpty())
                deliveries = deliveries.Where(dh => dh.Id.Equals(filter.Id.Value));

            if (filter.Ids.IsCollectionNotNullOrEmpty())
                deliveries = deliveries.Where(dh => filter.Ids.Contains(dh.Id));

            if (filter.RiceMillId.IsNotNullOrEmpty())
                deliveries = deliveries.Where(dh => dh.RiceMillId.Equals(filter.RiceMillId));

            if (filter.DeliveryTimeLower.HasValue)
                deliveries = deliveries.Where(rt => rt.DeliveryTime < filter.DeliveryTimeLower.Value);

            if (filter.DeliveryTime.HasValue)
                deliveries = deliveries.Where(rt => rt.DeliveryTime.Equals(filter.DeliveryTime.Value));

            if (filter.DeliveryTimeGreater.HasValue)
                deliveries = deliveries.Where(rt => rt.DeliveryTime > filter.DeliveryTimeGreater.Value);

            if (filter.UnbrokenRiceLower.HasValue)
                deliveries = deliveries.Where(p => p.UnbrokenRice < filter.UnbrokenRiceLower.Value);

            if (filter.UnbrokenRice.HasValue)
                deliveries = deliveries.Where(p => p.UnbrokenRice == filter.UnbrokenRice.Value);

            if (filter.UnbrokenRiceGreater.HasValue)
                deliveries = deliveries.Where(p => p.UnbrokenRice > filter.UnbrokenRiceGreater.Value);

            if (filter.BrokenRiceLower.HasValue)
                deliveries = deliveries.Where(p => p.BrokenRice < filter.BrokenRiceLower.Value);

            if (filter.BrokenRice.HasValue)
                deliveries = deliveries.Where(p => p.BrokenRice == filter.BrokenRice.Value);

            if (filter.BrokenRiceGreater.HasValue)
                deliveries = deliveries.Where(p => p.BrokenRice > filter.BrokenRiceGreater.Value);

            if (filter.ChickenRiceLower.HasValue)
                deliveries = deliveries.Where(p => p.ChickenRice < filter.ChickenRiceLower.Value);

            if (filter.ChickenRice.HasValue)
                deliveries = deliveries.Where(p => p.ChickenRice == filter.ChickenRice.Value);

            if (filter.ChickenRiceGreater.HasValue)
                deliveries = deliveries.Where(p => p.ChickenRice > filter.ChickenRiceGreater.Value);

            if (filter.FlourLower.HasValue)
                deliveries = deliveries.Where(p => p.Flour < filter.FlourLower.Value);

            if (filter.Flour.HasValue)
                deliveries = deliveries.Where(p => p.Flour == filter.Flour.Value);

            if (filter.FlourGreater.HasValue)
                deliveries = deliveries.Where(p => p.Flour > filter.FlourGreater.Value);

            if (filter.Description.IsNotNullOrEmpty())
                deliveries = deliveries.Where(p => p.Description.Contains(filter.Description));

            if (filter.DelivererPersonId.IsNotNullOrEmpty())
                deliveries = deliveries.Where(dh => dh.DelivererPersonId.Equals(filter.DelivererPersonId));

            if (filter.ReceiverPersonId.IsNotNullOrEmpty())
                deliveries = deliveries.Where(dh => dh.ReceiverPersonId.Equals(filter.ReceiverPersonId));

            if (filter.CarrierPersonId.IsNotNullOrEmpty())
                deliveries = deliveries.Where(dh => dh.CarrierPersonId.Equals(filter.CarrierPersonId));

            if (filter.VehicleId.IsNotNullOrEmpty())
                deliveries = deliveries.Where(dh => dh.VehicleId.Equals(filter.VehicleId));

            return deliveries;
        }
    }
}