using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MD.PersianDateTime.Standard;
using Microsoft.Maui.Platform;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Application.UseCases.InputLoadServices.Dto;
using RiceMill.Application.UseCases.PaymentServices.Dto;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Application.UseCases.VillageServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Services.UseCases.ConcernServices;
using RiceMill.Ui.Services.UseCases.InputLoadServices;
using RiceMill.Ui.Services.UseCases.PaymentServices;
using RiceMill.Ui.Services.UseCases.PersonServices;
using RiceMill.Ui.Services.UseCases.VillageServices;
using Shared.ExtensionMethods;
using System.Text;

namespace RiceMill.Ui.Pages.Payment;

public sealed partial class PaymentListPage : ContentPage
{
    private readonly IPaymentServices _paymentServices;
    private readonly IInputLoadServices _inputLoadServices;
    private readonly IPersonServices _personServices;
    private readonly IConcernServices _concernServices;
    private readonly IVillageServices _villageServices;
    private PaginatedList<DtoPayment> Payments;
    private PaginatedList<DtoInputLoad> InputLoads;
    private PaginatedList<DtoPerson> People;
    private PaginatedList<DtoConcern> Concerns;
    private PaginatedList<DtoVillage> Villages;
    private bool _isNewPayment = true;

    public PaymentListPage()
    {
        try
        {
            _paymentServices = new PaymentServices();
            _personServices = new PersonServices();
            _inputLoadServices = new InputLoadServices();
            _concernServices = new ConcernServices();
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
            PersianDatePicker.PersianDate = new PersianDateTime(DateTime.Now).ToShortDateString();
            TimePicker.Time = DateTime.Now.TimeOfDay;
            BtnRemove.IsEnabled = !ApplicationStaticContext.IsUser;
            BtnSave.IsEnabled = !ApplicationStaticContext.IsUser;
            BtnNew.IsEnabled = !ApplicationStaticContext.IsUser;
            await LoadPeople();
            await LoadVillages();
            await LoadConcerns();
            await LoadInputLoads();
            await RefreshPaymentList();
            FillRequireData();
            FillInputLoadRequireData();
            CVPayment.ItemsSource = Payments.Items;
            PickerPaidConcern.ItemsSource = Concerns.Items;
            PickerPaidPerson.ItemsSource = People.Items;
            PickerInputLoad.ItemsSource = InputLoads.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.InnerException.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private void OnNewBtnClicked(object sender, EventArgs e)
    {
        CVPayment.SelectedItem = null;
        PickerPaidConcern.SelectedItem = null;
        PickerPaidPerson.SelectedItem = null;
        PickerInputLoad.SelectedItem = null;
        TxtBrokenRice.Text = string.Empty;
        TxtFlour.Text = string.Empty;
        TxtDescription.Text = string.Empty;
        TxtMoney.Text = string.Empty;
        TxtUnbrokenRice.Text = string.Empty;
        PersianDatePicker.PersianDate = new PersianDateTime(DateTime.Now).ToShortDateString();
        TimePicker.Time = DateTime.Now.TimeOfDay;
        _isNewPayment = true;
    }

    private async void OnBtnRemoveClicked(object sender, EventArgs e)
    {
        try
        {
            if (CVPayment.SelectedItem is not DtoPayment selectedPayment)
            {
                await Toast.Make(ResultStatusEnum.PleaseSelectPayment.GetErrorMessage(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            await _paymentServices.Delete(selectedPayment.Id);
            OnNewBtnClicked(null, null);
            await RefreshPaymentList();
            FillRequireData();
            CVPayment.ItemsSource = Payments.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private async void OnCVPaymentSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (CVPayment.SelectedItem is not DtoPayment selectedPayment)
                return;

            PickerPaidConcern.SelectedItem = Concerns.Items.FirstOrDefault(x => x.Id.Equals(selectedPayment.ConcernId));
            PickerPaidPerson.SelectedItem = People.Items.FirstOrDefault(x => x.Id.Equals(selectedPayment.PaidPersonId));
            PickerInputLoad.SelectedItem = selectedPayment.InputLoadId.IsNullOrEmpty() ? null : (object)InputLoads.Items.FirstOrDefault(x => x.Id.Equals(selectedPayment.InputLoadId));
            TxtBrokenRice.Text = selectedPayment.BrokenRice.ToString();
            TxtFlour.Text = selectedPayment.Flour.ToString();
            TxtMoney.Text = selectedPayment.Money.ToString();
            TxtUnbrokenRice.Text = selectedPayment.UnbrokenRice.ToString();
            TxtDescription.Text = selectedPayment.Description;
            PersianDatePicker.PersianDate = new PersianDateTime(selectedPayment.PaymentTime).ToShortDateString();
            TimePicker.Time = selectedPayment.PaymentTime.TimeOfDay;
            _isNewPayment = false;
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
            if (_isNewPayment && ApplicationStaticContext.CurrentUser.RiceMillId.IsNullOrEmpty())
                return;

            var errorMessage = new StringBuilder();
            DtoPerson selectedPaidPerson = null;
            if (PickerPaidPerson.SelectedItem is DtoPerson paidPerson)
                selectedPaidPerson = paidPerson;
            else
                errorMessage.AppendLine(ResultStatusEnum.PaymentPaidPersonIdIsNotValid.GetErrorMessage());

            DtoConcern selectedPaidConcern = null;
            if (PickerPaidConcern.SelectedItem is DtoConcern paidConcern)
                selectedPaidConcern = paidConcern;
            else
                errorMessage.AppendLine(ResultStatusEnum.ConcernNotFound.GetErrorMessage());

            if (PersianDatePicker.PersianDate.IsNullOrEmpty() || TimePicker.Time.TotalSeconds == 0 ||
                PersianDateTime.Parse(PersianDatePicker.PersianDate).AddSeconds((int)TimePicker.Time.TotalSeconds) > PersianDateTime.Now)
            {
                errorMessage.AppendLine(ResultStatusEnum.PaymentPaymentTimeIsNotValid.GetErrorMessage());
            }
            var unbrokenRiceAmount = TxtUnbrokenRice.Text.ToFloat();
            var brokenRiceAmount = TxtBrokenRice.Text.ToFloat();
            var flourAmount = TxtFlour.Text.ToFloat();
            var moneyAmount = TxtMoney.Text.ToInt();
            if (moneyAmount == 0 && unbrokenRiceAmount == 0 && brokenRiceAmount == 0 && flourAmount == 0)
                errorMessage.AppendLine(ResultStatusEnum.PaymentPaidCostIsNotValid.GetErrorMessage());

            if (errorMessage.IsNotNullOrEmpty())
            {
                await Toast.Make(errorMessage.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            var receiveTime = PersianDateTime.Parse(PersianDatePicker.PersianDate).AddSeconds((int)TimePicker.Time.TotalSeconds);
            DtoInputLoad selectedInputLoad = null;
            if (PickerInputLoad.SelectedItem is DtoInputLoad inputLoad)
                selectedInputLoad = inputLoad;

            if (_isNewPayment)
            {
                var newPayment = new DtoCreatePayment(receiveTime.ToDateTime(), unbrokenRiceAmount, brokenRiceAmount, flourAmount, moneyAmount,
                    TxtDescription.Text, selectedPaidPerson.Id, selectedPaidConcern.Id, selectedInputLoad?.Id, ApplicationStaticContext.CurrentUser.RiceMillId);

                await _paymentServices.Add(newPayment);
            }
            else
            {
                if (CVPayment.SelectedItem is not DtoPayment selectedPayment)
                    return;

                var updatePayment = new DtoUpdatePayment(selectedPayment.Id, receiveTime.ToDateTime(), unbrokenRiceAmount, brokenRiceAmount, flourAmount, moneyAmount,
                    TxtDescription.Text, selectedPaidPerson.Id, selectedPaidConcern.Id, selectedInputLoad?.Id);

                await _paymentServices.Update(updatePayment);
            }
            OnNewBtnClicked(null, null);
            await RefreshPaymentList();
            FillRequireData();
            CVPayment.ItemsSource = Payments.Items;
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
        foreach (var item in Payments.Items)
        {
            var paidPersonDetail = People.Items.FirstOrDefault(x => x.Id.Equals(item.PaidPersonId));
            var paidConcernDetail = Concerns.Items.FirstOrDefault(x => x.Id.Equals(item.ConcernId));
            item.PaidPersonFullName = $"{paidPersonDetail?.FullName ?? "*نامشخص*"} بابت {paidConcernDetail?.Title ?? "*نامشخص*"}";
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

    private Task RefreshPaymentList()
    {
        return Task.Run(() =>
        {
            var filter = new DtoPaymentFilter();
            if (!ApplicationStaticContext.IsAdmin)
                filter.RiceMillId = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _paymentServices.Get(filter);
            Payments = result.Result.Data;
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

    private Task LoadConcerns()
    {
        return Task.Run(() =>
        {
            var filter = new DtoConcernFilter();
            if (!ApplicationStaticContext.IsAdmin)
                filter.RiceMillId = ApplicationStaticContext.CurrentUser.RiceMillId;

            var result = _concernServices.Get(filter);
            Concerns = result.Result.Data;
        });
    }
}