using RiceMill.Application.UseCases.BaseDto;
using RiceMill.Application.UseCases.DeliveryServices.Dto;
using RiceMill.Application.UseCases.DryerHistoryServices.Dto;
using RiceMill.Application.UseCases.IncomeServices.Dto;
using Shared.Attributes;

namespace RiceMill.Application.UseCases.RiceThreshingServices.Dto
{
    public class DtoRiceThreshing : DtoEventBaseWithUserAndRiceMill
    {
        public DateTime RiceThreshingStart { get; set; }

        public DateTime RiceThreshingEnd { get; set; }

        public short UnbrokenRice { get; set; }

        public short BrokenRice { get; set; }

        public short ChickenRice { get; set; }

        public short Flour { get; set; }

        public string Description { get; set; }

        public bool IsDelivered { get; set; }

        public Guid IncomeId { get; set; }

        [SwaggerExclude]
        public DtoIncome Income { get; set; }

        [SwaggerExclude]
        public ICollection<DtoDelivery> Deliveries { get; set; }

        [SwaggerExclude]
        public ICollection<DtoDryerHistory> DryerHistories { get; set; }
    }
}