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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.termComboBox = new System.Windows.Forms.ComboBox();
            this.PrefixTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.courseNumTextBox = new System.Windows.Forms.TextBox();
            this.ilnTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search For Classes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Term:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Prefix:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Course Number:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 327);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Instructor Last Name";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(348, 37);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(386, 444);
            this.listBox1.TabIndex = 5;
            // 
            // termComboBox
            // 
            this.termComboBox.FormattingEnabled = true;
            this.termComboBox.Location = new System.Drawing.Point(45, 118);
            this.termComboBox.Name = "termComboBox";
            this.termComboBox.Size = new System.Drawing.Size(217, 28);
            this.termComboBox.TabIndex = 6;
            // 
            // PrefixTextBox
            // 
            this.PrefixTextBox.Location = new System.Drawing.Point(45, 210);
            this.PrefixTextBox.Name = "PrefixTextBox";
            this.PrefixTextBox.Size = new System.Drawing.Size(217, 26);
            this.PrefixTextBox.TabIndex = 7;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(45, 400);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(217, 81);
            this.searchButton.TabIndex = 8;
            this.searchButton.Text = "Search!";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // courseNumTextBox
            // 
            this.courseNumTextBox.Location = new System.Drawing.Point(45, 280);
            this.courseNumTextBox.Name = "courseNumTextBox";
            this.courseNumTextBox.Size = new System.Drawing.Size(217, 26);
            this.courseNumTextBox.TabIndex = 10;
            // 
            // ilnTextBox
            // 
            this.ilnTextBox.Location = new System.Drawing.Point(45, 350);
            this.ilnTextBox.Name = "ilnTextBox";
            this.ilnTextBox.Size = new System.Drawing.Size(217, 26);
            this.ilnTextBox.TabIndex = 11;
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ilnTextBox);
            this.Controls.Add(this.courseNumTextBox);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.PrefixTextBox);
            this.Controls.Add(this.termComboBox);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(186, 15);
            this.Name = "Search";
            this.Size = new System.Drawing.Size(1308, 789);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ComboBox termComboBox;
        private System.Windows.Forms.TextBox PrefixTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox courseNumTextBox;
        private System.Windows.Forms.TextBox ilnTextBox;
    }
}
