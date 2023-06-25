using RiceMill.Domain.Models.BaseModels;
using Shared.Enums;

namespace RiceMill.Domain.Models
{
    /// <summary>
    /// This class contain all information about a <see cref="InputLoad"/>
    /// </summary>
    public sealed class InputLoad : EventBaseModelWithUserAndRiceMill
    {
        /// <summary>
        /// Number Of Bags that received in this <see cref="InputLoad"/>
        /// </summary>
        public short NumberOfBags { get; set; }

        /// <summary>
        /// Determine this <see cref="InputLoad"/> loaded into a <see cref="Dryer"/>
        /// </summary>
        public bool IsInDryer { get; set; }

        /// <summary>
        /// Description of this <see cref="InputLoad"/>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Time of Receive this <see cref="InputLoad"/>
        /// </summary>
        public DateTime ReceiveTime { get; set; }

        /// <summary>
        /// Determine how notice <see cref="NoticesTypeEnum"/> to customer about this <see cref="InputLoad"/>
        /// </summary>
        public NoticesTypeEnum NoticesType { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="Vehicle"/> that carrier used for transfer this <see cref="Delivery"/>
        /// </summary>
        public Guid VillageId { get; set; }

        /// <summary>
        /// This property Contain <see cref="Village"/> detail in this class that show this <see cref="InputLoad"/> come from which <see cref="Village"/>
        /// </summary>
        public Village Village { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="Person"/> that deliverer this <see cref="InputLoad"/>
        /// </summary>
        public Guid DelivererPersonId { get; set; }

        /// <summary>
        /// This property Contain <see cref="Person"/> detail in this class that deliverer this <see cref="InputLoad"/>
        /// </summary>
        public Person DelivererPerson { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="Person"/> that receiver this <see cref="InputLoad"/>
        /// </summary>
        public Guid ReceiverPersonId { get; set; }

        /// <summary>
        /// This property Contain <see cref="Person"/> detail in this class that receiver this <see cref="InputLoad"/>
        /// </summary>
        public Person ReceiverPerson { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="Person"/> that carrier this <see cref="InputLoad"/>
        /// </summary>
        public Guid CarrierPersonId { get; set; }

        /// <summary>
        /// This property Contain <see cref="Person"/> detail in this class that carrier this <see cref="InputLoad"/>
        /// </summary>
        public Person CarrierPerson { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="Person"/> that determine this <see cref="InputLoad"/> is owned by who <see cref="Person"/>
        /// </summary>
        public Guid OwnerPersonId { get; set; }

        /// <summary>
        /// This property Contain <see cref="Person"/> detail in this class that determine this <see cref="InputLoad"/> is owned by who <see cref="Person"/>
        /// </summary>
        public Person OwnerPerson { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="Vehicle"/> that carrier used for transfer this <see cref="InputLoad"/>
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// This property Contain <see cref="Vehicle"/> detail in this class that carrier used for transfer this <see cref="InputLoad"/>
        /// </summary>
        public Vehicle Vehicle { get; set; }

        /// <summary>
        /// This property Contain <see cref="Payment"/> detail in this class that determine <see cref="Payment"/> info of this <see cref="InputLoad"/>
        /// </summary>
        public Payment Payment { get; set; }

        /// <summary>
        /// Collection of <see cref="DryerHistory"/> that show this <see cref="InputLoad"/> in which <see cref="DryerHistory"/>
        /// </summary>
        public ICollection<DryerHistory> DryerHistories { get; set; }
    }
}