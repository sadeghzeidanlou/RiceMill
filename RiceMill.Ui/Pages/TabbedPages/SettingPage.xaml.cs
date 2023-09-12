using RiceMill.Application.Common.Models.Resource;
using System.Diagnostics;

namespace RiceMill.Ui.Pages.TabbedPages;

public partial class SettingPage : ContentPage
{
    public SettingPage()
    {
        InitializeComponent();
    }

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

    private void OnBtnExitClicked(object sender, EventArgs e)
    {
        SecureStorage.Default.RemoveAll();
        SecureStorage.RemoveAll();
#if ANDROID
        Process.GetCurrentProcess().Kill();
#endif

#if WINDOWS
        Process.GetCurrentProcess().Kill();
#endif

    }
}