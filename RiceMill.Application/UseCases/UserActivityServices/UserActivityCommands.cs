using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.UserActivityServices.Dto;
using RiceMill.Domain.Models;
using Shared.Enums;
using System.Net;

namespace RiceMill.Application.UseCases.UserActivityServices
{
    public interface IUserActivityCommands
    {
        Result<DtoUserActivity> Create(DtoCreateUserActivity userActivity);

        Result<DtoUserActivity> CreateGeneral(UserActivityTypeEnum userActivityType, EntityTypeEnum type, string beforeEdit, string afterEdit, Guid? riceMillId);
    }

    public sealed class UserActivityCommands : IUserActivityCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;

        public UserActivityCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService, ICacheService cacheService)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
        }

        public Result<DtoUserActivity> Create(DtoCreateUserActivity createUserActivity)
        {
            var validationResult = createUserActivity.Validate();
            if (!validationResult.IsValid)
                return Result<DtoUserActivity>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var userActivity = createUserActivity.Adapt<UserActivity>();
            _applicationDbContext.UserActivities.Add(userActivity);
            _applicationDbContext.SaveChanges();
            _cacheService.Maintain(EntityTypeEnum.UserActivities, userActivity);
            return Result<DtoUserActivity>.Success(userActivity.Adapt<DtoUserActivity>());
        }

        public Result<DtoUserActivity> CreateGeneral(UserActivityTypeEnum userActivityType, EntityTypeEnum type, string beforeEdit, string afterEdit, Guid? riceMillId)
        {
            var _userActivity = new DtoCreateUserActivity(_currentRequestService.UserId, _currentRequestService.Ip, userActivityType, type, ApplicationIdEnum.Mobile, beforeEdit, afterEdit, riceMillId);
            return Create(_userActivity);
        }
    }
}