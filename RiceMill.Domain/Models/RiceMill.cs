using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    /// <summary>
    /// This class contain all information about a <see cref="RiceMill"/>
    /// </summary>
    public sealed class RiceMill : EventBaseModel
    {
        /// <summary>
        /// Title of <see cref="RiceMill"/>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Address of <see cref="RiceMill"/>
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Wage much of <see cref="RiceMill"/>
        /// </summary>
        public byte Wage { get; set; }

        /// <summary>
        /// Phone of <see cref="RiceMill"/>
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Postal Code of <see cref="RiceMill"/>
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Description of <see cref="RiceMill"/>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="Person"/> that determine this <see cref="RiceMill"/> is owned by who <see cref="Person"/>
        /// </summary>
        public Guid OwnerPersonId { get; set; }

        /// <summary>
        /// This property Contain <see cref="Person"/> detail in this class that determine this <see cref="RiceMill"/> is owned by who <see cref="Person"/>
        /// </summary>
        public Person OwnerPerson { get; set; }

        /// <summary>
        /// Collection of <see cref="Concern"/> in this <see cref="RiceMill"/>
        /// </summary>
        public ICollection<Concern> Concerns { get; set; }

        /// <summary>
        /// Collection of <see cref="Delivery"/> in this <see cref="RiceMill"/>
        /// </summary>
        public ICollection<Delivery> Deliveries { get; set; }

        /// <summary>
        /// Collection of <see cref="Dryer"/> in this <see cref="RiceMill"/>
        /// </summary>
        public ICollection<Dryer> Dryers { get; set; }

        /// <summary>
        /// Collection of <see cref="DryerHistory"/> in this <see cref="RiceMill"/>
        /// </summary>
        public ICollection<DryerHistory> DryerHistories { get; set; }

        /// <summary>
        /// Collection of <see cref="Income"/> in this <see cref="RiceMill"/>
        /// </summary>
        public ICollection<Income> Incomes { get; set; }

        /// <summary>
        /// Collection of <see cref="InputLoad"/> in this <see cref="RiceMill"/>
        /// </summary>
        public ICollection<InputLoad> InputLoads { get; set; }

        /// <summary>
        /// Collection of <see cref="Payment"/> in this <see cref="RiceMill"/>
        /// </summary>
        public ICollection<Payment> Payments { get; set; }

        /// <summary>
        /// Collection of <see cref="Person"/> in this <see cref="RiceMill"/>
        /// </summary>
        public ICollection<Person> MemberPeople { get; set; }

        /// <summary>
        /// Collection of <see cref="RiceThreshing"/> in this <see cref="RiceMill"/>
        /// </summary>
        public ICollection<RiceThreshing> RiceThreshings { get; set; }

        /// <summary>
        /// Collection of <see cref="User"/> in this <see cref="RiceMill"/>
        /// </summary>
        public ICollection<User> Users { get; set; }

        /// <summary>
        /// Collection of <see cref="UserActivity"/> in this <see cref="RiceMill"/>
        /// </summary>
        public ICollection<UserActivity> UserActivities { get; set; }

        /// <summary>
        /// Collection of <see cref="Vehicle"/> in this <see cref="RiceMill"/>
        /// </summary>
        public ICollection<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Collection of <see cref="Village"/> in this <see cref="RiceMill"/>
        /// </summary>
        public ICollection<Village> Villages { get; set; }
    }
}