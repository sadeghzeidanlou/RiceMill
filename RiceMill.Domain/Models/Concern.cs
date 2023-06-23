using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    /// <summary>
    /// This class contain all information about a <see cref="Concern"/>
    /// </summary>
    public sealed class Concern : EventBaseModelWithUserAndRiceMill
    {
        /// <summary>
        /// Title of <see cref="Concern"/>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Collection of <see cref="Payment"/> that paid with this <see cref="Concern"/>
        /// </summary>
        public ICollection<Payment> Payments { get; set; }
    }
}