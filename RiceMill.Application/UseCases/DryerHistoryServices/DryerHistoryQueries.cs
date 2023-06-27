using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DryerHistoryServices.Dto;

namespace RiceMill.Application.UseCases.DryerHistoryServices
{
    public interface IDryerHistoryQueries
    {
        Task<Result<int>> GetCountAsync();

        Task<Result<List<DtoDryerHistory>>> GetAllAsync();
    }

    public class DryerHistoryQueries : IDryerHistoryQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DryerHistoryQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<List<DtoDryerHistory>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}