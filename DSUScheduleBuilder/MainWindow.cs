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

            state = States.Login;

            LoginPanel.Show();
            NewUserPanel.Hide();
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
                User user = null;
                HttpRequester.Default.Login(Login_UsernameTxt.Text, Login_PasswordTxt.Text, (LoginResponse lr) =>
                {
                    if (lr.errorCode == null)
                    {
                        user = lr.user.ToUser();
                        Console.WriteLine("SUCCESSFULLY LOGGED IN AS: " + user.FirstName);
                        return true;
                    }

                    switch (lr.errorCode)
                    {
                        case 6:
                        case 7:
                            MessageBox.Show("Incorrect Password");
                            return false;

                        default:
                            MessageBox.Show("Unknown error: " + lr.errorCode + " : " + lr.errorMessage);
                            return false;
                    }
                });

                if (user != null)
                {
                    LoginPanel.Hide();

                    List<Course> courses = HttpRequester.Default.GetEnrolledCourses();
                    weekView1.SetCourses(courses);

                    WelcomeLabel.Text = "Welcome " + user.FirstName + "!";

                    MainMenuPanel.Show();
                }
            }
            else
                MessageBox.Show("Please provide a valid username and password");
        }

        private void Login_NewUserBtn_Click(object sender, EventArgs e)
        {
            state = States.NewUser;
            LoginPanel.Hide();
            NewUserPanel.Show();
        }

        #endregion

        #region NEW USER EVENTS
        private void NewUser_CreateUserBtn_Click(object sender, EventArgs e)
        {
            // Code to add user to database goes here
            if (NewUser_PasswordTxt.Text != NewUser_ConfirmTxt.Text)
            {
                MessageBox.Show("Passwords do not match.");
            }

            bool successful = false;
            HttpRequester.Default.NewUser(NewUser_NameTxt.Text, NewUser_PasswordTxt.Text, NewUser_FirstNameTxt.Text, NewUser_LastNameTxt.Text, (SuccessResponse s) =>
            {
                if (s.errorCode != null)
                {
                    switch (s.errorCode)
                    {
                        case 13:
                            MessageBox.Show("Email already taken.");
                            return false;
                        default:
                            break;
                    }
                }
                successful = true;
                return true;
            });

            if (successful)
            {
                state = States.Login;
                NewUserPanel.Hide();
                LoginPanel.Show();
            }
        }

        private void NewUser_CancelBtn_Click(object sender, EventArgs e)
        {
            state = States.Login;
            NewUserPanel.Hide();
            LoginPanel.Show();
        }

        #endregion

        #region MAIN MENU EVENTS
        private void MainMenu_LogoutBtn_Click(object sender, EventArgs e)
        {
            HttpRequester.Default.Logout();
            MainMenuPanel.Hide();
            LoginPanel.Show();
        }
        #endregion

        #region MAIN WINDOW EVENTS
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            HttpRequester.Default.Logout();
            Application.Exit();
        }
        #endregion
    }
}
