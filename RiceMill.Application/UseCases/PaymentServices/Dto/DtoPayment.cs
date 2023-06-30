using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Application.UseCases.RiceMillServices.Dto;
using RiceMill.Application.UseCases.UserServices.Dto;

namespace RiceMill.Application.UseCases.PaymentServices.Dto
{
    public class DtoPayment
    {
        public Guid Id { get; set; }

        public DateTime PaymentTime { get; set; }

        public string Description { get; set; }

        public float UnbrokenRice { get; set; }

        public float BrokenRice { get; set; }

        public float Flour { get; set; }

        public int Money { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public DateTime? DeleteTime { get; set; }

        public bool IsDeleted { get; set; }

        public Guid PaidPersonId { get; set; }

        //public DtoPerson PaidPerson { get; set; }

        public Guid ConcernId { get; set; }

        public DtoConcern Concern { get; set; }

        public Guid InputLoadId { get; set; }

        //public DtoInputLoad InputLoad { get; set; }

        public Guid RiceMillId { get; set; }

        //public DtoRiceMill RiceMill { get; set; }

        public Guid UserId { get; set; }

        //public DtoUser User { get; set; }
    }
}