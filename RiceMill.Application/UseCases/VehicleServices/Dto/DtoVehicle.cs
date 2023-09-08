using RiceMill.Application.UseCases.BaseDto;
using Shared.Enums;

namespace RiceMill.Application.UseCases.VehicleServices.Dto
{
    public sealed class DtoVehicle : DtoEventBaseWithUserAndRiceMill
    {
        public string Plate { get; set; }

        public string Description { get; set; }

        public VehicleTypeEnum VehicleType { get; set; }

        public Guid OwnerPersonId { get; set; }

        public string HumaneReadable=> $"{Shared.Enums.VehicleType.GetAll.FirstOrDefault(x => x.Type == VehicleType)?.Title} ({Plate})";

        public string OwnerFullName { get; set; }

        //[SwaggerExclude]
        //public DtoPerson OwnerPerson { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoDelivery> Deliveries { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoInputLoad> InputLoads { get; set; }
    }
}