using RiceMill.Ui.Services;
using RiceMill.Ui.Services.UseCases.UserServices;
using System.Net.Http;

namespace RiceMill.Ui
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App()
        {
            InitializeComponent();

            var sendRequestService = new SendRequestService();
            DependencyService.RegisterSingleton<ISendRequestService>(sendRequestService);

            var userServices = new UserServices(sendRequestService);
            DependencyService.RegisterSingleton<IUserServices>(userServices);
            MainPage = new LoginPage(userServices);
        }
    }
}