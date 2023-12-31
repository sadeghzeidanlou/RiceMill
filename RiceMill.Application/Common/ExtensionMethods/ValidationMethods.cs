﻿using FluentValidation;
using FluentValidation.Results;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.Common.ExtensionMethods
{
    public static class ValidationMethods
    {
        public static ValidationResult Validate<T>(this T instance) where T : class
        {
            var validator = Activator.CreateInstance(instance.ValidatorType()) as IValidator<T>;
            return validator.Validate(instance);
        }

        public static List<Error> GetErrorEnums(this List<ValidationFailure> validationFailures) =>
            validationFailures.Select(e => Error.CreateError(
                Enum.IsDefined(typeof(ResultStatusEnum), e.ErrorCode)
                ? (ResultStatusEnum)Enum.Parse(typeof(ResultStatusEnum), e.ErrorCode) : ResultStatusEnum.Fail)).ToList();

        private static Type ValidatorType<T>(this T input) => Type.GetType($"{typeof(T).FullName}Validator");
    }
}