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
    using Main_Menu;
    
    public partial class MainWindow: Form
    {
        public enum States
        {
            Login, NewUser, MainMenu, ForgotPassword
        }

        public enum ActiveController
        {
            WeekView, Search, AccountSettings
        }

        private ActiveController controller;
        private States state;
        public MainWindow()
        {
            InitializeComponent();

            ChangeState(States.Login);
            ChangeActiveController(ActiveController.WeekView);
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
                    ChangeState(States.MainMenu);

                    List<Course> courses = HttpRequester.Default.GetEnrolledCourses();
                    Control_WeekView.SetCourses(courses);
                    
                    WelcomeLabel.Text = "Welcome " + user.FirstName + "!";
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

        #region MAIN MENU EVENTS
        private void MainMenu_LogoutBtn_Click(object sender, EventArgs e)
        {
            HttpRequester.Default.Logout();
            ChangeState(States.Login);
        }

        public void ChangeState(States st)
        {
            LoginPanel.Hide();
            NewUserPanel.Hide();
            MainMenuPanel.Hide();

            state = st;
            if (state == States.Login) LoginPanel.Show();
            if (state == States.NewUser) NewUserPanel.Show();
            if (state == States.MainMenu) MainMenuPanel.Show();
        }

        public void ChangeActiveController(ActiveController ac)
        {
            controller = ac;
            Control_WeekView.Hide();
            Control_Search.Hide();
            Control_AccountSettings.Hide();

            if (controller == ActiveController.WeekView)
            {
                Control_WeekView.Show();
                List<Course> courses = HttpRequester.Default.GetEnrolledCourses();
                Control_WeekView.SetCourses(courses);
            }
            if (controller == ActiveController.Search) Control_Search.Show();
            if (controller == ActiveController.AccountSettings) Control_AccountSettings.Show();
        
        }
        #endregion

        #region MAIN WINDOW EVENTS
        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            HttpRequester.Default.Logout();
            Application.Exit();
        }
        #endregion

        private void MainMenu_SearchBtn_Click(object sender, EventArgs e)
        {
            ChangeActiveController(ActiveController.Search);
        }

        private void MainMenu_WeekViewBtn_Click(object sender, EventArgs e)
        {
            ChangeActiveController(ActiveController.WeekView);
        }

        private void Control_Search_Load(object sender, EventArgs e)
        {

        }

        private void AccountSettingsBtn_Click(object sender, EventArgs e)
        {
            ChangeActiveController(ActiveController.AccountSettings);
        }
    }
}
