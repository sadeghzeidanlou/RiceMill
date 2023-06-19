using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.Base;
using RiceMill.Application.UseCases.Person.Dto;

namespace RiceMill.Application.UseCases.Person
{
    public interface IPersonQueries : IBaseUseCaseQueries
    {
        Task<Result<DtoPerson>> GetAsync(int id);

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

        public Task<Result<DtoPerson>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}