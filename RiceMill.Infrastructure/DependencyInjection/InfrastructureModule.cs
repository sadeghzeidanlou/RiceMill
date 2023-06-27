using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Infrastructure.Caching;

namespace RiceMill.Infrastructure.DependencyInjection
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ICacheService, MemoryCacheService>();

            return services;
        }
    }
}