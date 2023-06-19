using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.Base;
using RiceMill.Application.UseCases.User.Dto;

namespace RiceMill.Application.UseCases.User
{
    public interface IUserCommands : IBaseUseCaseCommands
    {
        Task<Result<DtoUser>> CreateAsync(DtoCreateUser user);

        Task<Result<DtoUser>> UpdateAsync(DtoUpdateUser user);
    }

    public class UserCommands : IUserCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UserCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoUser>> CreateAsync(DtoCreateUser user)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoUser>> UpdateAsync(DtoUpdateUser user)
        {
            throw new NotImplementedException();
        }
    }
}