using Mapster;
using RiceMill.Application.Common.Models.Resource;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Ui.Common;
using RiceMill.Ui.Common.Models;
using Shared.ExtensionMethods;
using Shared.UtilityMethods;
using System.Collections;
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
            using var client = CreateHttpClient(sendRequest);
            AddQueryString(requestObject, sendRequest);
            var requestUri = BuildRequestUri(sendRequest);
            var requestMessage = CreateRequestMessage(sendRequest.HttpMethod, requestUri, requestObject);
            using var response = await client.SendAsync(requestMessage);
            var responseText = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(responseText))
                return responseText.DeserializeObject<TOut>();

            var result = responseText.DeserializeObject<TOut>();
            var defaultReturn = new Result<object>();
            var finalResult = result.Adapt(defaultReturn);
            throw new ApplicationException(string.Join(Environment.NewLine, finalResult.Errors.Select(e => e.Message)));
        }

        private static HttpClient CreateHttpClient(DtoSendRequest sendRequest)
        {
            var client = new HttpClient { Timeout = TimeSpan.FromSeconds(sendRequest.TimeOutInSecond) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(SharedResource.JsonContentTypeName));
            AddSecurityHeader(client.DefaultRequestHeaders);
            if (!string.IsNullOrEmpty(ApplicationStaticContext.Token))
                client.DefaultRequestHeaders.Add(SharedResource.AuthorizationKeyName, $"bearer {ApplicationStaticContext.Token}");

            return client;
        }

        private static void AddSecurityHeader(HttpRequestHeaders headers)
        {
            if (!headers.Contains(SharedResource.SecurityHeaderName))
            {
                var securityHeaderValue = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss").EncryptStringAes(SharedResource.EncryptDecryptKey);
                headers.Add(SharedResource.SecurityHeaderName, securityHeaderValue);
            }
        }

        private static Uri BuildRequestUri(DtoSendRequest sendRequest)
        {
            var query = MakeQueryString(sendRequest);
            return new UriBuilder($"{ApplicationStaticContext.ApiBaseAddress}{sendRequest.MethodName}{query}").Uri;
        }

        private static HttpRequestMessage CreateRequestMessage(HttpMethod httpMethod, Uri requestUri, object requestObject)
        {
            var requestMessage = new HttpRequestMessage(httpMethod, requestUri);
            if (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put)
            {
                var content = MakeBodyContent(requestObject);
                requestMessage.Content = content;
            }
            return requestMessage;
        }

        private static StringContent MakeBodyContent<TIn>(TIn requestObject) => new(requestObject.SerializeObject(), Encoding.UTF8, SharedResource.JsonContentTypeName);

        private static string MakeQueryString(DtoSendRequest sendRequest)
        {
            if (sendRequest.HttpMethod == HttpMethod.Post || sendRequest.HttpMethod == HttpMethod.Put || sendRequest.QueryString.IsCollectionNullOrEmpty())
                return string.Empty;

            var query = string.Join("&", sendRequest.QueryString.Select(parameter => $"{parameter.Key}={parameter.Value}"));
            return "?" + query;
        }

        private static void AddQueryString<TIn>(TIn filter, DtoSendRequest sendRequest)
        {
            if (sendRequest.HttpMethod == HttpMethod.Post || sendRequest.HttpMethod == HttpMethod.Put || filter == null)
                return;

            var queryParams = new Dictionary<string, string>();
            var properties = filter.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(filter);
                if (value != null)
                {
                    if (IsCollectionProperty(property.PropertyType))
                    {
                        var collectionValues = (IEnumerable)value;
                        foreach (var item in collectionValues)
                            queryParams[property.Name] = item.ToString();
                    }
                    else
                    {
                        queryParams[property.Name] = value.ToString();
                    }
                }
            }
            sendRequest.QueryString = queryParams;
        }

        public static bool IsCollectionProperty(Type propertyType) => typeof(IEnumerable).IsAssignableFrom(propertyType) && propertyType != typeof(string);
    }
}