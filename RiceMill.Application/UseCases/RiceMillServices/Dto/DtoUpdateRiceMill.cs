using FluentValidation;
using RiceMill.Application.Common.Models.Enums;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.RiceMillServices.Dto
{
    public record DtoUpdateRiceMill(Guid Id, string Title, string Address, byte Wage, string Phone, string PostalCode, string Description, Guid? OwnerPersonId);

    public class DtoUpdateRiceMillValidator : AbstractValidator<DtoUpdateRiceMill>
    {
        public DtoUpdateRiceMillValidator()
        {
            RuleFor(dto => dto.Id)
               .Must((id) => id.IsNotNullOrEmpty()).WithErrorCode(ResultStatusEnum.RiceMillIdIsNotValid.ToString());

            RuleFor(dto => dto.Title)
                .NotEmpty().WithErrorCode(ResultStatusEnum.RiceMillTitleIsNotValid.ToString())
                .MaximumLength(60).WithErrorCode(ResultStatusEnum.RiceMillTitleLengthIsNotValid.ToString());

            RuleFor(dto => dto.Address)
                .NotEmpty().WithErrorCode(ResultStatusEnum.RiceMillAddressIsNotValid.ToString())
                .MaximumLength(200).WithErrorCode(ResultStatusEnum.RiceMillAddressLengthIsNotValid.ToString());

            RuleFor(dto => dto.Wage)
                .Must((model, height) => model.Wage >= 0 && model.Wage < byte.MaxValue).WithErrorCode(ResultStatusEnum.RiceMillWageIsNotValid.ToString());

            RuleFor(dto => dto.Phone)
                .MaximumLength(11).WithErrorCode(ResultStatusEnum.RiceMillPhoneLengthIsNotValid.ToString())
                .Must((p) => p.IsNullOrEmpty() || p.IsNotNullOrEmpty() && !p.IsAllDigit()).WithErrorCode(ResultStatusEnum.RiceMillPhoneIsNotValid.ToString());

            RuleFor(dto => dto.PostalCode)
                .MaximumLength(10).WithErrorCode(ResultStatusEnum.RiceMillPostalCodeLengthIsNotValid.ToString())
                .Must((p) => p.IsNullOrEmpty() || p.IsNotNullOrEmpty() && !p.IsAllDigit()).WithErrorCode(ResultStatusEnum.RiceMillPostalCodeIsNotValid.ToString());

            RuleFor(dto => dto.Description)
                .MaximumLength(200).WithErrorCode(ResultStatusEnum.RiceMillDescriptionLengthIsNotValid.ToString());

            RuleFor(dto => dto.OwnerPersonId)
                .Must((opi) => opi.IsNullOrEmpty() || opi.HasValue && opi.IsNotNullOrEmpty()).WithErrorCode(ResultStatusEnum.RiceMillOwnerPersonIdIsNotValid.ToString());
        }
    }
}