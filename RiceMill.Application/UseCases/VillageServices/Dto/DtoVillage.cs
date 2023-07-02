using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Application.UseCases.VillageServices.Dto
{
    public class DtoVillage : EventBaseModelWithUserAndRiceMill
    {
        /// <summary>
        /// Title of <see cref="Village"/>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Collection of <see cref="InputLoad"/> that come from this <see cref="Village"/>
        /// </summary>
        public ICollection<DtoInputLoad> InputLoads { get; set; }
    }
}