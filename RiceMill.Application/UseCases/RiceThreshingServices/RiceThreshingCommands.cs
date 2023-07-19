using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.RiceThreshingServices.Dto;

namespace RiceMill.Application.UseCases.RiceThreshingServices
{
    public interface IRiceThreshingCommands : IBaseUseCaseCommands
    {
        Result<DtoRiceThreshing> Create(DtoCreateRiceThreshing riceThreshing);

        Result<DtoRiceThreshing> Update(DtoUpdateRiceThreshing riceThreshing);
    }

    public class RiceThreshingCommands : IRiceThreshingCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public RiceThreshingCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<DtoRiceThreshing> Create(DtoCreateRiceThreshing riceThreshing)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Result<DtoRiceThreshing> Update(DtoUpdateRiceThreshing riceThreshing)
        {
            throw new NotImplementedException();
        }
    }
}