using System.Collections.Generic;

namespace RiceMill.Application.Common.Models.ExternalDto
{
    public class DtoSmsDetail
    {
        public string Message { get; set; }
        public string From { get; set; }
        public List<string> ToWhom { get; set; }
        public string Subject { get; set; }
        public DateTime TimeToSend { get; set; }
    }
}