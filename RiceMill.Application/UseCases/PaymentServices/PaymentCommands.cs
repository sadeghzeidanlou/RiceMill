using Mapster;
using RiceMill.Application.Common.ExtensionMethods;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.BaseServices;
using RiceMill.Application.UseCases.PaymentServices.Dto;
using RiceMill.Application.UseCases.UserActivityServices;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Domain.Models;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Net;

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
        private readonly ICurrentRequestService _currentRequestService;
        private readonly ICacheService _cacheService;
        private readonly IUserActivityCommands _userActivityCommands;
        private readonly EntityTypeEnum _Key = EntityTypeEnum.Payments;

        public PaymentCommands(IApplicationDbContext applicationDbContext, ICurrentRequestService currentRequestService, ICacheService cacheService, IUserActivityCommands userActivityCommands)
        {
            _applicationDbContext = applicationDbContext;
            _currentRequestService = currentRequestService;
            _cacheService = cacheService;
            _userActivityCommands = userActivityCommands;
        }

        public Result<DtoPayment> Create(DtoCreatePayment createPayment)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoPayment>.Forbidden();

            var validationResult = createPayment.Validate();
            if (!validationResult.IsValid)
                return Result<DtoPayment>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var payment = createPayment.Adapt<Payment>();
            payment.UserId = _currentRequestService.UserId;
            _applicationDbContext.Payments.Add(payment);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.New, _Key, string.Empty, payment.SerializeObject(), payment.RiceMillId);
            _cacheService.Maintain(_Key, payment);
            return Result<DtoPayment>.Success(payment.Adapt<DtoPayment>());
        }

        public Result<DtoPayment> Update(DtoUpdatePayment updatePayment)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<DtoPayment>.Forbidden();

            var validationResult = updatePayment.Validate();
            if (!validationResult.IsValid)
                return Result<DtoPayment>.Failure(validationResult.Errors.GetErrorEnums(), HttpStatusCode.BadRequest);

            var payment = GetPaymentById(updatePayment.Id);
            if (payment == null)
                return Result<DtoPayment>.Failure(new Error(ResultStatusEnum.PaymentNotFound), HttpStatusCode.NotFound);

            var beforeEdit = payment.SerializeObject();
            payment = updatePayment.Adapt(payment);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Edit, _Key, beforeEdit, payment.SerializeObject(), payment.RiceMillId);
            _cacheService.Maintain(_Key, payment);
            return Result<DtoPayment>.Success(payment.Adapt<DtoPayment>());
        }

        public Result<bool> Delete(Guid id)
        {
            if (_currentRequestService.HaveNotAccessToWrite)
                return Result<bool>.Forbidden();

            var payment = GetPaymentById(id);
            if (payment == null)
                return Result<bool>.Failure(new Error(ResultStatusEnum.PaymentNotFound), HttpStatusCode.NotFound);

            var beforeEdit = payment.SerializeObject();
            _applicationDbContext.Payments.Remove(payment);
            _applicationDbContext.SaveChanges();
            _userActivityCommands.CreateGeneral(UserActivityTypeEnum.Delete, _Key, beforeEdit, payment.SerializeObject(), payment.RiceMillId);
            _cacheService.Maintain(_Key, payment);


            var validateCreateUserResult = ValidateUser(updateUser.Adapt<DtoCreateUser>());
            if (validateCreateUserResult != null)
                return validateCreateUserResult;


            return Result<bool>.Success(true);
        }

        private Payment GetPaymentById(Guid id) => _applicationDbContext.Payments.FirstOrDefault(c => c.Id == id);

        private Result<DtoPayment> ValidatePyment(DtoCreatePayment payment)
        {
            if (!_cacheService.GetConcerns().Any(c => c.Id.Equals(payment.PaidPersonId)))
                return Result<DtoPayment>.Failure(new Error(ResultStatusEnum.PersonNotFound), HttpStatusCode.NotFound);

            if (!_cacheService.GetRiceMills().Any(rm => rm.Id.Equals(payment.RiceMillId)))
                return Result<DtoPayment>.Failure(new Error(ResultStatusEnum.RiceMillNotFound), HttpStatusCode.NotFound);

            if (!_cacheService.GetConcerns().Any(c => c.Id.Equals(payment.ConcernId)))
                return Result<DtoPayment>.Failure(new Error(ResultStatusEnum.ConcernNotFound), HttpStatusCode.NotFound);

            return null;
        }
    }
}