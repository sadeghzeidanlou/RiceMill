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

    public sealed class RiceThreshingCommands : IRiceThreshingCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;
        private readonly IUserActivityCommands _userActivityCommands;
        private readonly EntityTypeEnum _riceThreshingKey = EntityTypeEnum.RiceThreshings;
        //private readonly EntityTypeEnum _dryerHistoryKey = EntityTypeEnum.DryerHistories;

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

            var validateRiceThreshing = ValidateRiceThreshing(createRiceThreshing);
            if (validateRiceThreshing != null)
                return validateRiceThreshing;

            var riceThreshing = createRiceThreshing.Adapt<RiceThreshing>();
            riceThreshing.UserId = _currentRequestService.UserId;
            _applicationDbContext.RiceThreshings.Add(riceThreshing);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _riceThreshingKey, string.Empty, riceThreshing.SerializeObject(), riceThreshing.RiceMillId);
            _cacheService.Maintain(_riceThreshingKey, riceThreshing);
            //foreach (var dryerHistoryId in createRiceThreshing.DryerHistoryIds)
            //{
            //    var dryerHistory = GetDryerHistoryById(dryerHistoryId);
            //    var beforeEdit = dryerHistory.SerializeObject();
            //    dryerHistory.RiceThreshingId = riceThreshing.Id;
            //    _applicationDbContext.SaveChanges();
            //    _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _dryerHistoryKey, beforeEdit, dryerHistory.SerializeObject(), dryerHistory.RiceMillId);
            //    _cacheService.Maintain(_dryerHistoryKey, dryerHistory);
            //}
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
                return Result<DtoRiceThreshing>.Failure(Error.CreateError(ResultStatusEnum.RiceThreshingNotFound), HttpStatusCode.NotFound);

            var createRiceThreshing = updateRiceThreshing.Adapt<DtoCreateRiceThreshing>();
            createRiceThreshing = createRiceThreshing with { RiceMillId = riceThreshing.RiceMillId };
            var validateRiceThreshing = ValidateRiceThreshing(createRiceThreshing);
            if (validateRiceThreshing != null)
                return validateRiceThreshing;

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
                return Result<bool>.Failure(Error.CreateError(ResultStatusEnum.RiceThreshingNotFound), HttpStatusCode.NotFound);

            var beforeEdit = riceThreshing.SerializeObject();
            _applicationDbContext.RiceThreshings.Remove(riceThreshing);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Delete, _riceThreshingKey, beforeEdit, riceThreshing.SerializeObject(), riceThreshing.RiceMillId);
            _cacheService.Maintain(_riceThreshingKey, riceThreshing);
            return Result<bool>.Success(true);
        }

        private RiceThreshing GetRiceThreshingById(Guid id) => _applicationDbContext.RiceThreshings.FirstOrDefault(c => c.Id.Equals(id));

        //private DryerHistory GetDryerHistoryById(Guid id) => _applicationDbContext.DryerHistories.FirstOrDefault(c => c.Id.Equals(id));

        private Result<DtoRiceThreshing> ValidateRiceThreshing(DtoCreateRiceThreshing riceThreshing)
        {
            if (!_cacheService.GetIncomes().Any(rm => rm.Id.Equals(riceThreshing.IncomeId)))
                return Result<DtoRiceThreshing>.Failure(Error.CreateError(ResultStatusEnum.IncomeNotFound), HttpStatusCode.NotFound);

            if (!_cacheService.GetRiceMills().Any(rm => rm.Id.Equals(riceThreshing.RiceMillId)))
                return Result<DtoRiceThreshing>.Failure(Error.CreateError(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound);

            return null;
        }
    }
}