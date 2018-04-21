using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DSUScheduleBuilder
{
    using Network;
    using Models;

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
                HttpRequester.Default.Login(UsernameTextbox.Text, PasswordTextbox.Text, (LoginResponse lr) =>
                {
                    if (lr.errorCode == null)
                    {
                        Console.WriteLine("SUCCESSFULLY LOGGED IN AS: " + lr.user.ToUser().FirstName);

                        MainMenu m = new MainMenu();
                        m.Show();
                        this.Hide();
                        return true;
                    }

                    switch (lr.errorCode)
                    {
                        case 6:
                            //Password failed;
                            return false;

                        default:
                            return false;
                    }
                });
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

        private void ForgotLbl_Click(object sender, EventArgs e)
        {
            LoginPanel.Visible = false;
            RecoveryPanel.Visible = true;
            ForgotLbl.ForeColor = Color.Purple;

        }

        private bool CheckRegex(string s)
        {
            try
            {
                return Regex.IsMatch(s, @"^[a-zA-Z0-9]+@trojans.dsu.edu");
                // Will change later. Test Regex

            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (CheckRegex(EmailTextBox.Text))
            {
                EmailTextBox.Text = "Request Sent";
                EmailTextBox.ReadOnly = true;
                // Add pasword recovery function here
            }
            else
            {
                EmailTextBox.Text = "Invalid email";
            }

        }

        private void GoBackButton_Click(object sender, EventArgs e)
        {
            RecoveryPanel.Visible = false;
            LoginPanel.Visible = true;
        }

        private void EmailTextBox_GotFocus(object sender, EventArgs e)
        {
            // Clear placeholder text
            if (EmailTextBox.Text == "Email")
                EmailTextBox.Text = "";
        }
        private void EmailTextBox_LostFocus(object sender, EventArgs e)
        {
            // Clear placeholder text
            if (EmailTextBox.Text == "")
                EmailTextBox.Text = "Email";
        }
    }
}
