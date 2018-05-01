using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace DSUScheduleBuilder.Drawing
{
    using Tabs;
    using Models;
    using Network;
    using Utils;

    public class AvailableCourseView : CourseList<AvailableCourse>
    {
        /// <summary>
        /// Rectangle of the button to return to the list of courses
        /// </summary>
        private Rectangle returnButtonRect;
        /// <summary>
        /// Rectangle of the button to enroll in the selected course
        /// </summary>
        private Rectangle enrollButtonRect;

        public AvailableCourseView()
        {
        }

        /// <summary>
        /// Sets the current course list to be "cs" and sets up other variables needed later
        /// </summary>
        /// <param name="cs"></param>
        public override void SetCourses(List<AvailableCourse> cs)
        {
            base.SetCourses(cs);

            int bx;
            bx = (this.Size.Width / 2 - 96) / 2;
            returnButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight - 48, 96, 48);
            bx += this.Size.Width / 2;
            enrollButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight - 48, 96, 48);
        }

        /// <summary>
        /// Draws the base course information and the description of the course
        /// </summary>
        /// <param name="g"></param>
        protected override void drawSelectedCourseExtra(Graphics g)
        {
            string text = selectedCourse.Description;
            Font font = new Font(FontFamily.GenericSansSerif, 14);
            SizeF textSize = g.MeasureString(text, font);

            text = Converter.IntersperseNewLines(selectedCourse.Description);
            g.DrawString(text, font, Brushes.Black, 4, 4 + (textSize.Height + 4) * 4);

            //Draw other buttons
            g.FillRectangle(Brushes.DarkSlateGray, returnButtonRect);
            g.FillRectangle(Brushes.DarkSlateGray, enrollButtonRect);

            textSize = g.MeasureString("Back", font);
            g.DrawString("Back", font, Brushes.White
                , returnButtonRect.X + (returnButtonRect.Width - textSize.Width) / 2
                , returnButtonRect.Y + (returnButtonRect.Height - textSize.Height) / 2);

            textSize = g.MeasureString("Enroll", font);
            g.DrawString("Enroll", font, Brushes.White
                , enrollButtonRect.X + (enrollButtonRect.Width - textSize.Width) / 2
                , enrollButtonRect.Y + (enrollButtonRect.Height - textSize.Height) / 2);
        }

        #region CLICK METHODS

        /// <summary>
        /// The main handler for a click
        /// </summary>
        /// <param name="mx"></param>
        /// <param name="my"></param>
        protected override void HandleClick(int mx, int my)
        {
            switch(state)
            {
                case CourseListState.ClassList:
                    ClickClassList(mx, my);
                    break;
                case CourseListState.SpecificClass:
                    clickSpecificClass(mx, my);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Called when a click event happens on a selected course
        /// </summary>
        /// <param name="mx"></param>
        /// <param name="my"></param>
        private void clickSpecificClass(int mx, int my)
        {
            if (returnButtonRect.Contains(mx, my))
            {
                state = CourseListState.ClassList;
            }

            if (enrollButtonRect.Contains(mx, my))
            {
                HttpRequester.Default.EnrollInCourse(selectedCourse.Key, (enr) =>
                {
                    if (enr.errorCode != null)
                    {
                        GeneralUtil.ShowError(enr);
                        return false;
                    }
                    else
                    {
                        MessageBox.Show("Successfully enrolled in class.");

                        if (enr.classes?[0]?.required?.Count > 0)
                        {
                            DialogResult ans = MessageBox.Show("There are required courses that must be taken concurrently with this course: " + enr.classes?[0]?.required?.Aggregate<string>((a,b) => a + ", " + b));
                        }
                        else if (enr.classes?[0]?.recommended?.Count > 0)
                        {
                            DialogResult ans = MessageBox.Show("There are recommended courses that should be taken concurrently with this course: " + enr.classes?[0]?.recommended?.Aggregate<string>((a, b) => a + ", " + b));
                        }
                    }

                    return true;
                });
                state = CourseListState.ClassList;
            }
        }
        #endregion
    }
}
