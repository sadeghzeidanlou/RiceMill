using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    public class Concern : EventBaseModelWithUserAndRiceMill
    {
        public string Title { get; set; }

        public virtual ICollection<Payment> Payment { get; set; }

        //public virtual ICollection<Income> Incomes { get; set; }
    }
}