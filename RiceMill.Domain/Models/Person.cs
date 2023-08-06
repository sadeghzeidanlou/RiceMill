using RiceMill.Domain.Models.BaseModels;
using Shared.Enums;

namespace RiceMill.Domain.Models
{
    /// <summary>
    /// This class contain all information about a <see cref="Person"/>
    /// </summary>
    public sealed class Person : EventBaseModel
    {
        /// <summary>
        /// Name of <see cref="Person"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Family of <see cref="Person"/>
        /// </summary>
        public string Family { get; set; }

        /// <summary>
        /// Gender <see cref="GenderEnum"/> of <see cref="Person"/>
        /// </summary>
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// Mobile number of <see cref="Person"/>
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Home number of <see cref="Person"/>
        /// </summary>
        public string HomeNumber { get; set; }

        /// <summary>
        /// Determine how notice <see cref="NoticesTypeEnum"/> to person about all operation that operate on person/>
        /// </summary>
        public NoticesTypeEnum NoticesType { get; set; }

        /// <summary>
        /// Address of <see cref="Person"/>
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Father name of <see cref="Person"/>
        /// </summary>
        public string FatherName { get; set; }

        /// <summary>
        /// This property used for reference navigation between any class and RiceMill
        /// </summary>
        public Guid RiceMillId { get; set; }

        /// <summary>
        /// This property Contain <see cref="RiceMill"/> detail in any other classes that have relation with <see cref="RiceMill"/>
        /// </summary>
        public RiceMill RiceMill { get; set; }

        /// <summary>
        /// Collection of <see cref="Payment"/> that paid to this <see cref="Person"/>
        /// </summary>
        public ICollection<Payment> Payments { get; set; }

        /// <summary>
        /// Collection of <see cref="Delivery"/> that this <see cref="Person"/> Deliverer Delivery 
        /// </summary>
        public ICollection<Delivery> DelivererDeliveries { get; set; }

        /// <summary>
        /// Collection of <see cref="Delivery"/> that this <see cref="Person"/> Receiver Delivery 
        /// </summary>
        public ICollection<Delivery> ReceiverDeliveries { get; set; }

        /// <summary>
        /// Collection of <see cref="Delivery"/> that this <see cref="Person"/> Carrier Delivery 
        /// </summary>
        public ICollection<Delivery> CarrierDeliveries { get; set; }

        /// <summary>
        /// Collection of <see cref="InputLoad"/> that this <see cref="Person"/> Deliverer InputLoad 
        /// </summary>
        public ICollection<InputLoad> DelivererInputLoads { get; set; }

        /// <summary>
        /// Collection of <see cref="InputLoad"/> that this <see cref="Person"/> Receiver InputLoad 
        /// </summary>
        public ICollection<InputLoad> ReceiverInputLoads { get; set; }

        /// <summary>
        /// Collection of <see cref="InputLoad"/> that this <see cref="Person"/> Carrier InputLoad 
        /// </summary>
        public ICollection<InputLoad> CarrierInputLoads { get; set; }

        /// <summary>
        /// Collection of <see cref="InputLoad"/> that this <see cref="Person"/> has owned 
        /// </summary>
        public ICollection<InputLoad> OwnedInputLoads { get; set; }

        /// <summary>
        /// Collection of <see cref="Vehicle"/> that this <see cref="Person"/> has owned 
        /// </summary>
        public ICollection<Vehicle> OwnedVehicles { get; set; }

        /// <summary>
        /// Collection of <see cref="RiceMill"/> that this <see cref="Person"/> has owned 
        /// </summary>
        public ICollection<RiceMill> OwnedRiceMills { get; set; }
    }
}