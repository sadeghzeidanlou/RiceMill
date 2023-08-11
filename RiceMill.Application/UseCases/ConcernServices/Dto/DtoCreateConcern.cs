using FluentValidation;
using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.UseCases.ConcernServices.Dto
{
    public record DtoCreateConcern(string Title, Guid RiceMillId);

    public class DtoCreateConcernValidator : AbstractValidator<DtoCreateConcern>
    {
        public DtoCreateConcernValidator()
        {
            RuleFor(dto => dto.Title)
                .NotEmpty().WithErrorCode(ResultStatusEnum.ConcernTitleIsNotValid.ToString())
                .MaximumLength(50).WithErrorCode(ResultStatusEnum.ConcernTitleLengthIsNotValid.ToString());

            RuleFor(dto => dto.RiceMillId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.RiceMillIdIsNotValid.ToString());
        }
    }
}