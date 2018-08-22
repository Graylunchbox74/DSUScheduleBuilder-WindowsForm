using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSUScheduleBuilder.Tabs
{
    using Drawing;
    using DSUScheduleBuilder.Utils;
    using Models;
    using Network;
    using System.Text.RegularExpressions;

    public partial class ManageCourses : UserControl, IResetable
    {
        public ManageCourses()
        {
            InitializeComponent();
        }

        #region BUTTON CLICK METHODS
        /// <summary>
        /// What happens when the user clicks the Add Previous button. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPreviousCourseBtn_Click(object sender, EventArgs e)
        {
            string tmp = PreviousCourseIdTxt.Text.ToUpper();
            if (!Regex.IsMatch(tmp, @"[A-Z][A-Z][A-Z][A-Z]?-[0-9][0-9][0-9]L?"))
            {
                MessageBox.Show("Invalid course code");
                return;
            }

            HttpRequester.Default.AddPreviousCourse(tmp, CourseNameTxt.Text.ToLower(), (int)CreditsUpDown.Value, (SuccessResponse succ) =>
            {
                if (succ.errorCode != null)
                {
                    GeneralUtil.ShowError(succ);
                    return false;
                }

                if (succ.success == 1)
                {
                    MessageBox.Show("Successfully added previous course.");
                    List<PreviousCourse> pCourses = HttpRequester.Default.GetPreviousCourses();
                    PreviousCourseView.SetCourses(pCourses);
                }
                return true;
            });
        }

        /// <summary>
        /// What happens when the user clicks the View Enrolled button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewEnrolledBtn_Click(object sender, EventArgs e)
        {
            EnrolledCourseView.Show();
            PreviousCourseView.Hide();
            EnrolledCourseView.Refresh();
            PreviousCourseView.Refresh();
        }

        /// <summary>
        /// What happens when the user clicks the View Previous button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewPreviousBtn_Click(object sender, EventArgs e)
        {
            PreviousCourseView.Show();
            EnrolledCourseView.Hide();
            EnrolledCourseView.Refresh();
            PreviousCourseView.Refresh();
        }

        /// <summary>
        /// What happens when the previous course list. Forwards click to the PreviousCourseView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousCourseView_Click(object sender, EventArgs e)
        {
            PreviousCourseView.OnClickEvent(e);
        }
        
        /// <summary>
        /// What happens when the previous course list. Forwards click to the EnrolledCourseView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnrolledCourseView_Click(object sender, EventArgs e)
        {
            EnrolledCourseView.OnClickEvent(e);
        }

        #endregion

        /// <summary>
        /// Used to reset the view to the default state
        /// </summary>
        public void ResetToDefault()
        {
            EnrolledCourseView.Show();
            PreviousCourseView.Hide();

            List<Course> courses = HttpRequester.Default.GetEnrolledCourses();
            EnrolledCourseView.SetCourses(courses);

            List<PreviousCourse> pCourses = HttpRequester.Default.GetPreviousCourses();
            PreviousCourseView.SetCourses(pCourses);

            CourseNameTxt.Text = "";
            PreviousCourseIdTxt.Text = "";
            CreditsUpDown.Value = 3;
        }
    }
}
