using RiceMill.Application.Common.Interfaces;
using Shared.Enums;

namespace RiceMill.Api.Services
{
    public class CurrentRequest : ICurrentRequestService
    {
        public Guid UserId => Guid.Parse("89CEBDF5-B19E-44CE-B197-DE5B999CADD6");

        public Guid RiceMillId => Guid.Parse("0425374F-ED0C-4D7C-A5FC-0D522FB7D890");

        private RoleEnum _userRole;
        public RoleEnum UserRole { get => _userRole; set => _userRole = RoleEnum.Admin; }
    }
}