using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.ConcernServices.Dto;

namespace RiceMill.Ui.Services.UseCases.ConcernServices
{
    internal interface IConcernServices
    {
        Task<Result<PaginatedList<DtoConcern>>> Get(DtoConcernFilter filter);

        Task<Result<DtoConcern>> Update(DtoUpdateConcern dtoUpdate);

        Task<Result<DtoConcern>> Add(DtoCreateConcern dtoCreate);

        Task<Result<bool>> Delete(Guid id);
    }
}