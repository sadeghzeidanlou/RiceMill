using RiceMill.Application.UseCases.BaseDto;
using RiceMill.Application.UseCases.RiceThreshingServices.Dto;

namespace RiceMill.Application.UseCases.IncomeServices.Dto
{
    public class DtoIncome : DtoEventBaseWithUserAndRiceMill
    {
        public DateTime IncomeTime { get; set; }

        public string Description { get; set; }

        public float UnbrokenRice { get; set; }

        public float BrokenRice { get; set; }

        public float Flour { get; set; }

        public DtoRiceThreshing RiceThreshing { get; set; }
    }
}