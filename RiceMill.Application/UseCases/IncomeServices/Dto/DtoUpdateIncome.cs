using FluentValidation;
using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.UseCases.IncomeServices.Dto
{
    public sealed record DtoUpdateIncome(Guid Id, DateTime IncomeTime, float UnbrokenRice, float BrokenRice, float Flour, string Description);

    public sealed class DtoUpdateIncomeValidator : AbstractValidator<DtoUpdateIncome>
    {
        public DtoUpdateIncomeValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithErrorCode(ResultStatusEnum.IncomeIdIsNotValid.ToString());

            RuleFor(dto => dto.IncomeTime)
                .NotEmpty().WithErrorCode(ResultStatusEnum.IncomeIdIsNotValid.ToString())
                .LessThanOrEqualTo(DateTime.Now).WithErrorCode(ResultStatusEnum.IncomeIncomeTimeIsNotValid.ToString());

            RuleFor(dto => dto.UnbrokenRice)
                .Must(ubr => ubr > -1).WithErrorCode(ResultStatusEnum.IncomeUnbrokenRiceIsNotValid.ToString());

            RuleFor(dto => dto.BrokenRice)
                .Must(br => br > -1).WithErrorCode(ResultStatusEnum.IncomeBrokenRiceIsNotValid.ToString());

            RuleFor(dto => dto.Flour)
                .Must(f => f > -1).WithErrorCode(ResultStatusEnum.IncomeFlourIsNotValid.ToString());

            RuleFor(dto => dto.Description)
                .MaximumLength(200).WithErrorCode(ResultStatusEnum.IncomeDescriptionLengthIsNotValid.ToString());
        }
    }
}