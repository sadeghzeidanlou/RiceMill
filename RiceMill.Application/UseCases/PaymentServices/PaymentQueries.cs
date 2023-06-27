using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.PaymentServices.Dto;

namespace RiceMill.Application.UseCases.PaymentServices
{
    public interface IPaymentQueries : IBaseUseCaseQueries
    {
        Task<Result<DtoPayment>> GetAsync(Guid id);

        Task<Result<List<DtoPayment>>> GetAllAsync();
    }

    public class PaymentQueries : IPaymentQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public PaymentQueries(IApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<Result<DtoPayment>> GetAsync(Guid id)
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