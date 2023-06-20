using RiceMill.Domain.Models.BaseModels;
using Shared.Enums;

namespace RiceMill.Domain.Models
{
    public sealed class User : EventBaseModelWithRiceMill
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public RoleEnum Role { get; set; }

        public bool IsActive { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int CreatorUserId { get; set; }
        public User Creator { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public ICollection<RiceThreshing> RiceThreshings { get; set; }
        public ICollection<Concern> Concerns { get; set; }
        public ICollection<Delivery> Deliveries { get; set; }
        public ICollection<Dryer> Dryers { get; set; }
        public ICollection<DryerHistory> DryerHistories { get; set; }
        public ICollection<InputLoad> InputLoads { get; set; }
        public ICollection<Person> People { get; set; }
        public ICollection<UserActivity> UserActivities { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Village> Villages { get; set; }
    }
}