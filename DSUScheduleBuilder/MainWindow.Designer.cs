using System;

namespace DSUScheduleBuilder
{
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
            this.label3 = new System.Windows.Forms.Label();
            this.NewUser_LastNameTxt = new System.Windows.Forms.TextBox();
            this.NewUser_FirstNameTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ConfirmLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.NewUser_CancelBtn = new System.Windows.Forms.Button();
            this.NewUser_CreateUserBtn = new System.Windows.Forms.Button();
            this.NewUser_ConfirmTxt = new System.Windows.Forms.TextBox();
            this.NewUser_PasswordTxt = new System.Windows.Forms.TextBox();
            this.NewUser_NameTxt = new System.Windows.Forms.TextBox();
            this.MainMenuPanel = new System.Windows.Forms.Panel();
            this.MainMenu_LogoutBtn = new System.Windows.Forms.Button();
            this.WelcomeLabel = new System.Windows.Forms.Label();
            this.weekView1 = new DSUScheduleBuilder.Drawing.WeekView();
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
            this.LoginPanel.Margin = new System.Windows.Forms.Padding(2);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(1007, 724);
            this.LoginPanel.TabIndex = 6;
            // 
            // Login_LoginBtn
            // 
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
            this.NewUserPanel.Controls.Add(this.label3);
            this.NewUserPanel.Controls.Add(this.NewUser_LastNameTxt);
            this.NewUserPanel.Controls.Add(this.NewUser_FirstNameTxt);
            this.NewUserPanel.Controls.Add(this.label2);
            this.NewUserPanel.Controls.Add(this.label1);
            this.NewUserPanel.Controls.Add(this.ConfirmLabel);
            this.NewUserPanel.Controls.Add(this.PasswordLabel);
            this.NewUserPanel.Controls.Add(this.UsernameLabel);
            this.NewUserPanel.Controls.Add(this.NewUser_CancelBtn);
            this.NewUserPanel.Controls.Add(this.NewUser_CreateUserBtn);
            this.NewUserPanel.Controls.Add(this.NewUser_ConfirmTxt);
            this.NewUserPanel.Controls.Add(this.NewUser_PasswordTxt);
            this.NewUserPanel.Controls.Add(this.NewUser_NameTxt);
            this.NewUserPanel.Location = new System.Drawing.Point(279, 159);
            this.NewUserPanel.Name = "NewUserPanel";
            this.NewUserPanel.Size = new System.Drawing.Size(457, 317);
            this.NewUserPanel.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(142, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 25);
            this.label3.TabIndex = 26;
            this.label3.Text = "Create a New User";
            // 
            // NewUser_LastNameTxt
            // 
            this.NewUser_LastNameTxt.Location = new System.Drawing.Point(216, 156);
            this.NewUser_LastNameTxt.Name = "NewUser_LastNameTxt";
            this.NewUser_LastNameTxt.Size = new System.Drawing.Size(149, 20);
            this.NewUser_LastNameTxt.TabIndex = 25;
            // 
            // NewUser_FirstNameTxt
            // 
            this.NewUser_FirstNameTxt.Location = new System.Drawing.Point(216, 130);
            this.NewUser_FirstNameTxt.Name = "NewUser_FirstNameTxt";
            this.NewUser_FirstNameTxt.Size = new System.Drawing.Size(149, 20);
            this.NewUser_FirstNameTxt.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Last name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "First name: ";
            // 
            // ConfirmLabel
            // 
            this.ConfirmLabel.AutoSize = true;
            this.ConfirmLabel.Location = new System.Drawing.Point(118, 110);
            this.ConfirmLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ConfirmLabel.Name = "ConfirmLabel";
            this.ConfirmLabel.Size = new System.Drawing.Size(94, 13);
            this.ConfirmLabel.TabIndex = 21;
            this.ConfirmLabel.Text = "Confirm Password:";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(157, 87);
            this.PasswordLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(56, 13);
            this.PasswordLabel.TabIndex = 20;
            this.PasswordLabel.Text = "Password:";
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(177, 64);
            this.UsernameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(35, 13);
            this.UsernameLabel.TabIndex = 19;
            this.UsernameLabel.Text = "Email:";
            // 
            // NewUser_CancelBtn
            // 
            this.NewUser_CancelBtn.Location = new System.Drawing.Point(147, 221);
            this.NewUser_CancelBtn.Margin = new System.Windows.Forms.Padding(2);
            this.NewUser_CancelBtn.Name = "NewUser_CancelBtn";
            this.NewUser_CancelBtn.Size = new System.Drawing.Size(150, 35);
            this.NewUser_CancelBtn.TabIndex = 18;
            this.NewUser_CancelBtn.Text = "Cancel";
            this.NewUser_CancelBtn.UseVisualStyleBackColor = true;
            this.NewUser_CancelBtn.Click += new System.EventHandler(this.NewUser_CancelBtn_Click);
            // 
            // NewUser_CreateUserBtn
            // 
            this.NewUser_CreateUserBtn.Location = new System.Drawing.Point(147, 181);
            this.NewUser_CreateUserBtn.Margin = new System.Windows.Forms.Padding(2);
            this.NewUser_CreateUserBtn.Name = "NewUser_CreateUserBtn";
            this.NewUser_CreateUserBtn.Size = new System.Drawing.Size(150, 36);
            this.NewUser_CreateUserBtn.TabIndex = 17;
            this.NewUser_CreateUserBtn.Text = "Create User";
            this.NewUser_CreateUserBtn.UseVisualStyleBackColor = true;
            this.NewUser_CreateUserBtn.Click += new System.EventHandler(this.NewUser_CreateUserBtn_Click);
            // 
            // NewUser_ConfirmTxt
            // 
            this.NewUser_ConfirmTxt.Location = new System.Drawing.Point(216, 107);
            this.NewUser_ConfirmTxt.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewUser_ConfirmTxt.Name = "NewUser_ConfirmTxt";
            this.NewUser_ConfirmTxt.Size = new System.Drawing.Size(149, 20);
            this.NewUser_ConfirmTxt.TabIndex = 16;
            // 
            // NewUser_PasswordTxt
            // 
            this.NewUser_PasswordTxt.Location = new System.Drawing.Point(216, 83);
            this.NewUser_PasswordTxt.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewUser_PasswordTxt.Name = "NewUser_PasswordTxt";
            this.NewUser_PasswordTxt.PasswordChar = '*';
            this.NewUser_PasswordTxt.Size = new System.Drawing.Size(149, 20);
            this.NewUser_PasswordTxt.TabIndex = 15;
            // 
            // NewUser_NameTxt
            // 
            this.NewUser_NameTxt.Location = new System.Drawing.Point(216, 61);
            this.NewUser_NameTxt.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewUser_NameTxt.Name = "NewUser_NameTxt";
            this.NewUser_NameTxt.Size = new System.Drawing.Size(149, 20);
            this.NewUser_NameTxt.TabIndex = 14;
            // 
            // MainMenuPanel
            // 
            this.MainMenuPanel.Controls.Add(this.MainMenu_LogoutBtn);
            this.MainMenuPanel.Controls.Add(this.WelcomeLabel);
            this.MainMenuPanel.Controls.Add(this.weekView1);
            this.MainMenuPanel.Location = new System.Drawing.Point(-1, 0);
            this.MainMenuPanel.Name = "MainMenuPanel";
            this.MainMenuPanel.Size = new System.Drawing.Size(1010, 728);
            this.MainMenuPanel.TabIndex = 7;
            // 
            // MainMenu_LogoutBtn
            // 
            this.MainMenu_LogoutBtn.Location = new System.Drawing.Point(922, 12);
            this.MainMenu_LogoutBtn.Name = "MainMenu_LogoutBtn";
            this.MainMenu_LogoutBtn.Size = new System.Drawing.Size(75, 23);
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
            // weekView1
            // 
            this.weekView1.Location = new System.Drawing.Point(72, 212);
            this.weekView1.Name = "weekView1";
            this.weekView1.Size = new System.Drawing.Size(855, 513);
            this.weekView1.TabIndex = 0;
            this.weekView1.Text = "weekView1";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.MainMenuPanel);
            this.Controls.Add(this.NewUserPanel);
            this.Controls.Add(this.LoginPanel);
            this.MaximumSize = new System.Drawing.Size(1024, 768);
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            this.NewUserPanel.ResumeLayout(false);
            this.NewUserPanel.PerformLayout();
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
        private Drawing.WeekView weekView1;
        private System.Windows.Forms.Label WelcomeLabel;
        private System.Windows.Forms.Panel NewUserPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox NewUser_LastNameTxt;
        private System.Windows.Forms.TextBox NewUser_FirstNameTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ConfirmLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Button NewUser_CancelBtn;
        private System.Windows.Forms.Button NewUser_CreateUserBtn;
        private System.Windows.Forms.TextBox NewUser_ConfirmTxt;
        private System.Windows.Forms.TextBox NewUser_PasswordTxt;
        private System.Windows.Forms.TextBox NewUser_NameTxt;
        private System.Windows.Forms.Button MainMenu_LogoutBtn;
    }
}