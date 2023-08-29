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
            LoginPage._isFirstView = true;
            base.OnStart();
        }

        protected override void OnSleep()
        {
            LoginPage._isFirstView = true;
            base.OnSleep();
        }

        protected override void OnResume()
        {
            LoginPage._isFirstView = true;
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