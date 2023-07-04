using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.UserServices.Dto;

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

        public UserCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoUser>> CreateAsync(DtoCreateUser user)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteAsync(Guid id, Guid riceMillId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoUser>> UpdateAsync(DtoUpdateUser user)
        {
            throw new NotImplementedException();
        }
    }
}