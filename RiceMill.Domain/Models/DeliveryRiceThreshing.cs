using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    public class DeliveryRiceThreshing : EventBaseModel
    {
        public Guid DeliveryId { get; set; }

        public Delivery Delivery { get; set; }

        public Guid RiceThreshingId { get; set; }

        public RiceThreshing RiceThreshing { get; set; }
    }
}