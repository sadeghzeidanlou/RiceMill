using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.UseCases.UserServices.Dto;
using RiceMill.Ui.Common;
using RiceMill.Ui.Services;
using RiceMill.Ui.Services.UseCases.UserServices;
using Shared.ExtensionMethods;
using System.Text;

namespace RiceMill.Ui
{
    public partial class LoginPage : ContentPage
    {
        //private readonly IUserServices _userServices;

        public LoginPage()
        {
            InitializeComponent();
            //_userServices = DependencyService.Resolve<IUserServices>(DependencyFetchTarget.NewInstance);
        }

        private void OnBtnLoginClicked(object sender, EventArgs e)
        {
            AciLoginProgress.IsRunning = true;
            //var errorMessage = new StringBuilder();
            //if (TxtUserName.Text.IsNullOrEmpty())
            //    errorMessage.AppendLine(MessageDictionary.GetMessageText(ResultStatusEnum.UserUsernameIsNotValid));

            //if (TxtPassword.Text.IsNullOrEmpty())
            //    errorMessage.AppendLine(MessageDictionary.GetMessageText(ResultStatusEnum.UserPasswordIsNotValid));

            //if (errorMessage.IsNotNullOrEmpty())
            //{
            //    Toast.Make(errorMessage.ToString(), ToastDuration.Long, ApplicationStaticContext.ToastMessageSize).Show();
            //    return;
            //}
            //var result = _userServices.GetToken(new DtoLogin(TxtUserName.Text, TxtPassword.Text.ToSha512()));
        }
    }
}