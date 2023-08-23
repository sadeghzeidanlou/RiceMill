using RiceMill.Ui.Services.UseCases.UserServices;

namespace RiceMill.Ui.DependencyInjection
{
    public static class ApiModule
    {
        public static IServiceCollection AddMauiServices(this IServiceCollection services)
        {
            services.AddTransient<IUserServices, UserServices>();
            //services.AddHttpClient();

            return services;
        }
    }
}