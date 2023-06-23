using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    /// <summary>
    /// This class contain detail about <see cref="Dryer"/> that exist in <see cref="RiceMill"/>
    /// </summary>
    public sealed class Dryer : EventBaseModelWithUserAndRiceMill
    {
        /// <summary>
        /// Title of <see cref="Dryer"/>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Collection of <see cref="DryerHistory"/> that operated on this <see cref="Dryer"/>
        /// </summary>
        public ICollection<DryerHistory> DryerHistories { get; set; }
    }
}