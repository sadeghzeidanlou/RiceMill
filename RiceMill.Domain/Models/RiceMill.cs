using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    public sealed class RiceMill : EventBaseModel
    {
        public string Title { get; set; }

        public string Address { get; set; }

        public byte Wage { get; set; }

        public string Phone { get; set; }

        public string PostalCode { get; set; }

        public string Description { get; set; }

        public int OwnerId { get; set; }
        public Person Owner { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public ICollection<RiceThreshing> RiceThreshings { get; set; }
        public ICollection<Concern> Concerns { get; set; }
        public ICollection<Delivery> Deliveries { get; set; }
        public ICollection<Dryer> Dryers { get; set; }
        public ICollection<DryerHistory> DryerHistories { get; set; }
        public ICollection<InputLoad> InputLoads { get; set; }
        public ICollection<Person> People { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<UserActivity> UserActivities { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Village> Villages { get; set; }
    }
}