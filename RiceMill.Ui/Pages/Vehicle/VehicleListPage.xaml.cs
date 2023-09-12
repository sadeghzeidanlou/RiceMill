using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Platform;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Application.UseCases.VehicleServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Services.UseCases.PersonServices;
using RiceMill.Ui.Services.UseCases.VehicleServices;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Text;

namespace RiceMill.Ui.Pages.Vehicle;

public partial class VehicleListPage : ContentPage
{
    private readonly IVehicleServices _vehicleServices;
    private readonly IPersonServices _personServices;
    private PaginatedList<DtoVehicle> Vehicles;
    private PaginatedList<DtoPerson> People;
    private bool _isNewVehicle = true;

    public VehicleListPage()
    {
        try
        {
            _vehicleServices = new VehicleServices();
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
            BtnRemove.IsEnabled = !ApplicationStaticContext.IsUser;
            BtnSave.IsEnabled = !ApplicationStaticContext.IsUser;
            BtnNew.IsEnabled = !ApplicationStaticContext.IsUser;
            await LoadPeople();
            //_ = Task.WhenAny(LoadPeople());
            await RefreshVehicleList();
            CVVehicle.ItemsSource = Vehicles.Items;
            PickerOwner.ItemsSource = People.Items;
            PickerType.ItemsSource = VehicleType.GetAll;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.InnerException.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private void OnNewBtnClicked(object sender, EventArgs e)
    {
        CVVehicle.SelectedItem = null;
        PickerOwner.SelectedItem = null;
        PickerType.SelectedItem = null;
        TxtDescription.Text = string.Empty;
        TxtPlate.Text = string.Empty;
        _isNewVehicle = true;
    }

    private async void OnBtnRemoveClicked(object sender, EventArgs e)
    {
        try
        {
            if (CVVehicle.SelectedItem is not DtoVehicle selectedVehicle)
            {
                await Toast.Make(ResultStatusEnum.PleaseSelectVehicle.GetErrorMessage(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            await _vehicleServices.Delete(selectedVehicle.Id);
            OnNewBtnClicked(null, null);
            await RefreshVehicleList();
            CVVehicle.ItemsSource = Vehicles.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private async void OnCVVehicleSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (CVVehicle.SelectedItem is not DtoVehicle selectedVehicle)
                return;

            TxtDescription.Text = selectedVehicle.Description;
            TxtPlate.Text = selectedVehicle.Plate;
            PickerType.SelectedIndex = VehicleType.GetAll.FirstOrDefault(x => x.Type == selectedVehicle.VehicleType).Index;
            PickerOwner.SelectedItem = People.Items.FirstOrDefault(x => x.Id.Equals(selectedVehicle.OwnerPersonId));
            _isNewVehicle = false;
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
            if (_isNewVehicle && ApplicationStaticContext.CurrentUser.RiceMillId.IsNullOrEmpty())
                return;

            var errorMessage = new StringBuilder();
            VehicleType selectedType = null;
            if (PickerType.SelectedItem is VehicleType type)
                selectedType = type;
            else
                errorMessage.AppendLine(ResultStatusEnum.VehicleVehicleTypeIsNotValid.GetErrorMessage());

            DtoPerson selectedOwner = null;
            if (PickerOwner.SelectedItem is DtoPerson owner)
                selectedOwner = owner;
            else
                errorMessage.AppendLine(ResultStatusEnum.VehicleOwnerPersonIdIsNotValid.GetErrorMessage());

            if (TxtPlate.Text.IsNullOrEmpty())
                errorMessage.AppendLine(ResultStatusEnum.VehiclePlateIsNotValid.GetErrorMessage());

            if (errorMessage.IsNotNullOrEmpty())
            {
                await Toast.Make(errorMessage.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            if (_isNewVehicle)
            {
                var newVehicle = new DtoCreateVehicle(TxtPlate.Text, TxtDescription.Text, selectedType.Type, selectedOwner.Id, ApplicationStaticContext.CurrentUser.RiceMillId);
                await _vehicleServices.Add(newVehicle);
            }
            else
            {
                if (CVVehicle.SelectedItem is not DtoVehicle selectedVehicle)
                    return;

                var updateVehicle = new DtoUpdateVehicle(selectedVehicle.Id, TxtPlate.Text, TxtDescription.Text, selectedType.Type, selectedOwner.Id);
                await _vehicleServices.Update(updateVehicle);
            }
            OnNewBtnClicked(null, null);
            await RefreshVehicleList();
            CVVehicle.ItemsSource = Vehicles.Items;
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

    private Task RefreshVehicleList()
    {
        return Task.Run(() =>
        {
            var filter = new DtoVehicleFilter();
            if (!ApplicationStaticContext.IsAdmin)
                filter.RiceMillId = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _vehicleServices.Get(filter);
            Vehicles = result.Result.Data;
            Vehicles.Items.ForEach(item => { item.OwnerFullName = People.Items.FirstOrDefault(x => x.Id.Equals(item.OwnerPersonId))?.FullName; });
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