using Microsoft.IdentityModel.Tokens;
using RiceMill.Api.Configurations.Jwt;
using RiceMill.Api.Services.Interfaces;
using RiceMill.Application.Common.Models.Resource;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RiceMill.Api.Services
{
    public class JwtService : IJwtService
    {
        public string GenerateToken(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(SharedResource.TokenClaimUserIdName, userId.ToString()) }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(JwtConfiguration.Key), SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = DateTime.Now,
                Issuer = SharedResource.Issuer,
                Audience = SharedResource.Audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = JwtConfiguration.TokenValidationParameters;
            try
            {
                return tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}