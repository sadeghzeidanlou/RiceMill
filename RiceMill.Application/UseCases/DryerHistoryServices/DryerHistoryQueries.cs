using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DryerHistoryServices.Dto;

namespace RiceMill.Application.UseCases.DryerHistoryServices
{
    public interface IDryerHistoryQueries
    {
        Result<PaginatedList<DtoDryerHistory>> GetAll();
    }

    public class DryerHistoryQueries : IDryerHistoryQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DryerHistoryQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<PaginatedList<DtoDryerHistory>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}