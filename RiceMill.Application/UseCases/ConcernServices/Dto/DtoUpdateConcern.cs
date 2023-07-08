using FluentValidation;
using RiceMill.Application.Common.Models.Enums;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.ConcernServices.Dto
{
    public record DtoUpdateConcern(Guid Id, string Title);

    public class DtoUpdateConcernValidator : AbstractValidator<DtoUpdateConcern>
    {
        public DtoUpdateConcernValidator()
        {
            RuleFor(dto => dto.Id)
                .Must((id) => id.IsNotNullOrEmpty()).WithErrorCode(ResultStatusEnum.ConcernIdIsNotValid.ToString());

            RuleFor(dto => dto.Title)
                .NotEmpty().WithErrorCode(ResultStatusEnum.ConcernTitleIsNotValid.ToString())
                .MaximumLength(50).WithErrorCode(ResultStatusEnum.ConcernTitleLengthIsNotValid.ToString());
        }
    }
}