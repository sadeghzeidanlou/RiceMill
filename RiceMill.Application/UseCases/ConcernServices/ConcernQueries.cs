using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.ConcernServices.Dto;

namespace RiceMill.Application.UseCases.ConcernServices
{
    public interface IConcernQueries : IBaseUseCaseQueries
    {
        Task<Result<DtoConcern>> GetAsync(int id);

        Task<Result<List<DtoConcern>>> GetAllAsync();
    }

    public class ConcernQueries : IConcernQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ConcernQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<List<DtoConcern>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoConcern>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}
