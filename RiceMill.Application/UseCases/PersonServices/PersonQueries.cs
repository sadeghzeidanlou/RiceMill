using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.PersonServices.Dto;

namespace RiceMill.Application.UseCases.PersonServices
{
    public interface IPersonQueries
    {
        Result<PaginatedList<DtoPerson>> GetAllAsync();
    }

    public class PersonQueries : IPersonQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public PersonQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<PaginatedList<DtoPerson>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}