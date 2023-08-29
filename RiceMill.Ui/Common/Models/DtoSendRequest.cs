using System.Reflection;

namespace RiceMill.Ui.Common.Models
{
    public class DtoSendRequest
    {
        public DtoSendRequest(string methodName, HttpMethod httpMethod)
        {
            MethodName = methodName;
            HttpMethod = httpMethod;
            QueryString = new Dictionary<string, string>();
        }

        public DtoSendRequest(string methodName, HttpMethod httpMethod, Dictionary<string, string> queryString)
        {
            MethodName = methodName;
            HttpMethod = httpMethod;
            QueryString = queryString;
        }

        public string MethodName { get; set; }

        public Dictionary<string, string> QueryString;

        public HttpMethod HttpMethod { get; set; }

        public byte TimeOutInSecond { get; set; } = 60;
    }
}