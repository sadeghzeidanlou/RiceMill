using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Application.UseCases.VillageServices.Dto
{
    public class DtoVillage : EventBaseModelWithUserAndRiceMill
    {
        public string Title { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoInputLoad> InputLoads { get; set; }
    }
}