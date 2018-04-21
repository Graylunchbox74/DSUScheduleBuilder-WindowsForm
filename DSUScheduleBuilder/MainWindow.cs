using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSUScheduleBuilder
{
    using Network;
    using Models;
    using Drawing;
    
    public partial class MainWindow: Form
    {
        private enum States
        {
            Login, NewUser, MainMenu, ForgotPassword
        }

        private States state;
        public MainWindow()
        {
            InitializeComponent();

            LoginPanel.Show();
            MainMenuPanel.Hide();
        }

        #region LOGIN PANEL EVENT HANDLERS
        private bool login_hide_password = false;
        private void Login_UsernameTxt_GotFocus(object sender, EventArgs e)
        {
            // Clear placeholder text
            if (Login_UsernameTxt.Text == "Username")
                Login_UsernameTxt.Text = "";
        }

        private void Login_UsernameTxt_LostFocus(object sender, EventArgs e)
        {
            // Restore placeholder text
            if (Login_UsernameTxt.Text == "")
                Login_UsernameTxt.Text = "Username";
        }

        private void Login_PasswordTxt_GotFocus(object sender, EventArgs e)
        {
            // Clear placeholder text
            if (Login_PasswordTxt.Text == "Password")
            {
                Login_PasswordTxt.Text = "";
                login_hide_password = true;
            }
        }

        private void Login_PasswordTxt_LostFocus(object sender, EventArgs e)
        {
            // Restore placeholder text and disable password hiding
            if (Login_PasswordTxt.Text == "")
            {
                login_hide_password = false;
                Login_PasswordTxt.PasswordChar = '\0';
                Login_PasswordTxt.Text = "Password";
            }
        }
        
        private void Login_PasswordTxt_TextChanged(object sender, EventArgs e)
        {
            // Hide Password if there is no placeholder
            if (login_hide_password)
                Login_PasswordTxt.PasswordChar = '*';
        }

        private void Login_LoginBtn_Click(object sender, EventArgs e)
        {
            if (Login_UsernameTxt.Text != "Username" && Login_PasswordTxt.Text != "Password")
            {
                bool succ = false;
                HttpRequester.Default.Login(Login_UsernameTxt.Text, Login_PasswordTxt.Text, (LoginResponse lr) =>
                {
                    if (lr.errorCode == null)
                    {
                        Console.WriteLine("SUCCESSFULLY LOGGED IN AS: " + lr.user.ToUser().FirstName);
                        succ = true;
                        return true;
                    }

                    switch (lr.errorCode)
                    {
                        case 6:
                            MessageBox.Show("Incorrect Password");
                            return false;

                        default:
                            return false;
                    }
                });

                if (succ)
                {
                    LoginPanel.Hide();
                    MainMenuPanel.Show();
                }
            }
            else
                MessageBox.Show("Please provide a valid username and password");
        }

        #endregion
    }
}
