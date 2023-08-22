using RiceMill.Application.Common.Models.Resource;
using RiceMill.Ui.Common;
using RiceMill.Ui.Common.Models;
using Shared.ExtensionMethods;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace RiceMill.Ui.Services
{
    public class SendRequestService : ISendRequestService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SendRequestService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public TOut SendRequest<TIn, TOut>(TIn requestObject, DtoSendRequest sendRequest) where TIn : class where TOut : class
        {
            using HttpClient client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(sendRequest.TimeOutInSecond);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(SharedResource.JsonContentTypeName));
            foreach (var customHeader in sendRequest.CustomHeaders)
                client.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);

            var getContent = MakeQueryString(sendRequest.QueryString);
            var postContent = MakeBodyContent(requestObject);
            using var httpMessage = new HttpRequestMessage(sendRequest.HttpMethod, sendRequest.MethodName) { Content = postContent };
            httpMessage.Headers.Add(SharedResource.AuthorizationKeyName, ApplicationStaticContext.Token);
            try
            {
                using var response = client.Send(httpMessage);
                var responseText = response.Content.ReadAsStringAsync().Result;
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    //TODO When server get back internal server error should manage
                    //throw new RequestException(response.StatusCode, responseText, response.Content.ToString(), statusCode);
                }
                if (responseText.IsNullOrEmpty())
                    return default;

                return responseText.DeserializeObject<TOut>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static StringContent MakeBodyContent<TIn>(TIn requestObject) => new(requestObject.SerializeObject(), Encoding.UTF8, SharedResource.JsonContentTypeName);

        private static string MakeQueryString(Dictionary<string, string> parameters)
        {
            if (parameters.IsCollectionNullOrEmpty())
                return string.Empty;

            var sbQueryString = new StringBuilder("?");
            foreach (var parameter in parameters)
                sbQueryString.Append($"{parameter.Key}={parameter.Value}&");

            sbQueryString.Length--;
            return sbQueryString.ToString();
        }
    }
}