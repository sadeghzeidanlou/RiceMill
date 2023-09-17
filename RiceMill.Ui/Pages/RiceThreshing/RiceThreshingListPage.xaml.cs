using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MD.PersianDateTime.Standard;
using PersianDatePickerMAUI.Controls;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Application.UseCases.IncomeServices.Dto;
using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Application.UseCases.PaymentServices.Dto;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Application.UseCases.RiceThreshingServices.Dto;
using RiceMill.Application.UseCases.VillageServices.Dto;
using RiceMill.Domain.Models;
using RiceMill.Ui.Common;
using RiceMill.Ui.Services.UseCases.IncomeServices;
using RiceMill.Ui.Services.UseCases.InputLoadServices;
using RiceMill.Ui.Services.UseCases.PersonServices;
using RiceMill.Ui.Services.UseCases.RiceThreshingServices;
using RiceMill.Ui.Services.UseCases.VillageServices;
using Shared.ExtensionMethods;
namespace RiceMill.Ui.Pages.RiceThreshing;

public sealed partial class RiceThreshingListPage : ContentPage
{
    private readonly IRiceThreshingServices _riceThreshingServices;
    private readonly IInputLoadServices _inputLoadServices;
    private readonly IPersonServices _personServices;
    private readonly IVillageServices _villageServices;
    private readonly IIncomeServices _incomeServices;
    private PaginatedList<DtoRiceThreshing> RiceThresgings;
    private PaginatedList<DtoInputLoad> InputLoads;
    private PaginatedList<DtoPerson> People;
    private PaginatedList<DtoVillage> Villages;
    private PaginatedList<DtoIncome> Incomes;
    private bool _isNewRiceThreshing = true;

    public RiceThreshingListPage()
    {
        try
        {
            _riceThreshingServices = new RiceThreshingServices();
            _inputLoadServices = new InputLoadServices();
            _personServices = new PersonServices();
            _villageServices = new VillageServices();
            _incomeServices = new IncomeServices();
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
            await LoadVillages();
            await LoadInputLoads();
            await LoadIncomes();
            await RefreshRiceThreshingList();
            FillInputLoadRequireData();
            FillRequireData();
            CVRiceThreshing.ItemsSource = RiceThresgings.Items;
            PickerInputLoad.ItemsSource = InputLoads.Items;
            PickerIncome.ItemsSource = Incomes.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.InnerException.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private void OnNewBtnClicked(object sender, EventArgs e)
    {
        CVRiceThreshing.SelectedItem = null;
        PickerInputLoad.SelectedItem = null;
        PickerIncome.SelectedItem = null;
        TxtUnbrokenRice.Text = string.Empty;
        TxtBrokenRice.Text = string.Empty;
        TxtChickenRice.Text = string.Empty;
        TxtFlour.Text = string.Empty;
        TxtDescription.Text = string.Empty;
        PersianDatePickerStart.PersianDate = string.Empty;
        TimePickerStart.Time = TimeSpan.Zero;
        PersianDatePickerEnd.PersianDate = string.Empty;
        TimePickerEnd.Time = TimeSpan.Zero;
        LblIsDeliverd.Text = string.Empty;
        _isNewRiceThreshing = true;
    }

    private async void OnBtnRemoveClicked(object sender, EventArgs e)
    {
        try
        {
            if (CVRiceThreshing.SelectedItem is not DtoRiceThreshing selectedRiceThreshing)
            {
                await Toast.Make(ResultStatusEnum.PleaseSelectRiceThreshing.GetErrorMessage(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            await _riceThreshingServices.Delete(selectedRiceThreshing.Id);
            OnNewBtnClicked(null, null);
            await RefreshRiceThreshingList();
            FillRequireData();
            CVRiceThreshing.ItemsSource = RiceThresgings.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private async void OnCVRiceThreshingSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (CVRiceThreshing.SelectedItem is not DtoRiceThreshing selectedRiceThreshing)
                return;

            PickerInputLoad.SelectedItem = InputLoads.Items.FirstOrDefault(x => x.Id.Equals(selectedRiceThreshing.InputLoadId));
            PickerIncome.SelectedItem = Incomes.Items.FirstOrDefault(x => x.Id.Equals(selectedRiceThreshing.IncomeId));
            TxtUnbrokenRice.Text = selectedRiceThreshing.UnbrokenRice.ToString();
            TxtBrokenRice.Text = selectedRiceThreshing.BrokenRice.ToString();
            TxtChickenRice.Text = selectedRiceThreshing.ChickenRice.ToString();
            TxtFlour.Text = selectedRiceThreshing.Flour.ToString();
            TxtDescription.Text = selectedRiceThreshing.Description;
            PersianDatePickerStart.PersianDate = new PersianDateTime(selectedRiceThreshing.StartTime).ToShortDateString();
            TimePickerStart.Time = selectedRiceThreshing.StartTime.TimeOfDay;
            PersianDatePickerEnd.PersianDate = new PersianDateTime(selectedRiceThreshing.EndTime).ToShortDateString();
            TimePickerEnd.Time = selectedRiceThreshing.EndTime.TimeOfDay;
            LblIsDeliverd.Text = $"شالیکوبی تحویل داده {(selectedRiceThreshing.IsDelivered ? "شده" : "نشده")}";
            _isNewRiceThreshing = false;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private void OnBtnSaveClicked(object sender, EventArgs e)
    {

    }

    private void FillRequireData()
    {
        foreach (var item in RiceThresgings.Items)
        {
            //var paidPersonDetail = People.Items.FirstOrDefault(x => x.Id.Equals(item.PaidPersonId));
            //var paidConcernDetail = Concerns.Items.FirstOrDefault(x => x.Id.Equals(item.ConcernId));
            //item.PaidPersonFullName = $"{paidPersonDetail?.FullName ?? "*نامشخص*"} بابت {paidConcernDetail?.Title ?? "*نامشخص*"}";
        }
    }

    private void FillInputLoadRequireData()
    {
        foreach (var item in InputLoads.Items)
        {
            var ownerDetail = People.Items.FirstOrDefault(x => x.Id.Equals(item.OwnerPersonId));
            var villageDetail = Villages.Items.FirstOrDefault(x => x.Id.Equals(item.VillageId));
            item.InputLoadDetail = $"{ownerDetail?.FullName ?? "*نامشخص*"} از {villageDetail?.Title ?? "*نامشخص*"} به تعداد {item.NumberOfBags} کیسه";
        }
    }

    private Task RefreshRiceThreshingList()
    {
        return Task.Run(() =>
        {
            var filter = new DtoRiceThreshingFilter();
            if (!ApplicationStaticContext.IsAdmin)
                filter.RiceMillId = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _riceThreshingServices.Get(filter);
            RiceThresgings = result.Result.Data;
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

    private Task LoadIncomes()
    {
        return Task.Run(() =>
        {
            var filter = new DtoIncomeFilter();
            if (!ApplicationStaticContext.IsAdmin)
                filter.RiceMillId = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _incomeServices.Get(filter);
            Incomes = result.Result.Data;
        });
    }
}