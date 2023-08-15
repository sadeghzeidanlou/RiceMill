using FluentValidation;
using RiceMill.Application.Common.Models.Enums;
using Shared.Enums;

namespace RiceMill.Application.UseCases.DryerHistoryServices.Dto
{
    public record DtoCreateDryerHistory(DryerOperationEnum Operation, DateTime StartTime, DateTime? StopTime, Guid DryerId, Guid? RiceThreshingId, Guid InputLoadId, short NumberOfBagsInDryer, Guid? RiceMillId);

    public class DtoCreateDryerHistoryValidator : AbstractValidator<DtoCreateDryerHistory>
    {
        public DtoCreateDryerHistoryValidator()
        {
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
                .Must(dto => dto > 0).WithErrorCode(ResultStatusEnum.InputLoadNumberOfBagsInDryerIsNotValid.ToString());

            RuleFor(dto => dto.RiceMillId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.RiceMillIdIsNotValid.ToString());
        }
    }
}