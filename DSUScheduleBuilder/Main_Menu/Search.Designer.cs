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
            this.AvailableCourseView = new DSUScheduleBuilder.Drawing.AvailableCourseView();
            this.slotsUpDown = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.slotsUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search For Classes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Term:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(36, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Prefix:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(36, 217);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "Course Number:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(37, 288);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 24);
            this.label5.TabIndex = 4;
            this.label5.Text = "Instructor Last Name:";
            // 
            // termComboBox
            // 
            this.termComboBox.FormattingEnabled = true;
            this.termComboBox.Location = new System.Drawing.Point(40, 105);
            this.termComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.termComboBox.Name = "termComboBox";
            this.termComboBox.Size = new System.Drawing.Size(237, 24);
            this.termComboBox.TabIndex = 6;
            // 
            // PrefixTextBox
            // 
            this.PrefixTextBox.Location = new System.Drawing.Point(40, 174);
            this.PrefixTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PrefixTextBox.Name = "PrefixTextBox";
            this.PrefixTextBox.Size = new System.Drawing.Size(237, 22);
            this.PrefixTextBox.TabIndex = 7;
            // 
            // searchButton
            // 
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.searchButton.Location = new System.Drawing.Point(40, 519);
            this.searchButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(237, 65);
            this.searchButton.TabIndex = 10;
            this.searchButton.Text = "Search!";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // CourseNumTextBox
            // 
            this.CourseNumTextBox.Location = new System.Drawing.Point(40, 240);
            this.CourseNumTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CourseNumTextBox.Name = "CourseNumTextBox";
            this.CourseNumTextBox.Size = new System.Drawing.Size(237, 22);
            this.CourseNumTextBox.TabIndex = 8;
            // 
            // IlnTextBox
            // 
            this.IlnTextBox.Location = new System.Drawing.Point(41, 314);
            this.IlnTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.IlnTextBox.Name = "IlnTextBox";
            this.IlnTextBox.Size = new System.Drawing.Size(236, 22);
            this.IlnTextBox.TabIndex = 9;
            // 
            // startTimePicker
            // 
            this.startTimePicker.CustomFormat = "hh:mm tt";
            this.startTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.startTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startTimePicker.Location = new System.Drawing.Point(42, 379);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.ShowUpDown = true;
            this.startTimePicker.Size = new System.Drawing.Size(114, 23);
            this.startTimePicker.TabIndex = 13;
            this.startTimePicker.Value = new System.DateTime(2069, 4, 20, 0, 0, 0, 0);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label6.Location = new System.Drawing.Point(38, 352);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 24);
            this.label6.TabIndex = 14;
            this.label6.Text = "Start Time:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label7.Location = new System.Drawing.Point(158, 352);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 24);
            this.label7.TabIndex = 16;
            this.label7.Text = "End Time;";
            // 
            // endTimePicker
            // 
            this.endTimePicker.CustomFormat = "hh:mm tt";
            this.endTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.endTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endTimePicker.Location = new System.Drawing.Point(162, 379);
            this.endTimePicker.Name = "endTimePicker";
            this.endTimePicker.ShowUpDown = true;
            this.endTimePicker.Size = new System.Drawing.Size(115, 23);
            this.endTimePicker.TabIndex = 15;
            this.endTimePicker.Value = new System.DateTime(2018, 4, 22, 23, 59, 0, 0);
            // 
            // timeCheckbox
            // 
            this.timeCheckbox.AutoSize = true;
            this.timeCheckbox.Location = new System.Drawing.Point(103, 408);
            this.timeCheckbox.Name = "timeCheckbox";
            this.timeCheckbox.Size = new System.Drawing.Size(124, 21);
            this.timeCheckbox.TabIndex = 17;
            this.timeCheckbox.Text = "Search by time";
            this.timeCheckbox.UseVisualStyleBackColor = true;
            // 
            // AvailableCourseView
            // 
            this.AvailableCourseView.Location = new System.Drawing.Point(315, 65);
            this.AvailableCourseView.Margin = new System.Windows.Forms.Padding(4);
            this.AvailableCourseView.Name = "AvailableCourseView";
            this.AvailableCourseView.Size = new System.Drawing.Size(844, 562);
            this.AvailableCourseView.TabIndex = 12;
            this.AvailableCourseView.Text = "availableCourseView1";
            this.AvailableCourseView.Click += new System.EventHandler(this.AvailableCourseView_Click);
            // 
            // slotsUpDown
            // 
            this.slotsUpDown.Location = new System.Drawing.Point(42, 470);
            this.slotsUpDown.Name = "slotsUpDown";
            this.slotsUpDown.Size = new System.Drawing.Size(235, 22);
            this.slotsUpDown.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label8.Location = new System.Drawing.Point(38, 442);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(174, 24);
            this.label8.TabIndex = 19;
            this.label8.Text = "Number of slots left:";
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Search";
            this.Size = new System.Drawing.Size(1163, 631);
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
