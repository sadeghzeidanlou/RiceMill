using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace RiceMill.Api.Versioning
{
    public static class VersionConfiguration
    {
        public static IServiceCollection AddApiVersioningConfiguration(this IServiceCollection services)
        {
            services.AddApiVersioning(v =>
            {
                v.AssumeDefaultVersionWhenUnspecified = true;
                v.ReportApiVersions = true;
                v.DefaultApiVersion = new ApiVersion(1, 0);
                v.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader("api-version"));
            });

            return services;
        }
    }
}
