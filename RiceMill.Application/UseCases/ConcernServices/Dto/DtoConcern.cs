using RiceMill.Application.UseCases.BaseDto;

namespace RiceMill.Application.UseCases.ConcernServices.Dto
{
    public sealed class DtoConcern : DtoEventBaseWithUserAndRiceMill
    {
        public string Title { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoPayment> Payments { get; set; }
    }
}