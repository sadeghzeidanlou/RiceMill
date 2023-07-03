using RiceMill.Application.UseCases.BaseDto;
using RiceMill.Application.UseCases.DryerHistoryServices.Dto;
using Shared.Attributes;

namespace RiceMill.Application.UseCases.DryerServices.Dto
{
    public class DtoDryer : DtoEventBaseWithUserAndRiceMill
    {
        public string Title { get; set; }

        [SwaggerExclude]
        public ICollection<DtoDryerHistory> DryerHistories { get; set; }
    }
}