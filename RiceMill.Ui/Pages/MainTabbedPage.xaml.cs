using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using RiceMill.Ui.Common;

namespace RiceMill.Ui.Pages;

public sealed partial class MainTabbedPage : TabbedPage
{
    public MainTabbedPage()
    {
        InitializeComponent();
        LoginPage.IsFirstView = false;
        Toast.Make($"{ApplicationStaticContext.CurrentPerson?.FullName ?? "کاربر"} عزیز خوش آمدید", ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
    }
}