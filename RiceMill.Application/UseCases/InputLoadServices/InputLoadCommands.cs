using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Application.UseCases.UserActivityServices;
using RiceMill.Domain.Models;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Net;

namespace RiceMill.Application.UseCases.InputLoadServices
{
    public interface IInputLoadCommands : IBaseUseCaseCommands
    {
        Result<DtoInputLoad> Create(DtoCreateInputLoad inputLoad);

        Result<DtoInputLoad> Update(DtoUpdateInputLoad inputLoad);
    }

    public sealed class InputLoadCommands : IInputLoadCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;
        private readonly IUserActivityCommands _userActivityCommands;
        private readonly EntityTypeEnum _Key = EntityTypeEnum.InputLoads;

        public InputLoadCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService, ICacheService cacheService, IUserActivityCommands userActivityCommands)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
            _userActivityCommands = userActivityCommands;
        }

        public Result<DtoInputLoad> Create(DtoCreateInputLoad createInputLoad)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoInputLoad>.Forbidden();

            var validationResult = createInputLoad.Validate();
            if (!validationResult.IsValid)
                return Result<DtoInputLoad>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var validateCreateInputLoadResult = ValidateInputLoad(createInputLoad);
            if (validateCreateInputLoadResult != null)
                return validateCreateInputLoadResult;

            var inputLoad = createInputLoad.Adapt<InputLoad>();
            inputLoad.UserId = _currentRequestService.UserId;
            _applicationDbContext.InputLoads.Add(inputLoad);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _Key, string.Empty, inputLoad.SerializeObject(), inputLoad.RiceMillId);
            _cacheService.Maintain(_Key, inputLoad);
            return Result<DtoInputLoad>.Success(inputLoad.Adapt<DtoInputLoad>());
        }

        public Result<DtoInputLoad> Update(DtoUpdateInputLoad updateInputLoad)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoInputLoad>.Forbidden();

            var validationResult = updateInputLoad.Validate();
            if (!validationResult.IsValid)
                return Result<DtoInputLoad>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var inputLoad = GetInputLoadById(updateInputLoad.Id);
            if (inputLoad == null)
                return Result<DtoInputLoad>.Failure(new Error(ResultStatusEnum.InputLoadNotFound), HttpStatusCode.NotFound);

            var validateCreateInputLoadResult = ValidateInputLoad(updateInputLoad.Adapt<DtoCreateInputLoad>());
            if (validateCreateInputLoadResult != null)
                return validateCreateInputLoadResult;

            var beforeEdit = inputLoad.SerializeObject();
            inputLoad = updateInputLoad.Adapt(inputLoad);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _Key, beforeEdit, inputLoad.SerializeObject(), inputLoad.RiceMillId);
            _cacheService.Maintain(_Key, inputLoad);
            return Result<DtoInputLoad>.Success(inputLoad.Adapt<DtoInputLoad>());
        }

        public Result<bool> Delete(Guid id)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<bool>.Forbidden();

            var inputLoad = GetInputLoadById(id);
            if (inputLoad == null)
                return Result<bool>.Failure(new Error(ResultStatusEnum.InputLoadNotFound), HttpStatusCode.NotFound);

            var beforeEdit = inputLoad.SerializeObject();
            _applicationDbContext.InputLoads.Remove(inputLoad);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Delete, _Key, beforeEdit, inputLoad.SerializeObject(), inputLoad.RiceMillId);
            _cacheService.Maintain(_Key, inputLoad);
            return Result<bool>.Success(true);
        }

        private InputLoad GetInputLoadById(Guid id) => _applicationDbContext.InputLoads.FirstOrDefault(c => c.Id == id);

        private Result<DtoInputLoad> ValidateInputLoad(DtoCreateInputLoad inputLoad)
        {
            if (!_cacheService.GetVillages().Any(c => c.Id.Equals(inputLoad.VillageId)))
                return Result<DtoInputLoad>.Failure(new Error(ResultStatusEnum.VillageNotFound), HttpStatusCode.NotFound);

            var people = _cacheService.GetPeople().ToList();
            if (!people.Any(c => c.Id.Equals(inputLoad.DelivererPersonId)))
                return Result<DtoInputLoad>.Failure(new Error(ResultStatusEnum.InputLoadDelivererPersonNotFound), HttpStatusCode.NotFound);

            if (!people.Any(c => c.Id.Equals(inputLoad.ReceiverPersonId)))
                return Result<DtoInputLoad>.Failure(new Error(ResultStatusEnum.InputLoadReceiverPersonNotFound), HttpStatusCode.NotFound);

            if (!people.Any(c => c.Id.Equals(inputLoad.CarrierPersonId)))
                return Result<DtoInputLoad>.Failure(new Error(ResultStatusEnum.InputLoadCarrierPersonNotFound), HttpStatusCode.NotFound);

            if (!people.Any(c => c.Id.Equals(inputLoad.OwnerPersonId)))
                return Result<DtoInputLoad>.Failure(new Error(ResultStatusEnum.InputLoadOwnerPersonNotFound), HttpStatusCode.NotFound);

            if (!people.Any(c => c.Id.Equals(inputLoad.VehicleId)))
                return Result<DtoInputLoad>.Failure(new Error(ResultStatusEnum.VehicleNotFound), HttpStatusCode.NotFound);

            if (!_cacheService.GetRiceMills().Any(rm => rm.Id.Equals(inputLoad.RiceMillId)))
                return Result<DtoInputLoad>.Failure(new Error(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound);

            return null;
        }
    }
}