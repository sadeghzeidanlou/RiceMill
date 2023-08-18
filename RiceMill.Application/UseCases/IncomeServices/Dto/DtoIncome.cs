using RiceMill.Application.UseCases.BaseDto;

namespace RiceMill.Application.UseCases.IncomeServices.Dto
{
    public sealed class DtoIncome : DtoEventBaseWithUserAndRiceMill
    {
        public DateTime IncomeTime { get; set; }

        public string Description { get; set; }

        public float UnbrokenRice { get; set; }

        public float BrokenRice { get; set; }

        public float Flour { get; set; }

        //[SwaggerExclude]
        //public DtoRiceThreshing RiceThreshing { get; set; }
    }
}