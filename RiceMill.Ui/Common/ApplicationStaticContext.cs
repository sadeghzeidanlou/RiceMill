using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Application.UseCases.UserServices.Dto;
using Shared.Enums;

namespace RiceMill.Ui.Common
{
    public class ApplicationStaticContext
    {
        public static string Token { get; set; }

        public static DtoUser CurrentUser { get; set; }

        public static DtoPerson CurrentPerson { get; set; }

        public static bool IsAdmin => CurrentUser.Role == RoleEnum.Admin;

        public static bool IsManager => CurrentUser.Role == RoleEnum.RiceMillManager;
        
        public static bool IsSupperUser => CurrentUser.Role == RoleEnum.SuperUser;
        
        public static bool IsUser => CurrentUser.Role == RoleEnum.User;

        public static bool HaveAccessToRiceMill => IsManager || IsAdmin;

        public static Uri ApiBaseAddress { get; set; } = new Uri("http://128.140.5.91/");

        public static readonly double ToastMessageSize = 20.0;
    }
}