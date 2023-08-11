using FluentValidation;
using RiceMill.Application.Common.Models.Enums;
using Shared.Enums;
using Shared.ExtensionMethods;

namespace RiceMill.Application.UseCases.UserServices.Dto
{
    public record DtoCreateUser(string Username, string Password, RoleEnum Role, Guid? UserPersonId, Guid? RiceMillId);

    public class DtoCreateUserValidator : AbstractValidator<DtoCreateUser>
    {
        public DtoCreateUserValidator()
        {
            RuleFor(dto => dto.Username)
                .NotEmpty().WithErrorCode(ResultStatusEnum.UserUsernameIsNotValid.ToString())
                .MaximumLength(30).WithErrorCode(ResultStatusEnum.UserUsernameLengthIsNotValid.ToString());

            RuleFor(dto => dto.Password)
                .NotEmpty().WithErrorCode(ResultStatusEnum.UserPasswordIsNotValid.ToString());

            RuleFor(dto => dto.Role)
                .IsInEnum().WithErrorCode(ResultStatusEnum.UserRoleIsNotValid.ToString());

            RuleFor(dto => dto.UserPersonId)
                .Must((up) => up.IsNullOrEmpty() || up.HasValue && up.IsNotNullOrEmpty()).WithErrorCode(ResultStatusEnum.UserUserPersonIdIsNotValid.ToString());

            RuleFor(dto => dto.RiceMillId)
                .Must((rm) => rm.IsNullOrEmpty() || rm.HasValue && rm.IsNotNullOrEmpty()).WithErrorCode(ResultStatusEnum.RiceMillIdIsNotValid.ToString());
        }
    }
}