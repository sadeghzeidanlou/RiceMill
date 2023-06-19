using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.UseCases.Base;
using RiceMill.Application.UseCases.Payment.Dto;

namespace RiceMill.Application.UseCases.Payment
{
    public interface IPaymentQueries : IBaseUseCaseQueries
    {
        Task<Result<DtoPayment>> GetAsync(int id);

        Task<Result<List<DtoPayment>>> GetAllAsync();
    }

    public class PaymentQueries : IPaymentQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public PaymentQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoPayment>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<DtoPayment>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}