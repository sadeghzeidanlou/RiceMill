using FluentValidation;
using RiceMill.Application.Common.Models.Enums;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.VillageServices.Dto
{
    public record DtoUpdateVillage(Guid Id, string Title);

    public class DtoUpdateVillageValidator : AbstractValidator<DtoUpdateVillage>
    {
        public DtoUpdateVillageValidator()
        {
            RuleFor(dto => dto.Id)
                .Must((id) => id.IsNotNullOrEmpty()).WithErrorCode(ResultStatusEnum.VillageIdIsNotValid.ToString());

            RuleFor(dto => dto.Title)
                .NotEmpty().WithErrorCode(ResultStatusEnum.VillageTitleIsNotValid.ToString())
                .MaximumLength(50).WithErrorCode(ResultStatusEnum.VillageTitleLengthIsNotValid.ToString());
        }
    }
}