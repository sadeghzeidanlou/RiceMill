using RiceMill.Application.UseCases.BaseDto;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Application.UseCases.RiceThreshingServices.Dto;
using RiceMill.Application.UseCases.VehicleServices.Dto;

namespace RiceMill.Application.UseCases.DeliveryServices.Dto
{
    public class DtoDelivery : DtoEventBaseWithUserAndRiceMill
    {
        public short UnbrokenRice { get; set; }

        public short BrokenRice { get; set; }

        public short ChickenRice { get; set; }

        public short Flour { get; set; }

        public DateTime DeliveryTime { get; set; }

        public string Description { get; set; }

        public Guid DelivererPersonId { get; set; }

        public DtoPerson DelivererPerson { get; set; }

        public Guid ReceiverPersonId { get; set; }

        public DtoPerson ReceiverPerson { get; set; }

        public Guid CarrierPersonId { get; set; }

        public DtoPerson CarrierPerson { get; set; }

        public Guid VehicleId { get; set; }

        public DtoVehicle Vehicle { get; set; }

        public ICollection<DtoRiceThreshing> RiceThreshings { get; set; }
    }
}