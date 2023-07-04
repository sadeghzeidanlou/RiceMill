using RiceMill.Application.Common.Interfaces;
using Shared.Enums;

namespace RiceMill.Api.Services
{
    public class CurrentRequest : ICurrentRequestService
    {
        public Guid UserId => Guid.Parse("89CEBDF5-B19E-44CE-B197-DE5B999CADD6");

        private RoleEnum _userRole;
        public RoleEnum UserRole { get => RoleEnum.RiceMillManager; set => _userRole = RoleEnum.Admin; }
    }
}