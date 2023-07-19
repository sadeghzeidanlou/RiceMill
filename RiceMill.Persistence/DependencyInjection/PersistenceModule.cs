using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Persistence.Caching;

namespace RiceMill.Persistence.DependencyInjection
{
    public static class PersistenceModule
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RiceMillDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RiceMill"), b => b.MigrationsAssembly(typeof(RiceMillDbContext).Assembly.FullName)),
                ServiceLifetime.Transient);

            services.AddScoped<IApplicationDbContext, RiceMillDbContext>();
            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}