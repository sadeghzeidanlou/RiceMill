using RiceMill.Application.Common.Models.Resource;
using System.Diagnostics;

namespace RiceMill.Ui.Pages.TabbedPages;

public sealed partial class SettingPage : ContentPage
{
    public SettingPage() => InitializeComponent();

    private void OnBtnSettingClicked(object sender, EventArgs e)
    {

    }

    private void OnBtnAccountsClicked(object sender, EventArgs e)
    {

    }

    private void OnBtnUserActivitiesClicked(object sender, EventArgs e)
    {

    }

    private void OnBtnUsersClicked(object sender, EventArgs e)
    {

    }

    private async void OnBtnExitClicked(object sender, EventArgs e)
    {
        SecureStorage.Default.RemoveAll();
        string tokenValue = await SecureStorage.Default.GetAsync(SharedResource.TokenKey);
#if ANDROID || WINDOWS
        Process.GetCurrentProcess().Kill();
#endif
    }
}