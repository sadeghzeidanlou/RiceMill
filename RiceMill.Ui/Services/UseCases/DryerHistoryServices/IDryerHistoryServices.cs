using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DryerHistoryServices.Dto;

namespace RiceMill.Ui.Services.UseCases.DryerHistoryServices
{
    internal interface IDryerHistoryServices
    {
        Task<Result<PaginatedList<DtoDryerHistory>>> Get(DtoDryerHistoryFilter filter);

        Task<Result<DtoDryerHistory>> Update(DtoUpdateDryerHistory dtoUpdate);

        Task<Result<DtoDryerHistory>> Add(DtoCreateDryerHistory dtoCreate);

        Task<Result<bool>> Delete(Guid id);
    }
}