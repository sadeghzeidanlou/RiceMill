using RiceMill.Application.UseCases.BaseDto;

namespace RiceMill.Application.UseCases.DeliveryServices.Dto
{
    public sealed class DtoDelivery : DtoEventBaseWithUserAndRiceMill
    {
        public short UnbrokenRice { get; set; }

        public short BrokenRice { get; set; }

        public short ChickenRice { get; set; }

        public short Flour { get; set; }

        public DateTime DeliveryTime { get; set; }

        public string Description { get; set; }

        public Guid DelivererPersonId { get; set; }

        //[SwaggerExclude]
        //public DtoPerson DelivererPerson { get; set; }

        public Guid ReceiverPersonId { get; set; }

        //[SwaggerExclude]
        //public DtoPerson ReceiverPerson { get; set; }

        public Guid CarrierPersonId { get; set; }

        //[SwaggerExclude]
        //public DtoPerson CarrierPerson { get; set; }

        public Guid VehicleId { get; set; }

        //[SwaggerExclude]
        //public DtoVehicle Vehicle { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoRiceThreshing> RiceThreshings { get; set; }
    }
}