using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.InputLoadServices.Dto;

namespace RiceMill.Application.UseCases.InputLoadServices
{
    public interface IInputLoadCommands : IBaseUseCaseCommands
    {
        Task<Result<DtoInputLoad>> CreateAsync(DtoCreateInputLoad inputLoad);

        Task<Result<DtoInputLoad>> UpdateAsync(DtoUpdateInputLoad inputLoad);
    }

    public class InputLoadCommands : IInputLoadCommands
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public InputLoadCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoInputLoad>> CreateAsync(DtoCreateInputLoad inputLoad)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoInputLoad>> UpdateAsync(DtoUpdateInputLoad inputLoad)
        {
            throw new NotImplementedException();
        }
    }
}