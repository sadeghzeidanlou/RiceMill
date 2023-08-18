using Shared.Enums;

namespace RiceMill.Application.UseCases.UserActivityServices.Dto
{
    public sealed record DtoUserActivityFilter(string Ip, UserActivityTypeEnum? UserActivityType, EntityTypeEnum? EntityType, ApplicationIdEnum? ApplicationId, string BeforeEdit, string AfterEdit, DateTime? CreateTime, Guid? RiceMillId);
}
