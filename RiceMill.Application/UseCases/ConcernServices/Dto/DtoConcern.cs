namespace RiceMill.Application.UseCases.ConcernServices.Dto
{
    public class DtoConcern
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Guid UserId { get; set; }

        public Guid RiceMillId { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoPayment> Payments { get; set; }
    }
}