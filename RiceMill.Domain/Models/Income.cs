using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    public sealed class Income : EventBaseModelWithUserAndRiceMill
    {
        public DateTime IncomeTime { get; set; }

        public string Description { get; set; }

        public float UnbrokenRice { get; set; }

        public float BrokenRice { get; set; }

        public float Flour { get; set; }

        //public int Money { get; set; }

        public int RiceThreshingId { get; set; }
        public RiceThreshing RiceThreshing { get; set; }

        //public int ConcernId { get; set; }
        //public Concern Concern { get; set; }
    }
}