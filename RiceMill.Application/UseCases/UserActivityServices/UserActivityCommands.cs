using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.UserActivityServices.Dto;
using RiceMill.Domain.Models;
using Shared.Enums;
using System.Net;

namespace RiceMill.Application.UseCases.UserActivityServices
{
    public interface IUserActivityCommands : IBaseUseCaseCommands
    {
        Result<DtoUserActivity> Create(DtoCreateUserActivity userActivity);

        Result<DtoUserActivity> Update(DtoUpdateUserActivity userActivity);
    }

    public class UserActivityCommands : IUserActivityCommands
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
            _cacheService.Add(nameof(EntityTypeEnum.UserActivities), userActivity);
            return Result<DtoUserActivity>.Success(userActivity.Adapt<DtoUserActivity>());
        }

        public Result<bool> Delete(Guid id) => Result<bool>.NotImplemented();

        public Result<DtoUserActivity> Update(DtoUpdateUserActivity userActivity) => Result<DtoUserActivity>.NotImplemented();
    }
}