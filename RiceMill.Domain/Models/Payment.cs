using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    /// <summary>
    /// This class contain all information about a <see cref="Payment"/>
    /// </summary>
    public sealed class Payment : EventBaseModelWithUserAndRiceMill
    {
        /// <summary>
        /// Time of payment this <see cref="Payment"/>
        /// </summary>
        public DateTime PaymentTime { get; set; }

        /// <summary>
        /// Description of this <see cref="Payment"/>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// How much Unbroken Rice paid in this <see cref="Payment"/>
        /// </summary>
        public float UnbrokenRice { get; set; }

        /// <summary>
        /// How much Broken Rice paid in this <see cref="Payment"/>
        /// </summary>
        public float BrokenRice { get; set; }

        /// <summary>
        /// How much Flour paid in this <see cref="Payment"/>
        /// </summary>
        public float Flour { get; set; }

        /// <summary>
        /// How much Money paid in this <see cref="Payment"/>
        /// </summary>
        public int Money { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="Person"/> that paid to this <see cref="Payment"/>
        /// </summary>
        public Guid PaidPersonId { get; set; }

        /// <summary>
        /// This property Contain <see cref="Person"/> detail in this class that paid to this <see cref="Payment"/>
        /// </summary>
        public Person PaidPerson { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="Concern"/> of this <see cref="Payment"/>
        /// </summary>
        public Guid ConcernId { get; set; }

        /// <summary>
        /// This property Contain <see cref="Concern"/> detail in this class that paid to this <see cref="Payment"/>
        /// </summary>
        public Concern Concern { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="InputLoad"/> that paid to this <see cref="Payment"/>
        /// </summary>
        public Guid InputLoadId { get; set; }

        /// <summary>
        /// This property Contain <see cref="InputLoad"/> detail in this class that paid to this <see cref="Payment"/>
        /// </summary>
        public InputLoad InputLoad { get; set; }
    }
}