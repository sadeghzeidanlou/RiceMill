namespace RiceMill.Application.UseCases.BaseDto
{
    public class DtoEventBase
    {
        public Guid Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public DateTime? DeleteTime { get; set; }

        public bool IsDeleted { get; set; }
    }
}