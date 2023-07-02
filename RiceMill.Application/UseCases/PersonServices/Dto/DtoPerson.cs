using RiceMill.Application.UseCases.BaseDto;
using RiceMill.Application.UseCases.DeliveryServices.Dto;
using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Application.UseCases.PaymentServices.Dto;
using RiceMill.Application.UseCases.RiceMillServices.Dto;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Application.UseCases.VehicleServices.Dto;
using Shared.Enums;

namespace RiceMill.Application.UseCases.PersonServices.Dto
{
    public class DtoPerson : DtoEventBaseWithUserAndRiceMill
    {
        public string Name { get; set; }

        public string Family { get; set; }

        public GenderEnum Gender { get; set; }

        public string MobileNumber { get; set; }

        public string HomeNumber { get; set; }

        public string Address { get; set; }

        public string FatherName { get; set; }

        public DtoUser RelatedUser { get; set; }

        public ICollection<DtoPayment> Payments { get; set; }

        public ICollection<DtoDelivery> DelivererDeliveries { get; set; }

        public ICollection<DtoDelivery> ReceiverDeliveries { get; set; }

        public ICollection<DtoDelivery> CarrierDeliveries { get; set; }

        public ICollection<DtoInputLoad> DelivererInputLoads { get; set; }

        public ICollection<DtoInputLoad> ReceiverInputLoads { get; set; }

        public ICollection<DtoInputLoad> CarrierInputLoads { get; set; }

        public ICollection<DtoInputLoad> OwnedInputLoads { get; set; }

        public ICollection<DtoVehicle> OwnedVehicles { get; set; }

        public ICollection<DtoRiceMill> OwnedRiceMills { get; set; }
    }
}