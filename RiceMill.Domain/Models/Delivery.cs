using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    /// <summary>
    /// This class contain all information about a <see cref="Delivery"/>
    /// </summary>
    public sealed class Delivery : EventBaseModelWithUserAndRiceMill
    {
        /// <summary>
        /// How much Unbroken Rice delivered in this <see cref="Delivery"/>
        /// </summary>
        public short UnbrokenRice { get; set; }

        /// <summary>
        /// How much Broken Rice delivered in this <see cref="Delivery"/>
        /// </summary>
        public short BrokenRice { get; set; }

        /// <summary>
        /// How much Chicken Rice delivered in this <see cref="Delivery"/>
        /// </summary>
        public short ChickenRice { get; set; }

        /// <summary>
        /// How much flour delivered in this <see cref="Delivery"/>
        /// </summary>
        public short Flour { get; set; }

        /// <summary>
        /// Time of this <see cref="Delivery"/>
        /// </summary>
        public DateTime DeliveryTime { get; set; }

        /// <summary>
        /// Description of this <see cref="Delivery"/>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="Person"/> that deliverer this <see cref="Delivery"/>
        /// </summary>
        public Guid DelivererPersonId { get; set; }

        /// <summary>
        /// This property Contain <see cref="Person"/> detail in this class that deliverer this <see cref="Delivery"/>
        /// </summary>
        public Person DelivererPerson { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="Person"/> that receiver this <see cref="Delivery"/>
        /// </summary>
        public Guid ReceiverPersonId { get; set; }

        /// <summary>
        /// This property Contain <see cref="Person"/> detail in this class that receiver this <see cref="Delivery"/>
        /// </summary>
        public Person ReceiverPerson { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="Person"/> that carrier this <see cref="Delivery"/>
        /// </summary>
        public Guid CarrierPersonId { get; set; }

        /// <summary>
        /// This property Contain <see cref="Person"/> detail in this class that carrier this <see cref="Delivery"/>
        /// </summary>
        public Person CarrierPerson { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="Vehicle"/> that carrier used for transfer this <see cref="Delivery"/>
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// This property Contain <see cref="Vehicle"/> detail in this class that carrier used for transfer this <see cref="Delivery"/>
        /// </summary>
        public Vehicle Vehicle { get; set; }

        /// <summary>
        /// Collection of <see cref="DeliveryRiceThreshing"/> that delivered in this <see cref="Delivery"/>
        /// </summary>
        public ICollection<DeliveryRiceThreshing> DeliveryRiceThreshings { get; set; }
    }
}