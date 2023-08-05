using RiceMill.Application.Common.Models.ResultObject;
using Shared.ExtensionMethods;

namespace RiceMill.Api.Middleware
{
    public class HttpStatusMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpStatusMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Replace the original response stream with a custom stream
            var originalBodyStream = context.Response.Body;
            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            // Continue with the request pipeline
            await _next(context);

            // Get the response body from the custom stream
            responseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();

            // Deserialize the response body into a Result<T> object
            var result = responseBody.DeserializeObject<Result<object>>();

            // Set the HTTP status code based on the HttpStatusCode property of the Result<T> object
            context.Response.StatusCode = (int)result.HttpStatusCode;

            // Write the response body back to the original response stream
            responseBodyStream.Seek(0, SeekOrigin.Begin);
            await responseBodyStream.CopyToAsync(originalBodyStream);
            context.Response.Body = originalBodyStream;
        }
    }
}