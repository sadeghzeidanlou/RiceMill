using MD.PersianDateTime.Standard;
using RiceMill.Application.Common.Interfaces;
using Serilog;
using Serilog.Exceptions;

namespace RiceMill.Application.Common.Implementations
{
    public class LoggingService : ILoggingService
    {
        public LoggingService()
        {
            var template = "{@PersianDate}{NewLine}[{Level}]{NewLine}{Message}{NewLine}{Exception}{NewLine}************************************{NewLine}";
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("PersianDate", string.Empty)
                .WriteTo.File("Logs/Log-.log",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: template)
                .CreateLogger();
        }

        public void Information(string message) => GetContext().Information(message);

        public void Warning(string message) => GetContext().Warning(message);

        public void Error(string message) => GetContext().Error(message);

        public void Error(string message, Exception ex) => GetContext().Error(ex, message);

        private ILogger GetContext() => Log.ForContext("PersianDate", PersianDateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss   dddd"));
    }
}