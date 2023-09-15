using RiceMill.Ui.Services;
using RiceMill.Ui.Services.UseCases.UserServices;

namespace RiceMill.Ui
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            LoginPage.IsFirstView = true;
            base.OnStart();
        }

        protected override void OnSleep()
        {
            LoginPage.IsFirstView = true;
            base.OnSleep();
        }

        protected override void OnResume()
        {
            LoginPage.IsFirstView = true;
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