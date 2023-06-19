using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.Base;
using RiceMill.Application.UseCases.UserActivity.Dto;

namespace RiceMill.Application.UseCases.UserActivity
{
    public interface IUserActivityQueries : IBaseUseCaseQueries
    {
        Task<Result<DtoUserActivity>> GetAsync(int id);

        Task<Result<List<DtoUserActivity>>> GetAllAsync();
    }

    public class UserActivityQueries : IUserActivityQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UserActivityQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<List<DtoUserActivity>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoUserActivity>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}