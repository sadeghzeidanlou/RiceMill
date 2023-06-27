using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.RiceMillServices.Dto;

namespace RiceMill.Application.UseCases.RiceMillServices
{
    public interface IRiceMillQueries : IBaseUseCaseQueries
    {
        Task<Result<DtoRiceMill>> GetAsync(Guid id);

        Task<Result<List<DtoRiceMill>>> GetAllAsync();
    }

    public class RiceMillQueries : IRiceMillQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public RiceMillQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<List<DtoRiceMill>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoRiceMill>> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}