using Microsoft.Extensions.Logging;
using RiceMill.Persistence;
using RiceMill.Infrastructure;

namespace RiceMill.Ui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddPersistenceServices(builder.Configuration).AddInfrastructureServices(builder.Configuration);
            return builder.Build();
        }
    }
}