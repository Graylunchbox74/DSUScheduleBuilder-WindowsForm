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
    using Network;

    public partial class NewUser : Form
    {
        public NewUser()
        {
            InitializeComponent();
        }

        private void CreateUserButton_Click(object sender, EventArgs e)
        {
            // Code to add user to database goes here
            if (PasswordTxt.Text != ConfirmTxt.Text)
            {
                //Passwords do not match
            }

            HttpRequester.Default.NewUser(NameTxt.Text, PasswordTxt.Text, FirstNameTxt.Text, LastNameTxt.Text, (SuccessResponse s) =>
            {
                if (s.errorCode != null)
                {
                    switch(s.errorCode)
                    {
                        //Check for username already taken, etc
                        default:
                            break;
                    }
                }
                return true;
            });

            MainMenu x = new MainMenu();
            x.Show();
            this.Hide();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Hide();
        }
    }
}
