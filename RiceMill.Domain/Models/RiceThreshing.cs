using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    /// <summary>
    /// This class contain all information about a <see cref="RiceThreshing"/>
    /// </summary>
    public sealed class RiceThreshing : EventBaseModelWithUserAndRiceMill
    {
        /// <summary>
        /// Time of start <see cref="RiceThreshing"/>
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Time of end <see cref="RiceThreshing"/>
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// How much Unbroken Rice generated in this <see cref="RiceThreshing"/>
        /// </summary>
        public float UnbrokenRice { get; set; }

        /// <summary>
        /// How much Broken Rice generated in this <see cref="RiceThreshing"/>
        /// </summary>
        public float BrokenRice { get; set; }

        /// <summary>
        /// How much Chicken Rice generated in this <see cref="RiceThreshing"/>
        /// </summary>
        public float ChickenRice { get; set; }

        /// <summary>
        /// How much Flour generated in this <see cref="RiceThreshing"/>
        /// </summary>
        public float Flour { get; set; }

        /// <summary>
        /// Description of this <see cref="RiceThreshing"/>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Determine this <see cref="RiceThreshing"/> was <see cref="Delivery"/>
        /// </summary>
        public bool IsDelivered { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="RiceThreshing"/> that determine <see cref="Income"/> info from this <see cref="RiceThreshing"/>
        /// </summary>
        public Guid IncomeId { get; set; }

        /// <summary>
        /// This property Contain <see cref="Income"/> detail in this class that show <see cref="Income"/> info from this <see cref="RiceThreshing"/>
        /// </summary>
        public Income Income { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="InputLoad"/> that <see cref="RiceThreshing"/> operated on that
        /// </summary>
        public Guid InputLoadId { get; set; }

        /// <summary>
        /// This property Contain <see cref="InputLoad"/> detail in this class that <see cref="RiceThreshing"/> operated on that
        /// </summary>
        public InputLoad InputLoad { get; set; }

        ///// <summary>
        ///// Collection of <see cref="DryerHistory"/> for this <see cref="RiceThreshing"/>
        ///// </summary>
        //public ICollection<DryerHistory> DryerHistories { get; set; }

        ///// <summary>
        ///// Collection of <see cref="DeliveryRiceThreshing"/> that delivered in this <see cref="RiceThreshing"/>
        ///// </summary>
        //public ICollection<DeliveryRiceThreshing> DeliveryRiceThreshings { get; set; }
    }
}