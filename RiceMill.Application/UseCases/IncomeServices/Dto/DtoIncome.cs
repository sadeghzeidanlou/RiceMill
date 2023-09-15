using MD.PersianDateTime.Standard;
using RiceMill.Application.UseCases.BaseDto;
using System.Text;

namespace RiceMill.Application.UseCases.IncomeServices.Dto
{
    public sealed class DtoIncome : DtoEventBaseWithUserAndRiceMill
    {
        public DateTime IncomeTime { get; set; }

        public string IncomeTimeReadable
        {
            get
            {
                var receiveTime = new PersianDateTime(IncomeTime);
                return $"روز {receiveTime.ToShortDateString()} ساعت {receiveTime.ToString("HH:mm")}";
            }
        }

        public string Description { get; set; }

        public float UnbrokenRice { get; set; }

        public float BrokenRice { get; set; }

        public float Flour { get; set; }

        public string IncomeDetail
        {
            get
            {
                var sbDetail = new StringBuilder();
                if (UnbrokenRice > 0)
                    sbDetail.Append($"{UnbrokenRice} ک برنج بلند,");

                if (BrokenRice > 0)
                    sbDetail.Append($" {BrokenRice} ک برنج نیمه,");

                if (Flour > 0)
                    sbDetail.Append($" {Flour} ک آرد,");

                return sbDetail.Remove(sbDetail.Length - 1, 1).ToString().TrimStart();
            }
        }

        //[SwaggerExclude]
        //public DtoRiceThreshing RiceThreshing { get; set; }
    }
}