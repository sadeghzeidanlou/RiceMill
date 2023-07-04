using Mapster;
using Microsoft.Extensions.DependencyInjection;
using RiceMill.Application.UseCases.ConcernServices;
using RiceMill.Application.UseCases.RiceMillServices;
using System.Reflection;

namespace RiceMill.Application.DependencyInjection
{
    public static class ApplicationModule
    {
        /// <summary>
        /// Register all application services and modules
        /// </summary>
        /// <param name="services">Service collection for dependency</param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IConcernCommands, ConcernCommands>();
            services.AddTransient<IConcernQueries, ConcernQueries>();

            services.AddTransient<IRiceMillCommands, RiceMillCommands>();
            services.AddTransient<IRiceMillQueries, RiceMillQueries>();

            AddMapsTerConfig();

            return services;
        }

        /// <summary>
        /// Register custom config of mapsTer
        /// This model of writing because spell checking
        /// </summary>
        /// <param name="services">Service collection</param>
        private static void AddMapsTerConfig() => TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}