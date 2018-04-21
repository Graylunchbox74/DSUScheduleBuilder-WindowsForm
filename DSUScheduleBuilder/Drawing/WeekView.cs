using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using DSUScheduleBuilder.Models;

namespace DSUScheduleBuilder.Drawing {

    public class WeekView : Control {
        private List<CourseView> courses;

        public readonly int DayOfWeekHeight = 32;
        public readonly int TimeSlotHeight = 32;
        public readonly int TimeSlotWidth = 40;
        public int CellWidth
        {
            get
            {
                return (this.Size.Width - TimeSlotWidth) / 5;
            }
        }

        private static readonly String[] _days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

        public WeekView()
        {
        }

        public void SetCourses(List<Course> courses) {
            this.courses = courses.ConvertAll<CourseView>((Course c) =>
            {
                CourseView cv = new CourseView(this);
                cv.Course = c;
                return cv;
            });
        }

        protected override void OnPaint(PaintEventArgs e) {

            //Draw the days of the week at the top
            for (int i = 0; i < 5; i++) {
                _drawEmptyCell(e.Graphics, new Rectangle(i * CellWidth + TimeSlotWidth, 0, CellWidth, DayOfWeekHeight), 2.0f);

                Font drawFont = SystemFonts.DefaultFont;
                SizeF stringSize;
                stringSize = e.Graphics.MeasureString(_days[i], drawFont);

                float fx = i * CellWidth + (CellWidth - stringSize.Width) / 2.0f + TimeSlotWidth;
                float fy = (DayOfWeekHeight - stringSize.Height) / 2.0f;
                e.Graphics.DrawString(_days[i], drawFont, Brushes.Black, fx, fy);
            }

            //Draw the lines between times (horizontal)
            Pen linePen;
            for (int i = 0; i <= 15; i++) {
                linePen = new Pen(Brushes.Black, 1);

                float y = i * TimeSlotHeight + DayOfWeekHeight;
                e.Graphics.DrawLine(linePen, 0, y, this.Size.Width - 2, y);
            }

            for (int i = 0; i <= 14; i++) {
                string msg = (i + 8).ToString() + ":00";

                Font drawFont = SystemFonts.DefaultFont;
                SizeF stringSize;
                stringSize = e.Graphics.MeasureString(msg, drawFont);

                float fx = (TimeSlotWidth - stringSize.Width) / 2.0f;
                float fy = (TimeSlotHeight - stringSize.Height) / 2.0f;
                e.Graphics.DrawString(msg, SystemFonts.DefaultFont, Brushes.Black, fx, fy + i * TimeSlotHeight + DayOfWeekHeight);
            }

            if (courses != null)
            {
                foreach (CourseView c in courses)
                {
                    c.Draw(e.Graphics);
                }
            }


            //Draw the lines between the days (vertical)
            for (int i = 0; i < 6; i++)
            {
                linePen = new Pen(Brushes.Black, (i == 0 || i == 5) ? 2 : 4);

                float x = i * CellWidth + (i == 0 ? 1 : 0) + TimeSlotWidth;
                e.Graphics.DrawLine(linePen, x, DayOfWeekHeight, x, this.Size.Height);
            }
        }

        private void _drawEmptyCell(Graphics g, Rectangle r, float padding = 1.0f) {
            g.FillRectangle(Brushes.Black, r);
            g.FillRectangle(Brushes.Aqua, r.X + padding, r.Y + padding, r.Width - 2 * padding, r.Height - 2 * padding);
        }
    }
}
