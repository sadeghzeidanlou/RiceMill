using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.IncomeServices.Dto;

namespace RiceMill.Ui.Services.UseCases.IncomeServices
{
    internal interface IIncomeServices
    {
        Task<Result<PaginatedList<DtoIncome>>> Get(DtoIncomeFilter filter);

        Task<Result<DtoIncome>> Update(DtoUpdateIncome dtoUpdate);

        Task<Result<DtoIncome>> Add(DtoCreateIncome dtoCreate);

        Task<Result<bool>> Delete(Guid id);
    }
}