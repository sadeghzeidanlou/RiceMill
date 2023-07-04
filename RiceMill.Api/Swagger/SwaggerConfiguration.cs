using Microsoft.OpenApi.Models;
using RiceMill.Api.Filter;
using System.Reflection;

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
                     //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                     //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                     //c.IncludeXmlComments(xmlPath);
                 });

            return services;
        }
    }
}