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

    public class AvailableCourseView : Control
    {
        private List<AvailableCourse> courses;
        private int totalPages;
        private int currPage;

        private int cellWidth;
        private int cellHeight;
        private int bottomBarHeight;

        private Rectangle backButtonRect;
        private Rectangle forwardButtonRect;


        public AvailableCourseView()
        {
        }

        public void SetCourses(List<AvailableCourse> cs)
        {
            this.courses = cs;
            this.totalPages = (this.courses.Count - 1) / 5;
            this.currPage = 0;

            this.bottomBarHeight = 32;
            this.cellWidth = this.Size.Width;
            this.cellHeight = (this.Size.Height - bottomBarHeight) / 5;

            int bx = (this.Size.Width / 2 - 32) / 2;
            backButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight, 32, 32);
            bx += this.Size.Width / 2;
            forwardButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight, 32, 32);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (courses != null)
            {
                int t = courses.Count - currPage * 5;
                int len = 5 < courses.Count ? (t < 5 ? t : 5) : courses.Count;
                for (int i = 0; i < len; i++)
                {
                    drawCourse(e.Graphics, i, courses[i + currPage * 5]);
                }
            }

            if (totalPages > 0)
            {
                drawBottomBar(e.Graphics);
            }
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
            
            string text = course.DaysOfWeek;
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

        public void OnClick(EventArgs e)
        {
            int sx = PointToScreen(Point.Empty).X;
            int sy = PointToScreen(Point.Empty).Y;

            int mx = MousePosition.X - sx;
            int my = MousePosition.Y - sy;
            Console.WriteLine(mx);
            Console.WriteLine(my);

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

            this.Refresh();
        }
    }
}
