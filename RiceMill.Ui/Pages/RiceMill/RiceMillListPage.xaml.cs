using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Platform;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Application.UseCases.RiceMillServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Services.UseCases.PersonServices;
using RiceMill.Ui.Services.UseCases.RiceMillServices;
using Shared.ExtensionMethods;
using System.Text;

namespace RiceMill.Ui.Pages.RiceMill;

public partial class RiceMillListPage : ContentPage
{
    private readonly IRiceMillServices _riceMillServices;
    private readonly IPersonServices _personServices;
    private PaginatedList<DtoPerson> People;
    private PaginatedList<DtoRiceMill> RiceMills;
    private bool _isNewRiceMill = true;

    public RiceMillListPage()
    {
        try
        {
            _riceMillServices = new RiceMillServices();
            _personServices = new PersonServices();
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
            BtnRemove.IsEnabled = ApplicationStaticContext.IsAdmin;
            BtnSave.IsEnabled = ApplicationStaticContext.HaveAccessToRiceMill;
            BtnNew.IsEnabled = ApplicationStaticContext.IsAdmin;
            await LoadPeople();
            await RefreshRiceMillList();
            CVRiceMill.ItemsSource = RiceMills.Items;
            PickerOwner.ItemsSource = People.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.InnerException.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private void OnNewBtnClicked(object sender, EventArgs e)
    {
        CVRiceMill.SelectedItem = null;
        PickerOwner.SelectedItem = null;
        TxtDescription.Text = string.Empty;
        TxtTitle.Text = string.Empty;
        TxtAddress.Text = string.Empty;
        Txtphone.Text = string.Empty;
        TxtPostalCode.Text = string.Empty;
        TxtWage.Text = string.Empty;
        _isNewRiceMill = true;
    }

    private async void OnBtnRemoveClicked(object sender, EventArgs e)
    {
        try
        {
            if (CVRiceMill.SelectedItem is not DtoRiceMill selectedRiceMill)
            {
                await Toast.Make(MessageDictionary.GetMessageText(ResultStatusEnum.PleaseSelectRiceMill), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            await _riceMillServices.Delete(selectedRiceMill.Id);
            OnNewBtnClicked(null, null);
            await RefreshRiceMillList();
            CVRiceMill.ItemsSource = RiceMills.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private async void OnCVRiceMillSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (CVRiceMill.SelectedItem is not DtoRiceMill selectedRiceMill)
                return;

            TxtDescription.Text = selectedRiceMill.Description;
            TxtTitle.Text = selectedRiceMill.Title;
            TxtAddress.Text = selectedRiceMill.Address;
            Txtphone.Text = selectedRiceMill.Phone;
            TxtPostalCode.Text = selectedRiceMill.PostalCode;
            TxtWage.Text = selectedRiceMill.Wage.ToString();
            PickerOwner.SelectedItem = People.Items.FirstOrDefault(x => x.Id.Equals(selectedRiceMill.OwnerPersonId));
            _isNewRiceMill = false;
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
            if (_isNewRiceMill && ApplicationStaticContext.CurrentUser.RiceMillId.IsNullOrEmpty())
                return;

            var errorMessage = new StringBuilder();
            DtoPerson selectedOwner = null;
            if (PickerOwner.SelectedItem is DtoPerson owner)
                selectedOwner = owner;

            if (TxtTitle.Text.IsNullOrEmpty())
                errorMessage.AppendLine(MessageDictionary.GetMessageText(ResultStatusEnum.RiceMillTitleIsNotValid));

            if (TxtAddress.Text.IsNullOrEmpty())
                errorMessage.AppendLine(MessageDictionary.GetMessageText(ResultStatusEnum.RiceMillAddressIsNotValid));

            if (TxtWage.Text.IsNullOrEmpty())
                errorMessage.AppendLine(MessageDictionary.GetMessageText(ResultStatusEnum.RiceMillWageIsNotValid));

            if (errorMessage.IsNotNullOrEmpty())
            {
                await Toast.Make(errorMessage.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            if (_isNewRiceMill)
            {
                var newRiceMill = new DtoCreateRiceMill(TxtTitle.Text, TxtAddress.Text, TxtWage.Text.ToByte(), Txtphone.Text, TxtPostalCode.Text, TxtDescription.Text, selectedOwner?.Id);
                await _riceMillServices.Add(newRiceMill);
            }
            else
            {
                if (CVRiceMill.SelectedItem is not DtoRiceMill selectedRiceMill)
                    return;

                var updateRiceMill = new DtoUpdateRiceMill(selectedRiceMill.Id, TxtTitle.Text, TxtAddress.Text, TxtWage.Text.ToByte(), Txtphone.Text, TxtPostalCode.Text, TxtDescription.Text, selectedOwner?.Id);
                await _riceMillServices.Update(updateRiceMill);
            }
            OnNewBtnClicked(null, null);
            await RefreshRiceMillList();
            CVRiceMill.ItemsSource = RiceMills.Items;
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

    private Task RefreshRiceMillList()
    {
        return Task.Run(() =>
        {
            var filter = new DtoRiceMillFilter();
            if (!ApplicationStaticContext.IsAdmin)
                filter.Id = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _riceMillServices.Get(filter);
            RiceMills = result.Result.Data;
            RiceMills.Items.ForEach(item => { item.OwnerFullName = People.Items.FirstOrDefault(x => x.Id.Equals(item.OwnerPersonId))?.FullName; });
        });
    }

    private Task LoadPeople()
    {
        return Task.Run(() =>
        {
            var filter = new DtoPersonFilter();
            if (!ApplicationStaticContext.IsAdmin)
                filter.RiceMillId = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _personServices.Get(filter);
            People = result.Result.Data;
        });
    }
}