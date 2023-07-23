﻿using Mapster;
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

    public class UserCommands : IUserCommands
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

            createUser = createUser with { ParentUserId = _currentRequestService.UserId };
            var validateCreateUserResult = ValidateCreateUser(createUser);
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
                return Result<bool>.Failure(new Error(ResultStatusEnum.UserNotFound), HttpStatusCode.NotFound);

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
                return Result<DtoUser>.Failure(new Error(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound);

            var beforeEdit = user.SerializeObject();
            var validateCreateUserResult = ValidateCreateUser(updateUser.Adapt<DtoCreateUser>());
            if (validateCreateUserResult != null)
                return validateCreateUserResult;

            user = updateUser.Adapt(user);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _Key, beforeEdit, user.SerializeObject(), user.RiceMillId);
            _cacheService.Maintain(_Key, user);
            return Result<DtoUser>.Success(user.Adapt<DtoUser>());
        }

        private Result<DtoUser> ValidateCreateUser(DtoCreateUser createUser)
        {
            var requestedUser = GetUserById(_currentRequestService.UserId);
            if (requestedUser == null)
                return Result<DtoUser>.Forbidden();

            if (requestedUser.Role == RoleEnum.RiceMillManager)
            {
                createUser = createUser with { RiceMillId = requestedUser.RiceMillId };
            }
            else
            {
                if (createUser.Role != RoleEnum.Admin && createUser.RiceMillId.IsNullOrEmpty())
                    return Result<DtoUser>.Failure(new Error(ResultStatusEnum.UserRiceMillIdIsNotValid), HttpStatusCode.BadRequest);
            }
            return null;
        }

        private User GetUserById(Guid userId) => _applicationDbContext.Users.FirstOrDefault(c => c.Id == userId);
    }
}