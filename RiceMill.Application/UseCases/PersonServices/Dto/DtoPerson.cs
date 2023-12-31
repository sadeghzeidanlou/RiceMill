﻿using RiceMill.Application.UseCases.BaseDto;
using Shared.Enums;

namespace RiceMill.Application.UseCases.PersonServices.Dto
{
    public sealed class DtoPerson : DtoEventBaseWithUserAndRiceMill
    {
        public string Name { get; set; }

        public string Family { get; set; }

        public GenderEnum Gender { get; set; }

        public string MobileNumber { get; set; }

        public string HomeNumber { get; set; }

        public NoticesTypeEnum NoticesType { get; set; }

        public string Address { get; set; }

        public string FatherName { get; set; }

        public string FullName => $"{Name} {Family}";

        public string FullNameWithFatherName => $"{Name} {Family} فرزند {FatherName}";

        //[SwaggerExclude]
        //public DtoUser RelatedUser { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoPayment> Payments { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoDelivery> DelivererDeliveries { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoDelivery> ReceiverDeliveries { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoDelivery> CarrierDeliveries { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoInputLoad> DelivererInputLoads { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoInputLoad> ReceiverInputLoads { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoInputLoad> CarrierInputLoads { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoInputLoad> OwnedInputLoads { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoVehicle> OwnedVehicles { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoRiceMill> OwnedRiceMills { get; set; }
    }
}