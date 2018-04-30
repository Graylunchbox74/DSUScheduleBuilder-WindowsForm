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
            this.startTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.endTimePicker = new System.Windows.Forms.DateTimePicker();
            this.timeCheckbox = new System.Windows.Forms.CheckBox();
            this.slotsUpDown = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.AvailableCourseView = new DSUScheduleBuilder.Drawing.AvailableCourseView();
            ((System.ComponentModel.ISupportInitialize)(this.slotsUpDown)).BeginInit();
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
            this.label2.Location = new System.Drawing.Point(27, 67);
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
            this.label3.Location = new System.Drawing.Point(27, 120);
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
            this.label4.Location = new System.Drawing.Point(27, 176);
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
            this.label5.Location = new System.Drawing.Point(28, 234);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "Instructor Last Name:";
            // 
            // termComboBox
            // 
            this.termComboBox.BackColor = System.Drawing.Color.Gray;
            this.termComboBox.ForeColor = System.Drawing.Color.White;
            this.termComboBox.FormattingEnabled = true;
            this.termComboBox.Location = new System.Drawing.Point(30, 85);
            this.termComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.termComboBox.Name = "termComboBox";
            this.termComboBox.Size = new System.Drawing.Size(179, 21);
            this.termComboBox.TabIndex = 10;
            // 
            // PrefixTextBox
            // 
            this.PrefixTextBox.BackColor = System.Drawing.Color.Gray;
            this.PrefixTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PrefixTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PrefixTextBox.ForeColor = System.Drawing.Color.White;
            this.PrefixTextBox.Location = new System.Drawing.Point(30, 141);
            this.PrefixTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.PrefixTextBox.Name = "PrefixTextBox";
            this.PrefixTextBox.Size = new System.Drawing.Size(179, 20);
            this.PrefixTextBox.TabIndex = 11;
            this.PrefixTextBox.TextChanged += new System.EventHandler(this.PrefixTextBox_TextChanged);
            // 
            // searchButton
            // 
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Location = new System.Drawing.Point(30, 422);
            this.searchButton.Margin = new System.Windows.Forms.Padding(2);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(178, 53);
            this.searchButton.TabIndex = 19;
            this.searchButton.Text = "Search!";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // CourseNumTextBox
            // 
            this.CourseNumTextBox.BackColor = System.Drawing.Color.Gray;
            this.CourseNumTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CourseNumTextBox.ForeColor = System.Drawing.Color.White;
            this.CourseNumTextBox.Location = new System.Drawing.Point(30, 195);
            this.CourseNumTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.CourseNumTextBox.Name = "CourseNumTextBox";
            this.CourseNumTextBox.Size = new System.Drawing.Size(179, 20);
            this.CourseNumTextBox.TabIndex = 12;
            this.CourseNumTextBox.TextChanged += new System.EventHandler(this.CourseNumTextBox_TextChanged);
            // 
            // IlnTextBox
            // 
            this.IlnTextBox.BackColor = System.Drawing.Color.Gray;
            this.IlnTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IlnTextBox.ForeColor = System.Drawing.Color.White;
            this.IlnTextBox.Location = new System.Drawing.Point(31, 255);
            this.IlnTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.IlnTextBox.Name = "IlnTextBox";
            this.IlnTextBox.Size = new System.Drawing.Size(178, 20);
            this.IlnTextBox.TabIndex = 13;
            // 
            // startTimePicker
            // 
            this.startTimePicker.CustomFormat = "hh:mm tt";
            this.startTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.startTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startTimePicker.Location = new System.Drawing.Point(32, 308);
            this.startTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.ShowUpDown = true;
            this.startTimePicker.Size = new System.Drawing.Size(86, 20);
            this.startTimePicker.TabIndex = 14;
            this.startTimePicker.Value = new System.DateTime(2069, 4, 20, 0, 0, 0, 0);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label6.Location = new System.Drawing.Point(28, 286);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 14;
            this.label6.Text = "Start Time:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label7.Location = new System.Drawing.Point(118, 286);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 18);
            this.label7.TabIndex = 16;
            this.label7.Text = "End Time;";
            // 
            // endTimePicker
            // 
            this.endTimePicker.CustomFormat = "hh:mm tt";
            this.endTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.endTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endTimePicker.Location = new System.Drawing.Point(122, 308);
            this.endTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.endTimePicker.Name = "endTimePicker";
            this.endTimePicker.ShowUpDown = true;
            this.endTimePicker.Size = new System.Drawing.Size(87, 20);
            this.endTimePicker.TabIndex = 15;
            this.endTimePicker.Value = new System.DateTime(2018, 4, 22, 23, 59, 0, 0);
            // 
            // timeCheckbox
            // 
            this.timeCheckbox.AutoSize = true;
            this.timeCheckbox.Location = new System.Drawing.Point(77, 332);
            this.timeCheckbox.Margin = new System.Windows.Forms.Padding(2);
            this.timeCheckbox.Name = "timeCheckbox";
            this.timeCheckbox.Size = new System.Drawing.Size(96, 17);
            this.timeCheckbox.TabIndex = 17;
            this.timeCheckbox.Text = "Search by time";
            this.timeCheckbox.UseVisualStyleBackColor = true;
            // 
            // slotsUpDown
            // 
            this.slotsUpDown.BackColor = System.Drawing.Color.Gray;
            this.slotsUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.slotsUpDown.ForeColor = System.Drawing.Color.White;
            this.slotsUpDown.Location = new System.Drawing.Point(32, 382);
            this.slotsUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.slotsUpDown.Name = "slotsUpDown";
            this.slotsUpDown.Size = new System.Drawing.Size(176, 20);
            this.slotsUpDown.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label8.Location = new System.Drawing.Point(28, 359);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(141, 18);
            this.label8.TabIndex = 19;
            this.label8.Text = "Number of slots left:";
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
            this.Controls.Add(this.label8);
            this.Controls.Add(this.slotsUpDown);
            this.Controls.Add(this.timeCheckbox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.endTimePicker);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.startTimePicker);
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
            ((System.ComponentModel.ISupportInitialize)(this.slotsUpDown)).EndInit();
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
        private System.Windows.Forms.DateTimePicker startTimePicker;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker endTimePicker;
        private System.Windows.Forms.CheckBox timeCheckbox;
        private System.Windows.Forms.NumericUpDown slotsUpDown;
        private System.Windows.Forms.Label label8;
    }
}
