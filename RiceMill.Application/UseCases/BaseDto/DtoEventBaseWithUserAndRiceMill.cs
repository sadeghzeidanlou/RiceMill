using RiceMill.Application.UseCases.RiceMillServices.Dto;
using RiceMill.Application.UseCases.UserServices.Dto;

namespace RiceMill.Application.UseCases.BaseDto
{
    public class DtoEventBaseWithUserAndRiceMill : DtoEventBase
    {
        /// <summary>
        /// This property used for reference navigation between any class and RiceMill
        /// </summary>
        public Guid RiceMillId { get; set; }

        /// <summary>
        /// This property Contain <see cref="RiceMill"/> detail in any other classes that have relation with <see cref="RiceMill"/>
        /// </summary>
        public DtoRiceMill RiceMill { get; set; }

        /// <summary>
        /// This property used for reference navigation between any class and User
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// This property Contain <see cref="User"/> detail in any other classes that have relation with <see cref="User"/>
        /// </summary>
        public DtoUser User { get; set; }
    }
}