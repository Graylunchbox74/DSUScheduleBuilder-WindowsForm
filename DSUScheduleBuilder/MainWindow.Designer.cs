using System;

namespace DSUScheduleBuilder
{
    using Main_Menu;

    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LoginPanel = new System.Windows.Forms.Panel();
            this.Login_LoginBtn = new System.Windows.Forms.Button();
            this.Login_ForgotLbl = new System.Windows.Forms.Label();
            this.Login_UsernameTxt = new System.Windows.Forms.TextBox();
            this.Login_NewUserBtn = new System.Windows.Forms.Button();
            this.Login_PasswordTxt = new System.Windows.Forms.TextBox();
            this.NewUserPanel = new System.Windows.Forms.Panel();
            this.MainMenuPanel = new System.Windows.Forms.Panel();
            this.MainMenu_WeekViewBtn = new System.Windows.Forms.Button();
            this.MainMenu_SearchBtn = new System.Windows.Forms.Button();
            this.MainMenu_LogoutBtn = new System.Windows.Forms.Button();
            this.WelcomeLabel = new System.Windows.Forms.Label();
            this.AccountSettingsBtn = new System.Windows.Forms.Button();
            this.Control_AccountSettings = new DSUScheduleBuilder.Main_Menu.UpdateUser();
            this.Control_Search = new DSUScheduleBuilder.Main_Menu.Search();
            this.Control_WeekView = new DSUScheduleBuilder.Drawing.WeekView();
            this.NewUserControl = new DSUScheduleBuilder.Main_Menu.NewUser();
            this.LoginPanel.SuspendLayout();
            this.NewUserPanel.SuspendLayout();
            this.MainMenuPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginPanel
            // 
            this.LoginPanel.Controls.Add(this.Login_LoginBtn);
            this.LoginPanel.Controls.Add(this.Login_ForgotLbl);
            this.LoginPanel.Controls.Add(this.Login_UsernameTxt);
            this.LoginPanel.Controls.Add(this.Login_NewUserBtn);
            this.LoginPanel.Controls.Add(this.Login_PasswordTxt);
            this.LoginPanel.Location = new System.Drawing.Point(0, 2);
            this.LoginPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(1007, 724);
            this.LoginPanel.TabIndex = 6;
            // 
            // Login_LoginBtn
            // 
            this.Login_LoginBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Login_LoginBtn.Location = new System.Drawing.Point(426, 331);
            this.Login_LoginBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Login_LoginBtn.Name = "Login_LoginBtn";
            this.Login_LoginBtn.Size = new System.Drawing.Size(84, 34);
            this.Login_LoginBtn.TabIndex = 0;
            this.Login_LoginBtn.Text = "Login";
            this.Login_LoginBtn.UseVisualStyleBackColor = true;
            this.Login_LoginBtn.Click += new System.EventHandler(this.Login_LoginBtn_Click);
            // 
            // Login_ForgotLbl
            // 
            this.Login_ForgotLbl.AutoSize = true;
            this.Login_ForgotLbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Login_ForgotLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login_ForgotLbl.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Login_ForgotLbl.Location = new System.Drawing.Point(455, 378);
            this.Login_ForgotLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Login_ForgotLbl.Name = "Login_ForgotLbl";
            this.Login_ForgotLbl.Size = new System.Drawing.Size(120, 13);
            this.Login_ForgotLbl.TabIndex = 4;
            this.Login_ForgotLbl.Text = "Forgot my Password";
            // 
            // Login_UsernameTxt
            // 
            this.Login_UsernameTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login_UsernameTxt.Location = new System.Drawing.Point(426, 267);
            this.Login_UsernameTxt.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Login_UsernameTxt.Name = "Login_UsernameTxt";
            this.Login_UsernameTxt.Size = new System.Drawing.Size(174, 26);
            this.Login_UsernameTxt.TabIndex = 2;
            this.Login_UsernameTxt.Text = "Username";
            this.Login_UsernameTxt.GotFocus += new System.EventHandler(this.Login_UsernameTxt_GotFocus);
            this.Login_UsernameTxt.LostFocus += new System.EventHandler(this.Login_UsernameTxt_LostFocus);
            // 
            // Login_NewUserBtn
            // 
            this.Login_NewUserBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Login_NewUserBtn.Location = new System.Drawing.Point(516, 331);
            this.Login_NewUserBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Login_NewUserBtn.Name = "Login_NewUserBtn";
            this.Login_NewUserBtn.Size = new System.Drawing.Size(84, 34);
            this.Login_NewUserBtn.TabIndex = 1;
            this.Login_NewUserBtn.Text = "New User";
            this.Login_NewUserBtn.UseVisualStyleBackColor = true;
            this.Login_NewUserBtn.Click += new System.EventHandler(this.Login_NewUserBtn_Click);
            // 
            // Login_PasswordTxt
            // 
            this.Login_PasswordTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login_PasswordTxt.Location = new System.Drawing.Point(426, 299);
            this.Login_PasswordTxt.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Login_PasswordTxt.Name = "Login_PasswordTxt";
            this.Login_PasswordTxt.Size = new System.Drawing.Size(174, 26);
            this.Login_PasswordTxt.TabIndex = 3;
            this.Login_PasswordTxt.Text = "Password";
            this.Login_PasswordTxt.TextChanged += new System.EventHandler(this.Login_PasswordTxt_TextChanged);
            this.Login_PasswordTxt.GotFocus += new System.EventHandler(this.Login_PasswordTxt_GotFocus);
            this.Login_PasswordTxt.LostFocus += new System.EventHandler(this.Login_PasswordTxt_LostFocus);
            // 
            // NewUserPanel
            // 
            this.NewUserPanel.Controls.Add(this.NewUserControl);
            this.NewUserPanel.Location = new System.Drawing.Point(-1, 0);
            this.NewUserPanel.Name = "NewUserPanel";
            this.NewUserPanel.Size = new System.Drawing.Size(1010, 728);
            this.NewUserPanel.TabIndex = 2;
            // 
            // MainMenuPanel
            // 
            this.MainMenuPanel.Controls.Add(this.Control_AccountSettings);
            this.MainMenuPanel.Controls.Add(this.AccountSettingsBtn);
            this.MainMenuPanel.Controls.Add(this.MainMenu_WeekViewBtn);
            this.MainMenuPanel.Controls.Add(this.Control_Search);
            this.MainMenuPanel.Controls.Add(this.MainMenu_SearchBtn);
            this.MainMenuPanel.Controls.Add(this.MainMenu_LogoutBtn);
            this.MainMenuPanel.Controls.Add(this.WelcomeLabel);
            this.MainMenuPanel.Controls.Add(this.Control_WeekView);
            this.MainMenuPanel.Location = new System.Drawing.Point(-1, 0);
            this.MainMenuPanel.Name = "MainMenuPanel";
            this.MainMenuPanel.Size = new System.Drawing.Size(1010, 728);
            this.MainMenuPanel.TabIndex = 7;
            // 
            // MainMenu_WeekViewBtn
            // 
            this.MainMenu_WeekViewBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.MainMenu_WeekViewBtn.Location = new System.Drawing.Point(19, 54);
            this.MainMenu_WeekViewBtn.Name = "MainMenu_WeekViewBtn";
            this.MainMenu_WeekViewBtn.Size = new System.Drawing.Size(138, 46);
            this.MainMenu_WeekViewBtn.TabIndex = 5;
            this.MainMenu_WeekViewBtn.Text = "View Schedule";
            this.MainMenu_WeekViewBtn.UseVisualStyleBackColor = true;
            this.MainMenu_WeekViewBtn.Click += new System.EventHandler(this.MainMenu_WeekViewBtn_Click);
            // 
            // MainMenu_SearchBtn
            // 
            this.MainMenu_SearchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.MainMenu_SearchBtn.Location = new System.Drawing.Point(163, 54);
            this.MainMenu_SearchBtn.Name = "MainMenu_SearchBtn";
            this.MainMenu_SearchBtn.Size = new System.Drawing.Size(138, 46);
            this.MainMenu_SearchBtn.TabIndex = 3;
            this.MainMenu_SearchBtn.Text = "Search for classes";
            this.MainMenu_SearchBtn.UseVisualStyleBackColor = true;
            this.MainMenu_SearchBtn.Click += new System.EventHandler(this.MainMenu_SearchBtn_Click);
            // 
            // MainMenu_LogoutBtn
            // 
            this.MainMenu_LogoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.MainMenu_LogoutBtn.Location = new System.Drawing.Point(859, 54);
            this.MainMenu_LogoutBtn.Name = "MainMenu_LogoutBtn";
            this.MainMenu_LogoutBtn.Size = new System.Drawing.Size(138, 46);
            this.MainMenu_LogoutBtn.TabIndex = 2;
            this.MainMenu_LogoutBtn.Text = "Logout";
            this.MainMenu_LogoutBtn.UseVisualStyleBackColor = true;
            this.MainMenu_LogoutBtn.Click += new System.EventHandler(this.MainMenu_LogoutBtn_Click);
            // 
            // WelcomeLabel
            // 
            this.WelcomeLabel.AutoSize = true;
            this.WelcomeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WelcomeLabel.Location = new System.Drawing.Point(14, 13);
            this.WelcomeLabel.Name = "WelcomeLabel";
            this.WelcomeLabel.Size = new System.Drawing.Size(137, 25);
            this.WelcomeLabel.TabIndex = 1;
            this.WelcomeLabel.Text = "welcome text";
            // 
            // AccountSettingsBtn
            // 
            this.AccountSettingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AccountSettingsBtn.Location = new System.Drawing.Point(715, 54);
            this.AccountSettingsBtn.Name = "AccountSettingsBtn";
            this.AccountSettingsBtn.Size = new System.Drawing.Size(138, 46);
            this.AccountSettingsBtn.TabIndex = 6;
            this.AccountSettingsBtn.Text = "Account Settings";
            this.AccountSettingsBtn.UseVisualStyleBackColor = true;
            this.AccountSettingsBtn.Click += new System.EventHandler(this.AccountSettingsBtn_Click);
            // 
            // Control_AccountSettings
            // 
            this.Control_AccountSettings.Location = new System.Drawing.Point(72, 174);
            this.Control_AccountSettings.Margin = new System.Windows.Forms.Padding(2);
            this.Control_AccountSettings.Name = "Control_AccountSettings";
            this.Control_AccountSettings.Size = new System.Drawing.Size(872, 513);
            this.Control_AccountSettings.TabIndex = 7;
            this.Control_AccountSettings.Visible = false;
            // 
            // Control_Search
            // 
            this.Control_Search.Location = new System.Drawing.Point(72, 174);
            this.Control_Search.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Control_Search.Name = "Control_Search";
            this.Control_Search.Size = new System.Drawing.Size(872, 513);
            this.Control_Search.TabIndex = 4;
            this.Control_Search.Visible = false;
            this.Control_Search.Load += new System.EventHandler(this.Control_Search_Load);
            // 
            // Control_WeekView
            // 
            this.Control_WeekView.Location = new System.Drawing.Point(4, 212);
            this.Control_WeekView.Name = "Control_WeekView";
            this.Control_WeekView.Size = new System.Drawing.Size(993, 513);
            this.Control_WeekView.TabIndex = 0;
            this.Control_WeekView.Text = "weekView1";
            // 
            // NewUserControl
            // 
            this.NewUserControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.NewUserControl.Location = new System.Drawing.Point(72, 40);
            this.NewUserControl.Margin = new System.Windows.Forms.Padding(2);
            this.NewUserControl.Name = "NewUserControl";
            this.NewUserControl.Size = new System.Drawing.Size(872, 513);
            this.NewUserControl.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1007, 727);
            this.Controls.Add(this.MainMenuPanel);
            this.Controls.Add(this.NewUserPanel);
            this.Controls.Add(this.LoginPanel);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.MaximumSize = new System.Drawing.Size(1023, 766);
            this.MinimumSize = new System.Drawing.Size(1023, 766);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            this.NewUserPanel.ResumeLayout(false);
            this.MainMenuPanel.ResumeLayout(false);
            this.MainMenuPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LoginPanel;
        private System.Windows.Forms.Button Login_LoginBtn;
        private System.Windows.Forms.Label Login_ForgotLbl;
        private System.Windows.Forms.TextBox Login_UsernameTxt;
        private System.Windows.Forms.Button Login_NewUserBtn;
        private System.Windows.Forms.TextBox Login_PasswordTxt;
        private System.Windows.Forms.Panel MainMenuPanel;
        private Drawing.WeekView Control_WeekView;
        private System.Windows.Forms.Label WelcomeLabel;
        private System.Windows.Forms.Panel NewUserPanel;
        private System.Windows.Forms.Button MainMenu_LogoutBtn;
        private NewUser NewUserControl;
        private System.Windows.Forms.Button MainMenu_SearchBtn;
        private Search Control_Search;
        private System.Windows.Forms.Button MainMenu_WeekViewBtn;
        private System.Windows.Forms.Button AccountSettingsBtn;
        private UpdateUser Control_AccountSettings;
    }
}