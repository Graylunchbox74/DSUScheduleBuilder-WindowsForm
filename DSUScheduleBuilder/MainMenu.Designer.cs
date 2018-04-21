namespace DSUScheduleBuilder
{
    using Drawing;

    partial class MainMenu
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
            this.SearchBtn = new System.Windows.Forms.Button();
            this.MajorBtn = new System.Windows.Forms.Button();
            this.EditAcctBtn = new System.Windows.Forms.Button();
            this.LogoutBtn = new System.Windows.Forms.Button();
            this.CurrentBtn = new System.Windows.Forms.Button();
            this.weekView1 = new DSUScheduleBuilder.Drawing.WeekView();
            this.SuspendLayout();
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(15, 77);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(164, 54);
            this.SearchBtn.TabIndex = 0;
            this.SearchBtn.Text = "Search For Classes";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // MajorBtn
            // 
            this.MajorBtn.Location = new System.Drawing.Point(14, 137);
            this.MajorBtn.Name = "MajorBtn";
            this.MajorBtn.Size = new System.Drawing.Size(164, 54);
            this.MajorBtn.TabIndex = 1;
            this.MajorBtn.Text = "Search For Major";
            this.MajorBtn.UseVisualStyleBackColor = true;
            // 
            // EditAcctBtn
            // 
            this.EditAcctBtn.Location = new System.Drawing.Point(15, 198);
            this.EditAcctBtn.Name = "EditAcctBtn";
            this.EditAcctBtn.Size = new System.Drawing.Size(164, 54);
            this.EditAcctBtn.TabIndex = 2;
            this.EditAcctBtn.Text = "Account Options";
            this.EditAcctBtn.UseVisualStyleBackColor = true;
            this.EditAcctBtn.Click += new System.EventHandler(this.EditAcctBtn_Click);
            // 
            // LogoutBtn
            // 
            this.LogoutBtn.Location = new System.Drawing.Point(15, 260);
            this.LogoutBtn.Name = "LogoutBtn";
            this.LogoutBtn.Size = new System.Drawing.Size(164, 54);
            this.LogoutBtn.TabIndex = 3;
            this.LogoutBtn.Text = "Logout";
            this.LogoutBtn.UseVisualStyleBackColor = true;
            this.LogoutBtn.Click += new System.EventHandler(this.LogoutBtn_Click);
            // 
            // CurrentBtn
            // 
            this.CurrentBtn.Location = new System.Drawing.Point(14, 15);
            this.CurrentBtn.Name = "CurrentBtn";
            this.CurrentBtn.Size = new System.Drawing.Size(164, 54);
            this.CurrentBtn.TabIndex = 4;
            this.CurrentBtn.Text = "Current Schedule";
            this.CurrentBtn.UseVisualStyleBackColor = true;
            this.CurrentBtn.Click += new System.EventHandler(this.CurrentBtn_Click);
            // 
            // weekView1
            // 
            this.weekView1.Location = new System.Drawing.Point(186, 15);
            this.weekView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.weekView1.Name = "weekView1";
            this.weekView1.Size = new System.Drawing.Size(1323, 803);
            this.weekView1.TabIndex = 5;
            this.weekView1.Text = "weekView1";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1508, 822);
            this.Controls.Add(this.weekView1);
            this.Controls.Add(this.CurrentBtn);
            this.Controls.Add(this.LogoutBtn);
            this.Controls.Add(this.EditAcctBtn);
            this.Controls.Add(this.MajorBtn);
            this.Controls.Add(this.SearchBtn);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainMenu_FormClosed);
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.Button MajorBtn;
        private System.Windows.Forms.Button EditAcctBtn;
        private System.Windows.Forms.Button LogoutBtn;
        private System.Windows.Forms.Button CurrentBtn;
        private WeekView weekView1;
    }
}