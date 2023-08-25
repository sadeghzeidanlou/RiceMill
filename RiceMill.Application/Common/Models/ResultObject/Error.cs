using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.Common.Models.ResultObject
{
    public sealed class Error
    {
        public ResultStatusEnum ResultStatus { get; set; }

        public string Message { get; set; }

        public static Error CreateError(ResultStatusEnum error) => new()
        {
            ResultStatus = error,
            Message = error.GetErrorMessage()
        };
    }
}