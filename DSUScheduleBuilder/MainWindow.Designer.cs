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
            this.MainMenuPanel = new System.Windows.Forms.Panel();
            this.LoginPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginPanel
            // 
            this.LoginPanel.Controls.Add(this.Login_LoginBtn);
            this.LoginPanel.Controls.Add(this.Login_ForgotLbl);
            this.LoginPanel.Controls.Add(this.Login_UsernameTxt);
            this.LoginPanel.Controls.Add(this.Login_NewUserBtn);
            this.LoginPanel.Controls.Add(this.Login_PasswordTxt);
            this.LoginPanel.Location = new System.Drawing.Point(277, 149);
            this.LoginPanel.Margin = new System.Windows.Forms.Padding(2);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(457, 317);
            this.LoginPanel.TabIndex = 6;
            // 
            // Login_LoginBtn
            // 
            this.Login_LoginBtn.Location = new System.Drawing.Point(130, 153);
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
            this.Login_ForgotLbl.Location = new System.Drawing.Point(159, 200);
            this.Login_ForgotLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Login_ForgotLbl.Name = "Login_ForgotLbl";
            this.Login_ForgotLbl.Size = new System.Drawing.Size(120, 13);
            this.Login_ForgotLbl.TabIndex = 4;
            this.Login_ForgotLbl.Text = "Forgot my Password";
            // 
            // Login_UsernameTxt
            // 
            this.Login_UsernameTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login_UsernameTxt.Location = new System.Drawing.Point(130, 89);
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
            this.Login_NewUserBtn.Location = new System.Drawing.Point(220, 153);
            this.Login_NewUserBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Login_NewUserBtn.Name = "Login_NewUserBtn";
            this.Login_NewUserBtn.Size = new System.Drawing.Size(84, 34);
            this.Login_NewUserBtn.TabIndex = 1;
            this.Login_NewUserBtn.Text = "New User";
            this.Login_NewUserBtn.UseVisualStyleBackColor = true;
            // 
            // Login_PasswordTxt
            // 
            this.Login_PasswordTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login_PasswordTxt.Location = new System.Drawing.Point(130, 121);
            this.Login_PasswordTxt.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Login_PasswordTxt.Name = "Login_PasswordTxt";
            this.Login_PasswordTxt.Size = new System.Drawing.Size(174, 26);
            this.Login_PasswordTxt.TabIndex = 3;
            this.Login_PasswordTxt.Text = "Password";
            this.Login_PasswordTxt.TextChanged += new System.EventHandler(this.Login_PasswordTxt_TextChanged);
            this.Login_PasswordTxt.GotFocus += new System.EventHandler(this.Login_PasswordTxt_GotFocus);
            this.Login_PasswordTxt.LostFocus += new System.EventHandler(this.Login_PasswordTxt_LostFocus);
            // 
            // MainMenuPanel
            // 
            this.MainMenuPanel.Location = new System.Drawing.Point(-1, 0);
            this.MainMenuPanel.Name = "MainMenuPanel";
            this.MainMenuPanel.Size = new System.Drawing.Size(1010, 729);
            this.MainMenuPanel.TabIndex = 7;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.MainMenuPanel);
            this.Controls.Add(this.LoginPanel);
            this.MaximumSize = new System.Drawing.Size(1024, 768);
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
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
    }
}