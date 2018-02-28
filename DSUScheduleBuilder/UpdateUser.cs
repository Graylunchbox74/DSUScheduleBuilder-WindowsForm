using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DSUScheduleBuilder
{
    public partial class UpdateUser : Form
    {
        public UpdateUser()
        {
            InitializeComponent();
        }


        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            MainMenu x = new MainMenu();
            x.Show();
            this.Hide();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            MainMenu x = new MainMenu();
            x.Show();
            this.Hide();
        }
    }
}
