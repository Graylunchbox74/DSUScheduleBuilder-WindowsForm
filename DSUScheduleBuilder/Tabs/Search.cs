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

namespace DSUScheduleBuilder.Tabs
{
    using Network;
    using Models;
    using Utils;

    public partial class Search : UserControl, IResetable
    {
        public Search()
        {
            InitializeComponent();
        }

        #region CLICK METHODS

        /// <summary>
        /// What happens when the user clicks the search button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            this.AvailableCourseView.ResetToDefault();
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
                        GeneralUtil.ShowError(res);
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

        /// <summary>
        /// What happens when the user clicks on the AvailableCourseView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AvailableCourseView_Click(object sender, EventArgs e)
        {
            AvailableCourseView.OnClickEvent(e);
        }

        #endregion

        /// <summary>
        /// Resets the view to the default state.
        /// </summary>
        public void ResetToDefault()
        {
            termComboBox.Text = "";
            PrefixTextBox.Text = "";
            CourseNumTextBox.Text = "";
            IlnTextBox.Text = "";
            timeCheckbox.Checked = false;
            slotsUpDown.Value = 0;
        }
    }
}
