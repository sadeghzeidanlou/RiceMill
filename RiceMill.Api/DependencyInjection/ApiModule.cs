using Mapster;
using RiceMill.Api.Services;
using RiceMill.Api.Services.Interfaces;
using RiceMill.Application.Common.Implementations;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Mapping;

namespace RiceMill.Api.DependencyInjection
{
    public static class ApiModule
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentRequestService, CurrentRequest>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddSingleton<IRegister>(new MappingConfig());
            services.AddSingleton<ILoggingService, LoggingService>();

            return services;
        }
    }
}