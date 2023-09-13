using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.DeliveryServices.Dto;
using RiceMill.Application.UseCases.UserActivityServices;
using RiceMill.Domain.Models;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Net;

namespace RiceMill.Application.UseCases.DeliveryServices
{
    public interface IDeliveryCommands : IBaseUseCaseCommands
    {
        Result<DtoDelivery> Create(DtoCreateDelivery delivery);

        Result<DtoDelivery> Update(DtoUpdateDelivery delivery);
    }

    public sealed class DeliveryCommands : IDeliveryCommands
    {

        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;
        private readonly IUserActivityCommands _userActivityCommands;
        private readonly EntityTypeEnum _deliveryKey = EntityTypeEnum.Deliveries;
        private readonly EntityTypeEnum _deliveryRiceThreshingKey = EntityTypeEnum.DeliveryRiceThreshings;

        public DeliveryCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService, ICacheService cacheService, IUserActivityCommands userActivityCommands)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
            _userActivityCommands = userActivityCommands;
        }

        public Result<DtoDelivery> Create(DtoCreateDelivery createDelivery)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoDelivery>.Forbidden();

            var validationResult = createDelivery.Validate();
            if (!validationResult.IsValid)
                return Result<DtoDelivery>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var validateDelivery = ValidateDelivery(createDelivery);
            if (validateDelivery != null)
                return validateDelivery;

            var delivery = createDelivery.Adapt<Delivery>();
            delivery.UserId = _currentRequestService.UserId;
            _applicationDbContext.Deliveries.Add(delivery);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _deliveryKey, string.Empty, delivery.SerializeObject(), delivery.RiceMillId);
            _cacheService.Maintain(_deliveryKey, delivery);
            foreach (var riceThreshingId in createDelivery.RiceThreshingIds)
            {
                var deliveryRiceThreshing = new DeliveryRiceThreshing { DeliveryId = delivery.Id, RiceThreshingId = riceThreshingId };
                _applicationDbContext.DeliveryRiceThreshing.Add(deliveryRiceThreshing);
                _applicationDbContext.SaveChanges();
                _userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _deliveryRiceThreshingKey, string.Empty, deliveryRiceThreshing.SerializeObject(), createDelivery.RiceMillId);
                _cacheService.Maintain(_deliveryRiceThreshingKey, deliveryRiceThreshing);
            }
            return Result<DtoDelivery>.Success(delivery.Adapt<DtoDelivery>());
        }

        public Result<DtoDelivery> Update(DtoUpdateDelivery updateDelivery)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoDelivery>.Forbidden();

            var validationResult = updateDelivery.Validate();
            if (!validationResult.IsValid)
                return Result<DtoDelivery>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var delivery = GetDeliveryById(updateDelivery.Id);
            if (delivery == null)
                return Result<DtoDelivery>.Failure(Error.CreateError(ResultStatusEnum.DeliveryNotFound), HttpStatusCode.NotFound);

            var createDelivery = updateDelivery.Adapt<DtoCreateDelivery>();
            createDelivery = createDelivery with { RiceMillId = delivery.RiceMillId };
            var validateDelivery = ValidateDelivery(createDelivery);
            if (validateDelivery != null)
                return validateDelivery;

            var beforeEdit = delivery.SerializeObject();
            delivery = updateDelivery.Adapt(delivery);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _deliveryKey, beforeEdit, delivery.SerializeObject(), delivery.RiceMillId);
            _cacheService.Maintain(_deliveryKey, delivery);
            return Result<DtoDelivery>.Success(delivery.Adapt<DtoDelivery>());
        }

        public Result<bool> Delete(Guid id)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<bool>.Forbidden();

            var delivery = GetDeliveryById(id);
            if (delivery == null)
                return Result<bool>.Failure(Error.CreateError(ResultStatusEnum.DeliveryNotFound), HttpStatusCode.NotFound);

            var beforeEdit = delivery.SerializeObject();
            _applicationDbContext.Deliveries.Remove(delivery);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Delete, _deliveryKey, beforeEdit, delivery.SerializeObject(), delivery.RiceMillId);
            _cacheService.Maintain(_deliveryKey, delivery);
            return Result<bool>.Success(true);
        }

        private Delivery GetDeliveryById(Guid id) => _applicationDbContext.Deliveries.FirstOrDefault(c => c.Id.Equals(id));

        private Result<DtoDelivery> ValidateDelivery(DtoCreateDelivery delivery)
        {
            var people = _cacheService.GetPeople().ToList();
            if (!people.Any(c => c.Id.Equals(delivery.DelivererPersonId)))
                return Result<DtoDelivery>.Failure(Error.CreateError(ResultStatusEnum.DeliveryDelivererPersonNotFound), HttpStatusCode.NotFound);

            if (!people.Any(c => c.Id.Equals(delivery.ReceiverPersonId)))
                return Result<DtoDelivery>.Failure(Error.CreateError(ResultStatusEnum.DeliveryReceiverPersonNotFound), HttpStatusCode.NotFound);

            if (!people.Any(c => c.Id.Equals(delivery.CarrierPersonId)))
                return Result<DtoDelivery>.Failure(Error.CreateError(ResultStatusEnum.DeliveryCarrierPersonNotFound), HttpStatusCode.NotFound);

            if (!_cacheService.GetVehicles().Any(c => c.Id.Equals(delivery.VehicleId)))
                return Result<DtoDelivery>.Failure(Error.CreateError(ResultStatusEnum.VehicleNotFound), HttpStatusCode.NotFound);

            if (_cacheService.GetRiceThreshings().Select(rt => rt.Id).Intersect(delivery.RiceThreshingIds).Count() != delivery.RiceThreshingIds.Count)
                return Result<DtoDelivery>.Failure(Error.CreateError(ResultStatusEnum.RiceThreshingNotFound), HttpStatusCode.NotFound);

            if (!_cacheService.GetRiceMills().Any(rm => rm.Id.Equals(delivery.RiceMillId)))
                return Result<DtoDelivery>.Failure(Error.CreateError(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound);

            return null;
        }
    }
}