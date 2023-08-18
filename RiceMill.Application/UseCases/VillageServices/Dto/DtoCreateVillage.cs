using FluentValidation;
using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.UseCases.VillageServices.Dto
{
    public sealed record DtoCreateVillage(string Title, Guid RiceMillId);

    public sealed class DtoCreateVillageValidator : AbstractValidator<DtoCreateVillage>
    {
        public DtoCreateVillageValidator()
        {
            RuleFor(dto => dto.Title)
                .NotEmpty().WithErrorCode(ResultStatusEnum.VillageTitleIsNotValid.ToString())
                .MaximumLength(50).WithErrorCode(ResultStatusEnum.VillageTitleLengthIsNotValid.ToString());

            RuleFor(dto => dto.RiceMillId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.RiceMillIdIsNotValid.ToString());
        }
    }
}