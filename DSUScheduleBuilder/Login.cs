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
        bool placeholder = true;

        public Login()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (UsernameTextbox.Text != "Username" && PasswordTextbox.Text != "Password")
            {
                MainMenu x = new MainMenu();
                x.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Please provide a valid username and password");
        }

        private void UsernameTextbox_GotFocus(object sender, EventArgs e)
        {
            // Clear placeholder text
            if (UsernameTextbox.Text == "Username")
                UsernameTextbox.Text = "";
        }

        private void UsernameTextbox_LostFocus(object sender, EventArgs e)
        {
            // Restore placeholder text
            if (UsernameTextbox.Text == "")
                UsernameTextbox.Text = "Username";
        }

        private void PasswordTextbox_GotFocus(object sender, EventArgs e)
        {
            // Clear placeholder text
            if (PasswordTextbox.Text == "Password")
            {
                PasswordTextbox.Text = "";
                placeholder = false;
            }
        }

        private void PasswordTextbox_LostFocus(object sender, EventArgs e)
        {
            // Restore placeholder text and disable password hiding
            if (PasswordTextbox.Text == "")
            {
                placeholder = true;
                PasswordTextbox.PasswordChar = '\0';
                PasswordTextbox.Text = "Password";
            }
        }

        private void PasswordTextbox_TextChanged(object sender, EventArgs e)
        {
            // Hide Password if there is no placeholder
            if(placeholder == false)
                PasswordTextbox.PasswordChar = '*';
        }

        private void NewUserBtn_Click(object sender, EventArgs e)
        {
            NewUser n = new NewUser();
            n.Show();
            this.Hide();
        }
    }
}
