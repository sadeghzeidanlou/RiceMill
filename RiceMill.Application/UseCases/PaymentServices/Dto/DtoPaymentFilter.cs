using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.PaymentServices.Dto
{
    public class DtoPaymentFilter : PagingInfo
    {
        public Guid? Id { get; set; }
        
        public DateTime? PaymentTime { get; set; }
        
        public string Description { get; set; }
        
        public float? UnbrokenRice { get; set; }
        
        public float? BrokenRice { get; set; }
        
        public float? Flour { get; set; }
        
        public int? Money { get; set; }
        
        public Guid? PaidPersonId { get; set; }
        
        public Guid? ConcernId { get; set; }
        
        public Guid? InputLoadId { get; set; }
        
        public Guid? RiceMillId { get; set; }
    }
}