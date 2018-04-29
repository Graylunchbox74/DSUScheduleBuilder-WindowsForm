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
    using Drawing;
    using Models;
    using Network;
    using System.Text.RegularExpressions;

    public partial class ManageCourses : UserControl, IResetable
    {
        public ManageCourses()
        {
            InitializeComponent();
        }

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
                    MessageBox.Show("Error " + succ.errorCode + " : " + succ.errorMessage);
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

        private void ViewEnrolledBtn_Click(object sender, EventArgs e)
        {
            EnrolledCourseView.Show();
            PreviousCourseView.Hide();
            EnrolledCourseView.Refresh();
            PreviousCourseView.Refresh();
        }

        private void ViewPreviousBtn_Click(object sender, EventArgs e)
        {
            PreviousCourseView.Show();
            EnrolledCourseView.Hide();
            EnrolledCourseView.Refresh();
            PreviousCourseView.Refresh();
        }

        private void PreviousCourseView_Click(object sender, EventArgs e)
        {
            PreviousCourseView.OnClickEvent(e);
        }

        private void EnrolledCourseView_Click(object sender, EventArgs e)
        {
            EnrolledCourseView.OnClickEvent(e);
        }

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
