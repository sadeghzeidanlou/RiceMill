using RiceMill.Domain.Models.BaseModels;
using Shared.Enums;

namespace RiceMill.Domain.Models
{
    /// <summary>
    /// This class contain all information about a <see cref="DryerHistory"/>
    /// </summary>
    public sealed class DryerHistory : EventBaseModelWithUserAndRiceMill
    {
        /// <summary>
        /// Type of operation <see cref="DryerOperationEnum"/> that operated on a <see cref="Dryer"/>
        /// </summary>
        public DryerOperationEnum Operation { get; set; }

        /// <summary>
        /// Time of start this operation <see cref="Operation"/>
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Time of end this operation <see cref="Operation"/>
        /// </summary>
        public DateTime? StopTime { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="Dryer"/> that <see cref="DryerHistory"/> operated on that
        /// </summary>
        public int DryerId { get; set; }

        /// <summary>
        /// This property Contain <see cref="Dryer"/> detail in this class that <see cref="DryerHistory"/> operated on that
        /// </summary>
        public Dryer Dryer { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="RiceThreshing"/> that <see cref="DryerHistory"/> operated on that
        /// </summary>
        public int RiceThreshingId { get; set; }

        /// <summary>
        /// This property Contain <see cref="RiceThreshing"/> detail in this class that <see cref="DryerHistory"/> operated on that
        /// </summary>
        public RiceThreshing RiceThreshing { get; set; }

        /// <summary>
        /// Collection of <see cref="InputLoad"/> that used in the <see cref="DryerHistory"/>
        /// </summary>
        public ICollection<InputLoad> InputLoads { get; set; }
    }
}