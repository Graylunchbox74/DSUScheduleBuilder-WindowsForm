namespace DSUScheduleBuilder.Main_Menu
{
    partial class Search
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.termComboBox = new System.Windows.Forms.ComboBox();
            this.PrefixTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.CourseNumTextBox = new System.Windows.Forms.TextBox();
            this.IlnTextBox = new System.Windows.Forms.TextBox();
            this.AvailableCourseView = new DSUScheduleBuilder.Drawing.AvailableCourseView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search For Classes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 120);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Term:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 175);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Prefix:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 229);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Course Number:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(27, 279);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "Instructor Last Name";
            // 
            // termComboBox
            // 
            this.termComboBox.FormattingEnabled = true;
            this.termComboBox.Location = new System.Drawing.Point(30, 138);
            this.termComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.termComboBox.Name = "termComboBox";
            this.termComboBox.Size = new System.Drawing.Size(146, 21);
            this.termComboBox.TabIndex = 6;
            // 
            // PrefixTextBox
            // 
            this.PrefixTextBox.Location = new System.Drawing.Point(30, 194);
            this.PrefixTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.PrefixTextBox.Name = "PrefixTextBox";
            this.PrefixTextBox.Size = new System.Drawing.Size(146, 20);
            this.PrefixTextBox.TabIndex = 7;
            // 
            // searchButton
            // 
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.searchButton.Location = new System.Drawing.Point(68, 422);
            this.searchButton.Margin = new System.Windows.Forms.Padding(2);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(145, 53);
            this.searchButton.TabIndex = 8;
            this.searchButton.Text = "Search!";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // CourseNumTextBox
            // 
            this.CourseNumTextBox.Location = new System.Drawing.Point(30, 248);
            this.CourseNumTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.CourseNumTextBox.Name = "CourseNumTextBox";
            this.CourseNumTextBox.Size = new System.Drawing.Size(146, 20);
            this.CourseNumTextBox.TabIndex = 10;
            // 
            // IlnTextBox
            // 
            this.IlnTextBox.Location = new System.Drawing.Point(31, 298);
            this.IlnTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.IlnTextBox.Name = "IlnTextBox";
            this.IlnTextBox.Size = new System.Drawing.Size(146, 20);
            this.IlnTextBox.TabIndex = 11;
            // 
            // AvailableCourseView
            // 
            this.AvailableCourseView.Location = new System.Drawing.Point(236, 53);
            this.AvailableCourseView.Name = "AvailableCourseView";
            this.AvailableCourseView.Size = new System.Drawing.Size(633, 457);
            this.AvailableCourseView.TabIndex = 12;
            this.AvailableCourseView.Text = "availableCourseView1";
            this.AvailableCourseView.Click += new System.EventHandler(this.AvailableCourseView_Click);
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AvailableCourseView);
            this.Controls.Add(this.IlnTextBox);
            this.Controls.Add(this.CourseNumTextBox);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.PrefixTextBox);
            this.Controls.Add(this.termComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(186, 15);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Search";
            this.Size = new System.Drawing.Size(872, 513);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox termComboBox;
        private System.Windows.Forms.TextBox PrefixTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox CourseNumTextBox;
        private System.Windows.Forms.TextBox IlnTextBox;
        private Drawing.AvailableCourseView AvailableCourseView;
    }
}
