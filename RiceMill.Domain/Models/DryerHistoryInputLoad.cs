using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    public class DryerHistoryInputLoad : EventBaseModel
    {
        public Guid DryerHistoryId { get; set; }

        public DryerHistory DryerHistory { get; set; }

        public Guid InputLoadId { get; set; }

        public InputLoad InputLoad { get; set; }
    }
}