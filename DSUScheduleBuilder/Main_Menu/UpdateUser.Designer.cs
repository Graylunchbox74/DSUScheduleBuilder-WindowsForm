namespace DSUScheduleBuilder.Main_Menu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.acctOptionsLabel = new System.Windows.Forms.Label();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.ConfirmLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.ViewEnrolledBtn = new System.Windows.Forms.Button();
            this.ViewPreviousBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AddPreviousCourseBtn = new System.Windows.Forms.Button();
            this.PreviousCourseIdTxt = new System.Windows.Forms.TextBox();
            this.PreviousCourseView = new DSUScheduleBuilder.Drawing.PreviousCourseView();
            this.EnrolledCourseView = new DSUScheduleBuilder.Drawing.EnrolledCouresView();
            this.SuspendLayout();
            // 
            // acctOptionsLabel
            // 
            this.acctOptionsLabel.AutoSize = true;
            this.acctOptionsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.acctOptionsLabel.Location = new System.Drawing.Point(20, 21);
            this.acctOptionsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.acctOptionsLabel.Name = "acctOptionsLabel";
            this.acctOptionsLabel.Size = new System.Drawing.Size(167, 24);
            this.acctOptionsLabel.TabIndex = 0;
            this.acctOptionsLabel.Text = "Account Settings";
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userNameLabel.Location = new System.Drawing.Point(24, 63);
            this.userNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(81, 18);
            this.userNameLabel.TabIndex = 1;
            this.userNameLabel.Text = "Username:";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLabel.Location = new System.Drawing.Point(24, 95);
            this.passwordLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(79, 18);
            this.passwordLabel.TabIndex = 2;
            this.passwordLabel.Text = "Password:";
            // 
            // ConfirmLabel
            // 
            this.ConfirmLabel.AutoSize = true;
            this.ConfirmLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfirmLabel.Location = new System.Drawing.Point(24, 120);
            this.ConfirmLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ConfirmLabel.Name = "ConfirmLabel";
            this.ConfirmLabel.Size = new System.Drawing.Size(132, 18);
            this.ConfirmLabel.TabIndex = 3;
            this.ConfirmLabel.Text = "Confirm Password";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(161, 63);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ShortcutsEnabled = false;
            this.textBox1.Size = new System.Drawing.Size(150, 20);
            this.textBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(161, 95);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(150, 20);
            this.textBox2.TabIndex = 5;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(161, 122);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(150, 20);
            this.textBox3.TabIndex = 6;
            // 
            // ViewEnrolledBtn
            // 
            this.ViewEnrolledBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ViewEnrolledBtn.Location = new System.Drawing.Point(470, 546);
            this.ViewEnrolledBtn.Margin = new System.Windows.Forms.Padding(2);
            this.ViewEnrolledBtn.Name = "ViewEnrolledBtn";
            this.ViewEnrolledBtn.Size = new System.Drawing.Size(149, 52);
            this.ViewEnrolledBtn.TabIndex = 7;
            this.ViewEnrolledBtn.Text = "View Enrolled";
            this.ViewEnrolledBtn.UseVisualStyleBackColor = true;
            this.ViewEnrolledBtn.Click += new System.EventHandler(this.ViewEnrolledBtn_Click);
            // 
            // ViewPreviousBtn
            // 
            this.ViewPreviousBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ViewPreviousBtn.Location = new System.Drawing.Point(666, 546);
            this.ViewPreviousBtn.Margin = new System.Windows.Forms.Padding(2);
            this.ViewPreviousBtn.Name = "ViewPreviousBtn";
            this.ViewPreviousBtn.Size = new System.Drawing.Size(149, 52);
            this.ViewPreviousBtn.TabIndex = 8;
            this.ViewPreviousBtn.Text = "View Taken";
            this.ViewPreviousBtn.UseVisualStyleBackColor = true;
            this.ViewPreviousBtn.Click += new System.EventHandler(this.ViewPreviousBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 407);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 24);
            this.label1.TabIndex = 13;
            this.label1.Text = "Add a previous course:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label2.Location = new System.Drawing.Point(24, 449);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(216, 18);
            this.label2.TabIndex = 14;
            this.label2.Text = "Previous Course ID (CSC-150):";
            // 
            // AddPreviousCourseBtn
            // 
            this.AddPreviousCourseBtn.Location = new System.Drawing.Point(24, 485);
            this.AddPreviousCourseBtn.Name = "AddPreviousCourseBtn";
            this.AddPreviousCourseBtn.Size = new System.Drawing.Size(216, 35);
            this.AddPreviousCourseBtn.TabIndex = 15;
            this.AddPreviousCourseBtn.Text = "Add Previous Course";
            this.AddPreviousCourseBtn.UseVisualStyleBackColor = true;
            this.AddPreviousCourseBtn.Click += new System.EventHandler(this.AddPreviousCourseBtn_Click);
            // 
            // PreviousCourseIdTxt
            // 
            this.PreviousCourseIdTxt.Location = new System.Drawing.Point(247, 446);
            this.PreviousCourseIdTxt.Name = "PreviousCourseIdTxt";
            this.PreviousCourseIdTxt.Size = new System.Drawing.Size(64, 20);
            this.PreviousCourseIdTxt.TabIndex = 16;
            // 
            // PreviousCourseView
            // 
            this.PreviousCourseView.Location = new System.Drawing.Point(414, 63);
            this.PreviousCourseView.Name = "PreviousCourseView";
            this.PreviousCourseView.Size = new System.Drawing.Size(455, 457);
            this.PreviousCourseView.TabIndex = 17;
            this.PreviousCourseView.Text = "previousCourseView1";
            this.PreviousCourseView.Visible = false;
            // 
            // EnrolledCourseView
            // 
            this.EnrolledCourseView.Location = new System.Drawing.Point(414, 63);
            this.EnrolledCourseView.Name = "EnrolledCourseView";
            this.EnrolledCourseView.Size = new System.Drawing.Size(455, 457);
            this.EnrolledCourseView.TabIndex = 11;
            this.EnrolledCourseView.Text = "EnrolledCourseView";
            this.EnrolledCourseView.Click += new System.EventHandler(this.EnrolledCourseView_Click);
            // 
            // UpdateUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PreviousCourseView);
            this.Controls.Add(this.PreviousCourseIdTxt);
            this.Controls.Add(this.AddPreviousCourseBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EnrolledCourseView);
            this.Controls.Add(this.ViewPreviousBtn);
            this.Controls.Add(this.ViewEnrolledBtn);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ConfirmLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.userNameLabel);
            this.Controls.Add(this.acctOptionsLabel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UpdateUser";
            this.Size = new System.Drawing.Size(872, 606);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label acctOptionsLabel;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label ConfirmLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button ViewEnrolledBtn;
        private System.Windows.Forms.Button ViewPreviousBtn;
        public Drawing.EnrolledCouresView EnrolledCourseView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button AddPreviousCourseBtn;
        private System.Windows.Forms.TextBox PreviousCourseIdTxt;
        public Drawing.PreviousCourseView PreviousCourseView;
    }
}
