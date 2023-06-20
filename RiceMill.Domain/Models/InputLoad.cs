using RiceMill.Domain.Models.BaseModels;
using Shared.Enums;

namespace RiceMill.Domain.Models
{
    public sealed class InputLoad : EventBaseModelWithUserAndRiceMill
    {
        public short NumberOfBags { get; set; }

        public bool IsInDryer { get; set; }

        public string Description { get; set; }

        public DateTime DeliveryTime { get; set; }

        public NoticesTypeEnum NoticesType { get; set; }

        public int VillageId { get; set; }
        public Village Village { get; set; }

        public int TransfereeId { get; set; }
        public Person Transferee { get; set; }

        public int DeliveryId { get; set; }
        public Person Delivery { get; set; }

        public int DriverPersonId { get; set; }
        public Person Driver { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public int OwnerId { get; set; }
        public Person Owner { get; set; }

        public ICollection<DryerHistory> DryerHistories { get; set; }
    }
}