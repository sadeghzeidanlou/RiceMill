using MD.PersianDateTime.Standard;
using Microsoft.AspNetCore.Http;
using RiceMill.Application.Common.Interfaces;
using Serilog;
using Serilog.Exceptions;

namespace RiceMill.Application.Common.Implementations
{
    public sealed class LoggingService : ILoggingService
    {
        private static readonly string _errorLogAddress = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\Error\Error-.Log";
        private static readonly string _warningLogAddress = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\Warning\Warning-.Log";
        private static readonly string _exceptionLogAddress = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\Exception\Exception-.Log";
        private static readonly string _informationLogAddress = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\Information\Information-.Log";
        private static readonly string _unauthorizedRequestLogAddress = AppDomain.CurrentDomain.BaseDirectory + @"\Logs\Unauthorized\UnauthorizedRequests-.Log";

        private readonly ILogger _errorLogger;
        private readonly ILogger _warningLogger;
        private readonly ILogger _exceptionLogger;
        private readonly ILogger _informationLogger;
        private readonly ILogger _unauthorizedRequestsLogger;

        public LoggingService()
        {
            _errorLogger = new LoggerConfiguration()
               .WriteTo.File(_errorLogAddress, rollingInterval: RollingInterval.Day, outputTemplate: GetGeneralTemplate())
               .Enrich.WithExceptionDetails()
               .CreateLogger();

            _warningLogger = new LoggerConfiguration()
                .WriteTo.File(_warningLogAddress, rollingInterval: RollingInterval.Day, outputTemplate: GetGeneralTemplate())
                .Enrich.WithExceptionDetails()
                .CreateLogger();

            _exceptionLogger = new LoggerConfiguration()
                .WriteTo.File(_exceptionLogAddress, rollingInterval: RollingInterval.Day, outputTemplate: GetExceptionTemplate())
                .Enrich.WithExceptionDetails()
                .CreateLogger();

            _informationLogger = new LoggerConfiguration()
                .WriteTo.File(_informationLogAddress, rollingInterval: RollingInterval.Day, outputTemplate: GetGeneralTemplate())
                .Enrich.WithExceptionDetails()
                .CreateLogger();

            _unauthorizedRequestsLogger = new LoggerConfiguration()
                .WriteTo.File(_unauthorizedRequestLogAddress, rollingInterval: RollingInterval.Day, outputTemplate: GetGeneralTemplate())
                .Enrich.WithExceptionDetails()
                .CreateLogger();
        }

        public void Error(string message)
        {
            var logMessage = "An error has occurred.\n" +
                "Error details: {0}";
            GetLoggerWithPersianDate(_errorLogger).Error(logMessage, message);
        }

        public void Error(string message, Exception ex)
        {
            var logMessage = "An exception was thrown.\n" +
                "Error details: {0}\n" +
                "Exception details:";
            GetLoggerWithPersianDate(_exceptionLogger).Error(ex, logMessage, message);
        }

        public void Error(Exception ex)
        {
            var logMessage = "An exception was thrown.\n" +
                "Exception details:";
            GetLoggerWithPersianDate(_exceptionLogger).Error(ex, logMessage);
        }

        public void UnauthorizedRequest(HttpContext httpContext)
        {
            var message = "Unauthorized request submitted.\n" +
                "Client ip address: {0}";
            GetLoggerWithPersianDate(_unauthorizedRequestsLogger).Error(message, httpContext.Connection.RemoteIpAddress);
        }

        public void Warning(string message) => GetLoggerWithPersianDate(_warningLogger).Warning(message);

        public void Information(string message) => GetLoggerWithPersianDate(_informationLogger).Information(message);

        private static string GetGeneralTemplate()
        {
            return "Date: {Timestamp:yyyy/MM/dd} | {@PersianDate}{NewLine}" +
                 "Time: {Timestamp:HH:mm:ss}{NewLine}" +
                 "[{Level}]{NewLine}" +
                 "{Message}{NewLine}" +
                 "**************************************************************{NewLine}{NewLine}";
        }

        private static string GetExceptionTemplate()
        {
            return "Date: {Timestamp:yyyy/MM/dd} | {@PersianDate}{NewLine}" +
                "Time: {Timestamp:HH:mm:ss}{NewLine}" +
                "[{Level}]{NewLine}" +
                "{Message}{NewLine}" +
                "{Exception}{NewLine}" +
                "**************************************************************{NewLine}{NewLine}";
        }

        private static ILogger GetLoggerWithPersianDate(ILogger logger) => logger.ForContext("PersianDate", PersianDateTime.Now.ToShortDateString());
    }
}