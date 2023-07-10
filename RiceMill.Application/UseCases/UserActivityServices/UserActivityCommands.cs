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
        Task<Result<DtoUserActivity>> CreateAsync(DtoCreateUserActivity userActivity);

        Task<Result<DtoUserActivity>> UpdateAsync(DtoUpdateUserActivity userActivity);
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

        public async Task<Result<DtoUserActivity>> CreateAsync(DtoCreateUserActivity createUserActivity)
        {
            var validationResult = createUserActivity.Validate();
            if (!validationResult.IsValid)
                return await Task.FromResult(Result<DtoUserActivity>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest));

            var userActivity = createUserActivity.Adapt<UserActivity>();
            _applicationDbContext.UserActivities.Add(userActivity);
            await _applicationDbContext.SaveChangesAsync();
            _cacheService.Add(nameof(EntityTypeEnum.UserActivities), userActivity);
            return await Task.FromResult(Result<DtoUserActivity>.Success(userActivity.Adapt<DtoUserActivity>()));
        }

        public async Task<Result<bool>> DeleteAsync(Guid id) => await Task.FromResult(Result<bool>.NotImplemented());

        public async Task<Result<DtoUserActivity>> UpdateAsync(DtoUpdateUserActivity userActivity) => await Task.FromResult(Result<DtoUserActivity>.NotImplemented());
    }
}