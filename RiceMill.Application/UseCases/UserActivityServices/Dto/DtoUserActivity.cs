using RiceMill.Application.UseCases.BaseDto;
using Shared.Enums;

namespace RiceMill.Application.UseCases.UserActivityServices.Dto
{
    public class DtoUserActivity : DtoEventBaseWithUserAndRiceMill
    {
        public string Ip { get; set; }

        public UserActivityTypeEnum UserActivityType { get; set; }

        public EntityTypeEnum EntityType { get; set; }

        public ApplicationIdEnum ApplicationId { get; set; }

        public string BeforeEdit { get; set; }

        public string AfterEdit { get; set; }
    }
}