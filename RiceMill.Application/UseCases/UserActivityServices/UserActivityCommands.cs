using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.UserActivityServices.Dto;

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

        public UserActivityCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoUserActivity>> CreateAsync(DtoCreateUserActivity userActivity)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> DeleteAsync(Guid id) => await Task.FromResult(Result<bool>.NotImplemented());

        public async Task<Result<DtoUserActivity>> UpdateAsync(DtoUpdateUserActivity userActivity) => await Task.FromResult(Result<DtoUserActivity>.NotImplemented());
    }
}