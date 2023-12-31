﻿using MD.PersianDateTime.Standard;
using RiceMill.Application.UseCases.BaseDto;
using Shared.Enums;

namespace RiceMill.Application.UseCases.InputLoadServices.Dto
{
    public sealed class DtoInputLoad : DtoEventBaseWithUserAndRiceMill
    {
        public short NumberOfBags { get; set; }

        public short NumberOfBagsInDryer { get; set; }

        public string Description { get; set; }

        public DateTime ReceiveTime { get; set; }

        public string ReceiveTimeReadable
        {
            get
            {
                var receiveTime = new PersianDateTime(ReceiveTime);
                return $"روز {receiveTime.ToShortDateString()} ساعت {receiveTime.ToString("HH:mm")}";
            }
        }

        public Guid VillageId { get; set; }

        public string VillageTitle { get; set; }

        //[SwaggerExclude]
        //public DtoVillage Village { get; set; }

        public Guid DelivererPersonId { get; set; }

        //[SwaggerExclude]
        //public DtoPerson DelivererPerson { get; set; }

        public Guid ReceiverPersonId { get; set; }

        //[SwaggerExclude]
        //public DtoPerson ReceiverPerson { get; set; }

        public Guid CarrierPersonId { get; set; }

        //[SwaggerExclude]
        //public DtoPerson CarrierPerson { get; set; }

        public Guid OwnerPersonId { get; set; }

        public string OwnerFullName { get; set; }

        public string InputLoadDetail { get; set; }

        //[SwaggerExclude]
        //public DtoPerson OwnerPerson { get; set; }

        public Guid VehicleId { get; set; }

        //[SwaggerExclude]
        //public DtoVehicle Vehicle { get; set; }

        //[SwaggerExclude]
        //public DtoPayment Payment { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoDryerHistory> DryerHistories { get; set; }
    }
}