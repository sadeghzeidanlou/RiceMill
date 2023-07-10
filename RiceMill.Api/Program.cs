using RiceMill.Api.DependencyInjection;
using RiceMill.Api.Swagger;
using RiceMill.Api.Versioning;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.DependencyInjection;
using RiceMill.Infrastructure.DependencyInjection;
using RiceMill.Persistence.DependencyInjection;

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
            using (var scope = app.Services.CreateScope())
            {
                var iApplicationDbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                if (cacheService != null && iApplicationDbContext != null)
                    PreloadCache(cacheService, iApplicationDbContext);
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void PreloadCache(ICacheService cacheService, IApplicationDbContext applicationDbContext) => applicationDbContext.GetAllData().ToList().ForEach(x => cacheService.Set(x.Key, x.Value));
    }
}