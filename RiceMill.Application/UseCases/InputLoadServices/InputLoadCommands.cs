using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.InputLoadServices.Dto;

namespace RiceMill.Application.UseCases.InputLoadServices
{
    public interface IInputLoadCommands : IBaseUseCaseCommands
    {
        Result<DtoInputLoad> Create(DtoCreateInputLoad inputLoad);

        Result<DtoInputLoad> Update(DtoUpdateInputLoad inputLoad);
    }

    public class InputLoadCommands : IInputLoadCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public InputLoadCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<DtoInputLoad> Create(DtoCreateInputLoad inputLoad)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Result<DtoInputLoad> Update(DtoUpdateInputLoad inputLoad)
        {
            throw new NotImplementedException();
        }
    }
}