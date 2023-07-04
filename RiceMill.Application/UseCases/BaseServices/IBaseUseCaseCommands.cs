using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.BaseServices
{
    public interface IBaseUseCaseCommands
    {
        Task<Result<bool>> DeleteAsync(Guid id, Guid riceMillId);
    }
}