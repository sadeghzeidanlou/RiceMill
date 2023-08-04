using RiceMill.Domain.Models.BaseModels;
using Shared.Enums;

namespace RiceMill.Domain.Models
{
    /// <summary>
    /// This class contain all information about a <see cref="Vehicle"/>
    /// </summary>
    public sealed class Vehicle : EventBaseModelWithUserAndRiceMill
    {
        /// <summary>
        /// Plate of <see cref="Vehicle"/>
        /// </summary>
        public string Plate { get; set; }

        /// <summary>
        /// Description of <see cref="Vehicle"/>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// VehicleType <see cref="VehicleTypeEnum"/> of <see cref="Vehicle"/>
        /// </summary>
        public VehicleTypeEnum VehicleType { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="Person"/> that determine this <see cref="Vehicle"/> is owned by who <see cref="Person"/>
        /// </summary>
        public Guid OwnerPersonId { get; set; }

        /// <summary>
        /// This property Contain <see cref="Person"/> detail in this class that determine this <see cref="Vehicle"/> is owned by who <see cref="Person"/>
        /// </summary>
        public Person OwnerPerson { get; set; }

        /// <summary>
        /// Collection of <see cref="Delivery"/> that by this <see cref="Vehicle"/> was delivered
        /// </summary>
        public ICollection<Delivery> Deliveries { get; set; }

        /// <summary>
        /// Collection of <see cref="InputLoad"/> that by this <see cref="Vehicle"/> was delivered
        /// </summary>
        public ICollection<InputLoad> InputLoads { get; set; }
    }
}