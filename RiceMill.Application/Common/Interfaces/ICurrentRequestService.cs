using Shared.Enums;

namespace RiceMill.Application.Common.Interfaces
{
    public interface ICurrentRequestService
    {
        public Guid UserId { get; }

        public Guid RiceMillId { get; }

        public RoleEnum UserRole { get; set; }

        bool IsAuthenticated => UserId != Guid.Empty && UserId != default;

        bool HaveWriteAccess => UserRole != RoleEnum.User;
    }
}