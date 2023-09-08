using Microsoft.IdentityModel.Tokens;
using RiceMill.Api.Services.Interfaces;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Resource;
using Shared.UtilityMethods;
using System.Globalization;

namespace RiceMill.Api.Middleware
{
    public class RequestHeaderInspectorMiddleware
    {
        private readonly RequestDelegate _next;
        private ICurrentRequestService? _currentRequestService;
        private IJwtService? _jwtService;
        private readonly ICacheService _cacheService;
        public RequestHeaderInspectorMiddleware(RequestDelegate next, ICacheService cacheService)
        {
            _next = next;
            _cacheService = cacheService;
        }

        public async Task Invoke(HttpContext context, ICurrentRequestService currentRequestService, IJwtService jwtService)
        {
            //if (!context.Request.Headers.TryGetValue(SharedResource.SecurityHeaderName, out var encryptedHeaderValue)
            //    || encryptedHeaderValue.ToString().IsNullOrEmpty() || !IsValidHeaderValue(encryptedHeaderValue.ToString()))
            //    throw new Exception("Unauthorized: Missing required header");

            _currentRequestService = currentRequestService;
            _jwtService = jwtService;
            SetUserInfoFromContext(context);
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

        private void SetUserInfoFromContext(HttpContext context)
        {
            if (_currentRequestService == null)
                return;

            if (context.Connection.RemoteIpAddress != null)
                _currentRequestService.Ip = context.Connection.RemoteIpAddress.ToString();

            context.Request.Headers.TryGetValue(SharedResource.AuthorizationKeyName, out var tokenValue);
            if (tokenValue.ToString().IsNullOrEmpty() || _jwtService == null)
                return;

            var tokenInfo = _jwtService.ValidateToken(tokenValue.ToString().Replace("Bearer", string.Empty).Trim());
            var claim = tokenInfo?.Claims.FirstOrDefault(c => c.Type.Equals(SharedResource.TokenClaimUserIdName));
            if (claim != null)
            {
                var userId = Guid.Parse(claim.Value);
                var userInfo = _cacheService.GetUsers().FirstOrDefault(x => x.Id.Equals(userId)) ?? throw new Exception("Unauthorized: User not found");
                _currentRequestService.UserId = userId;
                _currentRequestService.UserRole = userInfo.Role;
            }
        }
    }
}