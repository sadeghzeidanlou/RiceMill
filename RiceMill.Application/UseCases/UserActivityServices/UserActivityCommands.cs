using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
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

        public Task<Result<bool>> DeleteAsync(Guid id, Guid riceMillId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoUserActivity>> UpdateAsync(DtoUpdateUserActivity userActivity)
        {
            throw new NotImplementedException();
        }
    }
}