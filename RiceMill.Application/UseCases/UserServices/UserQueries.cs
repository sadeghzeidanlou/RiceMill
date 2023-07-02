using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.UserServices.Dto;

namespace RiceMill.Application.UseCases.UserServices
{
    public interface IUserQueries
    {
        Task<Result<PaginatedList<DtoUser>>> GetAllAsync(DtoUserFilter dtoUserFilter);
    }

    public class UserQueries : IUserQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UserQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<PaginatedList<DtoUser>>> GetAllAsync(DtoUserFilter dtoUserFilter)
        {
            throw new NotImplementedException();
        }
    }
}