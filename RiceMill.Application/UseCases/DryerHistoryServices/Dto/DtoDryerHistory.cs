using RiceMill.Application.UseCases.BaseDto;
using RiceMill.Application.UseCases.DryerServices.Dto;
using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Application.UseCases.RiceThreshingServices.Dto;
using Shared.Enums;

namespace RiceMill.Application.UseCases.DryerHistoryServices.Dto
{
    public class DtoDryerHistory : DtoEventBaseWithUserAndRiceMill
    {
        public DryerOperationEnum Operation { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? StopTime { get; set; }

        public Guid DryerId { get; set; }

        public DtoDryer Dryer { get; set; }

        public Guid RiceThreshingId { get; set; }

        public DtoRiceThreshing RiceThreshing { get; set; }

        public ICollection<DtoInputLoad> InputLoads { get; set; }
    }
}