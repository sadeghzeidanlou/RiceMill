using FluentValidation;
using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.UseCases.DryerServices.Dto
{
    public sealed record DtoCreateDryer(string Title, Guid RiceMillId);

    public sealed class DtoCreateDryerValidator : AbstractValidator<DtoCreateDryer>
    {
        public DtoCreateDryerValidator()
        {
            RuleFor(dto => dto.Title)
                .NotEmpty().WithErrorCode(ResultStatusEnum.DryerTitleIsNotValid.ToString())
                .MaximumLength(30).WithErrorCode(ResultStatusEnum.DryerTitleLengthIsNotValid.ToString());

            RuleFor(dto => dto.RiceMillId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.RiceMillIdIsNotValid.ToString());
        }
    }
}