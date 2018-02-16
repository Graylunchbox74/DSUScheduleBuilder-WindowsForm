namespace DSUScheduleBuilder
{
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
            this.SuspendLayout();
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(13, 61);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(145, 43);
            this.SearchBtn.TabIndex = 0;
            this.SearchBtn.Text = "Search For Classes";
            this.SearchBtn.UseVisualStyleBackColor = true;
            // 
            // MajorBtn
            // 
            this.MajorBtn.Location = new System.Drawing.Point(12, 110);
            this.MajorBtn.Name = "MajorBtn";
            this.MajorBtn.Size = new System.Drawing.Size(145, 43);
            this.MajorBtn.TabIndex = 1;
            this.MajorBtn.Text = "Search For Major";
            this.MajorBtn.UseVisualStyleBackColor = true;
            // 
            // EditAcctBtn
            // 
            this.EditAcctBtn.Location = new System.Drawing.Point(13, 159);
            this.EditAcctBtn.Name = "EditAcctBtn";
            this.EditAcctBtn.Size = new System.Drawing.Size(145, 43);
            this.EditAcctBtn.TabIndex = 2;
            this.EditAcctBtn.Text = "Account Options";
            this.EditAcctBtn.UseVisualStyleBackColor = true;
            // 
            // LogoutBtn
            // 
            this.LogoutBtn.Location = new System.Drawing.Point(13, 208);
            this.LogoutBtn.Name = "LogoutBtn";
            this.LogoutBtn.Size = new System.Drawing.Size(145, 43);
            this.LogoutBtn.TabIndex = 3;
            this.LogoutBtn.Text = "Logout";
            this.LogoutBtn.UseVisualStyleBackColor = true;
            this.LogoutBtn.Click += new System.EventHandler(this.LogoutBtn_Click);
            // 
            // CurrentBtn
            // 
            this.CurrentBtn.Location = new System.Drawing.Point(12, 12);
            this.CurrentBtn.Name = "CurrentBtn";
            this.CurrentBtn.Size = new System.Drawing.Size(145, 43);
            this.CurrentBtn.TabIndex = 4;
            this.CurrentBtn.Text = "Current Schedule";
            this.CurrentBtn.UseVisualStyleBackColor = true;
            this.CurrentBtn.Click += new System.EventHandler(this.CurrentBtn_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 699);
            this.Controls.Add(this.CurrentBtn);
            this.Controls.Add(this.LogoutBtn);
            this.Controls.Add(this.EditAcctBtn);
            this.Controls.Add(this.MajorBtn);
            this.Controls.Add(this.SearchBtn);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.Button MajorBtn;
        private System.Windows.Forms.Button EditAcctBtn;
        private System.Windows.Forms.Button LogoutBtn;
        private System.Windows.Forms.Button CurrentBtn;
    }
}