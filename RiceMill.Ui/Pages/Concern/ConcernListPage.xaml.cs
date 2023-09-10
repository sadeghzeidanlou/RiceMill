using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Platform;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Services.UseCases.ConcernServices;
using Shared.ExtensionMethods;

namespace RiceMill.Ui.Pages.Concern;

public partial class ConcernListPage : ContentPage
{
    private readonly IConcernServices _concernServices;
    private PaginatedList<DtoConcern> Concerns;
    private bool _isNewConcern = true;

    public ConcernListPage()
    {
        try
        {
            _concernServices = new ConcernServices();
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
            await RefreshList();
            CVConcern.ItemsSource = Concerns.Items;
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
            if (TxtTitle.Text.IsNullOrEmpty() || _isNewConcern && ApplicationStaticContext.CurrentUser.RiceMillId.IsNullOrEmpty())
            {
                await Toast.Make(MessageDictionary.GetMessageText(ResultStatusEnum.ConcernTitleIsNotValid), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }

            if (_isNewConcern)
            {
                var newConcern = new DtoCreateConcern(TxtTitle.Text, ApplicationStaticContext.CurrentUser.RiceMillId);
                await _concernServices.Add(newConcern);
                return;
            }

            if (CVConcern.SelectedItem is not DtoConcern selectedConcern)
                return;

            var updateConcern = new DtoUpdateConcern(selectedConcern.Id, TxtTitle.Text);
            await _concernServices.Update(updateConcern);
            return;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
        finally
        {
            OnNewBtnClicked(null, null);
            await RefreshList();
            CVConcern.ItemsSource = Concerns.Items;
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
            if (CVConcern.SelectedItem is not DtoConcern selectedConcern)
            {
                await Toast.Make(MessageDictionary.GetMessageText(ResultStatusEnum.PleaseSelectConcern), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                return;
            }

            await _concernServices.Delete(selectedConcern.Id);
            OnNewBtnClicked(null, null);
            await RefreshList();
            CVConcern.ItemsSource = Concerns.Items;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private void OnNewBtnClicked(object sender, EventArgs e)
    {
        _isNewConcern = true;
        TxtTitle.Text = string.Empty;
        CVConcern.SelectedItem = null;
    }

    private async void OnCVConcernSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (CVConcern.SelectedItem is not DtoConcern selectedConcern)
                return;

            TxtTitle.Text = selectedConcern.Title;
            _isNewConcern = false;
        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
        }
    }

    private Task RefreshList()
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