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
    public partial class Login : Form
    {
        bool UsernameChng = false;
        bool PasswordChng = false;

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UsernameTxt.Text != "Username" && PasswordTxt.Text != "Password")
            {
                MainMenu x = new MainMenu();
                x.Show();
                this.Hide();
            }
        }

        private void UsernameTxt_TextChanged(object sender, EventArgs e)
        {
            if (UsernameChng == false)
            {
                UsernameTxt.Text = "";
                UsernameChng = true;
            }
        }

        private void PasswordTxt_TextChanged(object sender, EventArgs e)
        {
            if (PasswordChng == false)
            {
                PasswordTxt.Text = "";
                PasswordChng = true;
            }
        }
    }
}
