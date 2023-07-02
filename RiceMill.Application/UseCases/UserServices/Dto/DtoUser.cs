using RiceMill.Application.UseCases.BaseDto;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Application.UseCases.DeliveryServices.Dto;
using RiceMill.Application.UseCases.DryerHistoryServices.Dto;
using RiceMill.Application.UseCases.DryerServices.Dto;
using RiceMill.Application.UseCases.IncomeServices.Dto;
using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Application.UseCases.PaymentServices.Dto;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Application.UseCases.RiceMillServices.Dto;
using RiceMill.Application.UseCases.RiceThreshingServices.Dto;
using RiceMill.Application.UseCases.UserActivityServices.Dto;
using RiceMill.Application.UseCases.VehicleServices.Dto;
using RiceMill.Application.UseCases.VillageServices.Dto;
using Shared.Enums;

namespace RiceMill.Application.UseCases.UserServices.Dto
{
    public class DtoUser : DtoEventBase
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public RoleEnum Role { get; set; }

        public bool IsActive { get; set; }

        public Guid? UserPersonId { get; set; }

        public DtoPerson UserPerson { get; set; }

        public Guid ParentUserId { get; set; }

        public DtoUser ParentUser { get; set; }

        public Guid RiceMillId { get; set; }

        public DtoRiceMill RiceMill { get; set; }

        public ICollection<DtoConcern> Concerns { get; set; }

        public ICollection<DtoDelivery> Deliveries { get; set; }

        public ICollection<DtoDryer> Dryers { get; set; }

        public ICollection<DtoDryerHistory> DryerHistories { get; set; }

        public ICollection<DtoIncome> Incomes { get; set; }

        public ICollection<DtoInputLoad> InputLoads { get; set; }

        public ICollection<DtoPayment> Payments { get; set; }

        public ICollection<DtoPerson> People { get; set; }

        public ICollection<DtoRiceThreshing> RiceThreshings { get; set; }

        public ICollection<DtoUserActivity> UserActivities { get; set; }

        public ICollection<DtoVehicle> Vehicles { get; set; }

        public ICollection<DtoVillage> Villages { get; set; }
    }
}