using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.Common.Models.ResultObject
{
    public class Error
    {
        public ResultStatusEnum ResultStatus { get; set; }

        public string Message { get; set; }
    }
}