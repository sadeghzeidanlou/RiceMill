using RiceMill.Application.UseCases.BaseDto;
using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Application.UseCases.VillageServices.Dto
{
    public sealed class DtoVillage : DtoEventBaseWithUserAndRiceMill
    {
        public string Title { get; set; }

        //[SwaggerExclude]
        //public ICollection<DtoInputLoad> InputLoads { get; set; }
    }
}