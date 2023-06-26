using RiceMill.Application.UseCases.PaymentServices.Dto;

namespace RiceMill.Application.UseCases.ConcernServices.Dto
{
    public class DtoConcern
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public ICollection<DtoPayment> Payments { get; set; }
    }
}