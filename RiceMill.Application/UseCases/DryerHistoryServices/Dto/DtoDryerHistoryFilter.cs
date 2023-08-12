using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.DryerHistoryServices.Dto
{
    public class DtoDryerHistoryFilter : PagingInfo
    {
        public Guid? RiceMillId { get; set; }
    }
}