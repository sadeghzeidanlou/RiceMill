using FluentValidation;
using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.UseCases.ConcernServices.Dto
{
    public record DtoUpdateConcern(Guid Id, string Title, Guid UserId, Guid RiceMillId);

    public class DtoUpdateConcernValidator : AbstractValidator<DtoUpdateConcern>
    {
        public DtoUpdateConcernValidator()
        {
            RuleFor(dto => dto.Id)
               .NotEmpty().WithMessage(ResultStatusEnum.ConcernIdIsNotValid.ToString());

            RuleFor(dto => dto.UserId)
               .NotEmpty().WithMessage(ResultStatusEnum.ConcernUserIdIsNotValid.ToString());

            RuleFor(dto => dto.RiceMillId)
               .NotEmpty().WithMessage(ResultStatusEnum.ConcernRiceMillIdIsNotValid.ToString());

            RuleFor(dto => dto.Title)
                .NotEmpty().WithMessage(ResultStatusEnum.ConcernTitleIsNotValid.ToString())
                .MaximumLength(50).WithMessage(ResultStatusEnum.ConcernTitleLengthIsNotValid.ToString());
        }
    }
}