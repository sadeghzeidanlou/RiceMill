using FluentValidation;
using RiceMill.Application.Common.Models.Enums;
using Shared.Enums;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.VehicleServices.Dto
{
    public sealed record DtoUpdateVehicle(Guid Id, string Plate, string Description, VehicleTypeEnum VehicleType, Guid OwnerPersonId);

    public sealed class DtoUpdateVehicleValidator : AbstractValidator<DtoUpdateVehicle>
    {
        public DtoUpdateVehicleValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithErrorCode(ResultStatusEnum.VehicleIdIsNotValid.ToString());

            RuleFor(dto => dto.Plate)
                .NotEmpty().WithErrorCode(ResultStatusEnum.VehiclePlateIsNotValid.ToString())
                .Must((vehicle, plate) =>
                {
                    return vehicle.VehicleType == VehicleTypeEnum.Motorcycle ? plate.IsMotorcyclePlate() : plate.IsGeneralPlate();
                }).WithErrorCode(ResultStatusEnum.VehiclePlateIsNotValid.ToString());

            RuleFor(dto => dto.Description)
                .MaximumLength(200).WithErrorCode(ResultStatusEnum.VehicleDescriptionLengthIsNotValid.ToString());

            RuleFor(dto => dto.VehicleType)
                .IsInEnum().WithErrorCode(ResultStatusEnum.VehicleVehicleTypeIsNotValid.ToString());

            RuleFor(dto => dto.OwnerPersonId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.VehicleOwnerPersonIdIsNotValid.ToString());
        }
    }
}