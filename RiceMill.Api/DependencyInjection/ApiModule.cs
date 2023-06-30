using RiceMill.Api.Services;
using RiceMill.Application.Common.Interfaces;

namespace RiceMill.Api.DependencyInjection
{
    public static class ApiModule
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddTransient<ICurrentRequestService, CurrentRequest>();

            return services;
        }
    }
}