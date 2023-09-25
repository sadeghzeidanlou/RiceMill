using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MD.PersianDateTime.Standard;
using Microsoft.Maui.Platform;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DeliveryServices.Dto;
using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Application.UseCases.RiceThreshingServices.Dto;
using RiceMill.Application.UseCases.VehicleServices.Dto;
using RiceMill.Application.UseCases.VillageServices.Dto;
using RiceMill.Domain.Models;
using RiceMill.Ui.Common;
using RiceMill.Ui.Services.UseCases.DeliveryServices;
using RiceMill.Ui.Services.UseCases.InputLoadServices;
using RiceMill.Ui.Services.UseCases.PersonServices;
using RiceMill.Ui.Services.UseCases.RiceThreshingServices;
using RiceMill.Ui.Services.UseCases.VehicleServices;
using RiceMill.Ui.Services.UseCases.VillageServices;
using Shared.ExtensionMethods;
using System.Text;

namespace RiceMill.Ui.Pages.Delivery;

public sealed partial class DeliveryListPage : ContentPage
{
    private readonly IDeliveryServices _deliveryServices;
    private readonly IInputLoadServices _inputLoadServices;
    private readonly IPersonServices _personServices;
    private readonly IVehicleServices _vehicleServices;
    private readonly IRiceThreshingServices _riceThreshingServices;
    private readonly IVillageServices _villageServices;
    private PaginatedList<DtoDelivery> Deliveries;
    private PaginatedList<DtoInputLoad> InputLoads;
    private PaginatedList<DtoPerson> People;
    private PaginatedList<DtoVehicle> Vehicles;
    private PaginatedList<DtoRiceThreshing> RiceThreshings;
    private PaginatedList<DtoVillage> Villages;
    private bool _isNewDelivery = true;

    public DeliveryListPage()
    {
        try
        {
            _deliveryServices = new DeliveryServices();
            _inputLoadServices = new InputLoadServices();
            _personServices = new PersonServices();
            _vehicleServices = new VehicleServices();
            _riceThreshingServices = new RiceThreshingServices();
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
            PersianDatePicker.PersianDate = PersianDateTime.Now.ToShortDateString();
            TimePicker.Time = DateTime.Now.TimeOfDay;
            BtnRemove.IsEnabled = !ApplicationStaticContext.IsUser;
            BtnSave.IsEnabled = !ApplicationStaticContext.IsUser;
            BtnNew.IsEnabled = !ApplicationStaticContext.IsUser;
            await LoadPeople();
            await LoadVehicles();
            await LoadInputLoads();
            await LoadRiceThreshings();
            await LoadVillages();
            await RefreshDeliveryList();
            FillRiceThreshingRequireData();
            FillRequireData();
            CVDelivery.ItemsSource = Deliveries.Items;
            PickerVehicle.ItemsSource = Vehicles.Items;
            PickerDeliverer.ItemsSource = People.Items;
            PickerReceiver.ItemsSource = People.Items;
            PickerCarrier.ItemsSource = People.Items;
            PickerRiceThreshing.ItemsSource = RiceThreshings.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.InnerException.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private void OnNewBtnClicked(object sender, EventArgs e)
    {
        CVDelivery.SelectedItem = null;
        PickerVehicle.SelectedItem = null;
        PickerDeliverer.SelectedItem = null;
        PickerReceiver.SelectedItem = null;
        PickerCarrier.SelectedItem = null;
        PickerRiceThreshing.SelectedItem = null;
        TxtUnbrokenRice.Text = string.Empty;
        TxtBrokenRice.Text = string.Empty;
        TxtChickenRice.Text = string.Empty;
        TxtFlour.Text = string.Empty;
        TxtDescription.Text = string.Empty;
        PersianDatePicker.PersianDate = PersianDateTime.Now.ToShortDateString();
        TimePicker.Time = DateTime.Now.TimeOfDay;
        _isNewDelivery = true;
    }

    private async void OnBtnRemoveClicked(object sender, EventArgs e)
    {
        try
        {
            if (CVDelivery.SelectedItem is not DtoDelivery selectedDelivery)
            {
                await Toast.Make(ResultStatusEnum.PleaseSelectDelivery.GetErrorMessage(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            var questionResult = await DisplayAlert("تاییدیه", "آیا از حذف این مورد اطمینان دارید", "بله", "خیر", FlowDirection.RightToLeft);
            if (!questionResult)
                return;

            await _deliveryServices.Delete(selectedDelivery.Id);
            OnNewBtnClicked(null, null);
            await RefreshDeliveryList();
            FillRequireData();
            CVDelivery.ItemsSource = Deliveries.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private async void OnCVDeliverySelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (CVDelivery.SelectedItem is not DtoDelivery selectedDelivery)
                return;

            PickerRiceThreshing.SelectedItem = RiceThreshings.Items.FirstOrDefault(x => x.Id.Equals(selectedDelivery.RiceThreshingId));
            PickerVehicle.SelectedItem = Vehicles.Items.FirstOrDefault(x => x.Id.Equals(selectedDelivery.VehicleId));
            PickerDeliverer.SelectedItem = People.Items.FirstOrDefault(x => x.Id.Equals(selectedDelivery.DelivererPersonId));
            PickerReceiver.SelectedItem = People.Items.FirstOrDefault(x => x.Id.Equals(selectedDelivery.ReceiverPersonId));
            PickerCarrier.SelectedItem = People.Items.FirstOrDefault(x => x.Id.Equals(selectedDelivery.CarrierPersonId));
            TxtUnbrokenRice.Text = selectedDelivery.UnbrokenRice.ToString();
            TxtBrokenRice.Text = selectedDelivery.BrokenRice.ToString();
            TxtChickenRice.Text = selectedDelivery.ChickenRice.ToString();
            TxtFlour.Text = selectedDelivery.Flour.ToString();
            TxtDescription.Text = selectedDelivery.Description;
            PersianDatePicker.PersianDate = new PersianDateTime(selectedDelivery.DeliveryTime).ToShortDateString();
            TimePicker.Time = selectedDelivery.DeliveryTime.TimeOfDay;
            _isNewDelivery = false;
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
            if (_isNewDelivery && ApplicationStaticContext.CurrentUser.RiceMillId.IsNullOrEmpty())
                return;

            var errorMessage = new StringBuilder();
            var unbrokenRice = TxtUnbrokenRice.Text.ToShort();
            var brokenRice = TxtBrokenRice.Text.ToShort();
            var chickenRice = TxtChickenRice.Text.ToShort();
            var flour = TxtFlour.Text.ToShort();
            if (unbrokenRice < 0)
                errorMessage.AppendLine(ResultStatusEnum.DeliveryUnbrokenRiceIsNotValid.GetErrorMessage());

            if (brokenRice < 0)
                errorMessage.AppendLine(ResultStatusEnum.DeliveryBrokenRiceIsNotValid.GetErrorMessage());

            if (chickenRice < 0)
                errorMessage.AppendLine(ResultStatusEnum.DeliveryChickenRiceIsNotValid.GetErrorMessage());

            if (flour < 0)
                errorMessage.AppendLine(ResultStatusEnum.DeliveryFlourIsNotValid.GetErrorMessage());

            if (unbrokenRice == 0 && brokenRice == 0 && chickenRice == 0 && flour == 0)
                errorMessage.AppendLine(ResultStatusEnum.DeliveryDeliveryValueIsNotValid.GetErrorMessage());

            if (PersianDatePicker.PersianDate.IsNullOrEmpty() || TimePicker.Time.TotalSeconds == 0 ||
                    PersianDateTime.Parse(PersianDatePicker.PersianDate).AddSeconds((int)TimePicker.Time.TotalSeconds) > PersianDateTime.Now)
            {
                errorMessage.AppendLine(ResultStatusEnum.DeliveryDeliveryTimeIsNotValid.GetErrorMessage());
            }
            DtoPerson selectedCarrier = null;
            if (PickerCarrier.SelectedItem is DtoPerson carrierPerson)
                selectedCarrier = carrierPerson;
            else
                errorMessage.AppendLine(ResultStatusEnum.DeliveryCarrierPersonNotFound.GetErrorMessage());

            DtoPerson selectedReceiver = null;
            if (PickerReceiver.SelectedItem is DtoPerson receiverPerson)
                selectedReceiver = receiverPerson;
            else
                errorMessage.AppendLine(ResultStatusEnum.DeliveryReceiverPersonNotFound.GetErrorMessage());

            DtoPerson selectedDeliverer = null;
            if (PickerDeliverer.SelectedItem is DtoPerson delivererPerson)
                selectedDeliverer = delivererPerson;
            else
                errorMessage.AppendLine(ResultStatusEnum.DeliveryDelivererPersonNotFound.GetErrorMessage());

            DtoRiceThreshing selectedRiceThreshing = null;
            if (PickerRiceThreshing.SelectedItem is DtoRiceThreshing riceThreshing)
                selectedRiceThreshing = riceThreshing;
            else
                errorMessage.AppendLine(ResultStatusEnum.RiceThreshingNotFound.GetErrorMessage());

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
            var deliveryTime = PersianDateTime.Parse(PersianDatePicker.PersianDate).AddSeconds((int)TimePicker.Time.TotalSeconds).ToDateTime();
            if (_isNewDelivery)
            {
                var newDelivery = new DtoCreateDelivery(deliveryTime, unbrokenRice, brokenRice, chickenRice, flour, TxtDescription.Text, selectedDeliverer.Id, selectedReceiver.Id, selectedCarrier.Id, selectedVehicle.Id, selectedRiceThreshing.Id, ApplicationStaticContext.CurrentUser.RiceMillId);
                await _deliveryServices.Add(newDelivery);
            }
            else
            {
                if (CVDelivery.SelectedItem is not DtoDelivery selectedDelivery)
                    return;

                var updateDelivery = new DtoUpdateDelivery(selectedDelivery.Id, deliveryTime, unbrokenRice, brokenRice, chickenRice, flour, TxtDescription.Text, selectedDeliverer.Id, selectedReceiver.Id, selectedCarrier.Id, selectedVehicle.Id, selectedRiceThreshing.Id);
                await _deliveryServices.Update(updateDelivery);
            }
            OnNewBtnClicked(null, null);
            await RefreshDeliveryList();
            FillRequireData();
            CVDelivery.ItemsSource = Deliveries.Items;
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
        foreach (var item in Deliveries.Items)
        {
            var riceThreshing = RiceThreshings.Items.FirstOrDefault(x => x.Id.Equals(item.RiceThreshingId));
            var inputLoad = InputLoads.Items.FirstOrDefault(x => x.Id.Equals(riceThreshing.InputLoadId));
            item.DeliveryInfo = $"بار ورودی {inputLoad.InputLoadDetail}";
        }
    }

    private void FillRiceThreshingRequireData()
    {
        foreach (var item in RiceThreshings.Items)
        {
            var inputLoad = InputLoads.Items.FirstOrDefault(x => x.Id.Equals(item.InputLoadId));
            var ownerDetail = People.Items.FirstOrDefault(x => x.Id.Equals(inputLoad?.OwnerPersonId));
            var villageDetail = Villages.Items.FirstOrDefault(x => x.Id.Equals(inputLoad?.VillageId));
            inputLoad.InputLoadDetail = $"{ownerDetail?.FullName ?? "*نامشخص*"} از {villageDetail?.Title ?? "*نامشخص*"} به تعداد {inputLoad?.NumberOfBags} کیسه";
            item.RiceThreshingHumanReadable = $"{inputLoad.InputLoadDetail}{Environment.NewLine}{item.RiceThreshingInfo}";
        }
    }

    private Task RefreshDeliveryList()
    {
        return Task.Run(() =>
        {
            var filter = new DtoDeliveryFilter();
            if (ApplicationStaticContext.IsNotAdmin)
                filter.RiceMillId = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _deliveryServices.Get(filter);
            Deliveries = result.Result.Data;
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

    private Task LoadInputLoads()
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

    private Task LoadRiceThreshings()
    {
        return Task.Run(() =>
        {
            var filter = new DtoRiceThreshingFilter();
            if (ApplicationStaticContext.IsNotAdmin)
                filter.RiceMillId = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _riceThreshingServices.Get(filter);
            RiceThreshings = result.Result.Data;
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
}