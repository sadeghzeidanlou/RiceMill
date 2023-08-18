using RiceMill.Api.Configurations.Cors;
using RiceMill.Api.Configurations.Jwt;
using RiceMill.Api.Configurations.Swagger;
using RiceMill.Api.Configurations.Versioning;
using RiceMill.Api.DependencyInjection;
using RiceMill.Api.Middleware;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.DependencyInjection;
using RiceMill.Infrastructure.DependencyInjection;
using RiceMill.Persistence.DependencyInjection;
using Shared.Enums;
using Shared.ExtensionMethods;

namespace RiceMill.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddEndpointsApiExplorer()
                .AddPersistenceServices(builder.Configuration)
                .AddInfrastructureServices()
                .AddApplicationServices()
                .AddApiServices()
                .AddSwaggerConfiguration()
                .AddApiVersioningConfiguration()
                .AddCorsConfiguration()
                .AddJwtConfiguration()
                .AddMemoryCache()
                .AddControllers();

            var app = builder.Build();
            var cacheService = app.Services.GetService<ICacheService>();
            cacheService?.LoadCache(EnumMethods.GetList<EntityTypeEnum>());
            app.AddMiddlewares();
            app.Run();
        }
    }
}