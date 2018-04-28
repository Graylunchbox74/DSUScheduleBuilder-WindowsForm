using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSUScheduleBuilder.Drawing
{
    using System.Drawing;
    using Models;

    public class PreviousCourseView : CourseList<PreviousCourse>
    {
        public PreviousCourseView() : base()
        {
            numbersPerPage = 10;
        }

        protected override void drawCourse(Graphics g, int number, PreviousCourse course)
        {
            g.FillRectangle(number % 2 == 0 ? Brushes.Blue : Brushes.Gold, 0, number * cellHeight, Size.Width, cellHeight);

            Font font = new Font(FontFamily.GenericSansSerif, 14);
            SizeF textSize;

            textSize = g.MeasureString(course.CourseID, font);

            g.DrawString(course.CourseID, font, Brushes.Black, 4, number * cellHeight + 4 + (textSize.Height + 4) * 0);
        }

        protected override void drawSelectedCourseExtra(Graphics g)
        {
        }

        protected override void HandleClick(int mx, int my)
        {
            base.CheckBottomBarClick(mx, my);
        }
    }
}
