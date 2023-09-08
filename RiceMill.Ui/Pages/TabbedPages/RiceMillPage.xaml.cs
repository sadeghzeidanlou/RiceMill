using RiceMill.Ui.Common;
using RiceMill.Ui.Pages.Concern;
using RiceMill.Ui.Pages.Dryer;
using RiceMill.Ui.Pages.Vehicle;
using RiceMill.Ui.Pages.Village;

namespace RiceMill.Ui.Pages.TabbedPages;

public partial class RiceMillPage : ContentPage
{
    public RiceMillPage()
    {
        InitializeComponent();
        if (ApplicationStaticContext.IsUser || ApplicationStaticContext.IsSupperUser)
            BtnRiceMill.IsVisible = false;
    }

    private void OnBtnRiceMillsClicked(object sender, EventArgs e)
    {

    }

    private void OnBtnPeopleClicked(object sender, EventArgs e)
    {

    }

    private async void OnBtnDryersClicked(object sender, EventArgs e) => await Navigation.PushAsync(new DryerListPage());

    private async void OnBtnConcernsClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ConcernListPage());

    private async void OnBtnVehiclesClicked(object sender, EventArgs e) => await Navigation.PushAsync(new VehicleListPage());

    private async void OnBtnVillagesClicked(object sender, EventArgs e) => await Navigation.PushAsync(new VillageListPage());
}