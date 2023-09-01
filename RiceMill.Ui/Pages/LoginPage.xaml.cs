﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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
#pragma warning disable CA1416 // Validate platform compatibility
            if (!_isFirstView)
                Process.GetCurrentProcess().Kill();
#pragma warning restore CA1416 // Validate platform compatibility

            _isFirstView = false;
            var isAuthenticated = await _userServices.TokenIsValid();
            if (isAuthenticated)
            {
                AciLoginProgress.IsRunning = true;
                await AssignCurrentUser();
                await Navigation.PushAsync(new MainTabbedPage());
                AciLoginProgress.IsRunning = false;
            }
            base.OnAppearing();
        }

        private async void OnBtnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                var errorMessage = new StringBuilder();
                if (TxtUserName.Text.IsNullOrEmpty())
                    errorMessage.AppendLine(MessageDictionary.GetMessageText(ResultStatusEnum.UserUsernameIsNotValid));

                if (TxtPassword.Text.IsNullOrEmpty())
                    errorMessage.AppendLine(MessageDictionary.GetMessageText(ResultStatusEnum.UserPasswordIsNotValid));

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
                var claim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type.Equals(SharedResource.TokenClaimName));
                if (claim != null)
                {
                    var userId = Guid.Parse(claim.Value);
                    var currentUserResult = await _userServices.GetUsers(new DtoUserFilter { Id = userId });
                    if (currentUserResult.Data.TotalCount > 0)
                        ApplicationStaticContext.CurrentUser = currentUserResult.Data.Items.First();
                    else
                        throw new Exception(MessageDictionary.GetMessageText(ResultStatusEnum.UserNotFound));
                }
            }
        }
    }
}