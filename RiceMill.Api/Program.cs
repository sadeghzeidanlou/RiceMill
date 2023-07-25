using RiceMill.Api.DependencyInjection;
using RiceMill.Api.Middleware;
using RiceMill.Api.Swagger;
using RiceMill.Api.Versioning;
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