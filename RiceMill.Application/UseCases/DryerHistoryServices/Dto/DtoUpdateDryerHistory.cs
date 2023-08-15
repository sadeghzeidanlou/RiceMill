using FluentValidation;
using RiceMill.Application.Common.Models.Enums;
using Shared.Enums;
using System.Data;

namespace RiceMill.Application.UseCases.DryerHistoryServices.Dto
{
    public record DtoUpdateDryerHistory(Guid Id, DryerOperationEnum Operation, DateTime StartTime, DateTime? StopTime, Guid DryerId, Guid? RiceThreshingId, Guid InputLoadId, short NumberOfBagsInDryer);

    public class DtoUpdateDryerHistoryValidator : AbstractValidator<DtoUpdateDryerHistory>
    {
        public DtoUpdateDryerHistoryValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithErrorCode(ResultStatusEnum.DryerHistoryIdIsNotValid.ToString());

            RuleFor(dto => dto.Operation)
                .IsInEnum().WithErrorCode(ResultStatusEnum.DryerHistoryOperationIsNotValid.ToString());

            RuleFor(dto => dto.StartTime)
                .NotEmpty().NotNull().WithErrorCode(ResultStatusEnum.DryerHistoryStartTimeIsNotValid.ToString());

            RuleFor(dto => new { dto.StopTime, dto.StartTime })
                .Must(st => !st.StopTime.HasValue || st.StopTime.HasValue && st.StartTime < st.StopTime).WithErrorCode(ResultStatusEnum.DryerHistoryStopTimeIsNotValid.ToString());

            RuleFor(dto => dto.DryerId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.DryerIdIsNotValid.ToString());

            RuleFor(dto => dto.RiceThreshingId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.RiceThreshingIdIsNotValid.ToString());

            RuleFor(dto => dto.InputLoadId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.InputLoadIdIsNotValid.ToString());

            RuleFor(dto => dto.NumberOfBagsInDryer)
                .Must(dto => dto > -1).WithErrorCode(ResultStatusEnum.InputLoadNumberOfBagsInDryerIsNotValid.ToString());
        }
    }
}