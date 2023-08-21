﻿using RiceMill.Application.Common.Models.Resource;
using Shared.ExtensionMethods;
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

        private bool IsValidHeaderValue(string encryptedHeaderValue)
        {
            var decryptedHeaderValue = encryptedHeaderValue.DecryptStringAes(SharedResource.HeaderTokenKey).Replace(SharedResource.Audience, string.Empty);
            DateTime receivedTimestamp = DateTime.ParseExact(decryptedHeaderValue, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            TimeSpan maxTimeDifference = TimeSpan.FromSeconds(5);
            return Math.Abs((DateTime.UtcNow - receivedTimestamp).TotalMilliseconds) <= maxTimeDifference.TotalMilliseconds;
        }
    }
}