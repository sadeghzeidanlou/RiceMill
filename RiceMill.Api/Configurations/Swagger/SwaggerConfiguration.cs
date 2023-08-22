using Microsoft.OpenApi.Models;
using RiceMill.Api.Filter;
using RiceMill.Application.Common.Models.Resource;
using System.Reflection;

namespace RiceMill.Api.Configurations.Swagger
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
                 {
                     s.SwaggerDoc("v1", new OpenApiInfo
                     {
                         Title = "Rice Mill Api",
                         Version = "v1",
                         Description = "Rice Mill application help managers to manage own business like Payment, Income, InputLoads and etc...",
                         TermsOfService = new Uri("https://github.com/sadeghzeidanlou/RiceMill"),
                         Contact = new OpenApiContact
                         {
                             Name = "Sadegh Zeidanlou",
                             Email = "SadeghZeidan@gmail.com",
                             Url = new Uri("https://github.com/sadeghzeidanlou/RiceMill")
                         }
                     });
                     var securitySchema = new OpenApiSecurityScheme
                     {
                         Description = "JWT Auth Bearer Scheme",
                         Name = SharedResource.AuthorizationKeyName,
                         In = ParameterLocation.Header,
                         Type = SecuritySchemeType.Http,
                         Scheme = "Bearer",
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                         }
                     };
                     s.AddSecurityDefinition("Bearer", securitySchema);
                     s.AddSecurityRequirement(new OpenApiSecurityRequirement { { securitySchema, Array.Empty<string>() } });
                     s.SchemaFilter<SwaggerExcludeFilter>();
                     var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                     var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                     s.IncludeXmlComments(xmlPath);
                 });

            return services;
        }
    }
}