using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.RiceThreshingServices.Dto;

namespace RiceMill.Ui.Services.UseCases.RiceThreshingServices
{
    internal interface IRiceThreshingServices
    {
        Task<Result<PaginatedList<DtoRiceThreshing>>> Get(DtoRiceThreshingFilter filter);

        Task<Result<DtoRiceThreshing>> Update(DtoUpdateRiceThreshing dtoUpdate);

        Task<Result<DtoRiceThreshing>> Add(DtoCreateRiceThreshing dtoCreate);

        Task<Result<bool>> Delete(Guid id);
    }
}