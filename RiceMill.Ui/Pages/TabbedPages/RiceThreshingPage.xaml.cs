using RiceMill.Ui.Pages.Delivery;
using RiceMill.Ui.Pages.DryerHistory;
using RiceMill.Ui.Pages.Income;
using RiceMill.Ui.Pages.InputLoad;
using RiceMill.Ui.Pages.Payment;
using RiceMill.Ui.Pages.RiceThreshing;

namespace RiceMill.Ui.Pages.TabbedPages;

public sealed partial class RiceThreshingPage : ContentPage
{
    public RiceThreshingPage() => InitializeComponent();

    private async void OnBtnIncomesClicked(object sender, EventArgs e) => await Navigation.PushAsync(new IncomeListPage());

    private async void OnBtnPaymentsClicked(object sender, EventArgs e) => await Navigation.PushAsync(new PaymentListPage());

    private async void OnBtnRiceThreshingsClicked(object sender, EventArgs e) => await Navigation.PushAsync(new RiceThreshingListPage());

    private async void OnBtnDeliveriesClicked(object sender, EventArgs e) => await Navigation.PushAsync(new DeliveryListPage());

    private async void OnBtnInputLoadsClicked(object sender, EventArgs e) => await Navigation.PushAsync(new InputLoadListPage());

    private async void OnBtnDryerHistoryClicked(object sender, EventArgs e) => await Navigation.PushAsync(new DryerHistoryListPage());
}