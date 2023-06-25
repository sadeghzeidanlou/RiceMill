using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    /// <summary>
    /// This class contain all information about a <see cref="Income"/>
    /// </summary>
    public sealed class Income : EventBaseModelWithUserAndRiceMill
    {
        /// <summary>
        /// Time of earn this <see cref="Income"/>
        /// </summary>
        public DateTime IncomeTime { get; set; }

        /// <summary>
        /// Description of this <see cref="Income"/>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// How much Unbroken Rice earn in this <see cref="Income"/>
        /// </summary>
        public float UnbrokenRice { get; set; }

        /// <summary>
        /// How much Broken Rice earn in this <see cref="Income"/>
        /// </summary>
        public float BrokenRice { get; set; }

        /// <summary>
        /// How much Flour earn in this <see cref="Income"/>
        /// </summary>
        public float Flour { get; set; }

        /// <summary>
        /// This property Contain <see cref="RiceThreshing"/> detail in this class that determine this earned from which <see cref="RiceThreshing"/>
        /// </summary>
        public RiceThreshing RiceThreshing { get; set; }
    }
}