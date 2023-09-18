using RiceMill.Application.UseCases.BaseDto;
using System.Text;

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

        public Guid InputLoadId { get; set; }

        public string InputLoadInfo { get; set; }

        public string RiceThreshingInfo {
            get
            {
                var sbDetail = new StringBuilder();
                if (UnbrokenRice > 0)
                    sbDetail.Append($"{UnbrokenRice} ک بلند,");

                if (BrokenRice > 0)
                    sbDetail.Append($" {BrokenRice} ک نیمه,");

                if (Flour > 0)
                    sbDetail.Append($" {Flour} ک آرد,");

                if (ChickenRice > 0)
                    sbDetail.Append($" {ChickenRice} ک مرغی,");

                return sbDetail.Remove(sbDetail.Length - 1, 1).ToString().TrimStart();
            }
        }

        public string RiceThreshingHumanReadable { get; set; }

        //[SwaggerExclude]
        //public DtoIncome Income { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoDelivery> Deliveries { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoDryerHistory> DryerHistories { get; set; }
    }
}