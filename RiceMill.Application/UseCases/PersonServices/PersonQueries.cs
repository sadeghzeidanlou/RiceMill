using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.PersonServices.Dto;

namespace RiceMill.Application.UseCases.PersonServices
{
    public interface IPersonQueries : IBaseUseCaseQueries
    {
        Task<Result<DtoPerson>> GetAsync(Guid id);

        Task<Result<List<DtoPerson>>> GetAllAsync();
    }

    public class PersonQueries : IPersonQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public PersonQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<List<DtoPerson>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoPerson>> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}