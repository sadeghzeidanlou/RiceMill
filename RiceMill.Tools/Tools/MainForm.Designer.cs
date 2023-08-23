namespace Tools
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TpSecurity = new TabPage();
            GbConnectionString = new GroupBox();
            TxtOutput = new TextBox();
            TxtInput = new TextBox();
            BtnDecrypt = new Button();
            BtnEncrypt = new Button();
            TxtKey = new TextBox();
            TcMain = new TabControl();
            TpSecurity.SuspendLayout();
            GbConnectionString.SuspendLayout();
            TcMain.SuspendLayout();
            SuspendLayout();
            // 
            // TpSecurity
            // 
            TpSecurity.Controls.Add(GbConnectionString);
            TpSecurity.Location = new Point(4, 24);
            TpSecurity.Name = "TpSecurity";
            TpSecurity.Padding = new Padding(3);
            TpSecurity.Size = new Size(792, 422);
            TpSecurity.TabIndex = 0;
            TpSecurity.Text = "Security";
            TpSecurity.UseVisualStyleBackColor = true;
            // 
            // GbConnectionString
            // 
            GbConnectionString.Controls.Add(TxtOutput);
            GbConnectionString.Controls.Add(TxtInput);
            GbConnectionString.Controls.Add(BtnDecrypt);
            GbConnectionString.Controls.Add(BtnEncrypt);
            GbConnectionString.Controls.Add(TxtKey);
            GbConnectionString.Dock = DockStyle.Top;
            GbConnectionString.Location = new Point(3, 3);
            GbConnectionString.Name = "GbConnectionString";
            GbConnectionString.Size = new Size(786, 411);
            GbConnectionString.TabIndex = 0;
            GbConnectionString.TabStop = false;
            GbConnectionString.Text = "Connection String";
            // 
            // TxtOutput
            // 
            TxtOutput.Dock = DockStyle.Bottom;
            TxtOutput.Location = new Point(3, 190);
            TxtOutput.Multiline = true;
            TxtOutput.Name = "TxtOutput";
            TxtOutput.Size = new Size(780, 218);
            TxtOutput.TabIndex = 4;
            // 
            // TxtInput
            // 
            TxtInput.Dock = DockStyle.Top;
            TxtInput.Location = new Point(3, 19);
            TxtInput.Multiline = true;
            TxtInput.Name = "TxtInput";
            TxtInput.Size = new Size(780, 117);
            TxtInput.TabIndex = 0;
            // 
            // BtnDecrypt
            // 
            BtnDecrypt.Location = new Point(460, 151);
            BtnDecrypt.Name = "BtnDecrypt";
            BtnDecrypt.Size = new Size(92, 24);
            BtnDecrypt.TabIndex = 3;
            BtnDecrypt.Text = "Decrypt";
            BtnDecrypt.UseVisualStyleBackColor = true;
            BtnDecrypt.Click += BtnDecrypt_Click;
            // 
            // BtnEncrypt
            // 
            BtnEncrypt.Location = new Point(362, 151);
            BtnEncrypt.Name = "BtnEncrypt";
            BtnEncrypt.Size = new Size(92, 24);
            BtnEncrypt.TabIndex = 2;
            BtnEncrypt.Text = "Encrypt";
            BtnEncrypt.UseVisualStyleBackColor = true;
            BtnEncrypt.Click += BtnEncrypt_Click;
            // 
            // TxtKey
            // 
            TxtKey.Location = new Point(12, 152);
            TxtKey.Name = "TxtKey";
            TxtKey.Size = new Size(344, 23);
            TxtKey.TabIndex = 1;
            // 
            // TcMain
            // 
            TcMain.Controls.Add(TpSecurity);
            TcMain.Dock = DockStyle.Fill;
            TcMain.Location = new Point(0, 0);
            TcMain.Name = "TcMain";
            TcMain.SelectedIndex = 0;
            TcMain.Size = new Size(800, 450);
            TcMain.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(TcMain);
            Name = "MainForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            TpSecurity.ResumeLayout(false);
            GbConnectionString.ResumeLayout(false);
            GbConnectionString.PerformLayout();
            TcMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabPage TpSecurity;
        private TabControl TcMain;
        private GroupBox GbConnectionString;
        private TextBox TxtOutput;
        private TextBox TxtInput;
        private Button BtnDecrypt;
        private Button BtnEncrypt;
        private TextBox TxtKey;
    }
}