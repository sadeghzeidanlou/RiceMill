using RiceMill.Application.UseCases.BaseDto;
using Shared.Enums;

namespace RiceMill.Application.UseCases.DryerHistoryServices.Dto
{
    public class DtoDryerHistory : DtoEventBaseWithUserAndRiceMill
    {
        public DryerOperationEnum Operation { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public Guid DryerId { get; set; }

        //[SwaggerExclude]
        //public DtoDryer Dryer { get; set; }

        public Guid RiceThreshingId { get; set; }

        //[SwaggerExclude]
        //public DtoRiceThreshing RiceThreshing { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoInputLoad> InputLoads { get; set; }
    }
}