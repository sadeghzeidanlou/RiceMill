﻿using FluentValidation;
using RiceMill.Application.Common.Models.Enums;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.PaymentServices.Dto
{
    public sealed record DtoCreatePayment(DateTime PaymentTime, float UnbrokenRice, float BrokenRice, float Flour, int Money, string Description, Guid PaidPersonId, Guid ConcernId, Guid? InputLoadId, Guid RiceMillId);

    public sealed class DtoCreatePaymentValidator : AbstractValidator<DtoCreatePayment>
    {
        public DtoCreatePaymentValidator()
        {
            RuleFor(dto => dto.PaymentTime)
                .NotEmpty().WithErrorCode(ResultStatusEnum.PaymentPaymentTimeIsNotValid.ToString());

            RuleFor(dto => dto.UnbrokenRice)
                .Must(ubr => ubr > -1).WithErrorCode(ResultStatusEnum.PaymentUnbrokenRiceIsNotValid.ToString());

            RuleFor(dto => dto.BrokenRice)
                .Must(br => br > -1).WithErrorCode(ResultStatusEnum.PaymentBrokenRiceIsNotValid.ToString());

            RuleFor(dto => dto.Flour)
                .Must(f => f > -1).WithErrorCode(ResultStatusEnum.PaymentFlourIsNotValid.ToString());

            RuleFor(dto => dto.Money)
                .Must(m => m > -1).WithErrorCode(ResultStatusEnum.PaymentMoneyIsNotValid.ToString());

            RuleFor(dto => dto.Description)
                .MaximumLength(200).WithErrorCode(ResultStatusEnum.PaymentDescriptionLengthIsNotValid.ToString());

            RuleFor(dto => dto.PaidPersonId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.PaymentPaidPersonIdIsNotValid.ToString());

            RuleFor(dto => dto.ConcernId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.ConcernIdIsNotValid.ToString());

            RuleFor(dto => dto.InputLoadId)
              .Must(il => il.IsNullOrEmpty() || il.HasValue && il.IsNotNullOrEmpty()).WithErrorCode(ResultStatusEnum.InputLoadIdIsNotValid.ToString());

            RuleFor(dto => dto.RiceMillId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.RiceMillIdIsNotValid.ToString());
        }
    }
}