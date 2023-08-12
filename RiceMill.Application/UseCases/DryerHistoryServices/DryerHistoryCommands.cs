using Mapster;
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

    public class DryerHistoryCommands : IDryerHistoryCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;
        private readonly IUserActivityCommands _userActivityCommands;
        private readonly EntityTypeEnum _Key = EntityTypeEnum.DryerHistories;

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

            var validateCreateDryerHistoryResult = ValidateDryerHistory(createDryerHistory);
            if (validateCreateDryerHistoryResult != null)
                return validateCreateDryerHistoryResult;

            var payment = createDryerHistory.Adapt<DryerHistory>();
            payment.UserId = _currentRequestService.UserId;
            _applicationDbContext.DryerHistories.Add(payment);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _Key, string.Empty, payment.SerializeObject(), payment.RiceMillId);
            _cacheService.Maintain(_Key, payment);
            return Result<DtoDryerHistory>.Success(payment.Adapt<DtoDryerHistory>());
        }

        public Result<DtoDryerHistory> Update(DtoUpdateDryerHistory updateDryerHistory)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoDryerHistory>.Forbidden();

            var validationResult = updateDryerHistory.Validate();
            if (!validationResult.IsValid)
                return Result<DtoDryerHistory>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var payment = GetDryerHistoryById(updateDryerHistory.Id);
            if (payment == null)
                return Result<DtoDryerHistory>.Failure(new Error(ResultStatusEnum.DryerHistoryNotFound), HttpStatusCode.NotFound);

            var validateCreateDryerHistoryResult = ValidateDryerHistory(updateDryerHistory.Adapt<DtoCreateDryerHistory>());
            if (validateCreateDryerHistoryResult != null)
                return validateCreateDryerHistoryResult;

            var beforeEdit = payment.SerializeObject();
            payment = updateDryerHistory.Adapt(payment);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _Key, beforeEdit, payment.SerializeObject(), payment.RiceMillId);
            _cacheService.Maintain(_Key, payment);
            return Result<DtoDryerHistory>.Success(payment.Adapt<DtoDryerHistory>());
        }

        public Result<bool> Delete(Guid id)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<bool>.Forbidden();

            var payment = GetDryerHistoryById(id);
            if (payment == null)
                return Result<bool>.Failure(new Error(ResultStatusEnum.DryerHistoryNotFound), HttpStatusCode.NotFound);

            var beforeEdit = payment.SerializeObject();
            _applicationDbContext.DryerHistories.Remove(payment);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Delete, _Key, beforeEdit, payment.SerializeObject(), payment.RiceMillId);
            _cacheService.Maintain(_Key, payment);
            return Result<bool>.Success(true);
        }

        private DryerHistory GetDryerHistoryById(Guid id) => _applicationDbContext.DryerHistories.FirstOrDefault(c => c.Id == id);

        private Result<DtoDryerHistory> ValidateDryerHistory(DtoCreateDryerHistory payment)
        {
            if (!_cacheService.GetPeople().Any(c => c.Id.Equals(payment.PaidPersonId)))
                return Result<DtoDryerHistory>.Failure(new Error(ResultStatusEnum.PersonNotFound), HttpStatusCode.NotFound);

            if (!_cacheService.GetConcerns().Any(c => c.Id.Equals(payment.ConcernId)))
                return Result<DtoDryerHistory>.Failure(new Error(ResultStatusEnum.ConcernNotFound), HttpStatusCode.NotFound);

            if (payment.InputLoadId.IsNotNullOrEmpty() && !_cacheService.GetInputLoads().Any(il => il.Id.Equals(payment.InputLoadId.Value)))
                return Result<DtoDryerHistory>.Failure(new Error(ResultStatusEnum.InputLoadNotFound), HttpStatusCode.NotFound);

            if (!_cacheService.GetRiceMills().Any(rm => rm.Id.Equals(payment.RiceMillId)))
                return Result<DtoDryerHistory>.Failure(new Error(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound);

            return null;
        }
    }
}