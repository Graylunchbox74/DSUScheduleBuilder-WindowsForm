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
    public partial class SearchCourses : Form
    {
        public SearchCourses()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            MainMenu x = new MainMenu();
            x.Show();
            this.Hide();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TermComboBox.Text))
            {
                // Ensure that required term field is supplied
                warningLabel.Text = "Please enter term.";
            }
            else
            {
                // Function to retrieve and display database results
                // Open new window to display results and retain current search form
                warningLabel.Text = "Results";
            }
        }
    }
}
