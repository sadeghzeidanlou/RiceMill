namespace RiceMill.Application.UseCases.BaseDto
{
    public class DtoEventBaseWithUserAndRiceMill : DtoEventBase
    {
        public Guid RiceMillId { get; set; }

        //public DtoRiceMill RiceMill { get; set; }

        public Guid UserId { get; set; }

        //public DtoUser User { get; set; }
    }
}