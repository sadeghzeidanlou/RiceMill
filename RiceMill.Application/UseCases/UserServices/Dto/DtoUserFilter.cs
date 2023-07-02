using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.UserServices.Dto
{
    public class DtoUserFilter : PagingInfo
    {
        public string UserName { get; set; }
    }
}