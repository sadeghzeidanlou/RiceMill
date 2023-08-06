using RiceMill.Application.Common.Models.ResultObject;
using Shared.Enums;

namespace RiceMill.Application.UseCases.PersonServices.Dto
{
    public class DtoPersonFilter : PagingInfo
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public GenderEnum? Gender { get; set; }

        public string MobileNumber { get; set; }

        public string HomeNumber { get; set; }

        public NoticesTypeEnum? NoticesType { get; set; }

        public string Address { get; set; }

        public string FatherName { get; set; }

        public Guid? RiceMillId { get; set; }
    }
}