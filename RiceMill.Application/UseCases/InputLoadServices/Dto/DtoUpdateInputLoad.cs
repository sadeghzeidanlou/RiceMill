using FluentValidation;
using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.UseCases.InputLoadServices.Dto
{
    public sealed record DtoUpdateInputLoad(Guid Id, short NumberOfBags, string Description, DateTime ReceiveTime, Guid VillageId, Guid DelivererPersonId, Guid ReceiverPersonId, Guid CarrierPersonId, Guid OwnerPersonId, Guid VehicleId);

    public sealed class DtoUpdateInputLoadValidator : AbstractValidator<DtoUpdateInputLoad>
    {
        public DtoUpdateInputLoadValidator()
        {
            RuleFor(dto => dto.Id)
               .NotEmpty().WithErrorCode(ResultStatusEnum.InputLoadIdIsNotValid.ToString());

            RuleFor(dto => dto.NumberOfBags)
              .Must(ubr => ubr > -1).WithErrorCode(ResultStatusEnum.InputLoadNumberOfBagsIsNotValid.ToString());

            RuleFor(dto => dto.Description)
                .MaximumLength(200).WithErrorCode(ResultStatusEnum.InputLoadDescriptionLengthIsNotValid.ToString());

            RuleFor(dto => dto.ReceiveTime)
                .NotEmpty().WithErrorCode(ResultStatusEnum.InputLoadReceiveTimeIsNotValid.ToString());

            RuleFor(dto => dto.VillageId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.InputLoadVillageIdIsNotValid.ToString());

            RuleFor(dto => dto.DelivererPersonId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.InputLoadDelivererPersonIdIsNotValid.ToString());

            RuleFor(dto => dto.ReceiverPersonId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.InputLoadReceiverPersonIdIsNotValid.ToString());

            RuleFor(dto => dto.CarrierPersonId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.InputLoadCarrierPersonIdIsNotValid.ToString());

            RuleFor(dto => dto.OwnerPersonId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.InputLoadOwnerPersonIdIsNotValid.ToString());

            RuleFor(dto => dto.VehicleId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.VehicleIdIsNotValid.ToString());
        }
    }
}