using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.IncomeServices.Dto
{
    public sealed class DtoIncomeFilter : PagingInfo
    {
        public Guid? Id { get; set; }

        public List<Guid> Ids { get; set; }

        public DateTime? IncomeTimeLower { get; set; }

        public DateTime? IncomeTime { get; set; }

        public DateTime? IncomeTimeGreater { get; set; }

        public string Description { get; set; }

        public float? UnbrokenRiceLower { get; set; }

        public float? UnbrokenRice { get; set; }

        public float? UnbrokenRiceGreater { get; set; }

        public float? BrokenRiceLower { get; set; }

        public float? BrokenRice { get; set; }

        public float? BrokenRiceGreater { get; set; }

        public float? FlourLower { get; set; }

        public float? Flour { get; set; }

        public float? FlourGreater { get; set; }

        public Guid? RiceMillId { get; set; }
    }
}