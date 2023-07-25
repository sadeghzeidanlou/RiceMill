using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Application.Common.Interfaces
{
    public interface ILoggingService
    {
        public void Information(string message);

        public void Warning(string message);

        public void Error(string message);

        public void Error(string message, Exception ex);
    }
}