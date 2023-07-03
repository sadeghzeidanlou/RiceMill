using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Domain.Models.BaseModels;
using Shared.Attributes;

namespace RiceMill.Application.UseCases.VillageServices.Dto
{
    public class DtoVillage : EventBaseModelWithUserAndRiceMill
    {
        public string Title { get; set; }

        [SwaggerExclude]
        public ICollection<DtoInputLoad> InputLoads { get; set; }
    }
}