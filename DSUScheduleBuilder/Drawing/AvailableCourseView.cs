using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace DSUScheduleBuilder.Drawing
{
    using Main_Menu;
    using Models;
    using Network;
    using Utils;

    enum AvailableCourseViewState
    {
        ClassList,
        SpecificClass
    }

    public class AvailableCourseView : Control
    {
        private List<AvailableCourse> courses;
        private int totalPages;
        private int currPage;

        private int cellWidth;
        private int cellHeight = 1;
        private int bottomBarHeight;

        private Rectangle backButtonRect;
        private Rectangle forwardButtonRect;
        private Rectangle returnButtonRect;
        private Rectangle enrollButtonRect;

        private AvailableCourseViewState state;
        private AvailableCourse selectedCourse;

        public AvailableCourseView()
        {
            state = AvailableCourseViewState.ClassList;
        }

        public void SetCourses(List<AvailableCourse> cs)
        {
            this.courses = cs;
            this.totalPages = (this.courses.Count - 1) / 5;
            this.currPage = 0;

            this.bottomBarHeight = 32;
            this.cellWidth = this.Size.Width;
            this.cellHeight = (this.Size.Height - bottomBarHeight) / 5;
            if (cellHeight == 0) cellHeight = 1;

            int bx = (this.Size.Width / 2 - 32) / 2;
            backButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight, 32, 32);
            bx += this.Size.Width / 2;
            forwardButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight, 32, 32);

            bx = (this.Size.Width / 2 - 96) / 2;
            returnButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight - 48, 96, 48);
            bx += this.Size.Width / 2;
            enrollButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight - 48, 96, 48);
        }

        #region Paint Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            switch (this.state)
            {
                case AvailableCourseViewState.ClassList:
                    drawClassList(e.Graphics);
                    break;
                case AvailableCourseViewState.SpecificClass:
                    drawSpecificClass(e.Graphics);
                    break;
                default:
                    break;
            }
        }

        private void drawClassList(Graphics g)
        {
            if (courses != null)
            {
                int t = courses.Count - currPage * 5;
                int len = 5 < courses.Count ? (t < 5 ? t : 5) : courses.Count;
                for (int i = 0; i < len; i++)
                {
                    drawCourse(g, i, courses[i + currPage * 5]);
                }
            }

            if (totalPages > 0)
            {
                drawBottomBar(g);
            }
        }

        private void drawSpecificClass(Graphics g)
        {
            if (selectedCourse == null)
            {
                state = AvailableCourseViewState.ClassList;
                return;
            }

            g.FillRectangle(Brushes.Aqua, 0, 0, this.Size.Width, this.Size.Height);

            Font font = new Font(FontFamily.GenericSansSerif, 14);
            string text = selectedCourse.CourseID;
            SizeF textSize = g.MeasureString(text, font);

            g.DrawString(selectedCourse.CourseID, font, Brushes.Black, 4, 4 + (textSize.Height + 4) * 0);
            g.DrawString(selectedCourse.CourseName, font, Brushes.Black, 4, 4 + (textSize.Height + 4) * 1);
            if (selectedCourse.Teacher != null && selectedCourse.Teacher.Count > 0)
                g.DrawString(selectedCourse.Teacher[0], font, Brushes.Black, 4, 4 + (textSize.Height + 4) * 2);

            text = selectedCourse.DaysOfWeekPresent;
            textSize = g.MeasureString(text, font);
            g.DrawString(text, font, Brushes.Black, cellWidth - textSize.Width - 4, 4 + (textSize.Height + 4) * 2);

            if (text != "Online")
            {
                text = Converter.TimeIntToString(selectedCourse.TimeStart) + " - " + Converter.TimeIntToString(selectedCourse.TimeEnd);
                textSize = g.MeasureString(text, font);
                g.DrawString(text, font, Brushes.Black, cellWidth - textSize.Width - 4, 4 + (textSize.Height + 4) * 0);
            }

            text = Converter.IntersperseNewLines(selectedCourse.Description);
            g.DrawString(text, font, Brushes.Black, 4, 4 + (textSize.Height + 4) * 4);

            //Draw back button
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

        private void drawCourse(Graphics g, int number, AvailableCourse course)
        {
            g.FillRectangle(number % 2 == 0 ? Brushes.Aqua : Brushes.Aquamarine, 0, number * cellHeight, Size.Width, cellHeight);

            Font font = new Font(FontFamily.GenericSansSerif, 14);
            SizeF textSize;

            textSize = g.MeasureString(course.CourseID, font);

            g.DrawString(course.CourseID, font, Brushes.Black, 4, number * cellHeight + 4 + (textSize.Height + 4) * 0);
            g.DrawString(course.CourseName, font, Brushes.Black, 4, number * cellHeight + 4 + (textSize.Height + 4) * 1);
            if (course.Teacher != null && course.Teacher.Count > 0)
                g.DrawString(course.Teacher[0], font, Brushes.Black, 4, number * cellHeight + 4 + (textSize.Height + 4) * 2);

            string text = course.DaysOfWeekPresent;
            textSize = g.MeasureString(text, font);
            g.DrawString(text, font, Brushes.Black, cellWidth - textSize.Width - 4, number * cellHeight + 4 + (textSize.Height + 4) * 2);

            //Don't draw times for online courses
            if (text != "Online")
            {
                text = Converter.TimeIntToString(course.TimeStart) + " - " + Converter.TimeIntToString(course.TimeEnd);
                textSize = g.MeasureString(text, font);
                g.DrawString(text, font, Brushes.Black, cellWidth - textSize.Width - 4, number * cellHeight + 4 + (textSize.Height + 4) * 0);
            }
        }

        private void drawBottomBar(Graphics g)
        {
            int topY = this.Size.Height - bottomBarHeight;

            Font font = new Font(FontFamily.GenericSansSerif, 12);
            string text = (currPage + 1) + " of " + (totalPages + 1) + " pages";
            SizeF textSize = g.MeasureString(text, font);

            g.DrawString(text, font, Brushes.White, (this.Size.Width - textSize.Width) / 2.0f, topY + 2);

            font = new Font(FontFamily.GenericMonospace, 24);
            g.FillRectangle(Brushes.Aquamarine, backButtonRect);
            g.DrawString("<", font, Brushes.Black, backButtonRect.X, backButtonRect.Y);
            g.FillRectangle(Brushes.Aquamarine, forwardButtonRect);
            g.DrawString(">", font, Brushes.Black, forwardButtonRect.X, forwardButtonRect.Y);
        }
        #endregion

        #region Click Methods
        public void OnClick(EventArgs e)
        {
            int sx = PointToScreen(Point.Empty).X;
            int sy = PointToScreen(Point.Empty).Y;

            int mx = MousePosition.X - sx;
            int my = MousePosition.Y - sy;

            switch(state)
            {
                case AvailableCourseViewState.ClassList:
                    clickClassList(mx, my);
                    break;
                case AvailableCourseViewState.SpecificClass:
                    clickSpecificClass(mx, my);
                    break;
                default:
                    break;
            }
            
            this.Refresh();
        }

        private void clickClassList(int mx, int my)
        {
            if (courses == null) return;
            if (backButtonRect.Contains(mx, my))
            {
                this.currPage -= 1;
                if (currPage < 0) currPage = 0;
            }

            if (forwardButtonRect.Contains(mx, my))
            {
                this.currPage += 1;
                if (currPage > totalPages) currPage = totalPages;
            }

            if (my < this.Size.Height - this.bottomBarHeight)
            {
                int index = (my / cellHeight) + currPage * 5;
                selectedCourse = courses[index];
                state = AvailableCourseViewState.SpecificClass;
            }
        }

        private void clickSpecificClass(int mx, int my)
        {
            if (returnButtonRect.Contains(mx, my))
            {
                state = AvailableCourseViewState.ClassList;
            }

            if (enrollButtonRect.Contains(mx, my))
            {
                HttpRequester.Default.EnrollInCourse(selectedCourse.Key, (succ) =>
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

                    if (succ.success != 1)
                    {
                        MessageBox.Show("Enrolling in class failed.");
                        return false;
                    }

                    if (succ.success == 1)
                    {
                        MessageBox.Show("Successfully enrolled in class.");
                    }

                    return true;
                });
            }
        }
        #endregion
    }
}
