using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Platform;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.Resource;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Pages;
using RiceMill.Ui.Services.UseCases.UserServices;
using Shared.ExtensionMethods;
using Shared.UtilityMethods;
using System.Diagnostics;
using System.Text;

namespace RiceMill.Ui
{
    public partial class LoginPage : ContentPage
    {
        private readonly IUserServices _userServices;
        public static bool _isFirstView = true;

        public LoginPage()
        {
            InitializeComponent();
            _userServices = new UserServices();
        }

        protected override async void OnAppearing()
        {
            try
            {
#if ANDROID || WINDOWS
                if (!_isFirstView)
                    Process.GetCurrentProcess().Kill();
#endif
                _isFirstView = false;
                var isAuthenticated = await _userServices.TokenIsValid();
                if (isAuthenticated)
                {
                    AciLoginProgress.IsRunning = true;
                    await AssignCurrentUser();
                    await Navigation.PushAsync(new MainTabbedPage());
                }
            }
            catch (Exception ex)
            {
                await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
            }
            finally
            {
                AciLoginProgress.IsRunning = false;
                base.OnAppearing();
            }
        }

        private async void OnBtnLoginClicked(object sender, EventArgs e)
        {
            try
            {
#if ANDROID
                if (Platform.CurrentActivity.CurrentFocus != null)
                    Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
#endif
                var errorMessage = new StringBuilder();
                if (TxtUserName.Text.IsNullOrEmpty())
                    errorMessage.AppendLine(ResultStatusEnum.UserUsernameIsNotValid.GetErrorMessage());

                if (TxtPassword.Text.IsNullOrEmpty())
                    errorMessage.AppendLine(ResultStatusEnum.UserPasswordIsNotValid.GetErrorMessage());

                if (errorMessage.IsNotNullOrEmpty())
                {
                    await Toast.Make(errorMessage.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
                    return;
                }
                AciLoginProgress.IsRunning = true;
                await _userServices.SetToken(new DtoLogin(TxtUserName.Text, TxtPassword.Text.ToSha512()));
                await AssignCurrentUser();
                await Navigation.PushAsync(new MainTabbedPage());
            }
            catch (Exception ex)
            {
                await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
            }
            finally
            {
                AciLoginProgress.IsRunning = false;
            }
        }

        private async Task AssignCurrentUser()
        {
            var jwtSecurityToken = _userServices.ReadToken(ApplicationStaticContext.Token);
            if (jwtSecurityToken != null)
            {
                var claim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type.Equals(SharedResource.TokenClaimUserIdName));
                if (claim != null)
                {
                    var userId = Guid.Parse(claim.Value);
                    var currentUserResult = await _userServices.GetUsers(new DtoUserFilter { Id = userId });
                    if (currentUserResult.Data.TotalCount > 0)
                        ApplicationStaticContext.CurrentUser = currentUserResult.Data.Items.First();
                    else
                        throw new Exception(ResultStatusEnum.UserNotFound.GetErrorMessage());
                }
            }
        }
    }
}