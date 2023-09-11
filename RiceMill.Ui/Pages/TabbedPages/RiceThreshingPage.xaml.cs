using RiceMill.Ui.Pages.InputLoad;
using RiceMill.Ui.Pages.Payment;

namespace RiceMill.Ui.Pages.TabbedPages;

public partial class RiceThreshingPage : ContentPage
{
    public RiceThreshingPage()
    {
        InitializeComponent();
    }

    private void OnBtnIncomesClicked(object sender, EventArgs e)
    {

    }

    private async void OnBtnPaymentsClicked(object sender, EventArgs e) => await Navigation.PushAsync(new PaymentListPage());

    private void OnBtnRiceThreshingsClicked(object sender, EventArgs e)
    {

    }

    private void OnBtnDeliveriesClicked(object sender, EventArgs e)
    {

    }

    private async void OnBtnInputLoadsClicked(object sender, EventArgs e) => await Navigation.PushAsync(new InputLoadListPage());

    private void OnBtnDryerHistoryClicked(object sender, EventArgs e)
    {

    }
}