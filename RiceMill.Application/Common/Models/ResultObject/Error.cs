using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.Common.Models.ResultObject
{
    public sealed class Error
    {
        public Error(ResultStatusEnum error)
        {
            ResultStatus = error;
            Message = error.GetErrorMessage();
        }

        public ResultStatusEnum ResultStatus { get; }

        public string Message { get; }
    }
}