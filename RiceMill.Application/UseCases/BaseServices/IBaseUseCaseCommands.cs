using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.BaseServices
{
    public interface IBaseUseCaseCommands
    {
        Result<bool> Delete(Guid id);
    }
}