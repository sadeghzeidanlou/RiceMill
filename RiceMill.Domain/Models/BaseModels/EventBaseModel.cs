namespace RiceMill.Domain.Models.BaseModels
{
    public class EventBaseModel : BaseModel
    {
        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public DateTime? DeleteTime { get; set; }

        public bool IsDeleted { get; set; }
    }
}