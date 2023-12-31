﻿using FluentValidation;
using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.UseCases.IncomeServices.Dto
{
    public sealed record DtoCreateIncome(DateTime IncomeTime, float UnbrokenRice, float BrokenRice, float Flour, string Description, Guid RiceMillId);

    public sealed class DtoCreateIncomeValidator : AbstractValidator<DtoCreateIncome>
    {
        public DtoCreateIncomeValidator()
        {
            RuleFor(dto => dto.IncomeTime)
                .NotEmpty().WithErrorCode(ResultStatusEnum.IncomeIdIsNotValid.ToString())
                .GreaterThan(DateTime.Now).WithErrorCode(ResultStatusEnum.IncomeIncomeTimeIsNotValid.ToString());

            RuleFor(dto => dto.UnbrokenRice)
                .Must(ubr => ubr > -1).WithErrorCode(ResultStatusEnum.IncomeUnbrokenRiceIsNotValid.ToString());

            RuleFor(dto => dto.BrokenRice)
                .Must(br => br > -1).WithErrorCode(ResultStatusEnum.IncomeBrokenRiceIsNotValid.ToString());

            RuleFor(dto => dto.Flour)
                .Must(f => f > -1).WithErrorCode(ResultStatusEnum.IncomeFlourIsNotValid.ToString());

            RuleFor(dto => dto.Description)
                .MaximumLength(200).WithErrorCode(ResultStatusEnum.IncomeDescriptionLengthIsNotValid.ToString());

            RuleFor(dto => dto.RiceMillId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.RiceMillIdIsNotValid.ToString());
        }
    }
}