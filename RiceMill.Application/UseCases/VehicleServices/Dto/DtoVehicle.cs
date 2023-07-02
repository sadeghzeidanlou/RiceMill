using RiceMill.Application.UseCases.BaseDto;
using RiceMill.Application.UseCases.DeliveryServices.Dto;
using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Application.UseCases.PersonServices.Dto;
using Shared.Enums;

namespace RiceMill.Application.UseCases.VehicleServices.Dto
{
    public class DtoVehicle : DtoEventBaseWithUserAndRiceMill
    {
        public string Title { get; set; }

        public string Plate { get; set; }

        public string Description { get; set; }

        public VehicleTypeEnum VehicleType { get; set; }

        public Guid OwnerPersonId { get; set; }

        public DtoPerson OwnerPerson { get; set; }

        public ICollection<DtoDelivery> Deliveries { get; set; }

        public ICollection<DtoInputLoad> InputLoads { get; set; }
    }
}