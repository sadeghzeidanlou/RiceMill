using Mapster;
using RiceMill.Api.Services;
using RiceMill.Application.Common.Implementations;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Mapping;

namespace RiceMill.Api.DependencyInjection
{
    public static class ApiModule
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddTransient<ICurrentRequestService, CurrentRequest>();
            services.AddSingleton<IRegister>(new MappingConfig());
            services.AddSingleton<ILoggingService, LoggingService>();

            return services;
        }
    }
}