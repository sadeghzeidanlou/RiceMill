using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.RiceMillServices.Dto;

namespace RiceMill.Application.UseCases.RiceMillServices
{
    public interface IRiceMillCommands : IBaseUseCaseCommands
    {
        Task<Result<DtoRiceMill>> CreateAsync(DtoCreateRiceMill riceMill);

        Task<Result<DtoRiceMill>> UpdateAsync(DtoUpdateRiceMill riceMill);
    }

    public class RiceMillCommands : IRiceMillCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public RiceMillCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoRiceMill>> CreateAsync(DtoCreateRiceMill riceMill)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoRiceMill>> UpdateAsync(DtoUpdateRiceMill riceMill)
        {
            throw new NotImplementedException();
        }
    }
}