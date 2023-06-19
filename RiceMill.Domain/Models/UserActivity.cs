using RiceMill.Domain.Enums;
using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    public sealed class UserActivity : EventBaseModelWithUserAndRiceMill
    {
        public string Ip { get; set; }

        public UserActivityTypeEnum UserActivityType { get; set; }

        public EntityTypeEnum EntityType { get; set; }

        public ApplicationIdEnum ApplicationId { get; set; }

        public string BeforeEdit { get; set; }

        public string AfterEdit { get; set; }
    }
}