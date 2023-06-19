using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    public sealed class Village : EventBaseModelWithUserAndRiceMill
    {
        public string Title { get; set; }

        public ICollection<InputLoad> InputLoads { get; set; }
    }
}