using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    public sealed class RiceThreshing : EventBaseModelWithUserAndRiceMill
    {
        public DateTime ThreshingStartTime { get; set; }

        public DateTime ThreshingEndTime { get; set; }

        public short UnbrokenRice { get; set; }

        public short BrokenRice { get; set; }

        public short ChickenRice { get; set; }

        public short Flour { get; set; }

        public string Description { get; set; }

        public bool IsDelivered { get; set; }

        public ICollection<Person> People { get; set; }

        public ICollection<DryerHistory> DryerHistories { get; set; }

        public ICollection<Delivery> Deliveries { get; set; }
    }
}