using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.Base;
using RiceMill.Application.UseCases.Payment.Dto;

namespace RiceMill.Application.UseCases.Payment
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

        public Task<Result<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<DtoPayment>> UpdateAsync(DtoUpdatePayment Payment)
        {
            throw new NotImplementedException();
        }
    }
}