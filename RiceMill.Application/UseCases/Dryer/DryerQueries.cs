using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.Base;
using RiceMill.Application.UseCases.Dryer.Dto;

namespace RiceMill.Application.UseCases.Dryer
{
    public interface IDryerQueries : IBaseUseCaseQueries
    {
        Task<Result<DtoDryer>> GetAsync(int id);

        Task<Result<List<DtoDryer>>> GetAllAsync();
    }

    public class DryerQueries : IDryerQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DryerQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<List<DtoDryer>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoDryer>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}