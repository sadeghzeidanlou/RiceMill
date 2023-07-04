using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.PaymentServices.Dto;

namespace RiceMill.Application.UseCases.PaymentServices
{
    public interface IPaymentCommand : IBaseUseCaseCommands
    {
        Task<Result<DtoPayment>> CreateAsync(DtoCreatePayment payment);

        Task<Result<DtoPayment>> UpdateAsync(DtoUpdatePayment payment);
    }

    public class PaymentCommands : IPaymentCommand
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public PaymentCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoPayment>> CreateAsync(DtoCreatePayment payment)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteAsync(Guid id, Guid riceMillId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoPayment>> UpdateAsync(DtoUpdatePayment Payment)
        {
            throw new NotImplementedException();
        }
    }
}