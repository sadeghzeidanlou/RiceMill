using FluentValidation;
using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.UseCases.VillageServices.Dto
{
    public record DtoCreateVillage(string Title, Guid RiceMillId);

    public class DtoCreateVillageValidator : AbstractValidator<DtoCreateVillage>
    {
        public DtoCreateVillageValidator()
        {
            RuleFor(dto => dto.Title)
                .NotEmpty().WithErrorCode(ResultStatusEnum.VillageTitleIsNotValid.ToString())
                .MaximumLength(50).WithErrorCode(ResultStatusEnum.VillageTitleLengthIsNotValid.ToString());

            RuleFor(dto => dto.RiceMillId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.VillageRiceMillIdIsNotValid.ToString());
        }
    }
}