using RiceMill.Application.Common.Models.ResultObject;
using Shared.Enums;

namespace RiceMill.Application.UseCases.UserServices.Dto
{
    public class DtoUserFilter : PagingInfo
    {
        public Guid? Id { get; set; }

        public List<Guid> Ids { get; set; }

        public string Username { get; set; }

        public RoleEnum? Role { get; set; }

        public Guid? UserPersonId { get; set; }

        public Guid? RiceMillId { get; set; }
    }
}