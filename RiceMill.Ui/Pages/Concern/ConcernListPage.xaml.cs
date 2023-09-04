using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Services.UseCases.ConcernServices;
using Shared.ExtensionMethods;

namespace RiceMill.Ui.Pages.Concern;

public partial class ConcernListPage : ContentPage
{
    private readonly IConcernServices _concernServices;
    private PaginatedList<DtoConcern> Concerns;
    private bool _isNewConcern = true;
    private DtoConcern _currentSelectedConcern;

    public ConcernListPage()
    {
        InitializeComponent();
        _concernServices = new ConcernServices();
    }

    protected override async void OnAppearing()
    {
        try
        {
            var concerns = await _concernServices.Get(new DtoConcernFilter());
            Concerns = concerns.Data;
            CVConcern.ItemsSource = Concerns.Items;
            base.OnAppearing();
        }
        catch (Exception)
        {

            throw;
        }
    }

    private async void OnBtnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            if (TxtTitle.Text.IsNullOrEmpty())
                return;

            if (_isNewConcern && ApplicationStaticContext.CurrentUser.RiceMillId.IsNullOrEmpty())
                return;

            if (_isNewConcern)
            {
                var newConcern = new DtoCreateConcern(TxtTitle.Text, ApplicationStaticContext.CurrentUser.RiceMillId);
                await _concernServices.Add(newConcern);
                return;
            }
            var updateConcern = new DtoUpdateConcern(_currentSelectedConcern.Id, TxtTitle.Text);
            await _concernServices.Update(updateConcern);
            return;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            OnAppearing();
            TxtTitle.Text = string.Empty;
        }
    }

    private async void OnBtnRemoveClicked(object sender, EventArgs e)
    {
        try
        {
            if (CVConcern.SelectedItem is not DtoConcern selectedConcern)
                return;

            await _concernServices.Delete(selectedConcern.Id);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            OnAppearing();
            TxtTitle.Text = string.Empty;
        }
    }

    private void OnNewBtnClicked(object sender, EventArgs e)
    {
        _isNewConcern = true;
        TxtTitle.Text = string.Empty;
    }

    private void OnCVConcernSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (CVConcern.SelectedItem is not DtoConcern selectedConcern)
                return;

            _currentSelectedConcern = selectedConcern;
            TxtTitle.Text = _currentSelectedConcern.Title;
            _isNewConcern = false;
        }
        catch (Exception)
        {

            throw;
        }
    }
}