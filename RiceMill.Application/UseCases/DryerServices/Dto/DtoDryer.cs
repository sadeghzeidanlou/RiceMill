using RiceMill.Application.UseCases.BaseDto;
using RiceMill.Application.UseCases.DryerHistoryServices.Dto;

namespace RiceMill.Application.UseCases.DryerServices.Dto
{
    public class DtoDryer : DtoEventBaseWithUserAndRiceMill
    {
        public string Title { get; set; }

        public ICollection<DtoDryerHistory> DryerHistories { get; set; }
    }
}