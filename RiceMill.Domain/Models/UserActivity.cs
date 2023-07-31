using RiceMill.Domain.Models.BaseModels;
using Shared.Enums;

namespace RiceMill.Domain.Models
{
    /// <summary>
    /// This class contain all information about a <see cref="UserActivity"/>
    /// </summary>
    public sealed class UserActivity : EventBaseModel
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

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="RiceMill"/> that determine this <see cref="User"/> is member of which <see cref="RiceMill"/>
        /// </summary>
        public Guid? RiceMillId { get; set; }

        /// <summary>
        /// This property Contain <see cref="RiceMill"/> detail in this class
        /// </summary>
        public RiceMill RiceMill { get; set; }

        /// <summary>
        /// This property used for reference navigation between any class and User
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// This property Contain <see cref="User"/> detail in any other classes that have relation with <see cref="User"/>
        /// </summary>
        public User User { get; set; }
    }
}