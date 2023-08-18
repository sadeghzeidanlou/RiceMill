using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.DeliveryServices.Dto
{
    public sealed class DtoDeliveryFilter : PagingInfo
    {
        public Guid? Id { get; set; }

        public List<Guid> Ids { get; set; }

        public DateTime? DeliveryTimeLower { get; set; }

        public DateTime? DeliveryTime { get; set; }

        public DateTime? DeliveryTimeGreater { get; set; }

        public float? UnbrokenRiceLower { get; set; }

        public float? UnbrokenRice { get; set; }

        public float? UnbrokenRiceGreater { get; set; }

        public float? BrokenRiceLower { get; set; }

        public float? BrokenRice { get; set; }

        public float? BrokenRiceGreater { get; set; }

        public float? ChickenRiceLower { get; set; }

        public float? ChickenRice { get; set; }

        public float? ChickenRiceGreater { get; set; }

        public float? FlourLower { get; set; }

        public float? Flour { get; set; }

        public float? FlourGreater { get; set; }

        public string Description { get; set; }

        public Guid? DelivererPersonId { get; set; }

        public Guid? ReceiverPersonId { get; set; }

        public Guid? CarrierPersonId { get; set; }

        public Guid? VehicleId { get; set; }

        public Guid? RiceMillId { get; set; }
    }
}