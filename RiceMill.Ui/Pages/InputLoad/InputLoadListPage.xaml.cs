using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MD.PersianDateTime.Standard;
using Microsoft.Maui.Platform;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Application.UseCases.VehicleServices.Dto;
using RiceMill.Application.UseCases.VillageServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Services.UseCases.InputLoadServices;
using RiceMill.Ui.Services.UseCases.PersonServices;
using RiceMill.Ui.Services.UseCases.VehicleServices;
using RiceMill.Ui.Services.UseCases.VillageServices;
using Shared.ExtensionMethods;
using System.Text;

namespace RiceMill.Ui.Pages.InputLoad;

public sealed partial class InputLoadListPage : ContentPage
{
    private readonly IInputLoadServices _inputLoadServices;
    private readonly IPersonServices _personServices;
    private readonly IVillageServices _villageServices;
    private readonly IVehicleServices _vehicleServices;
    private PaginatedList<DtoInputLoad> InputLoads;
    private PaginatedList<DtoPerson> People;
    private PaginatedList<DtoVehicle> Vehicles;
    private PaginatedList<DtoVillage> Villages;
    private bool _isNewInputLoad = true;

    public InputLoadListPage()
    {
        try
        {
            _personServices = new PersonServices();
            _inputLoadServices = new InputLoadServices();
            _villageServices = new VillageServices();
            _vehicleServices = new VehicleServices();
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
            PersianDatePicker.PersianDate = PersianDateTime.Now.ToShortDateString();
            TimePicker.Time = DateTime.Now.TimeOfDay;
            BtnRemove.IsEnabled = !ApplicationStaticContext.IsUser;
            BtnSave.IsEnabled = !ApplicationStaticContext.IsUser;
            BtnNew.IsEnabled = !ApplicationStaticContext.IsUser;
            await LoadPeople();
            await LoadVillages();
            await LoadVehicles();
            await RefreshInputLoadList();
            FillRequireData();
            CVInputLoad.ItemsSource = InputLoads.Items;
            PickerVillage.ItemsSource = Villages.Items;
            PickerVehicle.ItemsSource = Vehicles.Items;
            PickerDeliverer.ItemsSource = People.Items;
            PickerReceiver.ItemsSource = People.Items;
            PickerCarrier.ItemsSource = People.Items;
            PickerOwner.ItemsSource = People.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.InnerException.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private void OnNewBtnClicked(object sender, EventArgs e)
    {
        CVInputLoad.SelectedItem = null;
        PickerVillage.SelectedItem = null;
        PickerVehicle.SelectedItem = null;
        PickerDeliverer.SelectedItem = null;
        PickerReceiver.SelectedItem = null;
        PickerCarrier.SelectedItem = null;
        PickerOwner.SelectedItem = null;
        TxtNumberOfBags.Text = string.Empty;
        TxtNumberOfBagsInDryer.Text = string.Empty;
        TxtDescription.Text = string.Empty;
        PersianDatePicker.PersianDate = PersianDateTime.Now.ToShortDateString();
        TimePicker.Time = DateTime.Now.TimeOfDay;
        _isNewInputLoad = true;
    }

    private async void OnBtnRemoveClicked(object sender, EventArgs e)
    {
        try
        {
            if (CVInputLoad.SelectedItem is not DtoInputLoad selectedInputLoad)
            {
                await Toast.Make(ResultStatusEnum.PleaseSelectInputLoad.GetErrorMessage(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            var questionResult = await DisplayAlert("تاییدیه", "آیا از حذف این مورد اطمینان دارید", "بله", "خیر", FlowDirection.RightToLeft);
            if (!questionResult)
                return;

            await _inputLoadServices.Delete(selectedInputLoad.Id);
            OnNewBtnClicked(null, null);
            await RefreshInputLoadList();
            FillRequireData();
            CVInputLoad.ItemsSource = InputLoads.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private async void OnCVInputLoadSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (CVInputLoad.SelectedItem is not DtoInputLoad selectedInputLoad)
                return;

            PickerVillage.SelectedItem = Villages.Items.FirstOrDefault(x => x.Id.Equals(selectedInputLoad.VillageId));
            PickerVehicle.SelectedItem = Vehicles.Items.FirstOrDefault(x => x.Id.Equals(selectedInputLoad.VehicleId));
            PickerDeliverer.SelectedItem = People.Items.FirstOrDefault(x => x.Id.Equals(selectedInputLoad.DelivererPersonId));
            PickerReceiver.SelectedItem = People.Items.FirstOrDefault(x => x.Id.Equals(selectedInputLoad.ReceiverPersonId));
            PickerCarrier.SelectedItem = People.Items.FirstOrDefault(x => x.Id.Equals(selectedInputLoad.CarrierPersonId));
            PickerOwner.SelectedItem = People.Items.FirstOrDefault(x => x.Id.Equals(selectedInputLoad.OwnerPersonId));
            TxtNumberOfBags.Text = selectedInputLoad.NumberOfBags.ToString();
            TxtNumberOfBagsInDryer.Text = selectedInputLoad.NumberOfBagsInDryer.ToString();
            TxtDescription.Text = selectedInputLoad.Description;
            PersianDatePicker.PersianDate = new PersianDateTime(selectedInputLoad.ReceiveTime).ToShortDateString();
            TimePicker.Time = selectedInputLoad.ReceiveTime.TimeOfDay;
            _isNewInputLoad = false;
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
            if (_isNewInputLoad && ApplicationStaticContext.CurrentUser.RiceMillId.IsNullOrEmpty())
                return;

            var errorMessage = new StringBuilder();
            if (TxtNumberOfBags.Text.IsNullOrEmpty())
                errorMessage.AppendLine(ResultStatusEnum.InputLoadNumberOfBagsIsNotValid.GetErrorMessage());

            if (PersianDatePicker.PersianDate.IsNullOrEmpty() || TimePicker.Time.TotalSeconds == 0 ||
                PersianDateTime.Parse(PersianDatePicker.PersianDate).AddSeconds((int)TimePicker.Time.TotalSeconds) > PersianDateTime.Now)
            {
                errorMessage.AppendLine(ResultStatusEnum.InputLoadReceiveTimeIsNotValid.GetErrorMessage());
            }
            DtoPerson selectedCarrier = null;
            if (PickerCarrier.SelectedItem is DtoPerson carrierPerson)
                selectedCarrier = carrierPerson;
            else
                errorMessage.AppendLine(ResultStatusEnum.InputLoadCarrierPersonNotFound.GetErrorMessage());

            DtoPerson selectedReceiver = null;
            if (PickerReceiver.SelectedItem is DtoPerson receiverPerson)
                selectedReceiver = receiverPerson;
            else
                errorMessage.AppendLine(ResultStatusEnum.InputLoadReceiverPersonNotFound.GetErrorMessage());

            DtoPerson selectedDeliverer = null;
            if (PickerDeliverer.SelectedItem is DtoPerson delivererPerson)
                selectedDeliverer = delivererPerson;
            else
                errorMessage.AppendLine(ResultStatusEnum.InputLoadDelivererPersonNotFound.GetErrorMessage());

            DtoPerson selectedOwner = null;
            if (PickerOwner.SelectedItem is DtoPerson carrierOwner)
                selectedOwner = carrierOwner;
            else
                errorMessage.AppendLine(ResultStatusEnum.InputLoadOwnerPersonNotFound.GetErrorMessage());

            DtoVillage selectedVillage = null;
            if (PickerVillage.SelectedItem is DtoVillage village)
                selectedVillage = village;
            else
                errorMessage.AppendLine(ResultStatusEnum.VillageNotFound.GetErrorMessage());

            DtoVehicle selectedVehicle = null;
            if (PickerVehicle.SelectedItem is DtoVehicle vehicle)
                selectedVehicle = vehicle;
            else
                errorMessage.AppendLine(ResultStatusEnum.VehicleNotFound.GetErrorMessage());

            if (errorMessage.IsNotNullOrEmpty())
            {
                await Toast.Make(errorMessage.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            var receiveTime = PersianDateTime.Parse(PersianDatePicker.PersianDate).AddSeconds((int)TimePicker.Time.TotalSeconds).ToDateTime();
            if (_isNewInputLoad)
            {
                var newInputLoad = new DtoCreateInputLoad(TxtNumberOfBags.Text.ToShort(), TxtDescription.Text, receiveTime, selectedVillage.Id, selectedDeliverer.Id, selectedReceiver.Id, selectedCarrier.Id, selectedOwner.Id, selectedVehicle.Id, ApplicationStaticContext.CurrentUser.RiceMillId);
                await _inputLoadServices.Add(newInputLoad);
            }
            else
            {
                if (CVInputLoad.SelectedItem is not DtoInputLoad selectedInputLoad)
                    return;

                var updateInputLoad = new DtoUpdateInputLoad(selectedInputLoad.Id, TxtNumberOfBags.Text.ToShort(), TxtDescription.Text, receiveTime, selectedVillage.Id, selectedDeliverer.Id, selectedReceiver.Id, selectedCarrier.Id, selectedOwner.Id, selectedVehicle.Id);
                await _inputLoadServices.Update(updateInputLoad);
            }
            OnNewBtnClicked(null, null);
            await RefreshInputLoadList();
            FillRequireData();
            CVInputLoad.ItemsSource = InputLoads.Items;
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

    private void FillRequireData()
    {
        foreach (var item in InputLoads.Items)
        {
            var ownerDetail = People.Items.FirstOrDefault(x => x.Id.Equals(item.OwnerPersonId));
            var villageDetail = Villages.Items.FirstOrDefault(x => x.Id.Equals(item.VillageId));
            item.OwnerFullName = $"{ownerDetail?.FullName ?? "*نامشخص*"} {item.ReceiveTimeReadable}";
            item.VillageTitle = $"از {villageDetail?.Title ?? "*نامشخص*"} تعداد {item.NumberOfBags} کیسه";
        }
    }

    private Task RefreshInputLoadList()
    {
        return Task.Run(() =>
        {
            var filter = new DtoInputLoadFilter();
            if (ApplicationStaticContext.IsNotAdmin)
                filter.RiceMillId = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _inputLoadServices.Get(filter);
            InputLoads = result.Result.Data;
        });
    }

    private Task LoadVillages()
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

    private Task LoadVehicles()
    {
        return Task.Run(() =>
        {
            var filter = new DtoVehicleFilter();
            if (ApplicationStaticContext.IsNotAdmin)
                filter.RiceMillId = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _vehicleServices.Get(filter);
            Vehicles = result.Result.Data;
        });
    }

    private Task LoadPeople()
    {
        return Task.Run(() =>
        {
            var filter = new DtoPersonFilter();
            if (ApplicationStaticContext.IsNotAdmin)
                filter.RiceMillId = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _personServices.Get(filter);
            People = result.Result.Data;
        });
    }
}