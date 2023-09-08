using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.PersonServices.Dto;

namespace RiceMill.Ui.Services.UseCases.PersonServices
{
    internal interface IPersonServices
    {
        Task<Result<PaginatedList<DtoPerson>>> Get(DtoPersonFilter filter);

        Task<Result<DtoPerson>> Update(DtoUpdatePerson dtoUpdate);

        Task<Result<DtoPerson>> Add(DtoCreatePerson dtoCreate);

        Task<Result<bool>> Delete(Guid id);
    }
}