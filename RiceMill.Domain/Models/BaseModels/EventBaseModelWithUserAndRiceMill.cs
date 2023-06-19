namespace RiceMill.Domain.Models.BaseModels
{
    public class EventBaseModelWithUserAndRiceMill : EventBaseModelWithRiceMill
    {
        public int UserId { get; set; }

        public User User { get; set; }
    }
}