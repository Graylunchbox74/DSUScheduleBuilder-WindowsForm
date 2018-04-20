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
            this.SuspendLayout();
            // 
            // LoginBtn
            // 
            this.LoginBtn.Location = new System.Drawing.Point(14, 116);
            this.LoginBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(126, 52);
            this.LoginBtn.TabIndex = 0;
            this.LoginBtn.Text = "Login";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // NewUserBtn
            // 
            this.NewUserBtn.Location = new System.Drawing.Point(147, 116);
            this.NewUserBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewUserBtn.Name = "NewUserBtn";
            this.NewUserBtn.Size = new System.Drawing.Size(126, 52);
            this.NewUserBtn.TabIndex = 1;
            this.NewUserBtn.Text = "New User";
            this.NewUserBtn.UseVisualStyleBackColor = true;
            this.NewUserBtn.Click += new System.EventHandler(this.NewUserBtn_Click);
            // 
            // UsernameTextbox
            // 
            this.UsernameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameTextbox.Location = new System.Drawing.Point(14, 15);
            this.UsernameTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UsernameTextbox.Name = "UsernameTextbox";
            this.UsernameTextbox.Size = new System.Drawing.Size(259, 35);
            this.UsernameTextbox.TabIndex = 2;
            this.UsernameTextbox.Text = "Username";
            this.UsernameTextbox.GotFocus += new System.EventHandler(this.UsernameTextbox_GotFocus);
            this.UsernameTextbox.LostFocus += new System.EventHandler(this.UsernameTextbox_LostFocus);
            // 
            // PasswordTextbox
            // 
            this.PasswordTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordTextbox.Location = new System.Drawing.Point(14, 60);
            this.PasswordTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PasswordTextbox.Name = "PasswordTextbox";
            this.PasswordTextbox.Size = new System.Drawing.Size(259, 35);
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
            this.ForgotLbl.Location = new System.Drawing.Point(14, 186);
            this.ForgotLbl.Name = "ForgotLbl";
            this.ForgotLbl.Size = new System.Drawing.Size(171, 20);
            this.ForgotLbl.TabIndex = 4;
            this.ForgotLbl.Text = "Forgot my Password";
            this.ForgotLbl.Click += new System.EventHandler(this.ForgotLbl_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 218);
            this.Controls.Add(this.ForgotLbl);
            this.Controls.Add(this.PasswordTextbox);
            this.Controls.Add(this.UsernameTextbox);
            this.Controls.Add(this.NewUserBtn);
            this.Controls.Add(this.LoginBtn);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.Button NewUserBtn;
        private System.Windows.Forms.TextBox UsernameTextbox;
        private System.Windows.Forms.TextBox PasswordTextbox;
        private System.Windows.Forms.Label ForgotLbl;
    }
}

