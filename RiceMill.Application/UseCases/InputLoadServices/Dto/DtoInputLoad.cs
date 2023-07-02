using RiceMill.Application.UseCases.BaseDto;
using RiceMill.Application.UseCases.DryerHistoryServices.Dto;
using RiceMill.Application.UseCases.PaymentServices.Dto;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Application.UseCases.VehicleServices.Dto;
using RiceMill.Application.UseCases.VillageServices.Dto;
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

        public DtoVillage Village { get; set; }

        public Guid DelivererPersonId { get; set; }

        public DtoPerson DelivererPerson { get; set; }

        public Guid ReceiverPersonId { get; set; }

        public DtoPerson ReceiverPerson { get; set; }

        public Guid CarrierPersonId { get; set; }

        public DtoPerson CarrierPerson { get; set; }

        public Guid OwnerPersonId { get; set; }

        public DtoPerson OwnerPerson { get; set; }

        public Guid VehicleId { get; set; }

        public DtoVehicle Vehicle { get; set; }

        public DtoPayment Payment { get; set; }

        public ICollection<DtoDryerHistory> DryerHistories { get; set; }
    }
}