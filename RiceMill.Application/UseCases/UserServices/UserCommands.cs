using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Domain.Models;
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
            if (_currentRequestService.IsNotAdmin)
                return await Task.FromResult(Result<DtoUser>.Forbidden());

            var validationResult = createUser.Validate();
            if (!validationResult.IsValid)
                return await Task.FromResult(Result<DtoUser>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest));

            var user = createUser.Adapt<User>();
            _applicationDbContext.Users.Add(user);
            await _applicationDbContext.SaveChangesAsync();
            return await Task.FromResult(Result<DtoUser>.Success(user.Adapt<DtoUser>()));
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (_currentRequestService.IsNotAdmin)
                return await Task.FromResult(Result<bool>.Forbidden());

            var user = _applicationDbContext.Users.FirstOrDefault(c => c.Id == id);
            if (user == null)
                return await Task.FromResult(Result<bool>.Failure(new Error(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound));

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

            user = updateUser.Adapt(user);
            await _applicationDbContext.SaveChangesAsync();
            return await Task.FromResult(Result<DtoUser>.Success(user.Adapt<DtoUser>()));
        }
    }
}