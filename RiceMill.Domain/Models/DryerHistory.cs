using RiceMill.Domain.Enums;
using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    public sealed class DryerHistory : EventBaseModelWithUserAndRiceMill
    {
        public DryerOperationEnum Operation { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? StopTime { get; set; }

        public int DryerId { get; set; }
        public Dryer Dryer { get; set; }

        public ICollection<Person> PeoplePresent { get; set; }

        public ICollection<InputLoad> InputLoads { get; set; }
    }
}