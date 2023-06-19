using RiceMill.Domain.Enums;
using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    public sealed class Person : EventBaseModelWithUserAndRiceMill
    {
        public string Name { get; set; }

        public string Family { get; set; }

        public GenderEnum Gender { get; set; }

        public string MobileNumber { get; set; }

        public string HomeNumber { get; set; }

        public string Address { get; set; }

        public string FatherName { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public ICollection<Delivery> Deliveries { get; set; }
        public ICollection<Delivery> DeliveryDriverPerson { get; set; }
        public ICollection<DryerHistory> DryerHistories { get; set; }
        public ICollection<InputLoad> InputLoadTransferees { get; set; }
        public ICollection<InputLoad> InputLoadDeliveries { get; set; }
        public ICollection<InputLoad> InputLoadDrivers { get; set; }
        public ICollection<InputLoad> InputLoadOwners { get; set; }
    }
}