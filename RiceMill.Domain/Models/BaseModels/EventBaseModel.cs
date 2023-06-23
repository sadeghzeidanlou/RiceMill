namespace RiceMill.Domain.Models.BaseModels
{
    public class EventBaseModel
    {
        /// <summary>
        /// Identify column of any classes
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Time of an object was created
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Time of an object was updated
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// Time of an object was deleted, this can be <see langword="null"/> when <see cref="IsDeleted"/> is false,
        /// but if <see cref="IsDeleted"/> is true this should have value
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// Determine a record was deleted or not
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}