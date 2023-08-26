using RiceMill.Application.Common.Models.Resource;
using Shared.UtilityMethods;
using System.Globalization;

namespace RiceMill.Api.Middleware
{
    public class RequestHeaderInspectorMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestHeaderInspectorMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(SharedResource.SecurityHeaderName, out var encryptedHeaderValue))
                throw new Exception("Unauthorized: Missing required header");

#pragma warning disable CS8604 // Possible null reference argument.
            if (!IsValidHeaderValue(encryptedHeaderValue))
                throw new Exception("Unauthorized: Missing required header");
#pragma warning restore CS8604 // Possible null reference argument.

            await _next(context);
        }

        private static bool IsValidHeaderValue(string encryptedHeaderValue)
        {
            var decryptedHeaderValue = encryptedHeaderValue.DecryptStringAes(SharedResource.EncryptDecryptKey).Replace(SharedResource.Audience, string.Empty);
            DateTime receivedTimestamp = DateTime.ParseExact(decryptedHeaderValue, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            TimeSpan maxTimeDifference = TimeSpan.FromSeconds(5);
            var currentData = DateTime.UtcNow;
            return Math.Abs((currentData - receivedTimestamp).TotalSeconds) <= maxTimeDifference.TotalSeconds;
        }
    }
}