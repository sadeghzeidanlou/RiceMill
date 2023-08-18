using Mapster;
using Microsoft.Extensions.Primitives;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.DryerHistoryServices.Dto;
using RiceMill.Application.UseCases.UserActivityServices;
using RiceMill.Domain.Models;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Net;

namespace RiceMill.Application.UseCases.DryerHistoryServices
{
    public interface IDryerHistoryCommands : IBaseUseCaseCommands
    {
        Result<DtoDryerHistory> Create(DtoCreateDryerHistory dryerHistory);

        Result<DtoDryerHistory> Update(DtoUpdateDryerHistory dryerHistory);
    }

    public sealed class DryerHistoryCommands : IDryerHistoryCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;
        private readonly IUserActivityCommands _userActivityCommands;
        private readonly EntityTypeEnum _dryerHistoryKey = EntityTypeEnum.DryerHistories;
        private readonly EntityTypeEnum _inputLoadKey = EntityTypeEnum.InputLoads;
        private readonly EntityTypeEnum _dryerHistoryInputLoadKey = EntityTypeEnum.DryerHistoryInputLoads;

        public DryerHistoryCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService, ICacheService cacheService, IUserActivityCommands userActivityCommands)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
            _userActivityCommands = userActivityCommands;
        }

        public Result<DtoDryerHistory> Create(DtoCreateDryerHistory createDryerHistory)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoDryerHistory>.Forbidden();

            var validationResult = createDryerHistory.Validate();
            if (!validationResult.IsValid)
                return Result<DtoDryerHistory>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var validateCreateDryerHistoryResult = ValidateDryerHistory(createDryerHistory, true);
            if (validateCreateDryerHistoryResult != null)
                return validateCreateDryerHistoryResult;

            var dryerHistory = createDryerHistory.Adapt<DryerHistory>();
            dryerHistory.UserId = _currentRequestService.UserId;
            _applicationDbContext.DryerHistories.Add(dryerHistory);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _dryerHistoryKey, string.Empty, dryerHistory.SerializeObject(), dryerHistory.RiceMillId);
            _cacheService.Maintain(_dryerHistoryKey, dryerHistory);

            var inputLoad = GetInputLoadById(createDryerHistory.InputLoadId);
            var beforeEdit = inputLoad.SerializeObject();
            inputLoad.NumberOfBagsInDryer += createDryerHistory.NumberOfBagsInDryer;
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _inputLoadKey, beforeEdit, inputLoad.SerializeObject(), inputLoad.RiceMillId);
            _cacheService.Maintain(_inputLoadKey, inputLoad);

            var dryerHistoryInputLoad = new DryerHistoryInputLoad { DryerHistoryId = dryerHistory.Id, InputLoadId = inputLoad.Id };
            _applicationDbContext.DryerHistoryInputLoad.Add(dryerHistoryInputLoad);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _dryerHistoryInputLoadKey, string.Empty, dryerHistoryInputLoad.SerializeObject(), inputLoad.RiceMillId);
            _cacheService.Maintain(_dryerHistoryInputLoadKey, dryerHistoryInputLoad);

            return Result<DtoDryerHistory>.Success(dryerHistory.Adapt<DtoDryerHistory>());
        }

        public Result<DtoDryerHistory> Update(DtoUpdateDryerHistory updateDryerHistory)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoDryerHistory>.Forbidden();

            var validationResult = updateDryerHistory.Validate();
            if (!validationResult.IsValid)
                return Result<DtoDryerHistory>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var dryerHistory = GetDryerHistoryById(updateDryerHistory.Id);
            if (dryerHistory == null)
                return Result<DtoDryerHistory>.Failure(new Error(ResultStatusEnum.DryerHistoryNotFound), HttpStatusCode.NotFound);

            var validateCreateDryerHistoryResult = ValidateDryerHistory(updateDryerHistory.Adapt<DtoCreateDryerHistory>(), false);
            if (validateCreateDryerHistoryResult != null)
                return validateCreateDryerHistoryResult;

            var beforeEdit = dryerHistory.SerializeObject();
            dryerHistory = updateDryerHistory.Adapt(dryerHistory);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _dryerHistoryKey, beforeEdit, dryerHistory.SerializeObject(), dryerHistory.RiceMillId);
            _cacheService.Maintain(_dryerHistoryKey, dryerHistory);
            return Result<DtoDryerHistory>.Success(dryerHistory.Adapt<DtoDryerHistory>());
        }

        public Result<bool> Delete(Guid id)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<bool>.Forbidden();

            var dryerHistory = GetDryerHistoryById(id);
            if (dryerHistory == null)
                return Result<bool>.Failure(new Error(ResultStatusEnum.DryerHistoryNotFound), HttpStatusCode.NotFound);

            var beforeEdit = dryerHistory.SerializeObject();
            _applicationDbContext.DryerHistories.Remove(dryerHistory);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Delete, _dryerHistoryKey, beforeEdit, dryerHistory.SerializeObject(), dryerHistory.RiceMillId);
            _cacheService.Maintain(_dryerHistoryKey, dryerHistory);
            return Result<bool>.Success(true);
        }

        private DryerHistory GetDryerHistoryById(Guid id) => _applicationDbContext.DryerHistories.FirstOrDefault(c => c.Id == id);

        private InputLoad GetInputLoadById(Guid id) => _applicationDbContext.InputLoads.FirstOrDefault(c => c.Id == id);

        private Result<DtoDryerHistory> ValidateDryerHistory(DtoCreateDryerHistory dryerHistory, bool isNew)
        {
            if ((isNew && dryerHistory.Operation == DryerOperationEnum.Unload) || (!isNew && dryerHistory.Operation == DryerOperationEnum.Load))
                return Result<DtoDryerHistory>.Failure(new Error(ResultStatusEnum.DryerHistoryOperationIsNotValid), HttpStatusCode.BadRequest);

            if (dryerHistory.Operation == DryerOperationEnum.Unload && !dryerHistory.EndTime.HasValue)
                return Result<DtoDryerHistory>.Failure(new Error(ResultStatusEnum.DryerHistoryStopTimeIsNotValid), HttpStatusCode.BadRequest);

            if (!_cacheService.GetDryers().Any(c => c.Id.Equals(dryerHistory.DryerId)))
                return Result<DtoDryerHistory>.Failure(new Error(ResultStatusEnum.DryerNotFound), HttpStatusCode.NotFound);

            if (dryerHistory.RiceThreshingId.IsNotNullOrEmpty() && !_cacheService.GetRiceThreshings().Any(rt => rt.Id.Equals(dryerHistory.RiceThreshingId.Value)))
                return Result<DtoDryerHistory>.Failure(new Error(ResultStatusEnum.RiceThreshingNotFound), HttpStatusCode.NotFound);

            if (!_cacheService.GetInputLoads().Any(c => c.Id.Equals(dryerHistory.InputLoadId)))
                return Result<DtoDryerHistory>.Failure(new Error(ResultStatusEnum.InputLoadNotFound), HttpStatusCode.NotFound);

            if (!_cacheService.GetRiceMills().Any(rm => rm.Id.Equals(dryerHistory.RiceMillId)))
                return Result<DtoDryerHistory>.Failure(new Error(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound);

            return null;
        }
    }
}