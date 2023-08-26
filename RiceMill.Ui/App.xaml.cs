using RiceMill.Ui.Services;
using RiceMill.Ui.Services.UseCases.UserServices;

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

            MainPage = new NavigationPage(new LoginPage(userServices));
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void CleanUp()
        {
            base.CleanUp();
        }

        public override void OpenWindow(Window window)
        {
            base.OpenWindow(window);
        }
    }
}