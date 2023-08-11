using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Domain.Models;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.InputLoadServices
{
    public interface IInputLoadQueries
    {
        Result<PaginatedList<DtoInputLoad>> GetAll(DtoInputLoadFilter filter);
    }

    public class InputLoadQueries : IInputLoadQueries
    {
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;

        public InputLoadQueries(ICurrentRequestService currentRequestService, ICacheService cacheService)
        {
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
        }

        public Result<PaginatedList<DtoInputLoad>> GetAll(DtoInputLoadFilter filter)
        {
            var inputLoads = GetFilter(filter);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoInputLoad>.Create(inputLoads, pageNumber, pageSize);
            return Result<PaginatedList<DtoInputLoad>>.Success(result);
        }

        private IQueryable<InputLoad> GetFilter(DtoInputLoadFilter filter)
        {
            var inputLoads = _cacheService.GetInputLoads();
            if (filter == null || (_currentRequestService.IsNotAdmin && filter.RiceMillId.IsNullOrEmpty()))
                return inputLoads.Where(il => false);

            if (filter.Id.IsNotNullOrEmpty())
                inputLoads = inputLoads.Where(il => il.Id.Equals(filter.Id));

            if (filter.RiceMillId.IsNotNullOrEmpty())
                inputLoads = inputLoads.Where(il => il.RiceMillId.Equals(filter.RiceMillId));

            if (filter.NumberOfBagsLower.HasValue)
                inputLoads = inputLoads.Where(il => il.NumberOfBags < filter.NumberOfBagsLower.Value);

            if (filter.NumberOfBags.HasValue)
                inputLoads = inputLoads.Where(il => il.NumberOfBags.Equals(filter.NumberOfBags.Value));

            if (filter.NumberOfBagsGreater.HasValue)
                inputLoads = inputLoads.Where(il => il.NumberOfBags > filter.NumberOfBagsGreater.Value);

            if (filter.NumberOfBagsInDryerLower.HasValue)
                inputLoads = inputLoads.Where(il => il.NumberOfBagsInDryer < filter.NumberOfBagsInDryerLower.Value);

            if (filter.NumberOfBagsInDryer.HasValue)
                inputLoads = inputLoads.Where(il => il.NumberOfBagsInDryer.Equals(filter.NumberOfBagsInDryer.Value));

            if (filter.NumberOfBagsInDryerGreater.HasValue)
                inputLoads = inputLoads.Where(il => il.NumberOfBagsInDryer > filter.NumberOfBagsInDryerGreater.Value);

            if (filter.IsCompletelyInDryer.HasValue)
            {
                if (filter.IsCompletelyInDryer.Value)
                {
                    inputLoads = inputLoads.Where(il => il.NumberOfBagsInDryer.Equals(filter.NumberOfBags.Value));
                }
                else
                {
                    inputLoads = inputLoads.Where(il => il.NumberOfBagsInDryer < filter.NumberOfBags.Value);
                }
            }
            if (filter.Description.IsNotNullOrEmpty())
                inputLoads = inputLoads.Where(il => il.Description.Contains(filter.Description));

            if (filter.ReceiveTimeLower.HasValue)
                inputLoads = inputLoads.Where(il => il.ReceiveTime < filter.ReceiveTimeLower.Value);

            if (filter.ReceiveTime.HasValue)
                inputLoads = inputLoads.Where(il => il.ReceiveTime.Equals(filter.ReceiveTime.Value));

            if (filter.ReceiveTimeGreater.HasValue)
                inputLoads = inputLoads.Where(il => il.ReceiveTime > filter.ReceiveTimeGreater.Value);

            if (filter.VillageId.IsNotNullOrEmpty())
                inputLoads = inputLoads.Where(il => il.VillageId.Equals(filter.VillageId));

            if (filter.DelivererPersonId.IsNotNullOrEmpty())
                inputLoads = inputLoads.Where(il => il.DelivererPersonId.Equals(filter.DelivererPersonId));

            if (filter.ReceiverPersonId.IsNotNullOrEmpty())
                inputLoads = inputLoads.Where(il => il.ReceiverPersonId.Equals(filter.ReceiverPersonId));

            if (filter.CarrierPersonId.IsNotNullOrEmpty())
                inputLoads = inputLoads.Where(il => il.CarrierPersonId.Equals(filter.CarrierPersonId));

            if (filter.OwnerPersonId.IsNotNullOrEmpty())
                inputLoads = inputLoads.Where(il => il.OwnerPersonId.Equals(filter.OwnerPersonId));

            if (filter.VehicleId.IsNotNullOrEmpty())
                inputLoads = inputLoads.Where(il => il.VehicleId.Equals(filter.VehicleId));

            if (filter.RiceMillId.IsNotNullOrEmpty())
                inputLoads = inputLoads.Where(il => il.RiceMillId.Equals(filter.RiceMillId));

            return inputLoads;
        }
    }
}