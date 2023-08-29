using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.Resource;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Pages;
using RiceMill.Ui.Services.UseCases.UserServices;
using Shared.ExtensionMethods;
using Shared.UtilityMethods;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace RiceMill.Ui
{
    public partial class LoginPage : ContentPage
    {
        private readonly IUserServices _userServices;

        public LoginPage(IUserServices userServices)
        {
            InitializeComponent();
            _userServices = userServices;
        }

        private async void OnBtnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                AciLoginProgress.IsRunning = true;
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
                await _userServices.SetToken(new DtoLogin(TxtUserName.Text, TxtPassword.Text.ToSha512()));
                await AssignCurrentUser();
                await Navigation.PushAsync(new MainTabbedPage());
            }
            catch (Exception ex)
            {
                AciLoginProgress.IsRunning = false;
                await Toast.Make(ex.Message.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
            }
            finally
            {
                AciLoginProgress.IsRunning = false;
            }
        }

        private async Task AssignCurrentUser()
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(ApplicationStaticContext.Token);
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