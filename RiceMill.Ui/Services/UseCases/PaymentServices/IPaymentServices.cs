using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.PaymentServices.Dto;

namespace RiceMill.Ui.Services.UseCases.PaymentServices
{
    internal interface IPaymentServices
    {
        Task<Result<PaginatedList<DtoPayment>>> Get(DtoPaymentFilter filter);

        Task<Result<DtoPayment>> Update(DtoUpdatePayment dtoUpdate);

        Task<Result<DtoPayment>> Add(DtoCreatePayment dtoCreate);

        Task<Result<bool>> Delete(Guid id);
    }
}