using System.Net;
using System.Text.Json;

namespace RiceMill.Api.Middleware
{
    public class HttpStatusMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpStatusMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            await _next(context);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
            var result = JsonSerializer.Deserialize<JsonElement>(responseBody, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            if (result.TryGetProperty("httpStatusCode", out var httpStatusCodeElement) && Enum.TryParse<HttpStatusCode>(httpStatusCodeElement.GetRawText(), out var httpStatusCode))
                context.Response.StatusCode = (int)httpStatusCode;

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            await responseBodyStream.CopyToAsync(originalBodyStream);
            context.Response.Body = originalBodyStream;
        }
    }
}