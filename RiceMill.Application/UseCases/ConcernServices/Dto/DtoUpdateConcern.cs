using FluentValidation;
using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.UseCases.ConcernServices.Dto
{
    public sealed record DtoUpdateConcern(Guid Id, string Title);

    public sealed class DtoUpdateConcernValidator : AbstractValidator<DtoUpdateConcern>
    {
        public DtoUpdateConcernValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithErrorCode(ResultStatusEnum.ConcernIdIsNotValid.ToString());

            RuleFor(dto => dto.Title)
                .NotEmpty().WithErrorCode(ResultStatusEnum.ConcernTitleIsNotValid.ToString())
                .MaximumLength(50).WithErrorCode(ResultStatusEnum.ConcernTitleLengthIsNotValid.ToString());
        }
    }
}