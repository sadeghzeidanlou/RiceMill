using Microsoft.Extensions.DependencyInjection;
using RiceMill.Application.UseCases.ConcernServices;

namespace RiceMill.Application.DependencyInjection
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IConcernCommands, ConcernCommands>();
            services.AddTransient<IConcernQueries, ConcernQueries>();

            return services;
        }
    }
}