using RiceMill.Application.UseCases.UserServices.Dto;

namespace RiceMill.Ui.Common
{
    public class ApplicationStaticContext
    {
        public static string Token { get; set; }

        public static DtoUser CurrentUser { get; set; }

        public static Uri ApiBaseAddress { get; set; } = new Uri("http://128.140.5.91/");

        public static readonly double ToastMessageSize = 20.0;
    }
}