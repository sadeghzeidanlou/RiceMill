using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    public sealed class ReceiveType : EventBaseModelWithUserAndRiceMill
    {
        public string Title { get; set; }
    }
}