using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Ui.Services.UseCases.ConcernServices;

namespace RiceMill.Ui.Pages.Concern;

public partial class ConcernListPage : ContentPage
{
    private readonly IConcernServices _concernServices;
    private List<DtoConcern> Concerns;

    public ConcernListPage()
    {
        InitializeComponent();
        _concernServices = new ConcernServices();
    }

    protected override async void OnAppearing()
    {
        var concerns = await _concernServices.Get(new DtoConcernFilter());
        Concerns = concerns.Data.Items;
        ConcernCollectionView.ItemsSource = Concerns;
        base.OnAppearing();
    }

    private void OnBtnSaveClicked(object sender, EventArgs e)
    {

    }

    private void OnBtnRemoveClicked(object sender, EventArgs e)
    {

    }

    private void OnNewBtnClicked(object sender, EventArgs e)
    {

    }
}