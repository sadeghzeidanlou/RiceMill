using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Resource;
using RiceMill.Persistence.Caching;
using Shared.UtilityMethods;

namespace RiceMill.Persistence.DependencyInjection
{
    public static class PersistenceModule
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RiceMill").DecryptStringAes(SharedResource.EncryptDecryptKey).Replace("\\\\", "\\");
            services.AddDbContextPool<RiceMillDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(RiceMillDbContext).Assembly.FullName)));
            services.AddScoped<IApplicationDbContext, RiceMillDbContext>();
            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}