using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.ConcernServices.Dto
{
    public class DtoConcernFilter : PagingInfo
    {
        public string Title { get; set; }
    }
}