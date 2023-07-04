using Shared.Enums;
using Shared.ExtensionMethods;

namespace RiceMill.Application.Common.Interfaces
{
    public interface ICurrentRequestService
    {
        public Guid UserId { get; }

        public Guid RiceMillId { get; }

        public RoleEnum UserRole { get; set; }

        bool IsAuthenticated => UserId.IsNotNullOrEmpty();

        bool HaveWriteAccess => UserRole != RoleEnum.User;

        bool HaveNotWriteAccess => UserRole == RoleEnum.User;

        bool IsAdmin => UserRole == RoleEnum.Admin;

        bool IsNotAdmin => UserRole != RoleEnum.Admin;

        bool IsManager => UserRole == RoleEnum.RiceMillManager && RiceMillId.IsNotNullOrEmpty();

        bool HasAccessToRiceMills => (IsAdmin || UserRole == RoleEnum.RiceMillManager) && IsAuthenticated;
    }
}