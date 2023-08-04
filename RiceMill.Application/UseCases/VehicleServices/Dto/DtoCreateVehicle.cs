﻿using FluentValidation;
using RiceMill.Application.Common.Models.Enums;
using Shared.Enums;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.VehicleServices.Dto
{
    public record DtoCreateVehicle(string Plate, string Description, VehicleTypeEnum VehicleType, Guid OwnerPersonId, Guid RiceMillId);

    public class DtoCreateVehicleValidator : AbstractValidator<DtoCreateVehicle>
    {
        public DtoCreateVehicleValidator()
        {
            RuleFor(dto => dto.Plate)
                .NotEmpty().WithErrorCode(ResultStatusEnum.VehiclePlateIsNotValid.ToString())
                .MinimumLength(8).WithErrorCode(ResultStatusEnum.VehiclePlateMinimumLengthIsNotValid.ToString())
                .MaximumLength(8).WithErrorCode(ResultStatusEnum.VehiclePlateMaximumLengthIsNotValid.ToString())
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

            RuleFor(dto => dto.RiceMillId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.VehicleRiceMillIdIsNotValid.ToString());
        }
    }
}