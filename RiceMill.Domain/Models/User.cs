using RiceMill.Domain.Models.BaseModels;
using Shared.Enums;

namespace RiceMill.Domain.Models
{
    /// <summary>
    /// This class contain all information about a <see cref="User"/>
    /// </summary>
    public sealed class User : EventBaseModel
    {
        /// <summary>
        /// User name of <see cref="User"/>
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password of <see cref="User"/>
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Role <see cref="RoleEnum"/> of <see cref="User"/>
        /// </summary>
        public RoleEnum Role { get; set; }

        /// <summary>
        /// Determine a <see cref="User"/> is active or not
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="Person"/> that determine this <see cref="User"/> has owned by who <see cref="Person"/>
        /// </summary>
        public Guid UserPersonId { get; set; }

        /// <summary>
        /// This property Contain <see cref="Person"/> detail in this class that determine this <see cref="User"/> is owned by who <see cref="Person"/>
        /// </summary>
        public Person UserPerson { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="User"/> that determine which <see cref="User"/> create this <see cref="User"/> 
        /// </summary>
        public Guid ParentUserId { get; set; }

        /// <summary>
        /// This property Contain <see cref="User"/> detail in this class that determine which <see cref="User"/> is created this <see cref="User"/>
        /// </summary>
        public User ParentUser { get; set; }

        /// <summary>
        /// This property used for reference navigation between this class and <see cref="RiceMill"/> that determine this <see cref="User"/> is member of which <see cref="RiceMill"/>
        /// </summary>
        public Guid RiceMillId { get; set; }

        /// <summary>
        /// This property Contain <see cref="RiceMill"/> detail in this class
        /// </summary>
        public RiceMill RiceMill { get; set; }

        /// <summary>
        /// Collection of <see cref="Concern"/> that created with this <see cref="User"/>
        /// </summary>
        public ICollection<Concern> Concerns { get; set; }

        /// <summary>
        /// Collection of <see cref="Delivery"/> that created with this <see cref="User"/>
        /// </summary>
        public ICollection<Delivery> Deliveries { get; set; }

        /// <summary>
        /// Collection of <see cref="Dryer"/> that created with this <see cref="User"/>
        /// </summary>
        public ICollection<Dryer> Dryers { get; set; }

        /// <summary>
        /// Collection of <see cref="DryerHistory"/> that created with this <see cref="User"/>
        /// </summary>
        public ICollection<DryerHistory> DryerHistories { get; set; }

        /// <summary>
        /// Collection of <see cref="Income"/> that created with this <see cref="User"/>
        /// </summary>
        public ICollection<Income> Incomes { get; set; }

        /// <summary>
        /// Collection of <see cref="InputLoad"/> that created with this <see cref="User"/>
        /// </summary>
        public ICollection<InputLoad> InputLoads { get; set; }

        /// <summary>
        /// Collection of <see cref="Payment"/> that created with this <see cref="User"/>
        /// </summary>
        public ICollection<Payment> Payments { get; set; }

        /// <summary>
        /// Collection of <see cref="Person"/> that created with this <see cref="User"/>
        /// </summary>
        public ICollection<Person> People { get; set; }

        /// <summary>
        /// Collection of <see cref="RiceThreshing"/> that created with this <see cref="User"/>
        /// </summary>
        public ICollection<RiceThreshing> RiceThreshings { get; set; }

        /// <summary>
        /// Collection of <see cref="UserActivity"/> that created with this <see cref="User"/>
        /// </summary>
        public ICollection<UserActivity> UserActivities { get; set; }

        /// <summary>
        /// Collection of <see cref="Vehicle"/> that created with this <see cref="User"/>
        /// </summary>
        public ICollection<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Collection of <see cref="Village"/> that created with this <see cref="User"/>
        /// </summary>
        public ICollection<Village> Villages { get; set; }
    }
}