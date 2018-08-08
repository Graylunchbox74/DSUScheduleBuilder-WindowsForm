using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using DSUScheduleBuilder.Models;

namespace DSUScheduleBuilder.Drawing
{
    using Tabs;
    using Utils;
    using Network;
    
    public class WeekView : Control, IResetable
    {
        /// <summary>
        /// Internal randomizer for the course colors
        /// </summary>
        private static Random courseColorRandomizer;

        /// <summary>
        /// The list of courses to display
        /// </summary>
        private List<WeekCourseView> courses;

        /// <summary>
        /// How tall the top row of days of week is
        /// </summary>
        public readonly int DayOfWeekHeight = 32;
        /// <summary>
        /// How tall each time slot is
        /// </summary>
        public readonly int TimeSlotHeight = 32;
        /// <summary>
        /// How wide the leftmost column is
        /// </summary>
        public readonly int TimeSlotWidth = 40;

        /// <summary>
        /// How wide each cell is. Calculated from size and TimeSlotWidth
        /// </summary>
        public int CellWidth
        {
            get
            {
                return (this.Size.Width - TimeSlotWidth) / 5;
            }
        }

        /// <summary>
        /// List of the days of the week from 
        /// </summary>
        private static readonly String[] _days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

        public WeekView()
        {
        }
        
        /// <summary>
        /// Sets the currentl list of displayed courses. Converts from Course to WeekCouresView
        /// </summary>
        /// <param name="courses"></param>
        public void SetCourses(List<Course> courses)
        {
            this.courses = courses?.ConvertAll<WeekCourseView>((Course c) =>
            {
                WeekCourseView cv = new WeekCourseView(this, courseColorRandomizer);
                cv.Course = c;
                return cv;
            });
        }

        #region DRAW METHODS
        /// <summary>
        /// Main entry point of drawing. Called when the window wants to draw the WeekView
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //Draw the days of the week at the top
            for (int i = 0; i < 5; i++)
            {
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
            for (int i = 0; i <= 15; i++)
            {
                linePen = new Pen(Brushes.Black, 1);

                float y = i * TimeSlotHeight + DayOfWeekHeight;
                e.Graphics.DrawLine(linePen, 0, y, this.Size.Width - 2, y);
            }

            for (int i = 0; i <= 14; i++)
            {
                string msg = (i + 8).ToString() + ":00";

                Font drawFont = SystemFonts.DefaultFont;
                SizeF stringSize;
                stringSize = e.Graphics.MeasureString(msg, drawFont);

                float fx = (TimeSlotWidth - stringSize.Width) / 2.0f;
                float fy = (TimeSlotHeight - stringSize.Height) / 2.0f;
                e.Graphics.DrawString(msg, SystemFonts.DefaultFont, Brushes.White, fx, fy + i * TimeSlotHeight + DayOfWeekHeight);
            }

            if (courses != null)
            {
                foreach (WeekCourseView c in courses)
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

        /// <summary>
        /// How to draw a blank and empty cell
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        /// <param name="padding"></param>
        private void _drawEmptyCell(Graphics g, Rectangle r, float padding = 1.0f)
        {
            g.FillRectangle(Brushes.Black, r);
            g.FillRectangle(Brushes.Aqua, r.X + padding, r.Y + padding, r.Width - 2 * padding, r.Height - 2 * padding);
        }

        #endregion
        
        /// <summary>
        /// Used to return the WeekView to its default state
        /// </summary>
        public void ResetToDefault()
        {
            courseColorRandomizer = new Random(0xB00B5);
            List<Course> courses = HttpRequester.Default.GetEnrolledCourses();
            SetCourses(courses);
        }
    }
}
