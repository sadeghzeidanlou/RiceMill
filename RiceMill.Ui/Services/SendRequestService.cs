using Mapster;
using RiceMill.Application.Common.Models.Resource;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Ui.Common;
using RiceMill.Ui.Common.Models;
using Shared.ExtensionMethods;
using Shared.UtilityMethods;
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
            AddSecurityHeader(client.DefaultRequestHeaders);
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
                if (response.StatusCode == HttpStatusCode.OK && responseText.IsNotNullOrEmpty())
                    return responseText.DeserializeObject<TOut>();

                var result = responseText.DeserializeObject<TOut>();
                var defaultReturn = new Result<object>();
                var finalResult = result.Adapt(defaultReturn);
                throw new ApplicationException(string.Join(Environment.NewLine, finalResult.Errors.Select(e => e.Message)));
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception("خطای ناشناخته ای اتفاق افتاده، لطفا دقایقی دیگر امتحان کنید");
            }
        }

        private static void AddSecurityHeader(HttpRequestHeaders header)
        {
            if (header.Contains(SharedResource.SecurityHeaderName))
                return;

            var SecurityHeaderValue = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss").EncryptStringAes(SharedResource.EncryptDecryptKey);
            header.Add(SharedResource.SecurityHeaderName, SecurityHeaderValue);
        }

        private static string MakeQueryString(Dictionary<string, string> parameters)
        {
            if (parameters.IsCollectionNullOrEmpty())
                return string.Empty;

            var query = string.Join("&", parameters.Select(parameter => $"{parameter.Key}={parameter.Value}"));
            return "?" + query;
        }

        private static StringContent MakeBodyContent<TIn>(TIn requestObject) => new(requestObject.SerializeObject(), Encoding.UTF8, SharedResource.JsonContentTypeName);
    }
}