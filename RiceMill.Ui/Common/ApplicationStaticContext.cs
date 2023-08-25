namespace RiceMill.Ui.Common
{
    public class ApplicationStaticContext
    {
        public static string Token { get; set; }

        public static Uri ApiBaseAddress { get; set; } = new Uri("https://localhost:7085/");

        public static readonly double ToastMessageSize = 20.0;
    }
}