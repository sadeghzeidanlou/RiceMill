using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DryerServices.Dto;

namespace RiceMill.Application.UseCases.DryerServices
{
    public interface IDryerQueries
    {
        Result<PaginatedList<DtoDryer>> GetAll();
    }

    public class DryerQueries : IDryerQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DryerQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<PaginatedList<DtoDryer>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}