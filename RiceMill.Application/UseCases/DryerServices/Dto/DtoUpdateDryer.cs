using FluentValidation;
using RiceMill.Application.Common.Models.Enums;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.DryerServices.Dto
{
    public record DtoUpdateDryer(Guid Id, string Title);

    public class DtoUpdateDryerValidator : AbstractValidator<DtoUpdateDryer>
    {
        public DtoUpdateDryerValidator()
        {
            RuleFor(dto => dto.Id)
                .Must((id) => id.IsNotNullOrEmpty()).WithErrorCode(ResultStatusEnum.DryerIdIsNotValid.ToString());

            RuleFor(dto => dto.Title)
                .NotEmpty().WithErrorCode(ResultStatusEnum.DryerTitleIsNotValid.ToString())
                .MaximumLength(30).WithErrorCode(ResultStatusEnum.DryerTitleLengthIsNotValid.ToString());
        }
    }
}