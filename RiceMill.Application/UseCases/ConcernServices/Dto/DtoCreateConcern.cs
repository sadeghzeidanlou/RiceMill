using FluentValidation;
using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.UseCases.ConcernServices.Dto
{
    public record DtoCreateConcern(string Title);

    public class DtoCreateConcernValidator : AbstractValidator<DtoCreateConcern>
    {
        public DtoCreateConcernValidator()
        {
            RuleFor(dto => dto.Title)
                .NotEmpty().WithErrorCode(ResultStatusEnum.ConcernTitleIsNotValid.ToString())
                .MaximumLength(50).WithErrorCode(ResultStatusEnum.ConcernTitleLengthIsNotValid.ToString());
        }
    }
}