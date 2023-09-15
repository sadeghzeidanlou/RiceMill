using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MD.PersianDateTime.Standard;
using Microsoft.Maui.Platform;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.DryerHistoryServices.Dto;
using RiceMill.Application.UseCases.DryerServices.Dto;
using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Application.UseCases.VillageServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Services.UseCases.DryerHistoryServices;
using RiceMill.Ui.Services.UseCases.DryerServices;
using RiceMill.Ui.Services.UseCases.InputLoadServices;
using RiceMill.Ui.Services.UseCases.PersonServices;
using RiceMill.Ui.Services.UseCases.VillageServices;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Text;

namespace RiceMill.Ui.Pages.DryerHistory;

public sealed partial class DryerHistoryListPage : ContentPage
{
    private readonly IDryerHistoryServices _dryerHistoryServices;
    private readonly IInputLoadServices _inputLoadServices;
    private readonly IDryerServices _dryerServices;
    private readonly IPersonServices _personServices;
    private readonly IVillageServices _villageServices;
    private PaginatedList<DtoDryerHistory> DryerHistories;
    private PaginatedList<DtoInputLoad> InputLoads;
    private PaginatedList<DtoDryer> Dryers;
    private PaginatedList<DtoPerson> People;
    private PaginatedList<DtoVillage> Villages;
    private bool _isNewDryerHistory = true;

    public DryerHistoryListPage()
    {
        try
        {
            _dryerHistoryServices = new DryerHistoryServices();
            _inputLoadServices = new InputLoadServices();
            _dryerServices = new DryerServices();
            _personServices = new PersonServices();
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
            PersianDatePickerStart.PersianDate = new PersianDateTime(DateTime.Now).ToShortDateString();
            TimePickerStart.Time = DateTime.Now.TimeOfDay;
            PersianDatePickerEnd.PersianDate = new PersianDateTime(DateTime.Now.AddDays(1)).ToShortDateString();
            TimePickerEnd.Time = DateTime.Now.TimeOfDay;
            BtnRemove.IsEnabled = !ApplicationStaticContext.IsUser;
            BtnSave.IsEnabled = !ApplicationStaticContext.IsUser;
            BtnNew.IsEnabled = !ApplicationStaticContext.IsUser;
            await LoadDryers();
            await LoadPeople();
            await LoadVillages();
            await LoadInputLoads();
            await RefreshDryerHistoryList();
            FillRequireData();
            FillInputLoadRequireData();
            CVDryerHistory.ItemsSource = DryerHistories.Items;
            PickerDryer.ItemsSource = Dryers.Items;
            PickerInputLoad.ItemsSource = InputLoads.Items;
            PickerDryerOperation.ItemsSource = DryerOperation.GetAll;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.InnerException.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private async void OnBtnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            if (_isNewDryerHistory && ApplicationStaticContext.CurrentUser.RiceMillId.IsNullOrEmpty())
                return;

            var errorMessage = new StringBuilder();
            DtoDryer selectedDryer = null;
            if (PickerDryer.SelectedItem is DtoDryer dryer)
                selectedDryer = dryer;
            else
                errorMessage.AppendLine(ResultStatusEnum.DryerNotFound.GetErrorMessage());

            DryerOperation selectedDryerOperation = null;
            if (PickerDryerOperation.SelectedItem is DryerOperation dryerOperation)
                selectedDryerOperation = dryerOperation;
            else
                errorMessage.AppendLine(ResultStatusEnum.InputLoadDelivererPersonNotFound.GetErrorMessage());

            if (PersianDatePickerStart.PersianDate.IsNullOrEmpty() || TimePickerStart.Time.TotalSeconds == 0 ||
                PersianDateTime.Parse(PersianDatePickerStart.PersianDate).AddSeconds((int)TimePickerStart.Time.TotalSeconds) > PersianDateTime.Now)
            {
                errorMessage.AppendLine(ResultStatusEnum.DryerHistoryStartTimeIsNotValid.GetErrorMessage());
            }
            if (PersianDatePickerEnd.PersianDate.IsNotNullOrEmpty() && TimePickerEnd.Time.TotalSeconds != 0 &&
                PersianDateTime.Parse(PersianDatePickerEnd.PersianDate).AddSeconds((int)TimePickerEnd.Time.TotalSeconds) < PersianDateTime.Parse(PersianDatePickerStart.PersianDate).AddSeconds((int)TimePickerStart.Time.TotalSeconds))
            {
                errorMessage.AppendLine(ResultStatusEnum.DryerHistoryStopTimeIsNotValid.GetErrorMessage());
            }
            if (selectedDryerOperation?.Operation == DryerOperationEnum.Load && TxtNumberOfBags.Text.ToShort() < 1)
                errorMessage.AppendLine(ResultStatusEnum.DryerHistoryNumberOfBagIsNotValid.GetErrorMessage());

            DtoInputLoad selectedInputLoad = null;
            if (PickerInputLoad.SelectedItem is DtoInputLoad inputLoad)
                selectedInputLoad = inputLoad;
            else
                errorMessage.AppendLine(ResultStatusEnum.InputLoadNotFound.GetErrorMessage());

            if (errorMessage.IsNotNullOrEmpty())
            {
                await Toast.Make(errorMessage.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            var startTime = PersianDateTime.Parse(PersianDatePickerStart.PersianDate).AddSeconds((int)TimePickerStart.Time.TotalSeconds).ToDateTime();

            DateTime? endDate = null;
            if (PersianDatePickerEnd.PersianDate.IsNotNullOrEmpty() && TimePickerEnd.Time.TotalSeconds != 0)
                endDate = PersianDateTime.Parse(PersianDatePickerEnd.PersianDate).AddSeconds((int)TimePickerEnd.Time.TotalSeconds).ToDateTime();

            if (_isNewDryerHistory)
            {
                var newDryerHistory = new DtoCreateDryerHistory(selectedDryerOperation.Operation, startTime, endDate, selectedDryer.Id, null, selectedInputLoad.Id, TxtNumberOfBags.Text.ToShort(), ApplicationStaticContext.CurrentUser.RiceMillId);
                await _dryerHistoryServices.Add(newDryerHistory);
            }
            else
            {
                if (CVDryerHistory.SelectedItem is not DtoDryerHistory selectedDryerHistory)
                    return;

                var updateInputLoad = new DtoUpdateDryerHistory(selectedDryerHistory.Id, selectedDryerOperation.Operation, startTime, endDate, selectedDryer.Id, selectedDryerHistory.RiceThreshingId, selectedInputLoad.Id);
                await _dryerHistoryServices.Update(updateInputLoad);
            }
            OnNewBtnClicked(null, null);
            await RefreshDryerHistoryList();
            FillRequireData();
            CVDryerHistory.ItemsSource = InputLoads.Items;
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

    private async void OnCVDryerHistorySelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (CVDryerHistory.SelectedItem is not DtoDryerHistory selectedDryerHistory)
                return;

            PickerDryer.SelectedItem = Dryers.Items.FirstOrDefault(x => x.Id.Equals(selectedDryerHistory.DryerId));
            PickerDryerOperation.SelectedIndex = DryerOperation.GetAll.FirstOrDefault(x => x.Operation == selectedDryerHistory.Operation).Index;
            //PickerInputLoad.SelectedItem = InputLoads.Items.FirstOrDefault(x => x.Id.Equals(selectedDryerHistory.));
            PersianDatePickerStart.PersianDate = new PersianDateTime(selectedDryerHistory.StartTime).ToShortDateString();
            TimePickerStart.Time = selectedDryerHistory.StartTime.TimeOfDay;
            if (!selectedDryerHistory.EndTime.HasValue)
            {
                PersianDatePickerEnd.PersianDate = string.Empty;
                TimePickerEnd.Time = TimeSpan.Zero;
            }
            else
            {
                PersianDatePickerEnd.PersianDate = new PersianDateTime(selectedDryerHistory.EndTime.Value).ToShortDateString();
                TimePickerEnd.Time = selectedDryerHistory.EndTime.Value.TimeOfDay;
            }
            _isNewDryerHistory = false;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private async void OnBtnRemoveClicked(object sender, EventArgs e)
    {
        try
        {
            if (CVDryerHistory.SelectedItem is not DtoDryerHistory selectedDryerHistory)
            {
                await Toast.Make(ResultStatusEnum.PleaseSelectDryerHistory.GetErrorMessage(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            await _inputLoadServices.Delete(selectedDryerHistory.Id);
            OnNewBtnClicked(null, null);
            await RefreshDryerHistoryList();
            FillRequireData();
            CVDryerHistory.ItemsSource = DryerHistories.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private void OnNewBtnClicked(object sender, EventArgs e)
    {
        PickerInputLoad.SelectedItem = null;
        CVDryerHistory.SelectedItem = null;
        PickerDryer.SelectedItem = null;
        PickerDryerOperation.SelectedItem = null;
        PersianDatePickerStart.PersianDate = new PersianDateTime(DateTime.Now).ToShortDateString();
        TimePickerStart.Time = DateTime.Now.TimeOfDay;
        PersianDatePickerEnd.PersianDate = new PersianDateTime(DateTime.Now.AddDays(1)).ToShortDateString();
        TimePickerEnd.Time = DateTime.Now.TimeOfDay;
        _isNewDryerHistory = true;
    }


    private void FillRequireData()
    {
        foreach (var item in DryerHistories.Items)
        {
            var dryer = Dryers.Items.FirstOrDefault(x => x.Id.Equals(item.DryerId));
            item.DryerHistoryReadable = $"{item.HumaneReadable} خشک کن ({dryer?.Title ?? "*نامشخص*"})";
        }
    }

    private void FillInputLoadRequireData()
    {
        foreach (var item in InputLoads.Items)
        {
            var ownerDetail = People.Items.FirstOrDefault(x => x.Id.Equals(item.OwnerPersonId));
            var villageDetail = Villages.Items.FirstOrDefault(x => x.Id.Equals(item.VillageId));
            item.InputLoadDetail = $"{ownerDetail?.FullName ?? "نامشخص"} از {villageDetail?.Title ?? "نامشخص"} به تعداد {item.NumberOfBags} کیسه";
        }
    }

    private Task RefreshDryerHistoryList()
    {
        return Task.Run(() =>
        {
            var filter = new DtoDryerHistoryFilter();
            if (!ApplicationStaticContext.IsAdmin)
                filter.RiceMillId = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _dryerHistoryServices.Get(filter);
            DryerHistories = result.Result.Data;
        });
    }

    private Task LoadDryers()
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

    private Task LoadVillages()
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

    private Task LoadInputLoads()
    {
        return Task.Run(() =>
        {
            var filter = new DtoInputLoadFilter();
            if (!ApplicationStaticContext.IsAdmin)
                filter.RiceMillId = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _inputLoadServices.Get(filter);
            InputLoads = result.Result.Data;
        });
    }
}