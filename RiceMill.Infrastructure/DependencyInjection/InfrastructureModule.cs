using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RiceMill.Infrastructure.DependencyInjection
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}