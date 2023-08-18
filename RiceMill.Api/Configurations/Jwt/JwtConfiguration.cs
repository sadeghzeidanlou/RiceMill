using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RiceMill.Application.Common.Models.Resource;
using System.Security.Cryptography;
using System.Text;

namespace RiceMill.Api.Configurations.Jwt
{
    public static class JwtConfiguration
    {
        public static readonly byte[] Key = ConvertPasswordToKey(SharedResource.TokenKey, 256);

        public static readonly TokenValidationParameters TokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuer = SharedResource.Issuer,
            ValidAudience = SharedResource.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Key)
        };

        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = TokenValidationParameters;
            });
            return services;
        }

        private static byte[] ConvertPasswordToKey(string password, int keySizeInBits)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashedBytes = SHA256.HashData(passwordBytes);
            Array.Resize(ref hashedBytes, keySizeInBits / 8);
            return hashedBytes;
        }
    }
}