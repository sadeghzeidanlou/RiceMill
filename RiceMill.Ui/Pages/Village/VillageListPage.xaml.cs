using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Platform;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.VillageServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Services.UseCases.VillageServices;
using Shared.ExtensionMethods;

namespace RiceMill.Ui.Pages.Village;

public sealed partial class VillageListPage : ContentPage
{
    private readonly IVillageServices _villageServices;
    private PaginatedList<DtoVillage> Villages;
    private bool _isNewVillage = true;

    public VillageListPage()
    {
        try
        {
            _villageServices = new VillageServices();
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
            await RefreshVillageList();
            CVVillage.ItemsSource = Villages.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.InnerException.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private void OnNewBtnClicked(object sender, EventArgs e)
    {
        _isNewVillage = true;
        TxtTitle.Text = string.Empty;
        CVVillage.SelectedItem = null;
    }

    private async void OnBtnRemoveClicked(object sender, EventArgs e)
    {
        try
        {
            if (CVVillage.SelectedItem is not DtoVillage selectedVillage)
            {
                await Toast.Make(ResultStatusEnum.PleaseSelectVillage.GetErrorMessage(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            var questionResult = await DisplayAlert("تاییدیه", "آیا از حذف این مورد اطمینان دارید", "بله", "خیر", FlowDirection.RightToLeft);
            if (!questionResult)
                return;

            await _villageServices.Delete(selectedVillage.Id);
            OnNewBtnClicked(null, null);
            await RefreshVillageList();
            CVVillage.ItemsSource = Villages.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private async void OnCVVillageSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (CVVillage.SelectedItem is not DtoVillage selectedVillage)
                return;

            TxtTitle.Text = selectedVillage.Title;
            _isNewVillage = false;
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
            if (TxtTitle.Text.IsNullOrEmpty() || _isNewVillage && ApplicationStaticContext.CurrentUser.RiceMillId.IsNullOrEmpty())
            {
                await Toast.Make(ResultStatusEnum.VillageTitleIsNotValid.GetErrorMessage(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            if (_isNewVillage)
            {
                var newVillage = new DtoCreateVillage(TxtTitle.Text, ApplicationStaticContext.CurrentUser.RiceMillId);
                await _villageServices.Add(newVillage);
            }
            else
            {
                if (CVVillage.SelectedItem is not DtoVillage selectedVillage)
                    return;

                var updateVillage = new DtoUpdateVillage(selectedVillage.Id, TxtTitle.Text);
                await _villageServices.Update(updateVillage);
            }
            OnNewBtnClicked(null, null);
            await RefreshVillageList();
            CVVillage.ItemsSource = Villages.Items;
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

    private Task RefreshVillageList()
    {
        return Task.Run(() =>
        {
            var filter = new DtoVillageFilter();
            if (!ApplicationStaticContext.IsAdmin)
                filter.RiceMillId = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _villageServices.Get(filter);
            Villages = result.Result.Data;
        });
    }
}