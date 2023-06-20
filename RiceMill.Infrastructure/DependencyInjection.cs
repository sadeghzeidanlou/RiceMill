using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RiceMill.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}