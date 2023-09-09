using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.RiceMillServices.Dto;

namespace RiceMill.Ui.Services.UseCases.RiceMillServices
{
    internal interface IRiceMillServices
    {
        Task<Result<PaginatedList<DtoRiceMill>>> Get(DtoRiceMillFilter filter);

        Task<Result<DtoRiceMill>> Update(DtoUpdateRiceMill dtoUpdate);

        Task<Result<DtoRiceMill>> Add(DtoCreateRiceMill dtoCreate);

        Task<Result<bool>> Delete(Guid id);
    }
}