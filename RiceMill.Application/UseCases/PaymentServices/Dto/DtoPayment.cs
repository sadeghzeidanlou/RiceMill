using RiceMill.Application.UseCases.BaseDto;
using System.Text;

namespace RiceMill.Application.UseCases.PaymentServices.Dto
{
    public sealed class DtoPayment : DtoEventBaseWithUserAndRiceMill
    {
        public DateTime PaymentTime { get; set; }

        public string Description { get; set; }

        public float UnbrokenRice { get; set; }

        public float BrokenRice { get; set; }

        public float Flour { get; set; }

        public int Money { get; set; }

        public Guid PaidPersonId { get; set; }

        public string PaidPersonFullName { get; set; }

        public string PaymentDetail
        {
            get
            {
                var sbDetail = new StringBuilder();
                if (Money > 0)
                    sbDetail.Append($"{Money} تومان,");

                if (UnbrokenRice > 0)
                    sbDetail.Append($" {UnbrokenRice} ک برنج بلند,");

                if (BrokenRice > 0)
                    sbDetail.Append($" {BrokenRice} ک برنج نیمه,");

                if (Flour > 0)
                    sbDetail.Append($" {Flour} ک آرد,");

                return sbDetail.Remove(sbDetail.Length - 1, 1).ToString().TrimStart();
            }
        }

        //[SwaggerExclude]
        //public DtoPerson PaidPerson { get; set; }

        public Guid ConcernId { get; set; }

        //[SwaggerExclude]
        //public DtoConcern Concern { get; set; }

        public Guid? InputLoadId { get; set; }

        //[SwaggerExclude]
        //public DtoInputLoad InputLoad { get; set; }
    }
}