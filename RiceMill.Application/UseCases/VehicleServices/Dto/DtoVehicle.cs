using RiceMill.Application.UseCases.BaseDto;
using Shared.Enums;

namespace RiceMill.Application.UseCases.VehicleServices.Dto
{
    public class DtoVehicle : DtoEventBaseWithUserAndRiceMill
    {
        public string Plate { get; set; }

        public string Description { get; set; }

        public VehicleTypeEnum VehicleType { get; set; }

        public Guid OwnerPersonId { get; set; }

        //[SwaggerExclude]
        //public DtoPerson OwnerPerson { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoDelivery> Deliveries { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoInputLoad> InputLoads { get; set; }
    }
}