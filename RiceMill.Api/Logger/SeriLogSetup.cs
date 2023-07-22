using Serilog;

namespace RiceMill.Api.Logger
{
    public static class SeriLogSetup
    {
        public static void AddSeriLog(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}