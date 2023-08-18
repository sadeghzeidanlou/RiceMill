using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.InputLoadServices.Dto
{
    public sealed class DtoInputLoadFilter : PagingInfo
    {
        public Guid? Id { get; set; }

        public List<Guid> Ids { get; set; }

        public short? NumberOfBagsLower { get; set; }

        public short? NumberOfBags { get; set; }

        public short? NumberOfBagsGreater { get; set; }

        public short? NumberOfBagsInDryerLower { get; set; }

        public short? NumberOfBagsInDryer { get; set; }

        public short? NumberOfBagsInDryerGreater { get; set; }

        public bool? IsCompletelyInDryer { get; set; }

        public string Description { get; set; }

        public DateTime? ReceiveTimeLower { get; set; }

        public DateTime? ReceiveTime { get; set; }

        public DateTime? ReceiveTimeGreater { get; set; }

        public Guid? VillageId { get; set; }

        public Guid? DelivererPersonId { get; set; }

        public Guid? ReceiverPersonId { get; set; }

        public Guid? CarrierPersonId { get; set; }

        public Guid? OwnerPersonId { get; set; }

        public Guid? VehicleId { get; set; }

        public Guid? RiceMillId { get; set; }
    }
}