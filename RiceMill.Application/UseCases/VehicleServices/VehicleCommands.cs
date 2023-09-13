using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.UserActivityServices;
using RiceMill.Application.UseCases.VehicleServices.Dto;
using RiceMill.Domain.Models;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Net;

namespace RiceMill.Application.UseCases.VehicleServices
{
    public interface IVehicleCommands : IBaseUseCaseCommands
    {
        Result<DtoVehicle> Create(DtoCreateVehicle vehicle);

        Result<DtoVehicle> Update(DtoUpdateVehicle vehicle);
    }

    public sealed class VehicleCommands : IVehicleCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;
        private readonly IUserActivityCommands _userActivityCommands;
        private readonly EntityTypeEnum _Key = EntityTypeEnum.Vehicles;

        public VehicleCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService, ICacheService cacheService, IUserActivityCommands userActivityCommands)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
            _userActivityCommands = userActivityCommands;
        }

        public Result<DtoVehicle> Create(DtoCreateVehicle createVehicle)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoVehicle>.Forbidden();

            var validationResult = createVehicle.Validate();
            if (!validationResult.IsValid)
                return Result<DtoVehicle>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var validateVehicle = ValidateVehicle(createVehicle);
            if (validateVehicle != null)
                return validateVehicle;

            var vehicle = createVehicle.Adapt<Vehicle>();
            vehicle.UserId = _currentRequestService.UserId;
            _applicationDbContext.Vehicles.Add(vehicle);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _Key, string.Empty, vehicle.SerializeObject(), vehicle.RiceMillId);
            _cacheService.Maintain(_Key, vehicle);
            return Result<DtoVehicle>.Success(vehicle.Adapt<DtoVehicle>());
        }

        public Result<DtoVehicle> Update(DtoUpdateVehicle updateVehicle)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoVehicle>.Forbidden();

            var validationResult = updateVehicle.Validate();
            if (!validationResult.IsValid)
                return Result<DtoVehicle>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var vehicle = GetVehicleById(updateVehicle.Id);
            if (vehicle == null)
                return Result<DtoVehicle>.Failure(Error.CreateError(ResultStatusEnum.VehicleNotFound), HttpStatusCode.NotFound);

            var createVehicle = updateVehicle.Adapt<DtoCreateVehicle>();
            createVehicle = createVehicle with { RiceMillId = vehicle.RiceMillId };
            var validateVehicle = ValidateVehicle(createVehicle);
            if (validateVehicle != null)
                return validateVehicle;

            var beforeEdit = vehicle.SerializeObject();
            vehicle = updateVehicle.Adapt(vehicle);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _Key, beforeEdit, vehicle.SerializeObject(), vehicle.RiceMillId);
            _cacheService.Maintain(_Key, vehicle);
            return Result<DtoVehicle>.Success(vehicle.Adapt<DtoVehicle>());
        }

        public Result<bool> Delete(Guid id)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<bool>.Forbidden();

            var vehicle = GetVehicleById(id);
            if (vehicle == null)
                return Result<bool>.Failure(Error.CreateError(ResultStatusEnum.VehicleNotFound), HttpStatusCode.NotFound);

            var beforeEdit = vehicle.SerializeObject();
            _applicationDbContext.Vehicles.Remove(vehicle);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Delete, _Key, beforeEdit, vehicle.SerializeObject(), vehicle.RiceMillId);
            _cacheService.Maintain(_Key, vehicle);
            return Result<bool>.Success(true);
        }

        private Vehicle GetVehicleById(Guid id) => _applicationDbContext.Vehicles.FirstOrDefault(c => c.Id.Equals(id));

        private Result<DtoVehicle> ValidateVehicle(DtoCreateVehicle vehicle)
        {
            if (!_cacheService.GetRiceMills().Any(x => x.Id.Equals(vehicle.RiceMillId)))
                return Result<DtoVehicle>.Failure(Error.CreateError(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound);

            if (!_cacheService.GetPeople().Any(x => x.Id.Equals(vehicle.OwnerPersonId)))
                return Result<DtoVehicle>.Failure(Error.CreateError(ResultStatusEnum.VehicleOwnerPersonIdIsNotValid), HttpStatusCode.NotFound);

            return null;
        }
    }
}