namespace DSUScheduleBuilder
{
    partial class NewUser
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
            this.NameTxt = new System.Windows.Forms.TextBox();
            this.PasswordTxt = new System.Windows.Forms.TextBox();
            this.ConfirmTxt = new System.Windows.Forms.TextBox();
            this.TakenCoursesBtn = new System.Windows.Forms.Button();
            this.MajorDdl = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // NameTxt
            // 
            this.NameTxt.Location = new System.Drawing.Point(14, 15);
            this.NameTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NameTxt.Name = "NameTxt";
            this.NameTxt.Size = new System.Drawing.Size(224, 26);
            this.NameTxt.TabIndex = 0;
            this.NameTxt.Text = "Name";
            // 
            // PasswordTxt
            // 
            this.PasswordTxt.Location = new System.Drawing.Point(14, 50);
            this.PasswordTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PasswordTxt.Name = "PasswordTxt";
            this.PasswordTxt.Size = new System.Drawing.Size(224, 26);
            this.PasswordTxt.TabIndex = 1;
            this.PasswordTxt.Text = "Password";
            // 
            // ConfirmTxt
            // 
            this.ConfirmTxt.Location = new System.Drawing.Point(14, 85);
            this.ConfirmTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ConfirmTxt.Name = "ConfirmTxt";
            this.ConfirmTxt.Size = new System.Drawing.Size(224, 26);
            this.ConfirmTxt.TabIndex = 2;
            this.ConfirmTxt.Text = "Confirm Password";
            // 
            // TakenCoursesBtn
            // 
            this.TakenCoursesBtn.Location = new System.Drawing.Point(14, 134);
            this.TakenCoursesBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TakenCoursesBtn.Name = "TakenCoursesBtn";
            this.TakenCoursesBtn.Size = new System.Drawing.Size(225, 55);
            this.TakenCoursesBtn.TabIndex = 3;
            this.TakenCoursesBtn.Text = "Select Already Taken Courses";
            this.TakenCoursesBtn.UseVisualStyleBackColor = true;
            // 
            // MajorDdl
            // 
            this.MajorDdl.FormattingEnabled = true;
            this.MajorDdl.Items.AddRange(new object[] {
            "Computer Science",
            "Cyber Operations",
            "Network Security Administration"});
            this.MajorDdl.Location = new System.Drawing.Point(14, 211);
            this.MajorDdl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MajorDdl.Name = "MajorDdl";
            this.MajorDdl.Size = new System.Drawing.Size(224, 28);
            this.MajorDdl.TabIndex = 4;
            // 
            // NewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 811);
            this.Controls.Add(this.MajorDdl);
            this.Controls.Add(this.TakenCoursesBtn);
            this.Controls.Add(this.ConfirmTxt);
            this.Controls.Add(this.PasswordTxt);
            this.Controls.Add(this.NameTxt);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "NewUser";
            this.Text = "New User";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NameTxt;
        private System.Windows.Forms.TextBox PasswordTxt;
        private System.Windows.Forms.TextBox ConfirmTxt;
        private System.Windows.Forms.Button TakenCoursesBtn;
        private System.Windows.Forms.ComboBox MajorDdl;
    }
}