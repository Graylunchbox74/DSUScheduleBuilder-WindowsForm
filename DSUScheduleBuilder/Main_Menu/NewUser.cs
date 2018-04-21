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
    public partial class NewUser : UserControl
    {
        public NewUser()
        {
            InitializeComponent();
        }

        private bool CheckRegex(string s)
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

        private void create_Click(object sender, EventArgs e)
        {
            if(emailTextBox.Text == "" && passwordTextBox.Text == "" && confirmTextBox.Text == "" && firstnameTextBox.Text == "" && lastnameTextBox.Text == "")
            {
                MessageBox.Show("Please fill in all textboxes");
            }
            else if(passwordTextBox.Text != confirmTextBox.Text)
            {
                MessageBox.Show("Error: Passwords do not match");
            }
            else if(!CheckRegex(emailTextBox.Text))
            {
                MessageBox.Show("Please enter a valid email address");
            }
            else
            {
                // Add new user to database
            }
        }
    }
}
