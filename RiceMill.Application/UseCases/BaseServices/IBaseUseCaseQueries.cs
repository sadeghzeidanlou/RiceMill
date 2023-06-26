using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.BaseServices
{
    public interface IBaseUseCaseQueries
    {
        Task<Result<int>> GetCountAsync();
    }
}