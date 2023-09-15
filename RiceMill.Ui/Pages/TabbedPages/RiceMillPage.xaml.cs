using RiceMill.Ui.Common;
using RiceMill.Ui.Pages.Concern;
using RiceMill.Ui.Pages.Dryer;
using RiceMill.Ui.Pages.Person;
using RiceMill.Ui.Pages.RiceMill;
using RiceMill.Ui.Pages.Vehicle;
using RiceMill.Ui.Pages.Village;

namespace RiceMill.Ui.Pages.TabbedPages;

public sealed partial class RiceMillPage : ContentPage
{
    public RiceMillPage()
    {
        InitializeComponent();
        BtnRiceMill.IsVisible = ApplicationStaticContext.HaveAccessToRiceMill;
    }

    private async void OnBtnRiceMillsClicked(object sender, EventArgs e) => await Navigation.PushAsync(new RiceMillListPage());

    private async void OnBtnPeopleClicked(object sender, EventArgs e) => await Navigation.PushAsync(new PersonListPage());

    private async void OnBtnDryersClicked(object sender, EventArgs e) => await Navigation.PushAsync(new DryerListPage());

    private async void OnBtnConcernsClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ConcernListPage());

    private async void OnBtnVehiclesClicked(object sender, EventArgs e) => await Navigation.PushAsync(new VehicleListPage());

    private async void OnBtnVillagesClicked(object sender, EventArgs e) => await Navigation.PushAsync(new VillageListPage());
}