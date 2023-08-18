using RiceMill.Application.Common.Models.Resource;
using Shared.ExtensionMethods;

namespace RiceMill.Api.Middleware
{
    public class RequestHeaderInspectorMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestHeaderInspectorMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(SharedResource.SecurityHeaderName, out var headerValue))
                throw new Exception("Unauthorized: Missing required header");

#pragma warning disable CS8604 // Possible null reference argument.
            if (!IsValidHeaderValue(headerValue))
                throw new Exception("Unauthorized: Missing required header");
#pragma warning restore CS8604 // Possible null reference argument.

            await _next(context);
        }

        private bool IsValidHeaderValue(string headerValue)
        {
            var decrypted = headerValue.DecryptStringAes(SharedResource.TokenKey);
            return headerValue == "ExpectedHeaderValue";
        }
    }
}