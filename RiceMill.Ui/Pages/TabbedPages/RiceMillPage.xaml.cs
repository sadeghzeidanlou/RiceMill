using RiceMill.Ui.Pages.Concern;

namespace RiceMill.Ui.Pages.TabbedPages;

public partial class RiceMillPage : ContentPage
{
	public RiceMillPage()
	{
		InitializeComponent();
	}

    private void OnBtnRiceMillsClicked(object sender, EventArgs e)
    {

    }

    private void OnBtnPeopleClicked(object sender, EventArgs e)
    {

    }

    private void OnBtnDryersClicked(object sender, EventArgs e)
    {

    }

    private async void OnBtnConcernsClicked(object sender, EventArgs e) => await Navigation.PushAsync(new ConcernListPage());

    private void OnBtnVehiclesClicked(object sender, EventArgs e)
    {

    }

    private void OnBtnVillagesClicked(object sender, EventArgs e)
    {

    }
}