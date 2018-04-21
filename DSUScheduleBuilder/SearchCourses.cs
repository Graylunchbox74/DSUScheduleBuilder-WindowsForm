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

        private void CancelButton_Click(object sender, EventArgs e)
        {
        }

        private void SearchButton_Click(object sender, EventArgs e)
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
