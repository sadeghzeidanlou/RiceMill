using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.RiceMillServices.Dto
{
    public class DtoRiceMillFilter : PagingInfo
    {
        public Guid? Id { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public byte? Wage { get; set; }

        public byte? WageGreeterThan { get; set; }

        public byte? WageLowerThan { get; set; }

        public string Phone { get; set; }

        public string PostalCode { get; set; }

        public string Description { get; set; }

        public Guid? OwnerPersonId { get; set; }
    }
}