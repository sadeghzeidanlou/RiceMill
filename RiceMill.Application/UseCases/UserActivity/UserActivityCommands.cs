using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.Base;
using RiceMill.Application.UseCases.UserActivity.Dto;

namespace RiceMill.Application.UseCases.UserActivity
{
    public interface IUserActivityCommands : IBaseUseCaseCommands
    {
        Task<Result<DtoUserActivity>> CreateAsync(DtoCreateUserActivity userActivity);

        Task<Result<DtoUserActivity>> UpdateAsync(DtoUpdateUserActivity userActivity);
    }

    public class UserActivityCommands : IUserActivityCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UserActivityCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoUserActivity>> CreateAsync(DtoCreateUserActivity userActivity)
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

        public Task<Result<DtoUserActivity>> UpdateAsync(DtoUpdateUserActivity userActivity)
        {
            throw new NotImplementedException();
        }
    }
}