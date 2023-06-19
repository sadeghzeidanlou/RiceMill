using RiceMill.Domain.Enums;
using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    public sealed class Vehicle : EventBaseModelWithUserAndRiceMill
    {
        public string Title { get; set; }

        public string Plate { get; set; }

        public string Description { get; set; }

        public VehicleTypeEnum VehicleType { get; set; }

        public int OwnerId { get; set; }
        public Person Owner { get; set; }

        public ICollection<Delivery> Deliveries { get; set; }
        public ICollection<InputLoad> InputLoads { get; set; }
    }
}