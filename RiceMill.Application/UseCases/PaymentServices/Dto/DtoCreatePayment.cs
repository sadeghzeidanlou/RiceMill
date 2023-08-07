using FluentValidation;
using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.UseCases.PaymentServices.Dto
{
    public record DtoCreatePayment(DateTime PaymentTime, float UnbrokenRice, float BrokenRice, float Flour, int Money, Guid PaidPersonId, Guid ConcernId, Guid? InputLoadId, Guid RiceMillId);
    public class DtoCreatePaymentValidator : AbstractValidator<DtoCreatePayment>
    {
        public DtoCreatePaymentValidator()
        {
            RuleFor(dto => dto.PaymentTime)
                .NotEmpty().WithErrorCode(ResultStatusEnum.DryerTitleIsNotValid.ToString());

            RuleFor(dto => dto.RiceMillId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.DryerRiceMillIdIsNotValid.ToString());
        }
    }
}