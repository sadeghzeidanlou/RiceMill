using FluentValidation;
using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.UseCases.RiceThreshingServices.Dto
{
    public record DtoUpdateRiceThreshing(Guid Id, DateTime StartTime, DateTime EndTime, float UnbrokenRice, float BrokenRice, float ChickenRice, float Flour, string Description);

    public class DtoUpdateRiceThreshingValidator : AbstractValidator<DtoUpdateRiceThreshing>
    {
        public DtoUpdateRiceThreshingValidator()
        {
            RuleFor(dto => dto.Id)
               .NotEmpty().WithErrorCode(ResultStatusEnum.RiceThreshingIdIsNotValid.ToString());

            RuleFor(dto => dto.StartTime)
                .NotEmpty().WithErrorCode(ResultStatusEnum.RiceThreshingStartTimeIsNotValid.ToString());

            RuleFor(dto => dto.EndTime)
                .NotEmpty().WithErrorCode(ResultStatusEnum.RiceThreshingEndTimeIsNotValid.ToString());

            RuleFor(dto => dto.UnbrokenRice)
                .Must(ubr => ubr > -1).WithErrorCode(ResultStatusEnum.RiceThreshingUnbrokenRiceIsNotValid.ToString());

            RuleFor(dto => dto.BrokenRice)
                .Must(br => br > -1).WithErrorCode(ResultStatusEnum.RiceThreshingBrokenRiceIsNotValid.ToString());

            RuleFor(dto => dto.ChickenRice)
                .Must(m => m > -1).WithErrorCode(ResultStatusEnum.RiceThreshingChickenRiceIsNotValid.ToString());

            RuleFor(dto => dto.Flour)
                .Must(f => f > -1).WithErrorCode(ResultStatusEnum.RiceThreshingFlourIsNotValid.ToString());

            RuleFor(dto => dto.Description)
                .MaximumLength(200).WithErrorCode(ResultStatusEnum.RiceThreshingDescriptionLengthIsNotValid.ToString());
        }
    }
}