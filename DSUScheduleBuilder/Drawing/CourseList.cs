using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace DSUScheduleBuilder.Drawing
{
    using Models;
    using Utils;

    public enum CourseListState
    {
        ClassList,
        SpecificClass
    }

    public abstract class CourseList<T> : Control where T : Course
    {
        protected readonly Brush courseColor1;
        protected readonly Brush courseColor2;

        protected List<T> courses;
        protected int numbersPerPage;
        protected int totalPages;
        protected int currPage;

        protected int cellWidth;
        protected int cellHeight = 1;
        protected int bottomBarHeight = 32;

        private Rectangle backButtonRect;
        private Rectangle forwardButtonRect;

        protected CourseListState state;
        protected T selectedCourse;

        public CourseList()
        {
            numbersPerPage = 5;
            courseColor1 = new SolidBrush(Color.FromArgb(255, 100, 181, 246));
            courseColor2 = new SolidBrush(Color.FromArgb(255, 255, 241, 118));
        }

        public virtual void SetCourses(List<T> cs)
        {
            this.courses = cs;
            if (courses == null)
            {
                this.totalPages = 0;
                this.currPage = 0;
                return;
            }
            this.totalPages = (this.courses.Count - 1) / numbersPerPage;
            this.currPage = 0;

            this.bottomBarHeight = 32;
            this.cellWidth = this.Size.Width;
            this.cellHeight = (this.Size.Height - bottomBarHeight) / numbersPerPage;
            if (cellHeight == 0) cellHeight = 1;

            int bx = (this.Size.Width / 2 - 32) / 2;
            backButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight, 32, 32);
            bx += this.Size.Width / 2;
            forwardButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight, 32, 32);
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int h = this.Size.Height;
            if (this.state == CourseListState.ClassList)
            {
                h -= bottomBarHeight;
            }
            e.Graphics.FillRectangle(Brushes.Black, 0, 0, this.Size.Width, h);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 64, 64, 64)), 2, 2, this.Size.Width - 4, h - 4);

            switch (this.state)
            {
                case CourseListState.ClassList:
                    drawClassList(e.Graphics);
                    break;
                case CourseListState.SpecificClass:
                    drawSpecificClass(e.Graphics);
                    break;
                default:
                    break;
            }
        }

        protected virtual void drawSpecificClass(Graphics g)
        {
            if (selectedCourse == null)
            {
                state = CourseListState.ClassList;
                return;
            }

            g.FillRectangle(courseColor2, 2, 2, this.Size.Width - 4, this.Size.Height - 4);

            Font font = new Font(FontFamily.GenericSansSerif, 14);
            string text = selectedCourse.CourseID;
            SizeF textSize = g.MeasureString(text, font);

            g.DrawString(selectedCourse.CourseID, font, Brushes.Black, 4, 4 + (textSize.Height + 4) * 0);
            g.DrawString(selectedCourse.CourseName, font, Brushes.Black, 4, 4 + (textSize.Height + 4) * 1);
            if (selectedCourse.Teacher != null)
                g.DrawString(selectedCourse.Teacher, font, Brushes.Black, 4, 4 + (textSize.Height + 4) * 2);

            text = selectedCourse.DaysOfWeekPresent;
            textSize = g.MeasureString(text, font);
            g.DrawString(text, font, Brushes.Black, cellWidth - textSize.Width - 4, 4 + (textSize.Height + 4) * 2);

            if (text != "Online")
            {
                text = Converter.TimeIntToString(selectedCourse.StartTime) + " - " + Converter.TimeIntToString(selectedCourse.EndTime);
                textSize = g.MeasureString(text, font);
                g.DrawString(text, font, Brushes.Black, cellWidth - textSize.Width - 4, 4 + (textSize.Height + 4) * 0);
            }

            drawSelectedCourseExtra(g);
        }

        protected abstract void drawSelectedCourseExtra(Graphics g);

        protected void drawClassList(Graphics g)
        {
            if (courses != null)
            {
                int t = courses.Count - currPage * numbersPerPage;
                int len = 5 < courses.Count ? (t < numbersPerPage ? t : numbersPerPage) : courses.Count;
                for (int i = 0; i < len; i++)
                {
                    drawCourse(g, i, courses[i + currPage * numbersPerPage]);
                }
            }

            if (totalPages > 0)
            {
                drawBottomBar(g);
            }
        }

        protected virtual void drawCourse(Graphics g, int number, T course)
        {
            g.FillRectangle(number % 2 == 0 ? courseColor1 : courseColor2, 2, number * cellHeight + 2, Size.Width - 4, cellHeight - 4);

            Font font = new Font(FontFamily.GenericSansSerif, 14);
            SizeF textSize;

            textSize = g.MeasureString(course.CourseID, font);

            g.DrawString(course.CourseID, font, Brushes.Black, 4, number * cellHeight + 4 + (textSize.Height + 4) * 0);
            g.DrawString(course.CourseName, font, Brushes.Black, 4, number * cellHeight + 4 + (textSize.Height + 4) * 1);
            if (course.Teacher != null)
                g.DrawString(course.Teacher, font, Brushes.Black, 4, number * cellHeight + 4 + (textSize.Height + 4) * 2);

            string text = course.DaysOfWeekPresent;
            textSize = g.MeasureString(text, font);
            g.DrawString(text, font, Brushes.Black, cellWidth - textSize.Width - 4, number * cellHeight + 4 + (textSize.Height + 4) * 2);

            //Don't draw times for online courses
            if (text != "Online")
            {
                text = Converter.TimeIntToString(course.StartTime) + " - " + Converter.TimeIntToString(course.EndTime);
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


        protected abstract void HandleClick(int mx, int my);
        public void OnClickEvent(EventArgs e)
        {
            int sx = PointToScreen(Point.Empty).X;
            int sy = PointToScreen(Point.Empty).Y;

            int mx = MousePosition.X - sx;
            int my = MousePosition.Y - sy;
            HandleClick(mx, my);
            this.Refresh();
        }

        protected bool CheckBottomBarClick(int mx, int my)
        {
            if (courses == null) return true;
            if (backButtonRect.Contains(mx, my))
            {
                currPage -= 1;
                if (currPage < 0) currPage = 0;
                return true;
            }

            if (forwardButtonRect.Contains(mx, my))
            {
                currPage += 1;
                if (currPage > totalPages) currPage = totalPages;
                return true;
            }
            return false;
        }

        protected void ClickClassList(int mx, int my)
        {
            if (!CheckBottomBarClick(mx, my))
            {
                if (my < this.Size.Height - this.bottomBarHeight)
                {
                    int index = (my / cellHeight) + currPage * numbersPerPage;
                    if (index >= courses.Count) return;

                    selectedCourse = courses?[index];
                    if (selectedCourse != null)
                        state = CourseListState.SpecificClass;
                }
            }
        }
    }

}
