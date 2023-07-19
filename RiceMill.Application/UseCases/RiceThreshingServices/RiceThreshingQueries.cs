using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.RiceThreshingServices.Dto;

namespace RiceMill.Application.UseCases.RiceThreshingServices
{
    public interface IRiceThreshingQueries
    {
        Result<PaginatedList<DtoRiceThreshing>> GetAll();
    }

    public class RiceThreshingQueries : IRiceThreshingQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public RiceThreshingQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<PaginatedList<DtoRiceThreshing>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}