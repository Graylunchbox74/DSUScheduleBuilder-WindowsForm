using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSUScheduleBuilder.Main_Menu
{
    using Network;
    using Models;
    using Utils;

    public partial class Search : UserControl
    {
        public Search()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            int startTime, endTime;

            if (timeCheckbox.Checked)
            {
                startTime = startTimePicker.Value.TimeOfDay.Hours * 100 + startTimePicker.Value.TimeOfDay.Minutes;
                endTime = endTimePicker.Value.TimeOfDay.Hours * 100 + endTimePicker.Value.TimeOfDay.Minutes;
            }
            else
            {
                startTime = -1;
                endTime = -1;
            }

            List<AvailableCourse> courses =
                HttpRequester.Default.SearchForCourses(termComboBox.Text, PrefixTextBox.Text, CourseNumTextBox.Text, IlnTextBox.Text, startTime, endTime, (int)slotsUpDown.Value,
                (FullAvailableCourseResponse res) =>
                {
                    if (res.errorCode != null)
                    {
                        switch(res.errorCode)
                        {
                            default:
                                MessageBox.Show("ERROR " + res.errorCode + ": " + res.errorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }
                        return false;
                    }

                    if (res.classes == null)
                    {
                        MessageBox.Show("No classes found that match criteria.");
                        return false;
                    }

                    return true;
                });
        
            if (courses != null)
            {
                AvailableCourseView.SetCourses(courses);
                AvailableCourseView.Refresh();
            }
        }

        private void AvailableCourseView_Click(object sender, EventArgs e)
        {
            AvailableCourseView.OnClick(e);
        }

        public void ClearFields()
        {
            termComboBox.Text = "";
            PrefixTextBox.Text = "";
            CourseNumTextBox.Text = "";
            IlnTextBox.Text = "";
            timeCheckbox.Checked = false;
        }
    }
}
