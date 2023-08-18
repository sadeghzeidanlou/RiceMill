using RiceMill.Application.UseCases.BaseDto;

namespace RiceMill.Application.UseCases.DryerServices.Dto
{
    public sealed class DtoDryer : DtoEventBaseWithUserAndRiceMill
    {
        public string Title { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoDryerHistory> DryerHistories { get; set; }
    }
}