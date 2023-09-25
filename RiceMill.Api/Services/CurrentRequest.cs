using RiceMill.Application.Common.Interfaces;
using Shared.Enums;

namespace RiceMill.Api.Services
{
    public sealed class CurrentRequest : ICurrentRequestService
    {
        public Guid UserId { get; set; }

        public Guid? RiceMillId { get; set; }

        public RoleEnum UserRole { get; set; }

        public string Ip { get; set; } = string.Empty;
    }
}