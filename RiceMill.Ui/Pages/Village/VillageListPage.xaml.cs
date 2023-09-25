using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Platform;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.RiceMillServices.Dto;
using RiceMill.Application.UseCases.VillageServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Services.UseCases.RiceMillServices;
using RiceMill.Ui.Services.UseCases.VillageServices;
using Shared.ExtensionMethods;
using System.Text;

namespace RiceMill.Ui.Pages.Village;

public sealed partial class VillageListPage : ContentPage
{
    private readonly IVillageServices _villageServices;
    private readonly IRiceMillServices _riceMillServices;
    private PaginatedList<DtoVillage> Villages;
    private PaginatedList<DtoRiceMill> RiceMills;
    private bool _isNewVillage = true;

    public VillageListPage()
    {
        try
        {
            _villageServices = new VillageServices();
            _riceMillServices = new RiceMillServices();
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
            PickerRiceMill.IsEnabled = ApplicationStaticContext.IsAdmin;
            await LoadRiceMillList();
            await RefreshVillageList();
            CVVillage.ItemsSource = Villages.Items;
            PickerRiceMill.ItemsSource = RiceMills.Items;
            if (ApplicationStaticContext.IsNotAdmin)
                PickerRiceMill.SelectedItem = RiceMills.Items.FirstOrDefault();
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
        PickerRiceMill.SelectedItem = ApplicationStaticContext.IsAdmin ? null : RiceMills.Items.FirstOrDefault();
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
            PickerRiceMill.SelectedItem = RiceMills.Items.FirstOrDefault(x => x.Id.Equals(selectedVillage.RiceMillId));
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
            DtoRiceMill selectedRiceMill = null;
            if (PickerRiceMill.SelectedItem is DtoRiceMill riceMill)
                selectedRiceMill = riceMill;

            var errorMessage = new StringBuilder();
            if (_isNewVillage && selectedRiceMill == null)
                errorMessage.AppendLine(ResultStatusEnum.RiceMillNotFound.GetErrorMessage());

            if (TxtTitle.Text.IsNullOrEmpty())
                errorMessage.AppendLine(ResultStatusEnum.VillageTitleIsNotValid.GetErrorMessage());

            if (errorMessage.IsNotNullOrEmpty())
            {
                await Toast.Make(errorMessage.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }

            if (_isNewVillage)
            {
                var newVillage = new DtoCreateVillage(TxtTitle.Text, selectedRiceMill.Id);
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

    private Task LoadRiceMillList()
    {
        return Task.Run(() =>
        {
            var filter = new DtoRiceMillFilter();
            if (ApplicationStaticContext.IsNotAdmin)
                filter.Id = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _riceMillServices.Get(filter);
            RiceMills = result.Result.Data;
        });
    }

    private Task RefreshVillageList()
    {
        return Task.Run(() =>
        {
            var filter = new DtoVillageFilter();
            if (ApplicationStaticContext.IsNotAdmin)
                filter.RiceMillId = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _villageServices.Get(filter);
            Villages = result.Result.Data;
        });
    }
}