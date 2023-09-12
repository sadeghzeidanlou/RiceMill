using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Platform;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DryerServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Services.UseCases.DryerServices;
using Shared.ExtensionMethods;

namespace RiceMill.Ui.Pages.Dryer;

public partial class DryerListPage : ContentPage
{
    private readonly IDryerServices _dryerServices;
    private PaginatedList<DtoDryer> Dryers;
    private bool _isNewDryer = true;

    public DryerListPage()
    {
        try
        {
            _dryerServices = new DryerServices();
            InitializeComponent();
            InitializeAsync();
        }
        catch (Exception ex)
        {
            Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private async void InitializeAsync()
    {
        try
        {
            BtnRemove.IsEnabled = !ApplicationStaticContext.IsUser;
            BtnSave.IsEnabled = !ApplicationStaticContext.IsUser;
            BtnNew.IsEnabled = !ApplicationStaticContext.IsUser;
            await RefreshDryerList();
            CVDryer.ItemsSource = Dryers.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.InnerException.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private void OnNewBtnClicked(object sender, EventArgs e)
    {
        _isNewDryer = true;
        TxtTitle.Text = string.Empty;
        CVDryer.SelectedItem = null;
    }

    private async void OnBtnRemoveClicked(object sender, EventArgs e)
    {
        try
        {
            if (CVDryer.SelectedItem is not DtoDryer selectedDryer)
            {
                await Toast.Make(ResultStatusEnum.PleaseSelectDryer.GetErrorMessage(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            await _dryerServices.Delete(selectedDryer.Id);
            OnNewBtnClicked(null, null);
            await RefreshDryerList();
            CVDryer.ItemsSource = Dryers.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private async void OnCVDryerSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (CVDryer.SelectedItem is not DtoDryer selectedDryer)
                return;

            TxtTitle.Text = selectedDryer.Title;
            _isNewDryer = false;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private async void OnBtnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            if (TxtTitle.Text.IsNullOrEmpty() || _isNewDryer && ApplicationStaticContext.CurrentUser.RiceMillId.IsNullOrEmpty())
            {
                await Toast.Make(ResultStatusEnum.DryerTitleIsNotValid.GetErrorMessage(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            if (_isNewDryer)
            {
                var newDryer = new DtoCreateDryer(TxtTitle.Text, ApplicationStaticContext.CurrentUser.RiceMillId);
                await _dryerServices.Add(newDryer);
                return;
            }
            else
            {
                if (CVDryer.SelectedItem is not DtoDryer selectedDryer)
                    return;

                var updateDryer = new DtoUpdateDryer(selectedDryer.Id, TxtTitle.Text);
                await _dryerServices.Update(updateDryer);
            }
            OnNewBtnClicked(null, null);
            await RefreshDryerList();
            CVDryer.ItemsSource = Dryers.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
        finally
        {
#if ANDROID
            if (Platform.CurrentActivity.CurrentFocus != null)
                Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
#endif
        }
    }

    private Task RefreshDryerList()
    {
        return Task.Run(() =>
        {
            var filter = new DtoDryerFilter();
            if (!ApplicationStaticContext.IsAdmin)
                filter.RiceMillId = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _dryerServices.Get(filter);
            Dryers = result.Result.Data;
        });
    }
}