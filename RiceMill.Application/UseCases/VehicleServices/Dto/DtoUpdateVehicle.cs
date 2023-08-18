using FluentValidation;
using RiceMill.Application.Common.Models.Enums;
using Shared.Enums;

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
                .MaximumLength(8).WithErrorCode(ResultStatusEnum.VehiclePlateMaximumLengthIsNotValid.ToString());

            RuleFor(dto => dto.Description)
                .MaximumLength(200).WithErrorCode(ResultStatusEnum.VehicleDescriptionLengthIsNotValid.ToString());

            RuleFor(dto => dto.VehicleType)
                .IsInEnum().WithErrorCode(ResultStatusEnum.VehicleVehicleTypeIsNotValid.ToString());

            RuleFor(dto => dto.OwnerPersonId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.VehicleOwnerPersonIdIsNotValid.ToString());
        }
    }
}