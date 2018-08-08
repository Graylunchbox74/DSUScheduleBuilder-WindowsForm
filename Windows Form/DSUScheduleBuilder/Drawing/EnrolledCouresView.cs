using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSUScheduleBuilder.Drawing
{
    using System.Drawing;
    using DSUScheduleBuilder.Utils;
    using Models;
    using Network;

    public class EnrolledCouresView : CourseList<Course>
    {
        /// <summary>
        /// Rectangle of the button to return to the list of courses
        /// </summary>
        private Rectangle returnButtonRect;
        /// <summary>
        /// Rectangle of the button to drop the selected course
        /// </summary>
        private Rectangle dropButtonRect;

        public EnrolledCouresView() : base()
        {
        }
        /// <summary>
        /// Sets the current course list to be "cs" and sets up other variables needed later
        /// </summary>
        /// <param name="cs"></param>
        public override void SetCourses(List<Course> cs)
        {
            base.SetCourses(cs);

            int bx;
            bx = (this.Size.Width / 2 - 96) / 2;
            returnButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight - 48, 96, 48);
            bx += this.Size.Width / 2;
            dropButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight - 48, 96, 48);
        }

        /// <summary>
        /// Draws the base course information and the description of the course
        /// </summary>
        /// <param name="g"></param>
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
            
            textSize = g.MeasureString("Drop", font);
            g.DrawString("Drop", font, Brushes.White
                , dropButtonRect.X + (dropButtonRect.Width - textSize.Width) / 2
                , dropButtonRect.Y + (dropButtonRect.Height - textSize.Height) / 2);
        }

        #region CLICK METHODS
        /// <summary>
        /// The main handler for a click
        /// </summary>
        /// <param name="mx"></param>
        /// <param name="my"></param>
        protected override void HandleClick(int mx, int my)
        {
            switch (state)
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
                HttpRequester.Default.DropCourse(selectedCourse.Key, (succ) =>
                {
                    if (succ.errorCode != null)
                    {
                        GeneralUtil.ShowError(succ);
                        return false;
                    }

                    if (succ?.success == 1)
                    {
                        MessageBox.Show("Successfully dropped course.");
                    }

                    return true;
                });

                List<Course> courses = HttpRequester.Default.GetEnrolledCourses();
                SetCourses(courses);
                state = CourseListState.ClassList;
            }
        }
        #endregion
    }
}
