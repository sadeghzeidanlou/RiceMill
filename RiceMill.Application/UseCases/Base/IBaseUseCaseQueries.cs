using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.Base
{
    public interface IBaseUseCaseQueries
    {
        Task<Result<int>> GetCountAsync();
    }
}