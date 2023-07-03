using RiceMill.Api.DependencyInjection;
using RiceMill.Api.Swagger;
using RiceMill.Api.Versioning;
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
                .AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
    }
}