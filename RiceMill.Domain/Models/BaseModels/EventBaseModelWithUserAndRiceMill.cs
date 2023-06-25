namespace RiceMill.Domain.Models.BaseModels
{
    public class EventBaseModelWithUserAndRiceMill : EventBaseModel
    {
        /// <summary>
        /// This property used for reference navigation between any class and RiceMill
        /// </summary>
        public Guid RiceMillId { get; set; }

        /// <summary>
        /// This property Contain <see cref="RiceMill"/> detail in any other classes that have relation with <see cref="RiceMill"/>
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