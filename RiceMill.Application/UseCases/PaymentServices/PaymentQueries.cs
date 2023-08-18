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

    public sealed class PaymentQueries : IPaymentQueries
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
                return payments.Where(p => false);

            if (filter.Id.IsNotNullOrEmpty())
                payments = payments.Where(p => p.Id.Equals(filter.Id.Value));

            if (filter.Ids.IsCollectionNotNullOrEmpty())
                payments = payments.Where(p => filter.Ids.Contains(p.Id));

            if (filter.RiceMillId.IsNotNullOrEmpty())
                payments = payments.Where(p => p.RiceMillId.Equals(filter.RiceMillId));

            if (filter.PaymentTimeLower.HasValue)
                payments = payments.Where(p => p.PaymentTime < filter.PaymentTimeLower.Value);

            if (filter.PaymentTime.HasValue)
                payments = payments.Where(p => p.PaymentTime.Equals(filter.PaymentTime.Value));

            if (filter.PaymentTime.HasValue)
                payments = payments.Where(p => p.PaymentTime > filter.PaymentTimeGreater.Value);

            if (filter.Description.IsNotNullOrEmpty())
                payments = payments.Where(p => p.Description.Contains(filter.Description));

            if (filter.UnbrokenRiceLower.HasValue)
                payments = payments.Where(p => p.UnbrokenRice < filter.UnbrokenRiceLower.Value);

            if (filter.UnbrokenRice.HasValue)
                payments = payments.Where(p => p.UnbrokenRice == filter.UnbrokenRice.Value);

            if (filter.UnbrokenRiceGreater.HasValue)
                payments = payments.Where(p => p.UnbrokenRice > filter.UnbrokenRiceGreater.Value);

            if (filter.BrokenRiceLower.HasValue)
                payments = payments.Where(p => p.BrokenRice < filter.BrokenRiceLower.Value);

            if (filter.BrokenRice.HasValue)
                payments = payments.Where(p => p.BrokenRice == filter.BrokenRice.Value);

            if (filter.BrokenRiceGreater.HasValue)
                payments = payments.Where(p => p.BrokenRice > filter.BrokenRiceGreater.Value);

            if (filter.FlourLower.HasValue)
                payments = payments.Where(p => p.Flour < filter.FlourLower.Value);

            if (filter.Flour.HasValue)
                payments = payments.Where(p => p.Flour == filter.Flour.Value);

            if (filter.FlourGreater.HasValue)
                payments = payments.Where(p => p.Flour > filter.FlourGreater.Value);

            if (filter.MoneyLower.HasValue)
                payments = payments.Where(p => p.Money < filter.MoneyLower.Value);

            if (filter.Money.HasValue)
                payments = payments.Where(p => p.Money == filter.Money.Value);

            if (filter.MoneyGreater.HasValue)
                payments = payments.Where(p => p.Money > filter.MoneyGreater.Value);

            if (filter.PaidPersonId.IsNotNullOrEmpty())
                payments = payments.Where(p => p.PaidPersonId.Equals(filter.PaidPersonId));

            if (filter.ConcernId.IsNotNullOrEmpty())
                payments = payments.Where(p => p.ConcernId.Equals(filter.ConcernId));

            if (filter.InputLoadId.IsNotNullOrEmpty())
                payments = payments.Where(p => p.InputLoadId.Equals(filter.InputLoadId));

            return payments;
        }
    }
}