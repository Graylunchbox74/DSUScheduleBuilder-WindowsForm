namespace DSUScheduleBuilder
{
    partial class SearchCourses
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
            this.termLabel = new System.Windows.Forms.Label();
            this.prefixLabel = new System.Windows.Forms.Label();
            this.numberLabel = new System.Windows.Forms.Label();
            this.instructorLabel = new System.Windows.Forms.Label();
            this.TermComboBox = new System.Windows.Forms.ComboBox();
            this.PrefixComboBox = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.InstructorTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.warningLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // termLabel
            // 
            this.termLabel.AutoSize = true;
            this.termLabel.Location = new System.Drawing.Point(29, 35);
            this.termLabel.Name = "termLabel";
            this.termLabel.Size = new System.Drawing.Size(45, 20);
            this.termLabel.TabIndex = 0;
            this.termLabel.Text = "Term";
            // 
            // prefixLabel
            // 
            this.prefixLabel.AutoSize = true;
            this.prefixLabel.Location = new System.Drawing.Point(234, 35);
            this.prefixLabel.Name = "prefixLabel";
            this.prefixLabel.Size = new System.Drawing.Size(52, 20);
            this.prefixLabel.TabIndex = 1;
            this.prefixLabel.Text = "Prefix:";
            // 
            // numberLabel
            // 
            this.numberLabel.AutoSize = true;
            this.numberLabel.Location = new System.Drawing.Point(29, 97);
            this.numberLabel.Name = "numberLabel";
            this.numberLabel.Size = new System.Drawing.Size(120, 20);
            this.numberLabel.TabIndex = 2;
            this.numberLabel.Text = "Course Number";
            // 
            // instructorLabel
            // 
            this.instructorLabel.AutoSize = true;
            this.instructorLabel.Location = new System.Drawing.Point(29, 131);
            this.instructorLabel.Name = "instructorLabel";
            this.instructorLabel.Size = new System.Drawing.Size(150, 20);
            this.instructorLabel.TabIndex = 3;
            this.instructorLabel.Text = "Instructor last name";
            // 
            // TermComboBox
            // 
            this.TermComboBox.FormattingEnabled = true;
            this.TermComboBox.Items.AddRange(new object[] {
            "Summer 2018",
            "Fall 2018",
            "Spring 2019"});
            this.TermComboBox.Location = new System.Drawing.Point(96, 27);
            this.TermComboBox.Name = "TermComboBox";
            this.TermComboBox.Size = new System.Drawing.Size(121, 28);
            this.TermComboBox.TabIndex = 4;
            // 
            // PrefixComboBox
            // 
            this.PrefixComboBox.FormattingEnabled = true;
            this.PrefixComboBox.Items.AddRange(new object[] {
            "CSC",
            "CIS"});
            this.PrefixComboBox.Location = new System.Drawing.Point(307, 27);
            this.PrefixComboBox.Name = "PrefixComboBox";
            this.PrefixComboBox.Size = new System.Drawing.Size(121, 28);
            this.PrefixComboBox.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(186, 97);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 26);
            this.textBox1.TabIndex = 6;
            // 
            // InstructorTextBox
            // 
            this.InstructorTextBox.Location = new System.Drawing.Point(186, 130);
            this.InstructorTextBox.Name = "InstructorTextBox";
            this.InstructorTextBox.Size = new System.Drawing.Size(100, 26);
            this.InstructorTextBox.TabIndex = 7;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(307, 74);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(121, 82);
            this.searchButton.TabIndex = 8;
            this.searchButton.Text = "Search!";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(33, 181);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(98, 38);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // warningLabel
            // 
            this.warningLabel.AutoSize = true;
            this.warningLabel.Location = new System.Drawing.Point(220, 199);
            this.warningLabel.Name = "warningLabel";
            this.warningLabel.Size = new System.Drawing.Size(0, 20);
            this.warningLabel.TabIndex = 10;
            // 
            // SearchCourses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 273);
            this.Controls.Add(this.warningLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.InstructorTextBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.PrefixComboBox);
            this.Controls.Add(this.TermComboBox);
            this.Controls.Add(this.instructorLabel);
            this.Controls.Add(this.numberLabel);
            this.Controls.Add(this.prefixLabel);
            this.Controls.Add(this.termLabel);
            this.Name = "SearchCourses";
            this.Text = "Search Courses";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label termLabel;
        private System.Windows.Forms.Label prefixLabel;
        private System.Windows.Forms.Label numberLabel;
        private System.Windows.Forms.Label instructorLabel;
        private System.Windows.Forms.ComboBox TermComboBox;
        private System.Windows.Forms.ComboBox PrefixComboBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox InstructorTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label warningLabel;
    }
}