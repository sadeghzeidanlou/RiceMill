using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.PaymentServices.Dto;
using RiceMill.Domain.Models;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.PaymentServices
{
    public interface IPaymentQueries
    {
        Result<PaginatedList<DtoPayment>> GetAll(DtoPaymentFilter filter);
    }

    public class PaymentQueries : IPaymentQueries
    {
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;

        public PaymentQueries(ICurrentRequestService currentRequestService, ICacheService cacheService)
        {
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
        }

        public Result<PaginatedList<DtoPayment>> GetAll(DtoPaymentFilter filter)
        {
            var payments = GetFilter(filter);
            PagingInfo.ApplyPaging(filter, out var pageNumber, out var pageSize);
            var result = PaginatedList<DtoPayment>.Create(payments, pageNumber, pageSize);
            return Result<PaginatedList<DtoPayment>>.Success(result);
        }

        private IQueryable<Payment> GetFilter(DtoPaymentFilter filter)
        {
            var payments = _cacheService.GetPayments();
            if (filter == null || (_currentRequestService.IsNotAdmin && filter.RiceMillId.IsNullOrEmpty()))
                return payments.Where(c => false);

            if (filter.Id.IsNotNullOrEmpty())
                payments = payments.Where(c => c.Id.Equals(filter.Id));

            if (filter.RiceMillId.IsNotNullOrEmpty())
                payments = payments.Where(c => c.RiceMillId.Equals(filter.RiceMillId));

            if (filter.PaymentTimeLower.HasValue)
                payments = payments.Where(c => c.PaymentTime < filter.PaymentTimeLower.Value);

            if (filter.PaymentTime.HasValue)
                payments = payments.Where(c => c.PaymentTime.Equals(filter.PaymentTime.Value));

            if (filter.PaymentTime.HasValue)
                payments = payments.Where(c => c.PaymentTime > filter.PaymentTimeGreater.Value);

            if (filter.Description.IsNotNullOrEmpty())
                payments = payments.Where(c => c.Description.Contains(filter.Description));

            if (filter.UnbrokenRiceLower.HasValue)
                payments = payments.Where(c => c.UnbrokenRice < filter.UnbrokenRiceLower.Value);

            if (filter.UnbrokenRice.HasValue)
                payments = payments.Where(c => c.UnbrokenRice == filter.UnbrokenRice.Value);

            if (filter.UnbrokenRiceGreater.HasValue)
                payments = payments.Where(c => c.UnbrokenRice > filter.UnbrokenRiceGreater.Value);

            if (filter.BrokenRiceLower.HasValue)
                payments = payments.Where(c => c.BrokenRice < filter.BrokenRiceLower.Value);

            if (filter.BrokenRice.HasValue)
                payments = payments.Where(c => c.BrokenRice == filter.BrokenRice.Value);

            if (filter.BrokenRiceGreater.HasValue)
                payments = payments.Where(c => c.BrokenRice > filter.BrokenRiceGreater.Value);

            if (filter.FlourLower.HasValue)
                payments = payments.Where(c => c.Flour < filter.FlourLower.Value);

            if (filter.Flour.HasValue)
                payments = payments.Where(c => c.Flour == filter.Flour.Value);

            if (filter.FlourGreater.HasValue)
                payments = payments.Where(c => c.Flour > filter.FlourGreater.Value);

            if (filter.MoneyLower.HasValue)
                payments = payments.Where(c => c.Money < filter.MoneyLower.Value);

            if (filter.Money.HasValue)
                payments = payments.Where(c => c.Money == filter.Money.Value);

            if (filter.MoneyGreater.HasValue)
                payments = payments.Where(c => c.Money > filter.MoneyGreater.Value);

            if (filter.PaidPersonId.IsNotNullOrEmpty())
                payments = payments.Where(c => c.PaidPersonId.Equals(filter.PaidPersonId));

            if (filter.ConcernId.IsNotNullOrEmpty())
                payments = payments.Where(c => c.ConcernId.Equals(filter.ConcernId));

            if (filter.InputLoadId.IsNotNullOrEmpty())
                payments = payments.Where(c => c.InputLoadId.Equals(filter.InputLoadId));

            return payments;
        }
    }
}