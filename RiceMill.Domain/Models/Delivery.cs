using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    public sealed class Delivery : EventBaseModelWithUserAndRiceMill
    {
        public short UnbrokenRiceAmount { get; set; }

        public short BrokenRiceAmount { get; set; }

        public short ChickenRiceAmount { get; set; }

        public short FlourAmount { get; set; }

        public DateTime DeliveryTime { get; set; }

        public string Description { get; set; }

        public int DeliveryPersonId { get; set; }
        public Person DeliveryPerson { get; set; }

        public int DriverPersonId { get; set; }
        public Person Driver { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public ICollection<RiceThreshing> RiceThreshings { get; set; }
    }
}