using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Platform;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.PersonServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Services.UseCases.PersonServices;
using Shared.Enums;
using Shared.ExtensionMethods;
using System.Text;

namespace RiceMill.Ui.Pages.Person;

public partial class PersonListPage : ContentPage
{
    private readonly IPersonServices _personServices;
    private PaginatedList<DtoPerson> People;
    private bool _isNewRiceMill = true;

    public PersonListPage()
    {
        try
        {
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
            await RefreshPeopleList();
            await RefreshPeopleList();
            CVPerson.ItemsSource = People.Items;
            PickerGender.ItemsSource = GenderType.GetAll;
            PickerNoticeType.ItemsSource = NoticesType.GetAll;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.InnerException.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private void OnNewBtnClicked(object sender, EventArgs e)
    {
        CVPerson.SelectedItem = null;
        PickerGender.SelectedItem = null;
        PickerNoticeType.SelectedItem = null;
        TxtName.Text = string.Empty;
        TxtFamily.Text = string.Empty;
        TxtAddress.Text = string.Empty;
        TxtPhoneNumber.Text = string.Empty;
        TxtHomeNumber.Text = string.Empty;
        TxtFatherName.Text = string.Empty;
        _isNewRiceMill = true;
    }

    private async void OnBtnRemoveClicked(object sender, EventArgs e)
    {
        try
        {
            if (CVPerson.SelectedItem is not DtoPerson selectedPerson)
            {
                await Toast.Make(MessageDictionary.GetMessageText(ResultStatusEnum.PleaseSelectPerson), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            await _personServices.Delete(selectedPerson.Id);
            OnNewBtnClicked(null, null);
            await RefreshPeopleList();
            CVPerson.ItemsSource = People.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private async void OnCVPersonSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (CVPerson.SelectedItem is not DtoPerson selectedPerson)
                return;

            TxtName.Text = selectedPerson.Name;
            TxtFamily.Text = selectedPerson.Family;
            TxtAddress.Text = selectedPerson.Address;
            TxtPhoneNumber.Text = selectedPerson.MobileNumber;
            TxtHomeNumber.Text = selectedPerson.HomeNumber;
            TxtFatherName.Text = selectedPerson.FatherName;
            PickerNoticeType.SelectedIndex = NoticesType.GetAll.FirstOrDefault(x => x.Type == selectedPerson.NoticesType).Index;
            PickerGender.SelectedIndex = GenderType.GetAll.FirstOrDefault(x => x.Type == selectedPerson.Gender).Index;
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
            GenderType selectedGender = null;
            if (PickerGender.SelectedItem is GenderType genderType)
                selectedGender = genderType;
            else
                errorMessage.AppendLine(MessageDictionary.GetMessageText(ResultStatusEnum.PersonGenderIsNotValid));

            NoticesType selectedNotice = null;
            if (PickerNoticeType.SelectedItem is NoticesType noticeType)
                selectedNotice = noticeType;
            else
                errorMessage.AppendLine(MessageDictionary.GetMessageText(ResultStatusEnum.PersonNoticesTypeIsNotValid));

            if (TxtName.Text.IsNullOrEmpty())
                errorMessage.AppendLine(MessageDictionary.GetMessageText(ResultStatusEnum.PersonNameIsNotValid));

            if (TxtFamily.Text.IsNullOrEmpty())
                errorMessage.AppendLine(MessageDictionary.GetMessageText(ResultStatusEnum.PersonFamilyIsNotValid));

            if (TxtAddress.Text.IsNullOrEmpty())
                errorMessage.AppendLine(MessageDictionary.GetMessageText(ResultStatusEnum.PersonAddressIsNotValid));

            if (TxtPhoneNumber.Text.IsNullOrEmpty())
                errorMessage.AppendLine(MessageDictionary.GetMessageText(ResultStatusEnum.PersonMobileNumberIsNotValid));

            if (TxtFatherName.Text.IsNullOrEmpty())
                errorMessage.AppendLine(MessageDictionary.GetMessageText(ResultStatusEnum.PersonFatherNameIsNotValid));

            if (errorMessage.IsNotNullOrEmpty())
            {
                await Toast.Make(errorMessage.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }
            if (_isNewRiceMill)
            {
                var newRiceMill = new DtoCreatePerson(TxtName.Text, TxtFamily.Text, selectedGender.Type, TxtPhoneNumber.Text, TxtHomeNumber.Text, selectedNotice.Type, TxtAddress.Text, TxtFatherName.Text, ApplicationStaticContext.CurrentUser.RiceMillId);
                await _personServices.Add(newRiceMill);
            }
            else
            {
                if (CVPerson.SelectedItem is not DtoPerson selectedPerson)
                    return;

                var updateRiceMill = new DtoUpdatePerson(selectedPerson.Id, TxtName.Text, TxtFamily.Text, selectedGender.Type, TxtPhoneNumber.Text, TxtHomeNumber.Text, selectedNotice.Type, TxtAddress.Text, TxtFatherName.Text);
                await _personServices.Update(updateRiceMill);
            }
            OnNewBtnClicked(null, null);
            await RefreshPeopleList();
            CVPerson.ItemsSource = People.Items;
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

    private Task RefreshPeopleList()
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