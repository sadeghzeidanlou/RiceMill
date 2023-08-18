using RiceMill.Application.Common.Models.ResultObject;
using Shared.Enums;

namespace RiceMill.Application.UseCases.DryerHistoryServices.Dto
{
    public sealed class DtoDryerHistoryFilter : PagingInfo
    {
        public Guid? Id { get; set; }

        public List<Guid> Ids { get; set; }

        public DryerOperationEnum? Operation { get; set; }

        public DateTime? StartTimeLower { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? StartTimeGreater { get; set; }

        public DateTime? EndTimeLower { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime? EndTimeGreater { get; set; }

        public Guid? DryerId { get; set; }

        public Guid? RiceThreshingId { get; set; }

        public Guid? RiceMillId { get; set; }
    }
}