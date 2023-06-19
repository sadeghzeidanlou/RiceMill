namespace RiceMill.Domain.Models.BaseModels
{
    public class EventBaseModelWithRiceMill : EventBaseModel
    {
        public int RiceMillId { get; set; }

        public RiceMill RiceMill { get; set; }
    }
}