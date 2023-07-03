using Microsoft.OpenApi.Models;
using RiceMill.Api.Filter;

namespace RiceMill.Api.Swagger
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
                 {
                     c.SwaggerDoc("v1", new OpenApiInfo
                     {
                         Title = "Rice Mill Api",
                         Version = "v1",
                         Contact = new OpenApiContact
                         {
                             Name = "Sadegh Zeidanlou",
                             Email = "SadeghZeidan@gmail.com"
                         }
                     });
                     var securitySchema = new OpenApiSecurityScheme
                     {
                         Description = "JWT Auth Bearer Scheme",
                         Name = "Authorization",
                         In = ParameterLocation.Header,
                         Type = SecuritySchemeType.Http,
                         Scheme = "Bearer",
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                         }
                     };
                     c.AddSecurityDefinition("Bearer", securitySchema);
                     c.AddSecurityRequirement(new OpenApiSecurityRequirement { { securitySchema, Array.Empty<string>() } });
                     c.SchemaFilter<SwaggerExcludeFilter>();
                 });

            return services;
        }
    }
}