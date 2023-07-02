using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.RiceMillServices.Dto
{
    public class DtoRiceMillFilter : PagingInfo
    {
        public string Title { get; set; }
    }
}