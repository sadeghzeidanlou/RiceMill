using RiceMill.Application.UseCases.BaseDto;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Application.UseCases.DeliveryServices.Dto;
using RiceMill.Application.UseCases.DryerHistoryServices.Dto;
using RiceMill.Application.UseCases.DryerServices.Dto;
using RiceMill.Application.UseCases.IncomeServices.Dto;
using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Application.UseCases.PaymentServices.Dto;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Application.UseCases.RiceThreshingServices.Dto;
using RiceMill.Application.UseCases.UserActivityServices.Dto;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Application.UseCases.VehicleServices.Dto;
using RiceMill.Application.UseCases.VillageServices.Dto;
using Shared.Attributes;

namespace RiceMill.Application.UseCases.RiceMillServices.Dto
{
    public class DtoRiceMill : DtoEventBase
    {
        public string Title { get; set; }

        public string Address { get; set; }

        public byte Wage { get; set; }

        public string Phone { get; set; }

        public string PostalCode { get; set; }

        public string Description { get; set; }

        public Guid? OwnerPersonId { get; set; }

        [SwaggerExclude]
        public DtoPerson OwnerPerson { get; set; }

        [SwaggerExclude]
        public ICollection<DtoConcern> Concerns { get; set; }

        [SwaggerExclude]
        public ICollection<DtoDelivery> Deliveries { get; set; }

        [SwaggerExclude]
        public ICollection<DtoDryer> Dryers { get; set; }

        [SwaggerExclude]
        public ICollection<DtoDryerHistory> DryerHistories { get; set; }

        [SwaggerExclude]
        public ICollection<DtoIncome> Incomes { get; set; }

        [SwaggerExclude]
        public ICollection<DtoInputLoad> InputLoads { get; set; }

        [SwaggerExclude]
        public ICollection<DtoPayment> Payments { get; set; }

        [SwaggerExclude]
        public ICollection<DtoPerson> MemberPeople { get; set; }

        [SwaggerExclude]
        public ICollection<DtoRiceThreshing> RiceThreshings { get; set; }

        [SwaggerExclude]
        public ICollection<DtoUser> Users { get; set; }

        [SwaggerExclude]
        public ICollection<DtoUserActivity> UserActivities { get; set; }

        [SwaggerExclude]
        public ICollection<DtoVehicle> Vehicles { get; set; }

        [SwaggerExclude]
        public ICollection<DtoVillage> Villages { get; set; }
    }
}