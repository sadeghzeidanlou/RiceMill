using FluentValidation;
using FluentValidation.Results;

namespace Shared.ExtensionMethods
{
    public static class ValidationMethods
    {
        public static ValidationResult Validate<T>(this T instance) where T : class
        {
            var validator = Activator.CreateInstance(typeof(T).ValidatorType()) as IValidator<T>;
            return validator.Validate(instance);
        }

        private static Type ValidatorType<T>(this T input) => Type.GetType($"{typeof(T).FullName}Validator");
    }
}
