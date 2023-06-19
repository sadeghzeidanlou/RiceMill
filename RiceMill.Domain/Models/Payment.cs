using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    public sealed class Payment : EventBaseModelWithUserAndRiceMill
    {
        public DateTime PaymentTime { get; set; }

        public string Description { get; set; }

        public float UnbrokenRice { get; set; }

        public float BrokenRice { get; set; }

        public float Flour { get; set; }

        public int Money { get; set; }

        public int PaymentToId { get; set; }
        public Person PaymentTo { get; set; }

        public int ConcernId { get; set; }
        public Concern Concern { get; set; }

        public int? InputLoadId { get; set; }
        public InputLoad InputLoad { get; set; }
    }
}