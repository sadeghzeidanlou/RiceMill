using RiceMill.Domain.Models.BaseModels;

namespace RiceMill.Domain.Models
{
    public sealed class Village : EventBaseModelWithUserAndRiceMill
    {
        /// <summary>
        /// Title of <see cref="Village"/>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Collection of <see cref="InputLoad"/> that come from this <see cref="Village"/>
        /// </summary>
        public ICollection<InputLoad> InputLoads { get; set; }
    }
}