using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.Base
{
    public interface IBaseUseCaseCommands
    {
        Task<Result<bool>> DeleteAsync(int id);

        Task<Result<int>> DeleteAllAsync();
    }
}