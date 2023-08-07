using FluentValidation;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.UseCases.BaseDto;
using RiceMill.Application.UseCases.DryerServices.Dto;

namespace RiceMill.Application.UseCases.PaymentServices.Dto
{
    public record DtoCreatePayment(DateTime PaymentTime, float UnbrokenRice, float BrokenRice, float Flour, int Money, Guid PaidPersonId, Guid ConcernId, Guid? InputLoadId, Guid RiceMillId);
    public class DtoCreatePaymentValidator : AbstractValidator<DtoCreatePayment>
    {
        public DtoCreatePaymentValidator()
        {
            RuleFor(dto => dto.Title)
                .NotEmpty().WithErrorCode(ResultStatusEnum.DryerTitleIsNotValid.ToString())
                .MaximumLength(30).WithErrorCode(ResultStatusEnum.DryerTitleLengthIsNotValid.ToString());

            RuleFor(dto => dto.RiceMillId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.DryerRiceMillIdIsNotValid.ToString());
        }
    }
}