using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.Base;
using RiceMill.Application.UseCases.RiceThreshing.Dto;

namespace RiceMill.Application.UseCases.RiceThreshing
{
    public interface IRiceThreshingQueries : IBaseUseCaseQueries
    {
        Task<Result<DtoRiceThreshing>> GetAsync(int id);

        Task<Result<List<DtoRiceThreshing>>> GetAllAsync();
    }

    public class RiceThreshingQueries : IRiceThreshingQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public RiceThreshingQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<List<DtoRiceThreshing>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoRiceThreshing>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}