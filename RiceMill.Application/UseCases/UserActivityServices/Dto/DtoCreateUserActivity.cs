using FluentValidation;
using RiceMill.Application.Common.Models.Enums;
using Shared.Enums;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.UserActivityServices.Dto
{
    public sealed record DtoCreateUserActivity(Guid UserId, string Ip, UserActivityTypeEnum UserActivityType, EntityTypeEnum EntityType, ApplicationIdEnum ApplicationId, string BeforeEdit, string AfterEdit, Guid? RiceMillId);

    public sealed class DtoCreateUserActivityValidator : AbstractValidator<DtoCreateUserActivity>
    {
        public DtoCreateUserActivityValidator()
        {
            RuleFor(dto => dto.UserId)
                .NotEmpty().WithErrorCode(ResultStatusEnum.UserActivityUserIdIsNotValid.ToString());

            RuleFor(dto => dto.Ip)
                .NotEmpty().WithErrorCode(ResultStatusEnum.UserActivityIpIsNotValid.ToString());

            RuleFor(dto => dto.UserActivityType)
                .IsInEnum().WithErrorCode(ResultStatusEnum.UserActivityUserActivityTypeIsNotValid.ToString());

            RuleFor(dto => dto.EntityType)
                .IsInEnum().WithErrorCode(ResultStatusEnum.UserActivityEntityTypeIsNotValid.ToString());

            RuleFor(dto => dto.ApplicationId)
                .IsInEnum().WithErrorCode(ResultStatusEnum.UserActivityApplicationIdIsNotValid.ToString());

            RuleFor(dto => dto.RiceMillId)
                .Must((rm) => rm.IsNullOrEmpty() || rm.HasValue && rm.IsNotNullOrEmpty()).WithErrorCode(ResultStatusEnum.RiceMillIdIsNotValid.ToString());
        }
    }
}