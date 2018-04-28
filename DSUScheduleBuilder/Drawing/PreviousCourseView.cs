using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSUScheduleBuilder.Drawing
{
    using System.Drawing;
    using System.Windows.Forms;
    using Models;
    using Network;

    public class PreviousCourseView : CourseList<PreviousCourse>
    {
        private Rectangle returnButtonRect;
        private Rectangle dropButtonRect;

        public PreviousCourseView() : base()
        {
            numbersPerPage = 10;
        }

        public override void SetCourses(List<PreviousCourse> cs)
        {
            base.SetCourses(cs);

            int bx;
            bx = (this.Size.Width / 2 - 96) / 2;
            returnButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight - 48, 96, 48);
            bx += this.Size.Width / 2;
            dropButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight - 48, 96, 48);
        }

        protected override void drawCourse(Graphics g, int number, PreviousCourse course)
        {
            g.FillRectangle(number % 2 == 0 ? Brushes.Blue : Brushes.Gold, 0, number * cellHeight, Size.Width, cellHeight);

            Font font = new Font(FontFamily.GenericSansSerif, 14);
            SizeF textSize;

            textSize = g.MeasureString(course.CourseID, font);

            g.DrawString(course.CourseID, font, Brushes.Black, 4, number * cellHeight + 4 + (textSize.Height + 4) * 0);
        }

        protected override void drawSpecificClass(Graphics g)
        {
            if (selectedCourse == null)
            {
                state = CourseListState.ClassList;
                return;
            }

            g.FillRectangle(Brushes.Gold, 0, 0, this.Size.Width, this.Size.Height);

            Font font = new Font(FontFamily.GenericSansSerif, 14);
            string text = selectedCourse.CourseID;
            SizeF textSize = g.MeasureString(text, font);

            g.DrawString(selectedCourse.CourseID, font, Brushes.Black, (this.Size.Width - textSize.Width) / 2, 4 + (textSize.Height + 4) * 1);

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
                        switch (succ.errorCode)
                        {
                            default:
                                MessageBox.Show("Error " + succ.errorCode + " : " + succ.errorMessage);
                                break;
                        }

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
    }
}
