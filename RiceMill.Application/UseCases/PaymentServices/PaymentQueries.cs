using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.PaymentServices.Dto;

namespace RiceMill.Application.UseCases.PaymentServices
{
    public interface IPaymentQueries
    {
        Result<PaginatedList<DtoPayment>> GetAll();
    }

    public class PaymentQueries : IPaymentQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public PaymentQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Result<PaginatedList<DtoPayment>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}