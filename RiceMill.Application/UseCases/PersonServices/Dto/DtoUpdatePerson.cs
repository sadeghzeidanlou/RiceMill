﻿using FluentValidation;
using RiceMill.Application.Common.Models.Enums;
using Shared.Enums;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.PersonServices.Dto
{
    public sealed record DtoUpdatePerson(Guid Id, string Name, string Family, GenderEnum Gender, string MobileNumber, string HomeNumber, NoticesTypeEnum NoticesType, string Address, string FatherName);

    public sealed class DtoUpdatePersonValidator : AbstractValidator<DtoUpdatePerson>
    {
        public DtoUpdatePersonValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithErrorCode(ResultStatusEnum.PersonIdIsNotValid.ToString());

            RuleFor(dto => dto.Name)
                .NotEmpty().WithErrorCode(ResultStatusEnum.PersonNameIsNotValid.ToString())
                .MaximumLength(20).WithErrorCode(ResultStatusEnum.PersonNameLengthIsNotValid.ToString());

            RuleFor(dto => dto.Family)
                .NotEmpty().WithErrorCode(ResultStatusEnum.PersonFamilyIsNotValid.ToString())
                .MaximumLength(20).WithErrorCode(ResultStatusEnum.PersonFamilyLengthIsNotValid.ToString());

            RuleFor(dto => dto.Gender)
                .IsInEnum().WithErrorCode(ResultStatusEnum.PersonGenderIsNotValid.ToString());

            RuleFor(dto => dto.MobileNumber)
                .NotEmpty().WithErrorCode(ResultStatusEnum.PersonMobileNumberIsNotValid.ToString())
                .Must(mn => mn.IsPhoneNumber()).WithErrorCode(ResultStatusEnum.PersonMobileNumberIsNotValid.ToString())
                .MaximumLength(11).WithErrorCode(ResultStatusEnum.PersonMobileNumberLengthIsNotValid.ToString());

            RuleFor(dto => dto.HomeNumber)
                .Must((hn) => (hn.IsNullOrEmpty() || hn.IsNullOrWhiteSpace()) || hn.IsNotNullOrEmpty() && hn.IsPhoneNumber()).WithErrorCode(ResultStatusEnum.PersonHomeNumberIsNotValid.ToString())
                .MaximumLength(11).WithErrorCode(ResultStatusEnum.PersonHomeNumberLengthIsNotValid.ToString());

            RuleFor(dto => dto.NoticesType)
             .IsInEnum().WithErrorCode(ResultStatusEnum.PersonNoticesTypeIsNotValid.ToString());

            RuleFor(dto => dto.Address)
                .NotEmpty().WithErrorCode(ResultStatusEnum.PersonAddressIsNotValid.ToString())
                .MaximumLength(200).WithErrorCode(ResultStatusEnum.PersonAddressLengthIsNotValid.ToString());

            RuleFor(dto => dto.FatherName)
                .NotEmpty().WithErrorCode(ResultStatusEnum.PersonFatherNameIsNotValid.ToString())
                .MaximumLength(20).WithErrorCode(ResultStatusEnum.PersonFatherNameLengthIsNotValid.ToString());
        }
    }
}