using System;

namespace DSUScheduleBuilder
{
    using Tabs;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.LoginPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Login_LoginBtn = new System.Windows.Forms.Button();
            this.Login_ForgotLbl = new System.Windows.Forms.Label();
            this.Login_UsernameTxt = new System.Windows.Forms.TextBox();
            this.Login_NewUserBtn = new System.Windows.Forms.Button();
            this.Login_PasswordTxt = new System.Windows.Forms.TextBox();
            this.NewUserPanel = new System.Windows.Forms.Panel();
            this.MainMenuPanel = new System.Windows.Forms.Panel();
            this.ManageCoursesBtn = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.AccountSettingsBtn = new System.Windows.Forms.Button();
            this.MainMenu_WeekViewBtn = new System.Windows.Forms.Button();
            this.MainMenu_SearchBtn = new System.Windows.Forms.Button();
            this.MainMenu_LogoutBtn = new System.Windows.Forms.Button();
            this.WelcomeLabel = new System.Windows.Forms.Label();
            this.NewUserControl = new DSUScheduleBuilder.Tabs.NewUser();
            this.Control_WeekView = new DSUScheduleBuilder.Drawing.WeekView();
            this.Control_ManageCourses = new DSUScheduleBuilder.Tabs.ManageCourses();
            this.Control_AccountSettings = new DSUScheduleBuilder.Tabs.UserSettings();
            this.Control_Search = new DSUScheduleBuilder.Tabs.Search();
            this.LoginPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.NewUserPanel.SuspendLayout();
            this.MainMenuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // LoginPanel
            // 
            this.LoginPanel.Controls.Add(this.pictureBox1);
            this.LoginPanel.Controls.Add(this.Login_LoginBtn);
            this.LoginPanel.Controls.Add(this.Login_ForgotLbl);
            this.LoginPanel.Controls.Add(this.Login_UsernameTxt);
            this.LoginPanel.Controls.Add(this.Login_NewUserBtn);
            this.LoginPanel.Controls.Add(this.Login_PasswordTxt);
            this.LoginPanel.Location = new System.Drawing.Point(0, 3);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(1510, 1114);
            this.LoginPanel.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(542, 80);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(465, 477);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // Login_LoginBtn
            // 
            this.Login_LoginBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Login_LoginBtn.Location = new System.Drawing.Point(648, 783);
            this.Login_LoginBtn.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Login_LoginBtn.Name = "Login_LoginBtn";
            this.Login_LoginBtn.Size = new System.Drawing.Size(126, 52);
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
            this.Login_ForgotLbl.Location = new System.Drawing.Point(692, 855);
            this.Login_ForgotLbl.Name = "Login_ForgotLbl";
            this.Login_ForgotLbl.Size = new System.Drawing.Size(171, 20);
            this.Login_ForgotLbl.TabIndex = 4;
            this.Login_ForgotLbl.Text = "Forgot my Password";
            // 
            // Login_UsernameTxt
            // 
            this.Login_UsernameTxt.BackColor = System.Drawing.Color.DimGray;
            this.Login_UsernameTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Login_UsernameTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Login_UsernameTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login_UsernameTxt.ForeColor = System.Drawing.Color.White;
            this.Login_UsernameTxt.Location = new System.Drawing.Point(648, 685);
            this.Login_UsernameTxt.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Login_UsernameTxt.Name = "Login_UsernameTxt";
            this.Login_UsernameTxt.Size = new System.Drawing.Size(260, 35);
            this.Login_UsernameTxt.TabIndex = 2;
            this.Login_UsernameTxt.Text = "Username";
            this.Login_UsernameTxt.GotFocus += new System.EventHandler(this.Login_UsernameTxt_GotFocus);
            this.Login_UsernameTxt.LostFocus += new System.EventHandler(this.Login_UsernameTxt_LostFocus);
            // 
            // Login_NewUserBtn
            // 
            this.Login_NewUserBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Login_NewUserBtn.Location = new System.Drawing.Point(783, 783);
            this.Login_NewUserBtn.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Login_NewUserBtn.Name = "Login_NewUserBtn";
            this.Login_NewUserBtn.Size = new System.Drawing.Size(126, 52);
            this.Login_NewUserBtn.TabIndex = 1;
            this.Login_NewUserBtn.Text = "New User";
            this.Login_NewUserBtn.UseVisualStyleBackColor = true;
            this.Login_NewUserBtn.Click += new System.EventHandler(this.Login_NewUserBtn_Click);
            // 
            // Login_PasswordTxt
            // 
            this.Login_PasswordTxt.BackColor = System.Drawing.Color.DimGray;
            this.Login_PasswordTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Login_PasswordTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login_PasswordTxt.ForeColor = System.Drawing.Color.White;
            this.Login_PasswordTxt.Location = new System.Drawing.Point(648, 734);
            this.Login_PasswordTxt.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Login_PasswordTxt.Name = "Login_PasswordTxt";
            this.Login_PasswordTxt.Size = new System.Drawing.Size(260, 35);
            this.Login_PasswordTxt.TabIndex = 3;
            this.Login_PasswordTxt.Text = "Password";
            this.Login_PasswordTxt.TextChanged += new System.EventHandler(this.Login_PasswordTxt_TextChanged);
            this.Login_PasswordTxt.GotFocus += new System.EventHandler(this.Login_PasswordTxt_GotFocus);
            this.Login_PasswordTxt.LostFocus += new System.EventHandler(this.Login_PasswordTxt_LostFocus);
            // 
            // NewUserPanel
            // 
            this.NewUserPanel.Controls.Add(this.NewUserControl);
            this.NewUserPanel.Location = new System.Drawing.Point(-2, 0);
            this.NewUserPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NewUserPanel.Name = "NewUserPanel";
            this.NewUserPanel.Size = new System.Drawing.Size(1515, 1120);
            this.NewUserPanel.TabIndex = 2;
            // 
            // MainMenuPanel
            // 
            this.MainMenuPanel.Controls.Add(this.ManageCoursesBtn);
            this.MainMenuPanel.Controls.Add(this.pictureBox2);
            this.MainMenuPanel.Controls.Add(this.AccountSettingsBtn);
            this.MainMenuPanel.Controls.Add(this.MainMenu_WeekViewBtn);
            this.MainMenuPanel.Controls.Add(this.MainMenu_SearchBtn);
            this.MainMenuPanel.Controls.Add(this.MainMenu_LogoutBtn);
            this.MainMenuPanel.Controls.Add(this.WelcomeLabel);
            this.MainMenuPanel.Controls.Add(this.Control_WeekView);
            this.MainMenuPanel.Controls.Add(this.Control_ManageCourses);
            this.MainMenuPanel.Controls.Add(this.Control_AccountSettings);
            this.MainMenuPanel.Controls.Add(this.Control_Search);
            this.MainMenuPanel.Location = new System.Drawing.Point(-2, 0);
            this.MainMenuPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MainMenuPanel.Name = "MainMenuPanel";
            this.MainMenuPanel.Size = new System.Drawing.Size(1515, 1120);
            this.MainMenuPanel.TabIndex = 7;
            // 
            // ManageCoursesBtn
            // 
            this.ManageCoursesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ManageCoursesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManageCoursesBtn.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.ManageCoursesBtn.Location = new System.Drawing.Point(460, 103);
            this.ManageCoursesBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ManageCoursesBtn.Name = "ManageCoursesBtn";
            this.ManageCoursesBtn.Size = new System.Drawing.Size(207, 71);
            this.ManageCoursesBtn.TabIndex = 3;
            this.ManageCoursesBtn.Text = "Manage";
            this.ManageCoursesBtn.UseVisualStyleBackColor = true;
            this.ManageCoursesBtn.Click += new System.EventHandler(this.ManageCoursesBtn_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(1344, 18);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(152, 155);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // AccountSettingsBtn
            // 
            this.AccountSettingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AccountSettingsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccountSettingsBtn.Location = new System.Drawing.Point(676, 103);
            this.AccountSettingsBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AccountSettingsBtn.Name = "AccountSettingsBtn";
            this.AccountSettingsBtn.Size = new System.Drawing.Size(207, 71);
            this.AccountSettingsBtn.TabIndex = 4;
            this.AccountSettingsBtn.Text = "Account";
            this.AccountSettingsBtn.UseVisualStyleBackColor = true;
            this.AccountSettingsBtn.Click += new System.EventHandler(this.AccountSettingsBtn_Click);
            // 
            // MainMenu_WeekViewBtn
            // 
            this.MainMenu_WeekViewBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MainMenu_WeekViewBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenu_WeekViewBtn.Location = new System.Drawing.Point(28, 103);
            this.MainMenu_WeekViewBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MainMenu_WeekViewBtn.Name = "MainMenu_WeekViewBtn";
            this.MainMenu_WeekViewBtn.Size = new System.Drawing.Size(207, 71);
            this.MainMenu_WeekViewBtn.TabIndex = 1;
            this.MainMenu_WeekViewBtn.Text = "Schedule";
            this.MainMenu_WeekViewBtn.UseVisualStyleBackColor = true;
            this.MainMenu_WeekViewBtn.Click += new System.EventHandler(this.MainMenu_WeekViewBtn_Click);
            // 
            // MainMenu_SearchBtn
            // 
            this.MainMenu_SearchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MainMenu_SearchBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenu_SearchBtn.Location = new System.Drawing.Point(244, 103);
            this.MainMenu_SearchBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MainMenu_SearchBtn.Name = "MainMenu_SearchBtn";
            this.MainMenu_SearchBtn.Size = new System.Drawing.Size(207, 71);
            this.MainMenu_SearchBtn.TabIndex = 2;
            this.MainMenu_SearchBtn.Text = "Register";
            this.MainMenu_SearchBtn.UseVisualStyleBackColor = true;
            this.MainMenu_SearchBtn.Click += new System.EventHandler(this.MainMenu_SearchBtn_Click);
            // 
            // MainMenu_LogoutBtn
            // 
            this.MainMenu_LogoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MainMenu_LogoutBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenu_LogoutBtn.Location = new System.Drawing.Point(892, 103);
            this.MainMenu_LogoutBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MainMenu_LogoutBtn.Name = "MainMenu_LogoutBtn";
            this.MainMenu_LogoutBtn.Size = new System.Drawing.Size(207, 71);
            this.MainMenu_LogoutBtn.TabIndex = 5;
            this.MainMenu_LogoutBtn.Text = "Logout";
            this.MainMenu_LogoutBtn.UseVisualStyleBackColor = true;
            this.MainMenu_LogoutBtn.Click += new System.EventHandler(this.MainMenu_LogoutBtn_Click);
            // 
            // WelcomeLabel
            // 
            this.WelcomeLabel.AutoSize = true;
            this.WelcomeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WelcomeLabel.Location = new System.Drawing.Point(21, 20);
            this.WelcomeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.WelcomeLabel.Name = "WelcomeLabel";
            this.WelcomeLabel.Size = new System.Drawing.Size(201, 37);
            this.WelcomeLabel.TabIndex = 1;
            this.WelcomeLabel.Text = "welcome text";
            // 
            // NewUserControl
            // 
            this.NewUserControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.NewUserControl.Location = new System.Drawing.Point(108, 62);
            this.NewUserControl.Name = "NewUserControl";
            this.NewUserControl.Size = new System.Drawing.Size(1308, 789);
            this.NewUserControl.TabIndex = 0;
            // 
            // Control_WeekView
            // 
            this.Control_WeekView.Location = new System.Drawing.Point(6, 326);
            this.Control_WeekView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Control_WeekView.Name = "Control_WeekView";
            this.Control_WeekView.Size = new System.Drawing.Size(1490, 789);
            this.Control_WeekView.TabIndex = 0;
            this.Control_WeekView.Text = "weekView1";
            // 
            // Control_ManageCourses
            // 
            this.Control_ManageCourses.Location = new System.Drawing.Point(108, 182);
            this.Control_ManageCourses.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Control_ManageCourses.Name = "Control_ManageCourses";
            this.Control_ManageCourses.Size = new System.Drawing.Size(1308, 932);
            this.Control_ManageCourses.TabIndex = 10;
            // 
            // Control_AccountSettings
            // 
            this.Control_AccountSettings.Location = new System.Drawing.Point(108, 182);
            this.Control_AccountSettings.Name = "Control_AccountSettings";
            this.Control_AccountSettings.Size = new System.Drawing.Size(1308, 932);
            this.Control_AccountSettings.TabIndex = 7;
            // 
            // Control_Search
            // 
            this.Control_Search.Location = new System.Drawing.Point(108, 268);
            this.Control_Search.Name = "Control_Search";
            this.Control_Search.Size = new System.Drawing.Size(1308, 789);
            this.Control_Search.TabIndex = 4;
            this.Control_Search.Visible = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1502, 1092);
            this.Controls.Add(this.NewUserPanel);
            this.Controls.Add(this.MainMenuPanel);
            this.Controls.Add(this.LoginPanel);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(1524, 1148);
            this.MinimumSize = new System.Drawing.Size(1524, 1018);
            this.Name = "MainWindow";
            this.Text = "TomAdvisor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.NewUserPanel.ResumeLayout(false);
            this.MainMenuPanel.ResumeLayout(false);
            this.MainMenuPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
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
        private UserSettings Control_AccountSettings;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button ManageCoursesBtn;
        private ManageCourses Control_ManageCourses;
    }
}