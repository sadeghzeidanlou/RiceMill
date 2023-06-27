using FluentValidation.Results;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.Common.ExtensionMethods
{
    public static class ValidationMethods
    {
        public static List<Error> GetErrorEnums(this List<ValidationFailure> validationFailures) =>
            validationFailures.Select(e => new Error(Enum.IsDefined(typeof(ResultStatusEnum), e.ErrorMessage) 
                ? (ResultStatusEnum)Enum.Parse(typeof(ResultStatusEnum), e.ErrorMessage) : ResultStatusEnum.Fail)).ToList();
    }
}