using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    public sealed class Dryer : EventBaseModelWithUserAndRiceMill
    {
        public string Title { get; set; }

        public ICollection<DryerHistory> DryerHistories { get; set; }
    }
}