﻿using RiceMill.Application.UseCases.BaseDto;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Application.UseCases.PersonServices.Dto;
using Shared.Attributes;

namespace RiceMill.Application.UseCases.PaymentServices.Dto
{
    public class DtoPayment : DtoEventBaseWithUserAndRiceMill
    {
        public DateTime PaymentTime { get; set; }

        public string Description { get; set; }

        public float UnbrokenRice { get; set; }

        public float BrokenRice { get; set; }

        public float Flour { get; set; }

        public int Money { get; set; }

        public Guid PaidPersonId { get; set; }

        [SwaggerExclude]
        public DtoPerson PaidPerson { get; set; }

        public Guid ConcernId { get; set; }

        [SwaggerExclude]
        public DtoConcern Concern { get; set; }

        public Guid InputLoadId { get; set; }

        [SwaggerExclude]
        public DtoInputLoad InputLoad { get; set; }
    }
}