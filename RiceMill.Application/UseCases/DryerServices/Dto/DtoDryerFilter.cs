using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.DryerServices.Dto
{
    public class DtoDryerFilter : PagingInfo
    {
        public Guid? Id { get; set; }

        public Guid? RiceMillId { get; set; }

        public string Title { get; set; }
    }
}