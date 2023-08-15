using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.RiceThreshingServices.Dto;
using RiceMill.Application.UseCases.UserActivityServices;
using RiceMill.Domain.Models;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Net;

namespace RiceMill.Application.UseCases.RiceThreshingServices
{
    public interface IRiceThreshingCommands : IBaseUseCaseCommands
    {
        Result<DtoRiceThreshing> Create(DtoCreateRiceThreshing riceThreshing);

        Result<DtoRiceThreshing> Update(DtoUpdateRiceThreshing riceThreshing);
    }

    public class RiceThreshingCommands : IRiceThreshingCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;
        private readonly IUserActivityCommands _userActivityCommands;
        private readonly EntityTypeEnum _riceThreshingKey = EntityTypeEnum.RiceThreshings;

        public RiceThreshingCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService, ICacheService cacheService, IUserActivityCommands userActivityCommands)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
            _userActivityCommands = userActivityCommands;
        }

        public Result<DtoRiceThreshing> Create(DtoCreateRiceThreshing createRiceThreshing)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoRiceThreshing>.Forbidden();

            var validationResult = createRiceThreshing.Validate();
            if (!validationResult.IsValid)
                return Result<DtoRiceThreshing>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var validateCreateRiceThreshingResult = ValidateRiceThreshing(createRiceThreshing, true);
            if (validateCreateRiceThreshingResult != null)
                return validateCreateRiceThreshingResult;

            var riceThreshing = createRiceThreshing.Adapt<RiceThreshing>();
            riceThreshing.UserId = _currentRequestService.UserId;
            _applicationDbContext.RiceThreshings.Add(riceThreshing);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _riceThreshingKey, string.Empty, riceThreshing.SerializeObject(), riceThreshing.RiceMillId);
            _cacheService.Maintain(_riceThreshingKey, riceThreshing);

            //var inputLoad = GetInputLoadById(createRiceThreshing.InputLoadId);
            //var beforeEdit = inputLoad.SerializeObject();
            //inputLoad.NumberOfBagsInDryer += createRiceThreshing.NumberOfBagsInDryer;
            //_applicationDbContext.SaveChanges();
            //_userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _inputLoadKey, beforeEdit, inputLoad.SerializeObject(), inputLoad.RiceMillId);
            //_cacheService.Maintain(_inputLoadKey, inputLoad);

            //var riceThreshingInputLoad = new RiceThreshingInputLoad { RiceThreshingId = riceThreshing.Id, InputLoadId = inputLoad.Id };
            //_applicationDbContext.RiceThreshingInputLoad.Add(riceThreshingInputLoad);
            //_applicationDbContext.SaveChanges();
            //_userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _riceThreshingInputLoadKey, string.Empty, riceThreshingInputLoad.SerializeObject(), inputLoad.RiceMillId);
            //_cacheService.Maintain(_riceThreshingInputLoadKey, riceThreshingInputLoad);

            return Result<DtoRiceThreshing>.Success(riceThreshing.Adapt<DtoRiceThreshing>());
        }

        public Result<DtoRiceThreshing> Update(DtoUpdateRiceThreshing updateRiceThreshing)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoRiceThreshing>.Forbidden();

            var validationResult = updateRiceThreshing.Validate();
            if (!validationResult.IsValid)
                return Result<DtoRiceThreshing>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var riceThreshing = GetRiceThreshingById(updateRiceThreshing.Id);
            if (riceThreshing == null)
                return Result<DtoRiceThreshing>.Failure(new Error(ResultStatusEnum.RiceThreshingNotFound), HttpStatusCode.NotFound);

            var validateCreateRiceThreshingResult = ValidateRiceThreshing(updateRiceThreshing.Adapt<DtoCreateRiceThreshing>(), false);
            if (validateCreateRiceThreshingResult != null)
                return validateCreateRiceThreshingResult;

            var beforeEdit = riceThreshing.SerializeObject();
            riceThreshing = updateRiceThreshing.Adapt(riceThreshing);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _riceThreshingKey, beforeEdit, riceThreshing.SerializeObject(), riceThreshing.RiceMillId);
            _cacheService.Maintain(_riceThreshingKey, riceThreshing);
            return Result<DtoRiceThreshing>.Success(riceThreshing.Adapt<DtoRiceThreshing>());
        }

        public Result<bool> Delete(Guid id)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<bool>.Forbidden();

            var riceThreshing = GetRiceThreshingById(id);
            if (riceThreshing == null)
                return Result<bool>.Failure(new Error(ResultStatusEnum.RiceThreshingNotFound), HttpStatusCode.NotFound);

            var beforeEdit = riceThreshing.SerializeObject();
            _applicationDbContext.RiceThreshings.Remove(riceThreshing);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Delete, _riceThreshingKey, beforeEdit, riceThreshing.SerializeObject(), riceThreshing.RiceMillId);
            _cacheService.Maintain(_riceThreshingKey, riceThreshing);
            return Result<bool>.Success(true);
        }

        private RiceThreshing GetRiceThreshingById(Guid id) => _applicationDbContext.RiceThreshings.FirstOrDefault(c => c.Id == id);

        //private InputLoad GetInputLoadById(Guid id) => _applicationDbContext.InputLoads.FirstOrDefault(c => c.Id == id);

        private Result<DtoRiceThreshing> ValidateRiceThreshing(DtoCreateRiceThreshing riceThreshing, bool isNew)
        {
            //if ((isNew && riceThreshing.Operation == DryerOperationEnum.Unload) || (!isNew && riceThreshing.Operation == DryerOperationEnum.Load))
            //    return Result<DtoRiceThreshing>.Failure(new Error(ResultStatusEnum.RiceThreshingOperationIsNotValid), HttpStatusCode.BadRequest);

            //if (riceThreshing.Operation == DryerOperationEnum.Unload && !riceThreshing.EndTime.HasValue)
            //    return Result<DtoRiceThreshing>.Failure(new Error(ResultStatusEnum.RiceThreshingStopTimeIsNotValid), HttpStatusCode.BadRequest);

            //if (!_cacheService.GetDryers().Any(c => c.Id.Equals(riceThreshing.DryerId)))
            //    return Result<DtoRiceThreshing>.Failure(new Error(ResultStatusEnum.DryerNotFound), HttpStatusCode.NotFound);

            //if (riceThreshing.RiceThreshingId.IsNotNullOrEmpty() && !_cacheService.GetRiceThreshings().Any(rt => rt.Id.Equals(riceThreshing.RiceThreshingId.Value)))
            //    return Result<DtoRiceThreshing>.Failure(new Error(ResultStatusEnum.RiceThreshingNotFound), HttpStatusCode.NotFound);

            //if (!_cacheService.GetInputLoads().Any(c => c.Id.Equals(riceThreshing.InputLoadId)))
            //    return Result<DtoRiceThreshing>.Failure(new Error(ResultStatusEnum.InputLoadNotFound), HttpStatusCode.NotFound);

            //if (!_cacheService.GetRiceMills().Any(rm => rm.Id.Equals(riceThreshing.RiceMillId)))
            //    return Result<DtoRiceThreshing>.Failure(new Error(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound);

            return null;
        }
    }
}