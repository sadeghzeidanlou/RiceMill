namespace RiceMill.Ui.Services
{
    public class SendRequestService : ISendRequestService
    {
        public async Task<T> MakeRequestAsync<T>(RequestBase<T> request, Uri baseAddress, string accessToken)
        {
            using (HttpClient client = new HttpClient(new HttpClientHandler()))
            {
                client.Timeout = TimeSpan.FromMinutes(20);
                //client.DefaultRequestHeaders
                //                        .Accept
                //                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                var getContent = request.QueryString();
                var postContent = request.CreateHttpContent();

                if (getContent != null)
                {
                    var path = baseAddress.AbsoluteUri;
                    for (int i = 0; i < getContent.Count; i++)
                    {
                        var item = getContent.ElementAt(i);
                        if (i == 0)
                        {
                            path = string.Format("{0}{1}?", path, request.MethodName);
                        }
                        else
                        {
                            path += "&";
                        }
                        path = string.Format("{0}{1}={2}", path, item.Key, item.Value);
                    }
                    client.BaseAddress = new Uri(path);
                    request.MethodName = "";

                }
                else
                    client.BaseAddress = baseAddress;


                using (var httpMessage = new HttpRequestMessage(HttpMethod.Get, request.MethodName))
                {

                    if (postContent != null && getContent == null)
                    {
                        httpMessage.Method = HttpMethod.Post;
                        httpMessage.Content = postContent;
                    }
                    //httpMessage.Properties["RequestTimeout"] = TimeSpan.FromMinutes(20);
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        httpMessage.Headers.Add("Authorization", accessToken);
                    }
                    try
                    {
                        using (var response = await client.SendAsync(httpMessage).ConfigureAwait(false))
                        {
                            var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                            var statusCode = 200;
                            if (!int.TryParse(response.StatusCode.ToString(), out statusCode))
                                statusCode = 200;

                            if (statusCode == 401 || statusCode == 400)
                            {
                                throw new RequestException(response.StatusCode, responseText, response.Content.ToString(), statusCode);
                            }
                            else if (statusCode >= 500)
                            {
                                response.EnsureSuccessStatusCode();
                            }
                            if (!response.IsSuccessStatusCode)
                            {
                                throw new RequestException(response.StatusCode, responseText, response.Content.ToString(), statusCode);
                            }
                            if (string.IsNullOrEmpty(responseText))
                            {
                                return (T)(object)0;
                            }

                            T result = DeserializeMessage<T>(responseText);
                            return result;
                        }
                    }
                    catch (Exception ex)
                    {
                        //throw new RequestException(ex.Message, ex.InnerException);
                        throw ex;
                    }
                }
            }
        }
        public T MakeRequest<T>(RequestBase<T> request, Uri baseAddress, string accessToken)
        {
            using (HttpClient client = new HttpClient(new HttpClientHandler()))
            {


                var getContent = request.QueryString();
                var postContent = request.CreateHttpContent();

                if (getContent != null)
                {
                    var path = baseAddress.AbsoluteUri;
                    for (int i = 0; i < getContent.Count; i++)
                    {
                        var item = getContent.ElementAt(i);
                        if (i == 0)
                        {
                            path = string.Format("{0}{1}?", path, request.MethodName);
                        }
                        else
                        {
                            path += "&";
                        }
                        path = string.Format("{0}{1}={2}", path, item.Key, item.Value);
                    }
                    client.BaseAddress = new Uri(path);
                    request.MethodName = "";

                }
                else
                    client.BaseAddress = baseAddress;


                using (var httpMessage = new HttpRequestMessage(HttpMethod.Get, request.MethodName))
                {

                    if (postContent != null && getContent == null)
                    {
                        httpMessage.Method = HttpMethod.Post;
                        httpMessage.Content = postContent;
                    }
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        httpMessage.Headers.Add("Authorization", accessToken);
                    }
                    using (var response = client.SendAsync(httpMessage).Result)
                    {
                        var responseText = response.Content.ReadAsStringAsync().Result;

                        if ((int)response.StatusCode >= 500)
                        {
                            response.EnsureSuccessStatusCode();
                        }
                        else
                            if ((int)response.StatusCode == 401 || (int)response.StatusCode == 400)
                        {
                            throw new RequestException(response.StatusCode, responseText, response.Content.ToString(), (int)response.StatusCode);
                        }

                        T result = DeserializeMessage<T>(responseText);
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new RequestException(response.StatusCode, responseText, response.Content.ToString(), (int)response.StatusCode);
                        }

                        return result;
                    }
                }
            }
        }
    }
}