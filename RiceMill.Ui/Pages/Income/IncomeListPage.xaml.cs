using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MD.PersianDateTime.Standard;
using Microsoft.Maui.Platform;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.IncomeServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Services.UseCases.IncomeServices;
using Shared.ExtensionMethods;
using System.Text;

namespace RiceMill.Ui.Pages.Income;

public sealed partial class IncomeListPage : ContentPage
{
    private readonly IIncomeServices _incomeServices;
    private PaginatedList<DtoIncome> Incomes;
    private bool _isNewIncome = true;

    public IncomeListPage()
    {
        try
        {
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
            PersianDatePicker.PersianDate = PersianDateTime.Now.ToShortDateString();
            TimePicker.Time = DateTime.Now.TimeOfDay;
            BtnRemove.IsEnabled = !ApplicationStaticContext.IsUser;
            BtnSave.IsEnabled = !ApplicationStaticContext.IsUser;
            BtnNew.IsEnabled = !ApplicationStaticContext.IsUser;
            await RefreshIncomeList();
            CVIncome.ItemsSource = Incomes.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.InnerException.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private async void OnCVIncomeSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (CVIncome.SelectedItem is not DtoIncome selectedIncome)
                return;

            TxtBrokenRice.Text = selectedIncome.BrokenRice.ToString();
            TxtFlour.Text = selectedIncome.Flour.ToString();
            TxtUnbrokenRice.Text = selectedIncome.UnbrokenRice.ToString();
            TxtDescription.Text = selectedIncome.Description;
            PersianDatePicker.PersianDate = new PersianDateTime(selectedIncome.IncomeTime).ToShortDateString();
            TimePicker.Time = selectedIncome.IncomeTime.TimeOfDay;
            _isNewIncome = false;
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
            if (_isNewIncome && ApplicationStaticContext.CurrentUser.RiceMillId.IsNullOrEmpty())
                return;

            var errorMessage = new StringBuilder();
            if (PersianDatePicker.PersianDate.IsNullOrEmpty() || TimePicker.Time.TotalSeconds == 0 ||
                PersianDateTime.Parse(PersianDatePicker.PersianDate).AddSeconds((int)TimePicker.Time.TotalSeconds) > PersianDateTime.Now)
            {
                errorMessage.AppendLine(ResultStatusEnum.PaymentPaymentTimeIsNotValid.GetErrorMessage());
            }
            var unbrokenRiceAmount = TxtUnbrokenRice.Text.ToFloat();
            var brokenRiceAmount = TxtBrokenRice.Text.ToFloat();
            var flourAmount = TxtFlour.Text.ToFloat();
            if (unbrokenRiceAmount == 0 && brokenRiceAmount == 0 && flourAmount == 0)
                errorMessage.AppendLine(ResultStatusEnum.IncomeValueIsNotValid.GetErrorMessage());

            if (errorMessage.IsNotNullOrEmpty())
            {
                await Toast.Make(errorMessage.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            var incomeTime = PersianDateTime.Parse(PersianDatePicker.PersianDate).AddSeconds((int)TimePicker.Time.TotalSeconds);
            if (_isNewIncome)
            {
                var newPayment = new DtoCreateIncome(incomeTime.ToDateTime(), unbrokenRiceAmount, brokenRiceAmount, flourAmount, TxtDescription.Text, ApplicationStaticContext.CurrentUser.RiceMillId);
                await _incomeServices.Add(newPayment);
            }
            else
            {
                if (CVIncome.SelectedItem is not DtoIncome selectedPayment)
                    return;

                var updateIncome = new DtoUpdateIncome(selectedPayment.Id, incomeTime.ToDateTime(), unbrokenRiceAmount, brokenRiceAmount, flourAmount, TxtDescription.Text);
                await _incomeServices.Update(updateIncome);
            }
            OnNewBtnClicked(null, null);
            await RefreshIncomeList();
            CVIncome.ItemsSource = Incomes.Items;
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

    private async void OnBtnRemoveClicked(object sender, EventArgs e)
    {
        try
        {
            if (CVIncome.SelectedItem is not DtoIncome selectedIncome)
            {
                await Toast.Make(ResultStatusEnum.PleaseSelectIncome.GetErrorMessage(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            var questionResult = await DisplayAlert("تاییدیه", "آیا از حذف این مورد اطمینان دارید", "بله", "خیر", FlowDirection.RightToLeft);
            if (!questionResult)
                return;

            await _incomeServices.Delete(selectedIncome.Id);
            OnNewBtnClicked(null, null);
            await RefreshIncomeList();
            CVIncome.ItemsSource = Incomes.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private void OnNewBtnClicked(object sender, EventArgs e)
    {
        CVIncome.SelectedItem = null;
        TxtBrokenRice.Text = string.Empty;
        TxtFlour.Text = string.Empty;
        TxtDescription.Text = string.Empty;
        TxtUnbrokenRice.Text = string.Empty;
        PersianDatePicker.PersianDate = PersianDateTime.Now.ToShortDateString();
        TimePicker.Time = DateTime.Now.TimeOfDay;
        _isNewIncome = true;
    }

    private Task RefreshIncomeList()
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