using Shared.Enums;
using Shared.ExtensionMethods;

namespace RiceMill.Application.Common.Interfaces
{
    public interface ICurrentRequestService
    {
        public string Ip { get; set; }

        public Guid UserId { get; }

        public RoleEnum UserRole { get; set; }

        bool IsAuthenticated => UserId.IsNotNullOrEmpty();

        bool IsNotAuthenticated => UserId.IsNullOrEmpty();

        bool HaveWriteAccess => UserRole != RoleEnum.User;

        bool JustCanRead => UserRole == RoleEnum.User;

        bool IsAdmin => UserRole == RoleEnum.Admin;

        bool IsNotAdmin => UserRole != RoleEnum.Admin;

        bool IsManager => UserRole == RoleEnum.RiceMillManager;

        bool HasAccessToRiceMills => (IsAdmin || IsManager) && IsAuthenticated;

        bool HasNotAccessToRiceMills => !HasAccessToRiceMills;

        bool HaveAccessToWrite => HaveWriteAccess && IsAuthenticated;

        bool HaveNotAccessToWrite => IsNotAuthenticated || JustCanRead;
    }
}