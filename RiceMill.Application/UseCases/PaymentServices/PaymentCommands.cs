using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.PaymentServices.Dto;

namespace RiceMill.Application.UseCases.PaymentServices
{
    public interface IPaymentCommand : IBaseUseCaseCommands
    {
        Result<DtoPayment> Create(DtoCreatePayment payment);

        Result<DtoPayment> Update(DtoUpdatePayment payment);
    }

    public class PaymentCommands : IPaymentCommand
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public PaymentCommands(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<DtoPayment> Create(DtoCreatePayment payment)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Result<DtoPayment> Update(DtoUpdatePayment payment)
        {
            throw new NotImplementedException();
        }
    }
}