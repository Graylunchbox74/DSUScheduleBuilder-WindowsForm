using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSUScheduleBuilder.Drawing
{
    using System.Drawing;
    using System.Windows.Forms;
    using Utils;
    using Models;
    using Network;

    public class PreviousCourseView : CourseList<PreviousCourse>
    {
        /// <summary>
        /// Rectangle of the button to return to the list of courses
        /// </summary>
        private Rectangle returnButtonRect;

        /// <summary>
        /// Rectangle of the button to drop the selected course
        /// </summary>
        private Rectangle dropButtonRect;

        public PreviousCourseView() : base()
        {
            numbersPerPage = 10;
        }

        /// <summary>
        /// Sets the current course list to be "cs" and sets up other variables needed later
        /// </summary>
        /// <param name="cs"></param>
        public override void SetCourses(List<PreviousCourse> cs)
        {
            base.SetCourses(cs);

            int bx;
            bx = (this.Size.Width / 2 - 96) / 2;
            returnButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight - 48, 96, 48);
            bx += this.Size.Width / 2;
            dropButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight - 48, 96, 48);
        }

        #region DRAW METHODS
        protected override void drawCourse(Graphics g, int number, PreviousCourse course)
        {
            g.FillRectangle(number % 2 == 0 ? courseColor1 : courseColor2, 2, number * cellHeight + 2, Size.Width - 4, cellHeight - 4);

            Font font = new Font(FontFamily.GenericSansSerif, 14);
            SizeF textSize;

            textSize = g.MeasureString(course.CourseID, font);
            g.DrawString(course.CourseID, font, Brushes.Black, 4, number * cellHeight + 4 + (textSize.Height + 4) * 0);

            string s = "Credits : " + course.Credits; 
            textSize = g.MeasureString(s, font);
            g.DrawString(s, font, Brushes.Black, Size.Width - 6 - textSize.Width, number * cellHeight + 4);

        }

        protected override void drawSpecificClass(Graphics g)
        {
            if (selectedCourse == null)
            {
                state = CourseListState.ClassList;
                return;
            }

            g.FillRectangle(courseColor2, 0, 0, this.Size.Width, this.Size.Height);

            Font font = new Font(FontFamily.GenericSansSerif, 14);
            string text = selectedCourse.CourseID;
            SizeF textSize = g.MeasureString(text, font);

            g.DrawString(text, font, Brushes.Black, (this.Size.Width - textSize.Width) / 2, 4 + (textSize.Height + 4) * 1);

            text = selectedCourse.CourseName;
            textSize = g.MeasureString(text, font);
            g.DrawString(text, font, Brushes.Black, (this.Size.Width - textSize.Width) / 2, 4 + (textSize.Height + 4) * 3);

            drawSelectedCourseExtra(g);
        }

        protected override void drawSelectedCourseExtra(Graphics g)
        {
            Font font = new Font(FontFamily.GenericSansSerif, 14);
            SizeF textSize;

            g.FillRectangle(Brushes.DarkSlateGray, returnButtonRect);
            g.FillRectangle(Brushes.DarkSlateGray, dropButtonRect);

            textSize = g.MeasureString("Back", font);
            g.DrawString("Back", font, Brushes.White
                , returnButtonRect.X + (returnButtonRect.Width - textSize.Width) / 2
                , returnButtonRect.Y + (returnButtonRect.Height - textSize.Height) / 2);

            textSize = g.MeasureString("Remove", font);
            g.DrawString("Remove", font, Brushes.White
                , dropButtonRect.X + (dropButtonRect.Width - textSize.Width) / 2
                , dropButtonRect.Y + (dropButtonRect.Height - textSize.Height) / 2);
        }

        #endregion


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
                    ClickSpecificClass(mx, my);
                    break;
            }
        }

        /// <summary>
        /// Called when a click event happens on a selected course
        /// </summary>
        /// <param name="mx"></param>
        /// <param name="my"></param>
        private void ClickSpecificClass(int mx, int my)
        {
            if (returnButtonRect.Contains(mx, my))
            {
                state = CourseListState.ClassList;
            }

            if (dropButtonRect.Contains(mx, my))
            {
                HttpRequester.Default.DeletePreviousCourse(selectedCourse.CourseID, (succ) =>
                {
                    if (succ.errorCode != null)
                    {
                        GeneralUtil.ShowError(succ);
                        return false;
                    }

                    if (succ?.success == 1)
                    {
                        MessageBox.Show("Successfully removed course from list.");
                    }

                    return true;
                });

                List<PreviousCourse> courses = HttpRequester.Default.GetPreviousCourses();
                SetCourses(courses);
                state = CourseListState.ClassList;
            }
        }

        #endregion
    }
}
