namespace DSUScheduleBuilder.Tabs
{
    partial class ManageCourses
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
            this.ViewPreviousBtn = new System.Windows.Forms.Button();
            this.ViewEnrolledBtn = new System.Windows.Forms.Button();
            this.PreviousCourseIdTxt = new System.Windows.Forms.TextBox();
            this.AddPreviousCourseBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CourseNameTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CreditsUpDown = new System.Windows.Forms.NumericUpDown();
            this.PreviousCourseView = new DSUScheduleBuilder.Drawing.PreviousCourseView();
            this.EnrolledCourseView = new DSUScheduleBuilder.Drawing.EnrolledCouresView();
            ((System.ComponentModel.ISupportInitialize)(this.CreditsUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ViewPreviousBtn
            // 
            this.ViewPreviousBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewPreviousBtn.Location = new System.Drawing.Point(999, 800);
            this.ViewPreviousBtn.Name = "ViewPreviousBtn";
            this.ViewPreviousBtn.Size = new System.Drawing.Size(224, 80);
            this.ViewPreviousBtn.TabIndex = 21;
            this.ViewPreviousBtn.Text = "View Taken";
            this.ViewPreviousBtn.UseVisualStyleBackColor = true;
            this.ViewPreviousBtn.Click += new System.EventHandler(this.ViewPreviousBtn_Click);
            // 
            // ViewEnrolledBtn
            // 
            this.ViewEnrolledBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewEnrolledBtn.Location = new System.Drawing.Point(705, 800);
            this.ViewEnrolledBtn.Name = "ViewEnrolledBtn";
            this.ViewEnrolledBtn.Size = new System.Drawing.Size(224, 80);
            this.ViewEnrolledBtn.TabIndex = 20;
            this.ViewEnrolledBtn.Text = "View Enrolled";
            this.ViewEnrolledBtn.UseVisualStyleBackColor = true;
            this.ViewEnrolledBtn.Click += new System.EventHandler(this.ViewEnrolledBtn_Click);
            // 
            // PreviousCourseIdTxt
            // 
            this.PreviousCourseIdTxt.BackColor = System.Drawing.Color.DimGray;
            this.PreviousCourseIdTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PreviousCourseIdTxt.ForeColor = System.Drawing.Color.White;
            this.PreviousCourseIdTxt.Location = new System.Drawing.Point(375, 123);
            this.PreviousCourseIdTxt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PreviousCourseIdTxt.Name = "PreviousCourseIdTxt";
            this.PreviousCourseIdTxt.Size = new System.Drawing.Size(196, 26);
            this.PreviousCourseIdTxt.TabIndex = 10;
            // 
            // AddPreviousCourseBtn
            // 
            this.AddPreviousCourseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddPreviousCourseBtn.Location = new System.Drawing.Point(134, 300);
            this.AddPreviousCourseBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AddPreviousCourseBtn.Name = "AddPreviousCourseBtn";
            this.AddPreviousCourseBtn.Size = new System.Drawing.Size(324, 54);
            this.AddPreviousCourseBtn.TabIndex = 13;
            this.AddPreviousCourseBtn.Text = "Add Previous Course";
            this.AddPreviousCourseBtn.UseVisualStyleBackColor = true;
            this.AddPreviousCourseBtn.Click += new System.EventHandler(this.AddPreviousCourseBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label2.Location = new System.Drawing.Point(42, 122);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(317, 26);
            this.label2.TabIndex = 23;
            this.label2.Text = "Previous Course ID (CSC-105):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 57);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(313, 33);
            this.label1.TabIndex = 22;
            this.label1.Text = "Add a previous course:";
            // 
            // CourseNameTxt
            // 
            this.CourseNameTxt.BackColor = System.Drawing.Color.DimGray;
            this.CourseNameTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CourseNameTxt.ForeColor = System.Drawing.Color.White;
            this.CourseNameTxt.Location = new System.Drawing.Point(375, 174);
            this.CourseNameTxt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CourseNameTxt.Name = "CourseNameTxt";
            this.CourseNameTxt.Size = new System.Drawing.Size(196, 26);
            this.CourseNameTxt.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label3.Location = new System.Drawing.Point(6, 174);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(349, 26);
            this.label3.TabIndex = 26;
            this.label3.Text = "Course name (Intro to Computers):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label4.Location = new System.Drawing.Point(166, 232);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 26);
            this.label4.TabIndex = 28;
            this.label4.Text = "Number of Credits:";
            // 
            // CreditsUpDown
            // 
            this.CreditsUpDown.BackColor = System.Drawing.Color.DimGray;
            this.CreditsUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CreditsUpDown.ForeColor = System.Drawing.Color.White;
            this.CreditsUpDown.Location = new System.Drawing.Point(375, 229);
            this.CreditsUpDown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CreditsUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.CreditsUpDown.Name = "CreditsUpDown";
            this.CreditsUpDown.Size = new System.Drawing.Size(196, 26);
            this.CreditsUpDown.TabIndex = 12;
            this.CreditsUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // PreviousCourseView
            // 
            this.PreviousCourseView.Location = new System.Drawing.Point(621, 57);
            this.PreviousCourseView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PreviousCourseView.Name = "PreviousCourseView";
            this.PreviousCourseView.Size = new System.Drawing.Size(682, 703);
            this.PreviousCourseView.TabIndex = 21;
            this.PreviousCourseView.Text = "PreviousCourseView";
            this.PreviousCourseView.Visible = false;
            this.PreviousCourseView.Click += new System.EventHandler(this.PreviousCourseView_Click);
            // 
            // EnrolledCourseView
            // 
            this.EnrolledCourseView.Location = new System.Drawing.Point(621, 57);
            this.EnrolledCourseView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.EnrolledCourseView.Name = "EnrolledCourseView";
            this.EnrolledCourseView.Size = new System.Drawing.Size(682, 703);
            this.EnrolledCourseView.TabIndex = 20;
            this.EnrolledCourseView.Text = "EnrolledCourseView";
            this.EnrolledCourseView.Click += new System.EventHandler(this.EnrolledCourseView_Click);
            // 
            // ManageCourses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CreditsUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CourseNameTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PreviousCourseIdTxt);
            this.Controls.Add(this.AddPreviousCourseBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PreviousCourseView);
            this.Controls.Add(this.EnrolledCourseView);
            this.Controls.Add(this.ViewPreviousBtn);
            this.Controls.Add(this.ViewEnrolledBtn);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ManageCourses";
            this.Size = new System.Drawing.Size(1308, 932);
            ((System.ComponentModel.ISupportInitialize)(this.CreditsUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Drawing.PreviousCourseView PreviousCourseView;
        public Drawing.EnrolledCouresView EnrolledCourseView;
        private System.Windows.Forms.Button ViewPreviousBtn;
        private System.Windows.Forms.Button ViewEnrolledBtn;
        private System.Windows.Forms.TextBox PreviousCourseIdTxt;
        private System.Windows.Forms.Button AddPreviousCourseBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CourseNameTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown CreditsUpDown;
    }
}
