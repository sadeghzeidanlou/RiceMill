using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.RiceMillServices.Dto;

namespace RiceMill.Application.UseCases.RiceMillServices
{
    public interface IRiceMillQueries
    {
        Task<Result<PaginatedList<DtoRiceMill>>> GetAllAsync(DtoRiceMillFilter riceMillFilter);
    }

    public class RiceMillQueries : IRiceMillQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public RiceMillQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<PaginatedList<DtoRiceMill>>> GetAllAsync(DtoRiceMillFilter riceMillFilter)
        {
            throw new NotImplementedException();
        }
    }
}