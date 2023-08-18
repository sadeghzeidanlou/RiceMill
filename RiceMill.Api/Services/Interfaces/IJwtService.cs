using System.Security.Claims;

namespace RiceMill.Api.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId);

        ClaimsPrincipal ValidateToken(string token);
    }
}