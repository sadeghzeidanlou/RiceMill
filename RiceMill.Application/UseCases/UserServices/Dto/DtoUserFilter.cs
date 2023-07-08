using RiceMill.Application.Common.Models.ResultObject;
using Shared.Enums;

namespace RiceMill.Application.UseCases.UserServices.Dto
{
    public class DtoUserFilter : PagingInfo
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public RoleEnum Role { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public DateTime? DeleteTime { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? UserPersonId { get; set; }

        public Guid ParentUserId { get; set; }

        public Guid? RiceMillId { get; set; }
    }
}