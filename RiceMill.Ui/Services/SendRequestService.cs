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
        public SendRequestService() { }

        public async Task<TOut> SendRequestAsync<TIn, TOut>(TIn requestObject, DtoSendRequest sendRequest)
            where TIn : class
            where TOut : class
        {
            using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(sendRequest.TimeOutInSecond) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(SharedResource.JsonContentTypeName));
            foreach (var customHeader in sendRequest.CustomHeaders)
                client.DefaultRequestHeaders.Add(customHeader.Key, customHeader.Value);

            if (ApplicationStaticContext.Token.IsNotNullOrEmpty())
                client.DefaultRequestHeaders.Add(SharedResource.AuthorizationKeyName, ApplicationStaticContext.Token);

            var query = MakeQueryString(sendRequest.QueryString);
            var content = MakeBodyContent(requestObject);
            try
            {
                var requestUri = new UriBuilder($"{ApplicationStaticContext.ApiBaseAddress}{sendRequest.MethodName}{query}").Uri;
                using var response = await client.SendAsync(new HttpRequestMessage(sendRequest.HttpMethod, requestUri) { Content = content });
                var responseText = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    // TODO: Handle internal server error
                    // throw new RequestException(response.StatusCode, responseText, response.Content.ToString(), statusCode);
                }
                if (responseText.IsNotNullOrEmpty())
                    return responseText.DeserializeObject<TOut>();

                return default;
            }
            catch (Exception ex)
            {
                // TODO: Handle exceptions or log errors
                throw;
            }
        }

        private static StringContent MakeBodyContent<TIn>(TIn requestObject) => new(requestObject.SerializeObject(), Encoding.UTF8, SharedResource.JsonContentTypeName);

        private static string MakeQueryString(Dictionary<string, string> parameters)
        {
            if (parameters.IsCollectionNullOrEmpty())
                return string.Empty;

            var query = string.Join("&", parameters.Select(parameter => $"{parameter.Key}={parameter.Value}"));
            return "?" + query;
        }
    }
}