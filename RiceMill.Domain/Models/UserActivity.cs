using RiceMill.Domain.Models.BaseModels;
using Shared.Enums;

namespace RiceMill.Domain.Models
{
    /// <summary>
    /// This class contain all information about a <see cref="UserActivity"/>
    /// </summary>
    public sealed class UserActivity : EventBaseModelWithUserAndRiceMill
    {
        /// <summary>
        /// Ip of <see cref="UserActivity"/>
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// User Activity Type <see cref="UserActivityTypeEnum"/> of <see cref="UserActivity"/>
        /// </summary>
        public UserActivityTypeEnum UserActivityType { get; set; }

        /// <summary>
        /// Entity Type <see cref="EntityTypeEnum"/> of <see cref="UserActivity"/>
        /// </summary>
        public EntityTypeEnum EntityType { get; set; }

        /// <summary>
        /// Application Id <see cref="ApplicationIdEnum"/> of <see cref="UserActivity"/>
        /// </summary>
        public ApplicationIdEnum ApplicationId { get; set; }

        /// <summary>
        /// Object before edit of <see cref="UserActivity"/>
        /// </summary>
        public string BeforeEdit { get; set; }

        /// <summary>
        /// Object after edit of <see cref="UserActivity"/>
        /// </summary>
        public string AfterEdit { get; set; }
    }
}