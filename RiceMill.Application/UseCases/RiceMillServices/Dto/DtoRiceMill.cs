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

        public DtoPerson OwnerPerson { get; set; }

        public ICollection<DtoConcern> Concerns { get; set; }

        public ICollection<DtoDelivery> Deliveries { get; set; }

        public ICollection<DtoDryer> Dryers { get; set; }

        public ICollection<DtoDryerHistory> DryerHistories { get; set; }

        public ICollection<DtoIncome> Incomes { get; set; }

        public ICollection<DtoInputLoad> InputLoads { get; set; }

        public ICollection<DtoPayment> Payments { get; set; }

        public ICollection<DtoPerson> MemberPeople { get; set; }

        public ICollection<DtoRiceThreshing> RiceThreshings { get; set; }

        public ICollection<DtoUser> Users { get; set; }

        public ICollection<DtoUserActivity> UserActivities { get; set; }

        public ICollection<DtoVehicle> Vehicles { get; set; }

        public ICollection<DtoVillage> Villages { get; set; }
    }
}