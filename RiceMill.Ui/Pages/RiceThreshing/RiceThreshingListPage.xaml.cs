using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MD.PersianDateTime.Standard;
using Microsoft.Maui.Platform;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.IncomeServices.Dto;
using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Application.UseCases.RiceMillServices.Dto;
using RiceMill.Application.UseCases.RiceThreshingServices.Dto;
using RiceMill.Application.UseCases.VillageServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Services.UseCases.IncomeServices;
using RiceMill.Ui.Services.UseCases.InputLoadServices;
using RiceMill.Ui.Services.UseCases.PersonServices;
using RiceMill.Ui.Services.UseCases.RiceMillServices;
using RiceMill.Ui.Services.UseCases.RiceThreshingServices;
using RiceMill.Ui.Services.UseCases.VillageServices;
using Shared.ExtensionMethods;
using System.Text;

namespace RiceMill.Ui.Pages.RiceThreshing;

public sealed partial class RiceThreshingListPage : ContentPage
{
    private readonly IRiceThreshingServices _riceThreshingServices;
    private readonly IInputLoadServices _inputLoadServices;
    private readonly IPersonServices _personServices;
    private readonly IVillageServices _villageServices;
    private readonly IIncomeServices _incomeServices;
    private readonly IRiceMillServices _riceMillServices;
    private PaginatedList<DtoRiceThreshing> RiceThresgings;
    private PaginatedList<DtoInputLoad> InputLoads;
    private PaginatedList<DtoPerson> People;
    private PaginatedList<DtoVillage> Villages;
    private PaginatedList<DtoIncome> Incomes;
    private DtoRiceMill CurrentRiceMill;
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
            _riceMillServices = new RiceMillServices();
            InitializeComponent();
            InitializeAsync();
        }
        catch (Exception ex)
        {
            Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await LoadRiceMill();
        if (ApplicationStaticContext.CurrentUser.RiceMillId.IsNullOrEmpty() || CurrentRiceMill == null)
        {
            await Toast.Make("کارخانه ای برای شما تعیین نشده", ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
            BtnRemove.IsEnabled = false;
            BtnSave.IsEnabled = false;
            BtnNew.IsEnabled = false;
            CVRiceThreshing.IsEnabled = false;
        }
    }

    private async void InitializeAsync()
    {
        try
        {
            PersianDatePickerStart.PersianDate = PersianDateTime.Now.ToShortDateString();
            TimePickerStart.Time = PersianDateTime.Now.SetTime(8, 0).GetTime();
            PersianDatePickerEnd.PersianDate = PersianDateTime.Now.ToShortDateString();
            TimePickerEnd.Time = DateTime.Now.TimeOfDay;
            BtnRemove.IsEnabled = ApplicationStaticContext.IsManager || ApplicationStaticContext.IsAdmin;
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
        PersianDatePickerStart.PersianDate = PersianDateTime.Now.ToShortDateString();
        TimePickerStart.Time = PersianDateTime.Now.SetTime(8, 0).GetTime();
        PersianDatePickerEnd.PersianDate = PersianDateTime.Now.ToShortDateString();
        TimePickerEnd.Time = DateTime.Now.TimeOfDay;
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
            var questionResult = await DisplayAlert("تاییدیه", "آیا از حذف این مورد اطمینان دارید", "بله", "خیر", FlowDirection.RightToLeft);
            if (!questionResult)
                return;

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

    private async void OnBtnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            if (_isNewRiceThreshing && ApplicationStaticContext.CurrentUser.RiceMillId.IsNullOrEmpty())
                return;

            var errorMessage = new StringBuilder();
            DtoInputLoad selectedInputLoad = null;
            if (PickerInputLoad.SelectedItem is DtoInputLoad inputLoad)
                selectedInputLoad = inputLoad;
            else
                errorMessage.AppendLine(ResultStatusEnum.InputLoadNotFound.GetErrorMessage());

            if (PersianDatePickerStart.PersianDate.IsNullOrEmpty() || TimePickerStart.Time.TotalSeconds == 0)
                errorMessage.AppendLine(ResultStatusEnum.RiceThreshingStartTimeIsNotValid.GetErrorMessage());

            if (PersianDatePickerEnd.PersianDate.IsNullOrEmpty() || TimePickerEnd.Time.TotalSeconds == 0)
                errorMessage.AppendLine(ResultStatusEnum.RiceThreshingEndTimeIsNotValid.GetErrorMessage());

            var startDate = PersianDateTime.Parse(PersianDatePickerStart.PersianDate).AddSeconds((int)TimePickerStart.Time.TotalSeconds);
            var endDate = PersianDateTime.Parse(PersianDatePickerEnd.PersianDate).AddSeconds((int)TimePickerEnd.Time.TotalSeconds);
            if (startDate > endDate)
                errorMessage.AppendLine(ResultStatusEnum.RiceThreshingEndTimeShouldGreaterThanStartTime.GetErrorMessage());

            var unbrokenRiceAmount = TxtUnbrokenRice.Text.ToFloat();
            var brokenRiceAmount = TxtBrokenRice.Text.ToFloat();
            if (unbrokenRiceAmount == 0)
                errorMessage.AppendLine(ResultStatusEnum.RiceThreshingUnbrokenRiceIsNotValid.GetErrorMessage());

            if (brokenRiceAmount == 0)
                errorMessage.AppendLine(ResultStatusEnum.RiceThreshingBrokenRiceIsNotValid.GetErrorMessage());

            if (errorMessage.IsNotNullOrEmpty())
            {
                await Toast.Make(errorMessage.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            var flourAmount = TxtFlour.Text.ToFloat();
            var chickenAmount = TxtChickenRice.Text.ToInt();
            DtoIncome selectedIncome = null;
            if (PickerIncome.SelectedItem is DtoIncome income)
            {
                selectedIncome = income;
            }
            else
            {
                var wagePercent = CurrentRiceMill.Wage / (float)100;
                var unbrokenWage = (wagePercent * unbrokenRiceAmount).ToString("n1");
                var brokenWage = (wagePercent * brokenRiceAmount).ToString("n1");
                var chickenWage = (wagePercent * chickenAmount).ToString("n1");
                var flourWage = (wagePercent * flourAmount).ToString("n1");
                var wageInfo = $"بلند: {unbrokenWage} کیلوگرم{Environment.NewLine}" +
                    $"نیمه: {brokenWage} کیلوگرم{Environment.NewLine}" +
                    $"مرغی: {chickenWage} کیلوگرم{Environment.NewLine}" +
                    $"آرد: {flourWage} کیلوگرم{Environment.NewLine}" +
                    "برای تایید بله را انتخاب و در صورت عدم تایید مقادیر را با - و ترتیب ذکر شده مانند نمونه وارد کنید";
                string result = await DisplayPromptAsync("آیا با ثبت درآمد خودکار به شکل زیر موافق هستید؟", wageInfo, "بله", "خیر", initialValue: $"{unbrokenWage}-{brokenWage}-{chickenWage}-{flourWage}");
                if (result == null)
                {
                    await Toast.Make(ResultStatusEnum.IncomeNotFound.GetErrorMessage().ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                    return;
                }
                if (result.IsNotNullOrEmpty())
                {
                    var wageDetail = result.Split("-");
                    if (wageDetail.Length != 4 || wageDetail.Any(x => x.IsNullOrEmpty()))
                    {
                        await Toast.Make(ResultStatusEnum.IncomeValueIsNotValid.GetErrorMessage().ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                        return;
                    }
                    unbrokenWage = wageDetail[0];
                    brokenWage = wageDetail[1];
                    chickenWage = wageDetail[2];
                    flourWage = wageDetail[3];
                    var createIncome = new DtoCreateIncome(DateTime.Now, unbrokenWage.ToFloat(), brokenWage.ToFloat(), flourWage.ToFloat(), TxtDescription.Text, ApplicationStaticContext.CurrentUser.RiceMillId);
                    var incomeData = await _incomeServices.Add(createIncome);
                    selectedIncome = incomeData.Data;
                    await LoadIncomes();
                }
            }
            if (_isNewRiceThreshing)
            {
                var newRiceThreshing = new DtoCreateRiceThreshing(startDate.ToDateTime(), endDate.ToDateTime(), unbrokenRiceAmount, brokenRiceAmount, chickenAmount, flourAmount, TxtDescription.Text, selectedInputLoad.Id, selectedIncome.Id, ApplicationStaticContext.CurrentUser.RiceMillId);
                await _riceThreshingServices.Add(newRiceThreshing);
            }
            else
            {
                if (CVRiceThreshing.SelectedItem is not DtoRiceThreshing selectedRiceThreshing)
                    return;

                var updatePayment = new DtoUpdateRiceThreshing(selectedRiceThreshing.Id, startDate.ToDateTime(), endDate.ToDateTime(), unbrokenRiceAmount, brokenRiceAmount, chickenAmount, flourAmount, TxtDescription.Text, selectedInputLoad.Id, selectedIncome.Id);
                await _riceThreshingServices.Update(updatePayment);
            }
            OnNewBtnClicked(null, null);
            await RefreshRiceThreshingList();
            FillRequireData();
            CVRiceThreshing.ItemsSource = RiceThresgings.Items;
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
        foreach (var item in RiceThresgings.Items)
        {
            var inputLoad = InputLoads.Items.FirstOrDefault(x => x.Id.Equals(item.InputLoadId));
            item.InputLoadInfo = inputLoad.InputLoadDetail;
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

    private Task LoadRiceMill()
    {
        return Task.Run(() =>
        {
            var filter = new DtoRiceMillFilter { Id = ApplicationStaticContext.CurrentUser.RiceMillId };
            var result = _riceMillServices.Get(filter);
            CurrentRiceMill = result.Result.Data.Items.FirstOrDefault();
        });
    }
}