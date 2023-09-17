using MD.PersianDateTime.Standard;
using RiceMill.Application.UseCases.BaseDto;
using Shared.Enums;

namespace RiceMill.Application.UseCases.DryerHistoryServices.Dto
{
    public sealed class DtoDryerHistory : DtoEventBaseWithUserAndRiceMill
    {
        public DryerOperationEnum Operation { get; set; }

        public string OperationHumaneReadable => $"{DryerOperation.GetAll.FirstOrDefault(x => x.Operation == Operation)?.Title}";

        public DateTime StartTime { get; set; }

        public string StartTimeReadable
        {
            get
            {
                var startTime = new PersianDateTime(StartTime);
                return $"روز {startTime.ToShortDateString()} ساعت {startTime.ToString("HH:mm")}";
            }
        }

        public DateTime? EndTime { get; set; }

        public string EndTimeReadable
        {
            get
            {
                if (!EndTime.HasValue)
                    return string.Empty;

                var endTime = new PersianDateTime(EndTime.Value);
                return $"روز {endTime.ToShortDateString()} ساعت {endTime.ToString("HH:mm")}";
            }
        }

        public Guid DryerId { get; set; }

        public string DryerTitle { get; set; }

        public string DryerHistoryReadable { get; set; }

        //[SwaggerExclude]
        //public DtoDryer Dryer { get; set; }

        //public Guid? RiceThreshingId { get; set; }

        //[SwaggerExclude]
        //public DtoRiceThreshing RiceThreshing { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoInputLoad> InputLoads { get; set; }

        public Guid InputLoadId { get; set; }

        public string InputLoadReadable { get; set; }
    }
}