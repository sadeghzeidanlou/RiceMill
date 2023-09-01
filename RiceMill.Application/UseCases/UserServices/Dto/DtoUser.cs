using RiceMill.Application.UseCases.BaseDto;
using Shared.Enums;

namespace RiceMill.Application.UseCases.UserServices.Dto
{
    public sealed class DtoUser : DtoEventBase
    {
        public string Username { get; set; }

        public RoleEnum Role { get; set; }

        public Guid? UserPersonId { get; set; }

        //[SwaggerExclude]
        //public DtoPerson UserPerson { get; set; }

        public Guid RiceMillId { get; set; }

        //[SwaggerExclude]
        //public DtoRiceMill RiceMill { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoConcern> Concerns { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoDelivery> Deliveries { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoDryer> Dryers { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoDryerHistory> DryerHistories { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoIncome> Incomes { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoInputLoad> InputLoads { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoPayment> Payments { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoPerson> People { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoRiceThreshing> RiceThreshings { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoUserActivity> UserActivities { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoVehicle> Vehicles { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoVillage> Villages { get; set; }
    }
}