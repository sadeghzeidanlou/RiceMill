using Serilog;

namespace RiceMill.Api.Logger
{
    public static class SeriLogSetup
    {
        public static void AddSeriLog(this WebApplicationBuilder builder)
        {
            var logger = new LoggerConfiguration()
                      .ReadFrom.Configuration(builder.Configuration)
                      .Enrich.FromLogContext()
                      .CreateLogger();

            builder.Host.UseSerilog(logger);
        }
    }
}