using RiceMill.Application.Common.Models.ResultObject;
using Shared.Enums;

namespace RiceMill.Application.UseCases.VehicleServices.Dto
{
    public class DtoVehicleFilter : PagingInfo
    {
        public Guid? Id { get; set; }

        public List<Guid> Ids { get; set; }

        public string Plate { get; set; }

        public string Description { get; set; }

        public VehicleTypeEnum? VehicleType { get; set; }

        public Guid? OwnerPersonId { get; set; }

        public Guid? RiceMillId { get; set; }
    }
}