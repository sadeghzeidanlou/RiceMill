using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.RiceThreshingServices.Dto
{
    public sealed class DtoRiceThreshingFilter : PagingInfo
    {
        public Guid? Id { get; set; }

        public List<Guid> Ids { get; set; }

        public DateTime? StartTimeLower { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? StartTimeGreater { get; set; }

        public DateTime? EndTimeLower { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime? EndTimeGreater { get; set; }

        public float? UnbrokenRiceLower { get; set; }

        public float? UnbrokenRice { get; set; }

        public float? UnbrokenRiceGreater { get; set; }

        public float? BrokenRiceLower { get; set; }

        public float? BrokenRice { get; set; }

        public float? BrokenRiceGreater { get; set; }

        public float? ChickenRiceLower { get; set; }

        public float? ChickenRice { get; set; }

        public float? ChickenRiceGreater { get; set; }

        public float? FlourLower { get; set; }

        public float? Flour { get; set; }

        public float? FlourGreater { get; set; }

        public string Description { get; set; }

        public bool? IsDelivered { get; set; }

        public Guid? IncomeId { get; set; }

        public Guid? RiceMillId { get; set; }
    }
}