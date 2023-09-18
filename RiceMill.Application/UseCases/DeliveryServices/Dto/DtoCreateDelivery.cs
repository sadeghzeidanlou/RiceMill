using FluentValidation;
using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.UseCases.DeliveryServices.Dto
{
    public sealed record DtoCreateDelivery(DateTime DeliveryTime, float UnbrokenRice, float BrokenRice, float ChickenRice, float Flour, string Description, Guid DelivererPersonId, Guid ReceiverPersonId, Guid CarrierPersonId, Guid VehicleId, Guid RiceThreshingId, Guid RiceMillId);

    public sealed class DtoCreateDeliveryValidator : AbstractValidator<DtoCreateDelivery>
    {
        public DtoCreateDeliveryValidator()
        {
            RuleFor(dto => dto.DeliveryTime)
                .NotEmpty().WithErrorCode(ResultStatusEnum.DeliveryDeliveryTimeIsNotValid.ToString())
                .LessThanOrEqualTo(DateTime.Now).WithErrorCode(ResultStatusEnum.DeliveryDeliveryTimeIsNotValid.ToString());

            RuleFor(dto => dto.UnbrokenRice)
                .Must(ubr => ubr > -1).WithErrorCode(ResultStatusEnum.DeliveryUnbrokenRiceIsNotValid.ToString());

            RuleFor(dto => dto.BrokenRice)
                .Must(br => br > -1).WithErrorCode(ResultStatusEnum.DeliveryBrokenRiceIsNotValid.ToString());

            RuleFor(dto => dto.ChickenRice)
                .Must(m => m > -1).WithErrorCode(ResultStatusEnum.DeliveryChickenRiceIsNotValid.ToString());

            RuleFor(dto => dto.Flour)
                .Must(f => f > -1).WithErrorCode(ResultStatusEnum.DeliveryFlourIsNotValid.ToString());

            RuleFor(dto => dto.Description)
                .MaximumLength(200).WithErrorCode(ResultStatusEnum.DeliveryDescriptionLengthIsNotValid.ToString());

            RuleFor(dto => dto.DelivererPersonId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.DeliveryDelivererPersonIdIsNotValid.ToString());

            RuleFor(dto => dto.ReceiverPersonId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.DeliveryReceiverPersonIdIsNotValid.ToString());

            RuleFor(dto => dto.CarrierPersonId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.DeliveryCarrierPersonIdIsNotValid.ToString());

            RuleFor(dto => dto.VehicleId)
             .NotEmpty().WithErrorCode(ResultStatusEnum.VehicleIdIsNotValid.ToString());

            RuleFor(dto => dto.RiceThreshingId)
             .NotEmpty().WithErrorCode(ResultStatusEnum.RiceThreshingIdIsNotValid.ToString());

            RuleFor(dto => dto.RiceMillId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.RiceMillIdIsNotValid.ToString());
        }
    }
}