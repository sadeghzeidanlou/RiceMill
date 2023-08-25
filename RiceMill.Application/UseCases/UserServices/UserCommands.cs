using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.UserActivityServices;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Domain.Models;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Net;

namespace RiceMill.Application.UseCases.UserServices
{
    public interface IUserCommands : IBaseUseCaseCommands
    {
        Result<DtoUser> Create(DtoCreateUser user);

        Result<DtoUser> Update(DtoUpdateUser user);
    }

    public sealed class UserCommands : IUserCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;
        private readonly IUserActivityCommands _userActivityCommands;
        private readonly EntityTypeEnum _Key = EntityTypeEnum.Users;

        public UserCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService, ICacheService cacheService, IUserActivityCommands userActivityCommands)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
            _userActivityCommands = userActivityCommands;
            _cacheService = cacheService;
        }

        public Result<DtoUser> Create(DtoCreateUser createUser)
        {
            if (_currentRequestService.HasNotAccessToRiceMills)
                return Result<DtoUser>.Forbidden();

            var validationResult = createUser.Validate();
            if (!validationResult.IsValid)
                return Result<DtoUser>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var validateCreateUserResult = ValidateUser(createUser);
            if (validateCreateUserResult != null)
                return validateCreateUserResult;

            var user = createUser.Adapt<User>();
            _applicationDbContext.Users.Add(user);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _Key, string.Empty, user.SerializeObject(), user.RiceMillId);
            _cacheService.Maintain(_Key, user);
            return Result<DtoUser>.Success(user.Adapt<DtoUser>());
        }

        public Result<bool> Delete(Guid id)
        {
            if (_currentRequestService.HasNotAccessToRiceMills)
                return Result<bool>.Forbidden();

            var user = GetUserById(id);
            if (user == null)
                return Result<bool>.Failure(Error.CreateError(ResultStatusEnum.UserNotFound), HttpStatusCode.NotFound);

            var beforeEdit = user.SerializeObject();
            _applicationDbContext.Users.Remove(user);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Delete, _Key, beforeEdit, user.SerializeObject(), user.RiceMillId);
            _cacheService.Maintain(_Key, user);
            return Result<bool>.Success(true);
        }

        public Result<DtoUser> Update(DtoUpdateUser updateUser)
        {
            if (_currentRequestService.HasNotAccessToRiceMills)
                return Result<DtoUser>.Forbidden();

            var validationResult = updateUser.Validate();
            if (!validationResult.IsValid)
                return Result<DtoUser>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var user = GetUserById(updateUser.Id);
            if (user == null)
                return Result<DtoUser>.Failure(Error.CreateError(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound);

            var validateCreateUserResult = ValidateUser(updateUser.Adapt<DtoCreateUser>());
            if (validateCreateUserResult != null)
                return validateCreateUserResult;

            var beforeEdit = user.SerializeObject();
            user = updateUser.Adapt(user);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _Key, beforeEdit, user.SerializeObject(), user.RiceMillId);
            _cacheService.Maintain(_Key, user);
            return Result<DtoUser>.Success(user.Adapt<DtoUser>());
        }

        private Result<DtoUser> ValidateUser(DtoCreateUser user)
        {
            var requestedUser = GetUserById(_currentRequestService.UserId);
            if (requestedUser == null)
                return Result<DtoUser>.Forbidden();

            if (requestedUser.Role == RoleEnum.RiceMillManager)
            {
                _ = user with { RiceMillId = requestedUser.RiceMillId };
            }
            else
            {
                if (user.Role != RoleEnum.Admin && user.RiceMillId.IsNullOrEmpty())
                    return Result<DtoUser>.Failure(Error.CreateError(ResultStatusEnum.RiceMillIdIsNotValid), HttpStatusCode.BadRequest);

                if (user.Role == RoleEnum.Admin)
                    _ = user with { RiceMillId = null };
            }
            return null;
        }

        private User GetUserById(Guid id) => _applicationDbContext.Users.FirstOrDefault(c => c.Id == id);
    }
}