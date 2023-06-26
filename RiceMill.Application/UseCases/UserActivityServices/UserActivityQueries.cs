using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.UserActivityServices.Dto;

namespace RiceMill.Application.UseCases.UserActivityServices
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