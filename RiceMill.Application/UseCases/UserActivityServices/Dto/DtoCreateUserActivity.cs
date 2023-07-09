using RiceMill.Application.UseCases.RiceMillServices.Dto;
using RiceMill.Application.UseCases.UserServices.Dto;
using Shared.Enums;

namespace RiceMill.Application.UseCases.UserActivityServices.Dto
{
    public record DtoCreateUserActivity(Guid UserId, string Ip, UserActivityTypeEnum UserActivityType, EntityTypeEnum EntityType, ApplicationIdEnum ApplicationId, string BeforeEdit, string AfterEdit);
}