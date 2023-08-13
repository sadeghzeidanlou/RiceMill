using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.PaymentServices.Dto
{
    public class DtoPaymentFilter : PagingInfo
    {
        public Guid? Id { get; set; }

        public List<Guid> Ids { get; set; }

        public DateTime? PaymentTimeLower { get; set; }

        public DateTime? PaymentTime { get; set; }

        public DateTime? PaymentTimeGreater { get; set; }

        public string Description { get; set; }
        
        public float? UnbrokenRiceLower { get; set; }
        
        public float? UnbrokenRice { get; set; }
        
        public float? UnbrokenRiceGreater { get; set; }
        
        public float? BrokenRiceLower { get; set; }
        
        public float? BrokenRice { get; set; }
        
        public float? BrokenRiceGreater { get; set; }
        
        public float? FlourLower { get; set; }
        
        public float? Flour { get; set; }
        
        public float? FlourGreater { get; set; }
        
        public int? MoneyLower { get; set; }
        
        public int? Money { get; set; }
        
        public int? MoneyGreater { get; set; }
        
        public Guid? PaidPersonId { get; set; }
        
        public Guid? ConcernId { get; set; }
        
        public Guid? InputLoadId { get; set; }
        
        public Guid? RiceMillId { get; set; }
    }
}