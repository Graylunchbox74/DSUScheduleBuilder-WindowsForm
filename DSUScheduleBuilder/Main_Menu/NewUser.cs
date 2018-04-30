using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DSUScheduleBuilder.Main_Menu
{
    using Network;

    public partial class NewUser : UserControl, IResetable
    {
        public NewUser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Checks to ensure that the email is valid.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool CheckEmailRegex(string s)
        {
            try
            {
                return ( Regex.IsMatch(s, @"^[a-zA-Z0-9]+.[a-zA-Z0-9]+@trojans.dsu.edu") || Regex.IsMatch(s, @"^[a-zA-Z0-9]+.[a-zA-Z0-9]+@pluto.dsu.edu") );
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        #region CLICK METHODS
        /// <summary>
        /// What happens when the user clicks the create button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Create_Click(object sender, EventArgs e)
        {
            if(emailTextBox.Text == "" && passwordTextBox.Text == "" && confirmTextBox.Text == "" && firstnameTextBox.Text == "" && lastnameTextBox.Text == "")
            {
                MessageBox.Show("Please fill in all textboxes");
            }
            else if(passwordTextBox.Text != confirmTextBox.Text)
            {
                MessageBox.Show("Error: Passwords do not match");
            }
            else if(!CheckEmailRegex(emailTextBox.Text))
            {
                MessageBox.Show("Please enter a valid email address");
            }
            else
            {
                bool successful = false;
                HttpRequester.Default.NewUser(emailTextBox.Text, passwordTextBox.Text, firstnameTextBox.Text, lastnameTextBox.Text, (SuccessResponse s) =>
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
                    ((MainWindow)this.Parent.Parent).ChangeState(MainWindow.States.Login);
                }
            }
        }

        /// <summary>
        /// What happens when the user clicks the back button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, EventArgs e)
        {
            ((MainWindow)this.Parent.Parent).ChangeState(MainWindow.States.Login);
        }

        #endregion

        /// <summary>
        /// Resets the view to the default state.
        /// </summary>
        public void ResetToDefault()
        {
            emailTextBox.Text = "";
            passwordTextBox.Text = "";
            confirmTextBox.Text = "";
        }
    }
}
