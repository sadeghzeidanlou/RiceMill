using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Services.UseCases.UserServices;
using Shared.ExtensionMethods;
using Shared.UtilityMethods;
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
            var result = await _userServices.GetToken(new DtoLogin(TxtUserName.Text, TxtPassword.Text.ToSha512()));
            var t = 0;
        }
    }
}