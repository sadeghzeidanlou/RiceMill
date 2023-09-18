using MD.PersianDateTime.Standard;
using RiceMill.Application.UseCases.BaseDto;
using System.Text;

namespace RiceMill.Application.UseCases.DeliveryServices.Dto
{
    public sealed class DtoDelivery : DtoEventBaseWithUserAndRiceMill
    {
        public short UnbrokenRice { get; set; }

        public short BrokenRice { get; set; }

        public short ChickenRice { get; set; }

        public short Flour { get; set; }

        public DateTime DeliveryTime { get; set; }

        public string DeliveryTimeReadable
        {
            get
            {
                var deliveryTime = new PersianDateTime(DeliveryTime);
                return $"روز {deliveryTime.ToShortDateString()} ساعت {deliveryTime.ToString("HH:mm")}";
            }
        }

        public string Description { get; set; }

        public Guid DelivererPersonId { get; set; }

        //[SwaggerExclude]
        //public DtoPerson DelivererPerson { get; set; }

        public Guid ReceiverPersonId { get; set; }

        //[SwaggerExclude]
        //public DtoPerson ReceiverPerson { get; set; }

        public Guid CarrierPersonId { get; set; }

        //[SwaggerExclude]
        //public DtoPerson CarrierPerson { get; set; }

        public Guid VehicleId { get; set; }


        //[SwaggerExclude]
        //public DtoVehicle Vehicle { get; set; }

        public Guid RiceThreshingId { get; set; }

        //[SwaggerExclude]
        //public DtoRiceThreshing RiceThreshing { get; set; }

        public string DeliveryInfo
        {
            get
            {
                var sbDetail = new StringBuilder();
                if (UnbrokenRice > 0)
                    sbDetail.Append($"{UnbrokenRice} ک بلند,");

                if (BrokenRice > 0)
                    sbDetail.Append($" {BrokenRice} ک نیمه,");

                if (Flour > 0)
                    sbDetail.Append($" {Flour} ک آرد,");

                if (ChickenRice > 0)
                    sbDetail.Append($" {ChickenRice} ک مرغی,");

                return sbDetail.Remove(sbDetail.Length - 1, 1).ToString().TrimStart();
            }
        }
    }
}