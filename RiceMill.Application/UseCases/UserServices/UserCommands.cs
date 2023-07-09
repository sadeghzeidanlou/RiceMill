using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Domain.Models;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Net;

namespace RiceMill.Application.UseCases.UserServices
{
    public interface IUserCommands : IBaseUseCaseCommands
    {
        Task<Result<DtoUser>> CreateAsync(DtoCreateUser user);

        Task<Result<DtoUser>> UpdateAsync(DtoUpdateUser user);
    }

    public class UserCommands : IUserCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentRequestService _currentRequestService;

        public UserCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
        }

        public async Task<Result<DtoUser>> CreateAsync(DtoCreateUser createUser)
        {
            if (_currentRequestService.HasNotAccessToRiceMills)
                return await Task.FromResult(Result<DtoUser>.Forbidden());

            var validationResult = createUser.Validate();
            if (!validationResult.IsValid)
                return await Task.FromResult(Result<DtoUser>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest));

            createUser = createUser with { ParentUserId = _currentRequestService.UserId };
            var validateCreateUserResult = await ValidateCreateUser(createUser);
            if (validateCreateUserResult != null)
                return validateCreateUserResult;

            var user = createUser.Adapt<User>();
            _applicationDbContext.Users.Add(user);
            await _applicationDbContext.SaveChangesAsync();
            return await Task.FromResult(Result<DtoUser>.Success(user.Adapt<DtoUser>()));
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (_currentRequestService.HasNotAccessToRiceMills)
                return await Task.FromResult(Result<bool>.Forbidden());

            var user = _applicationDbContext.Users.FirstOrDefault(c => c.Id == id);
            if (user == null)
                return await Task.FromResult(Result<bool>.Failure(new Error(ResultStatusEnum.UserNotFound), HttpStatusCode.NotFound));

            _applicationDbContext.Users.Remove(user);
            await _applicationDbContext.SaveChangesAsync();
            return await Task.FromResult(Result<bool>.Success(true));
        }

        public async Task<Result<DtoUser>> UpdateAsync(DtoUpdateUser updateUser)
        {
            if (_currentRequestService.HasNotAccessToRiceMills)
                return await Task.FromResult(Result<DtoUser>.Forbidden());

            var validationResult = updateUser.Validate();
            if (!validationResult.IsValid)
                return await Task.FromResult(Result<DtoUser>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest));

            var user = _applicationDbContext.Users.FirstOrDefault(c => c.Id == updateUser.Id);
            if (user == null)
                return await Task.FromResult(Result<DtoUser>.Failure(new Error(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound));

            var validateCreateUserResult = await ValidateCreateUser(updateUser.Adapt<DtoCreateUser>());
            if (validateCreateUserResult != null)
                return validateCreateUserResult;

            user = updateUser.Adapt(user);
            await _applicationDbContext.SaveChangesAsync();
            return await Task.FromResult(Result<DtoUser>.Success(user.Adapt<DtoUser>()));
        }

        private async Task<Result<DtoUser>> ValidateCreateUser(DtoCreateUser createUser)
        {
            var requestedUser = _applicationDbContext.Users.FirstOrDefault(u => u.Id.Equals(_currentRequestService.UserId));
            if (requestedUser == null)
                return await Task.FromResult(Result<DtoUser>.Forbidden());

            if (requestedUser.Role == RoleEnum.RiceMillManager)
            {
                createUser = createUser with { RiceMillId = requestedUser.RiceMillId };
            }
            else
            {
                if (createUser.Role != RoleEnum.Admin && createUser.RiceMillId.IsNullOrEmpty())
                    return await Task.FromResult(Result<DtoUser>.Failure(new Error(ResultStatusEnum.UserRiceMillIdIsNotValid), HttpStatusCode.BadRequest));
            }
            return null;
        }
    }
}