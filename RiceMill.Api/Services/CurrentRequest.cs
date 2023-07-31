using RiceMill.Application.Common.Interfaces;
using Shared.Enums;

namespace RiceMill.Api.Services
{
    public class CurrentRequest : ICurrentRequestService
    {
        public Guid UserId => Guid.Parse("89a69ac3-03a9-49fd-91bb-25c98e63a10c");

        private RoleEnum _userRole;
        public RoleEnum UserRole { get => _userRole; set => _userRole = RoleEnum.Admin; }

        private string _ip;
        public string Ip { get => "192.168.10.10"; set => _ip = "192.168.10.10"; }
    }
}