using RiceMill.Application.UseCases.BaseDto;
using RiceMill.Application.UseCases.DryerHistoryServices.Dto;
using RiceMill.Application.UseCases.PaymentServices.Dto;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Application.UseCases.VehicleServices.Dto;
using RiceMill.Application.UseCases.VillageServices.Dto;
using Shared.Attributes;
using Shared.Enums;

namespace RiceMill.Application.UseCases.InputLoadServices.Dto
{
    public class DtoInputLoad : DtoEventBaseWithUserAndRiceMill
    {
        public short NumberOfBags { get; set; }

        public short NumberOfBagsInDryer { get; set; }

        public string Description { get; set; }

        public DateTime ReceiveTime { get; set; }

        public NoticesTypeEnum NoticesType { get; set; }

        public Guid VillageId { get; set; }

        [SwaggerExclude]
        public DtoVillage Village { get; set; }

        public Guid DelivererPersonId { get; set; }

        [SwaggerExclude]
        public DtoPerson DelivererPerson { get; set; }

        public Guid ReceiverPersonId { get; set; }

        [SwaggerExclude]
        public DtoPerson ReceiverPerson { get; set; }

        public Guid CarrierPersonId { get; set; }

        [SwaggerExclude]
        public DtoPerson CarrierPerson { get; set; }

        public Guid OwnerPersonId { get; set; }

        [SwaggerExclude]
        public DtoPerson OwnerPerson { get; set; }

        public Guid VehicleId { get; set; }

        [SwaggerExclude]
        public DtoVehicle Vehicle { get; set; }

        [SwaggerExclude]
        public DtoPayment Payment { get; set; }

        [SwaggerExclude]
        public ICollection<DtoDryerHistory> DryerHistories { get; set; }
    }
}