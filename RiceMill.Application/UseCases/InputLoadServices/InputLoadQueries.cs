using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.InputLoadServices.Dto;

namespace RiceMill.Application.UseCases.InputLoadServices
{
    public interface IInputLoadQueries
    {
        Result<PaginatedList<DtoInputLoad>> GetAll();
    }

    public class InputLoadQueries : IInputLoadQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public InputLoadQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<PaginatedList<DtoInputLoad>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}