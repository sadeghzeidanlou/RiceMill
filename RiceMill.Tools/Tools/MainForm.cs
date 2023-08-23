using Shared.ExtensionMethods;
using Shared.UtilityMethods;

namespace Tools
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();

        private void BtnEncrypt_Click(object sender, EventArgs e)
        {
            if (TxtInput.Text.IsNullOrEmpty() || TxtKey.Text.IsNullOrEmpty())
                return;

            TxtOutput.Text = TxtInput.Text.EncryptStringAes(TxtKey.Text);
        }

        private void BtnDecrypt_Click(object sender, EventArgs e)
        {
            if (TxtInput.Text.IsNullOrEmpty() || TxtKey.Text.IsNullOrEmpty())
                return;

            TxtOutput.Text = TxtInput.Text.DecryptStringAes(TxtKey.Text);
        }
    }
}