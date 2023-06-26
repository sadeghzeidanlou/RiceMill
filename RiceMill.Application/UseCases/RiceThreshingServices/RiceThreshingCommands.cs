using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.RiceThreshingServices.Dto;

namespace RiceMill.Application.UseCases.RiceThreshingServices
{
    public interface IRiceThreshingCommands : IBaseUseCaseCommands
    {
        Task<Result<DtoRiceThreshing>> CreateAsync(DtoCreateRiceThreshing riceThreshing);

        Task<Result<DtoRiceThreshing>> UpdateAsync(DtoUpdateRiceThreshing riceThreshing);
    }

    public class RiceThreshingCommands : IRiceThreshingCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public RiceThreshingCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoRiceThreshing>> CreateAsync(DtoCreateRiceThreshing riceThreshing)
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

        public Task<Result<DtoRiceThreshing>> UpdateAsync(DtoUpdateRiceThreshing riceThreshing)
        {
            throw new NotImplementedException();
        }
    }
}