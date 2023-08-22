namespace RiceMill.Ui.Common.Models
{
    public class DtoSendRequest
    {
        public string MethodName { get; set; }

        public Dictionary<string, string> CustomHeaders;

        public Dictionary<string, string> QueryString;

        public HttpMethod HttpMethod { get; set; }

        public byte TimeOutInSecond { get; set; } = 60;
    }
}