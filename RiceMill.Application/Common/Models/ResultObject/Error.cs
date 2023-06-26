using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.Common.Models.ResultObject
{
    public class Error
    {
        public Error(ResultStatusEnum error)
        {
            ResultStatus = error;
            Message = ErrorDictionary.ResultStatusMessage[error];
        }
        public ResultStatusEnum ResultStatus { get; }

        public string Message { get; }
    }
}