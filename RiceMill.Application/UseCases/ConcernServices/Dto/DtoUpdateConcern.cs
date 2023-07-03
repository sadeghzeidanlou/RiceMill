using FluentValidation;
using RiceMill.Application.Common.Models.Enums;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.ConcernServices.Dto
{
    public record DtoUpdateConcern(Guid Id) : DtoCreateConcern(string.Empty);

    public class DtoUpdateConcernValidator : AbstractValidator<DtoUpdateConcern>
    {
        public DtoUpdateConcernValidator()
        {
            RuleFor(dto => dto.Id)
                .Must((id) => { return id.IsNotNullOrEmpty(); }).WithErrorCode(ResultStatusEnum.ConcernIdIsNotValid.ToString());


            RuleFor(dto => dto.Title)
                .NotEmpty().WithMessage(ResultStatusEnum.ConcernTitleIsNotValid.ToString())
                .MaximumLength(50).WithMessage(ResultStatusEnum.ConcernTitleLengthIsNotValid.ToString());
        }
    }
}