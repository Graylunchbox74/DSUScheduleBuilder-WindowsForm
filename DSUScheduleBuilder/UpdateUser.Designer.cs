namespace DSUScheduleBuilder
{
    partial class UpdateUser
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
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.UserTextbox = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.PasswdLabel = new System.Windows.Forms.Label();
            this.ConfirmPasswdLabel = new System.Windows.Forms.Label();
            this.PasswordTextbox = new System.Windows.Forms.TextBox();
            this.ConfirmTextbox = new System.Windows.Forms.TextBox();
            this.AddCourseLabel = new System.Windows.Forms.Label();
            this.AddTakenLabel = new System.Windows.Forms.Label();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.AddDropdown = new System.Windows.Forms.ComboBox();
            this.RemoveTakenLabel = new System.Windows.Forms.Label();
            this.AddTakenDropdown = new System.Windows.Forms.ComboBox();
            this.RemoveTakenDropdown = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Location = new System.Drawing.Point(124, 407);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(173, 47);
            this.ConfirmButton.TabIndex = 0;
            this.ConfirmButton.Text = "Confirm Changes";
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click_1);
            // 
            // UserTextbox
            // 
            this.UserTextbox.Location = new System.Drawing.Point(96, 26);
            this.UserTextbox.Name = "UserTextbox";
            this.UserTextbox.Size = new System.Drawing.Size(201, 26);
            this.UserTextbox.TabIndex = 1;
            this.UserTextbox.Text = "<username>";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(25, 26);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(55, 20);
            this.NameLabel.TabIndex = 2;
            this.NameLabel.Text = "Name:";
            // 
            // PasswdLabel
            // 
            this.PasswdLabel.AutoSize = true;
            this.PasswdLabel.Location = new System.Drawing.Point(25, 76);
            this.PasswdLabel.Name = "PasswdLabel";
            this.PasswdLabel.Size = new System.Drawing.Size(82, 20);
            this.PasswdLabel.TabIndex = 3;
            this.PasswdLabel.Text = "Password:";
            // 
            // ConfirmPasswdLabel
            // 
            this.ConfirmPasswdLabel.AutoSize = true;
            this.ConfirmPasswdLabel.Location = new System.Drawing.Point(25, 115);
            this.ConfirmPasswdLabel.Name = "ConfirmPasswdLabel";
            this.ConfirmPasswdLabel.Size = new System.Drawing.Size(137, 20);
            this.ConfirmPasswdLabel.TabIndex = 4;
            this.ConfirmPasswdLabel.Text = "Confirm Password";
            // 
            // PasswordTextbox
            // 
            this.PasswordTextbox.Location = new System.Drawing.Point(113, 76);
            this.PasswordTextbox.Name = "PasswordTextbox";
            this.PasswordTextbox.PasswordChar = '*';
            this.PasswordTextbox.Size = new System.Drawing.Size(184, 26);
            this.PasswordTextbox.TabIndex = 5;
            // 
            // ConfirmTextbox
            // 
            this.ConfirmTextbox.Location = new System.Drawing.Point(164, 115);
            this.ConfirmTextbox.Name = "ConfirmTextbox";
            this.ConfirmTextbox.PasswordChar = '*';
            this.ConfirmTextbox.Size = new System.Drawing.Size(133, 26);
            this.ConfirmTextbox.TabIndex = 6;
            // 
            // AddCourseLabel
            // 
            this.AddCourseLabel.AutoSize = true;
            this.AddCourseLabel.Location = new System.Drawing.Point(29, 172);
            this.AddCourseLabel.Name = "AddCourseLabel";
            this.AddCourseLabel.Size = new System.Drawing.Size(93, 20);
            this.AddCourseLabel.TabIndex = 7;
            this.AddCourseLabel.Text = "Add Course";
            // 
            // AddTakenLabel
            // 
            this.AddTakenLabel.AutoSize = true;
            this.AddTakenLabel.Location = new System.Drawing.Point(25, 250);
            this.AddTakenLabel.Name = "AddTakenLabel";
            this.AddTakenLabel.Size = new System.Drawing.Size(141, 20);
            this.AddTakenLabel.TabIndex = 8;
            this.AddTakenLabel.Text = "Add Taken Course";
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(29, 407);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(85, 47);
            this.CancelBtn.TabIndex = 9;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // AddDropdown
            // 
            this.AddDropdown.FormattingEnabled = true;
            this.AddDropdown.Location = new System.Drawing.Point(29, 195);
            this.AddDropdown.Name = "AddDropdown";
            this.AddDropdown.Size = new System.Drawing.Size(171, 28);
            this.AddDropdown.TabIndex = 10;
            // 
            // RemoveTakenLabel
            // 
            this.RemoveTakenLabel.AutoSize = true;
            this.RemoveTakenLabel.Location = new System.Drawing.Point(29, 325);
            this.RemoveTakenLabel.Name = "RemoveTakenLabel";
            this.RemoveTakenLabel.Size = new System.Drawing.Size(171, 20);
            this.RemoveTakenLabel.TabIndex = 11;
            this.RemoveTakenLabel.Text = "Remove Taken Course";
            // 
            // AddTakenDropdown
            // 
            this.AddTakenDropdown.FormattingEnabled = true;
            this.AddTakenDropdown.Location = new System.Drawing.Point(29, 273);
            this.AddTakenDropdown.Name = "AddTakenDropdown";
            this.AddTakenDropdown.Size = new System.Drawing.Size(171, 28);
            this.AddTakenDropdown.TabIndex = 12;
            // 
            // RemoveTakenDropdown
            // 
            this.RemoveTakenDropdown.FormattingEnabled = true;
            this.RemoveTakenDropdown.Location = new System.Drawing.Point(29, 348);
            this.RemoveTakenDropdown.Name = "RemoveTakenDropdown";
            this.RemoveTakenDropdown.Size = new System.Drawing.Size(171, 28);
            this.RemoveTakenDropdown.TabIndex = 13;
            // 
            // UpdateUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 466);
            this.Controls.Add(this.RemoveTakenDropdown);
            this.Controls.Add(this.AddTakenDropdown);
            this.Controls.Add(this.RemoveTakenLabel);
            this.Controls.Add(this.AddDropdown);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.AddTakenLabel);
            this.Controls.Add(this.AddCourseLabel);
            this.Controls.Add(this.ConfirmTextbox);
            this.Controls.Add(this.PasswordTextbox);
            this.Controls.Add(this.ConfirmPasswdLabel);
            this.Controls.Add(this.PasswdLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.UserTextbox);
            this.Controls.Add(this.ConfirmButton);
            this.Name = "UpdateUser";
            this.Text = "Update User";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConfirmButton;
        private System.Windows.Forms.TextBox UserTextbox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label PasswdLabel;
        private System.Windows.Forms.Label ConfirmPasswdLabel;
        private System.Windows.Forms.TextBox PasswordTextbox;
        private System.Windows.Forms.TextBox ConfirmTextbox;
        private System.Windows.Forms.Label AddCourseLabel;
        private System.Windows.Forms.Label AddTakenLabel;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.ComboBox AddDropdown;
        private System.Windows.Forms.Label RemoveTakenLabel;
        private System.Windows.Forms.ComboBox AddTakenDropdown;
        private System.Windows.Forms.ComboBox RemoveTakenDropdown;
    }
}