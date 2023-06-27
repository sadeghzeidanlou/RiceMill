using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.InputLoadServices.Dto;

namespace RiceMill.Application.UseCases.InputLoadServices
{
    public interface IInputLoadQueries
    {
        Task<Result<int>> GetCountAsync();

        Task<Result<List<DtoInputLoad>>> GetAllAsync();
    }

    public class InputLoadQueries : IInputLoadQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public InputLoadQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<List<DtoInputLoad>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}