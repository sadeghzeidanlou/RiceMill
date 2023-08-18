using RiceMill.Application.UseCases.BaseDto;

namespace RiceMill.Application.UseCases.RiceThreshingServices.Dto
{
    public sealed class DtoRiceThreshing : DtoEventBaseWithUserAndRiceMill
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime{ get; set; }

        public float UnbrokenRice { get; set; }

        public float BrokenRice { get; set; }

        public float ChickenRice { get; set; }

        public float Flour { get; set; }

        public string Description { get; set; }

        public bool IsDelivered { get; set; }

        public Guid IncomeId { get; set; }

        //[SwaggerExclude]
        //public DtoIncome Income { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoDelivery> Deliveries { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoDryerHistory> DryerHistories { get; set; }
    }
}