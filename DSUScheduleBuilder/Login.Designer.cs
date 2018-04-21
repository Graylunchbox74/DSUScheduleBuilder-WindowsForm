namespace DSUScheduleBuilder
{
    partial class Login
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
            this.LoginBtn = new System.Windows.Forms.Button();
            this.NewUserBtn = new System.Windows.Forms.Button();
            this.UsernameTextbox = new System.Windows.Forms.TextBox();
            this.PasswordTextbox = new System.Windows.Forms.TextBox();
            this.ForgotLbl = new System.Windows.Forms.Label();
            this.LoginPanel = new System.Windows.Forms.Panel();
            this.RecoveryPanel = new System.Windows.Forms.Panel();
            this.EmailTextBox = new System.Windows.Forms.TextBox();
            this.GoBackButton = new System.Windows.Forms.Button();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.RecoveryLabel = new System.Windows.Forms.Label();
            this.LoginPanel.SuspendLayout();
            this.RecoveryPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginBtn
            // 
            this.LoginBtn.Location = new System.Drawing.Point(3, 68);
            this.LoginBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(84, 34);
            this.LoginBtn.TabIndex = 0;
            this.LoginBtn.Text = "Login";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // NewUserBtn
            // 
            this.NewUserBtn.Location = new System.Drawing.Point(91, 68);
            this.NewUserBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.NewUserBtn.Name = "NewUserBtn";
            this.NewUserBtn.Size = new System.Drawing.Size(84, 34);
            this.NewUserBtn.TabIndex = 1;
            this.NewUserBtn.Text = "New User";
            this.NewUserBtn.UseVisualStyleBackColor = true;
            this.NewUserBtn.Click += new System.EventHandler(this.NewUserBtn_Click);
            // 
            // UsernameTextbox
            // 
            this.UsernameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameTextbox.Location = new System.Drawing.Point(2, 3);
            this.UsernameTextbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.UsernameTextbox.Name = "UsernameTextbox";
            this.UsernameTextbox.Size = new System.Drawing.Size(174, 26);
            this.UsernameTextbox.TabIndex = 2;
            this.UsernameTextbox.Text = "Username";
            this.UsernameTextbox.GotFocus += new System.EventHandler(this.UsernameTextbox_GotFocus);
            this.UsernameTextbox.LostFocus += new System.EventHandler(this.UsernameTextbox_LostFocus);
            // 
            // PasswordTextbox
            // 
            this.PasswordTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordTextbox.Location = new System.Drawing.Point(2, 31);
            this.PasswordTextbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.PasswordTextbox.Name = "PasswordTextbox";
            this.PasswordTextbox.Size = new System.Drawing.Size(174, 26);
            this.PasswordTextbox.TabIndex = 3;
            this.PasswordTextbox.Text = "Password";
            this.PasswordTextbox.TextChanged += new System.EventHandler(this.PasswordTextbox_TextChanged);
            this.PasswordTextbox.GotFocus += new System.EventHandler(this.PasswordTextbox_GotFocus);
            this.PasswordTextbox.LostFocus += new System.EventHandler(this.PasswordTextbox_LostFocus);
            // 
            // ForgotLbl
            // 
            this.ForgotLbl.AutoSize = true;
            this.ForgotLbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ForgotLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForgotLbl.ForeColor = System.Drawing.SystemColors.Highlight;
            this.ForgotLbl.Location = new System.Drawing.Point(2, 110);
            this.ForgotLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ForgotLbl.Name = "ForgotLbl";
            this.ForgotLbl.Size = new System.Drawing.Size(120, 13);
            this.ForgotLbl.TabIndex = 4;
            this.ForgotLbl.Text = "Forgot my Password";
            this.ForgotLbl.Click += new System.EventHandler(this.ForgotLbl_Click);
            // 
            // LoginPanel
            // 
            this.LoginPanel.Controls.Add(this.LoginBtn);
            this.LoginPanel.Controls.Add(this.ForgotLbl);
            this.LoginPanel.Controls.Add(this.UsernameTextbox);
            this.LoginPanel.Controls.Add(this.NewUserBtn);
            this.LoginPanel.Controls.Add(this.PasswordTextbox);
            this.LoginPanel.Location = new System.Drawing.Point(8, 8);
            this.LoginPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(207, 131);
            this.LoginPanel.TabIndex = 5;
            // 
            // RecoveryPanel
            // 
            this.RecoveryPanel.Controls.Add(this.EmailTextBox);
            this.RecoveryPanel.Controls.Add(this.GoBackButton);
            this.RecoveryPanel.Controls.Add(this.SubmitButton);
            this.RecoveryPanel.Controls.Add(this.RecoveryLabel);
            this.RecoveryPanel.Location = new System.Drawing.Point(8, 8);
            this.RecoveryPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RecoveryPanel.Name = "RecoveryPanel";
            this.RecoveryPanel.Size = new System.Drawing.Size(207, 131);
            this.RecoveryPanel.TabIndex = 6;
            this.RecoveryPanel.Visible = false;
            // 
            // EmailTextBox
            // 
            this.EmailTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmailTextBox.Location = new System.Drawing.Point(0, 30);
            this.EmailTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.EmailTextBox.Multiline = true;
            this.EmailTextBox.Name = "EmailTextBox";
            this.EmailTextBox.Size = new System.Drawing.Size(174, 24);
            this.EmailTextBox.TabIndex = 8;
            this.EmailTextBox.Text = "Email";
            this.EmailTextBox.GotFocus += new System.EventHandler(this.EmailTextBox_GotFocus);
            this.EmailTextBox.LostFocus += new System.EventHandler(this.EmailTextBox_LostFocus);
            // 
            // GoBackButton
            // 
            this.GoBackButton.Location = new System.Drawing.Point(0, 68);
            this.GoBackButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GoBackButton.Name = "GoBackButton";
            this.GoBackButton.Size = new System.Drawing.Size(84, 34);
            this.GoBackButton.TabIndex = 7;
            this.GoBackButton.Text = "Cancel";
            this.GoBackButton.UseVisualStyleBackColor = true;
            this.GoBackButton.Click += new System.EventHandler(this.GoBackButton_Click);
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(91, 68);
            this.SubmitButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(83, 34);
            this.SubmitButton.TabIndex = 6;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // RecoveryLabel
            // 
            this.RecoveryLabel.AutoSize = true;
            this.RecoveryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecoveryLabel.Location = new System.Drawing.Point(-3, 0);
            this.RecoveryLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RecoveryLabel.Name = "RecoveryLabel";
            this.RecoveryLabel.Size = new System.Drawing.Size(150, 17);
            this.RecoveryLabel.TabIndex = 5;
            this.RecoveryLabel.Text = "Password Recovery";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 148);
            this.Controls.Add(this.LoginPanel);
            this.Controls.Add(this.RecoveryPanel);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Login";
            this.Text = "Login";
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            this.RecoveryPanel.ResumeLayout(false);
            this.RecoveryPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.Button NewUserBtn;
        private System.Windows.Forms.TextBox UsernameTextbox;
        private System.Windows.Forms.TextBox PasswordTextbox;
        private System.Windows.Forms.Label ForgotLbl;
        private System.Windows.Forms.Panel LoginPanel;
        private System.Windows.Forms.Panel RecoveryPanel;
        private System.Windows.Forms.Button GoBackButton;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.Label RecoveryLabel;
        private System.Windows.Forms.TextBox EmailTextBox;
    }
}

