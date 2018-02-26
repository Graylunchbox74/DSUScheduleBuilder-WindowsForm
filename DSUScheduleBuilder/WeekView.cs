using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DSUScheduleBuilder {
    public class WeekView : Control {
        private static readonly String[] _days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

        public WeekView() {

        }

        protected override void OnPaint(PaintEventArgs e) {

            int dowHeight = 32;
            int timeSlotHeight = 32;
            int timeSlotWidth = 40;
            var cellWidth = (int)(this.Size.Width - timeSlotWidth) / 5;

            //Draw the days of the week at the top
            for (int i = 0; i < 5; i++) {
                _drawEmptyCell(e.Graphics, new Rectangle(i * cellWidth + timeSlotWidth, 0, cellWidth, dowHeight), 2.0f);

                Font drawFont = SystemFonts.DefaultFont;
                SizeF stringSize;
                stringSize = e.Graphics.MeasureString(_days[i], drawFont);

                float fx = i * cellWidth + (cellWidth - stringSize.Width) / 2.0f + timeSlotWidth;
                float fy = (dowHeight - stringSize.Height) / 2.0f;
                e.Graphics.DrawString(_days[i], drawFont, Brushes.Black, fx, fy);
            }

            //Draw the lines between the days (vertical)
            Pen linePen;
            for (int i = 0; i < 6; i++) {
                linePen = new Pen(Brushes.Black, (i == 0 || i == 5) ? 2 : 4);

                float x = i * cellWidth + (i == 0 ? 1 : 0) + timeSlotWidth;
                e.Graphics.DrawLine(linePen, x, dowHeight, x, this.Size.Height);
            }

            //Draw the lines between times (horizontal)
            for (int i = 0; i <= 15; i++) {
                linePen = new Pen(Brushes.Black, 1);

                float y = i * timeSlotHeight + dowHeight;
                e.Graphics.DrawLine(linePen, 0, y, this.Size.Width, y);
            }

            for (int i = 0; i <= 14; i++) {
                string msg = (i + 8).ToString() + ":00";

                Font drawFont = SystemFonts.DefaultFont;
                SizeF stringSize;
                stringSize = e.Graphics.MeasureString(msg, drawFont);

                float fx = (timeSlotWidth - stringSize.Width) / 2.0f;
                float fy = (timeSlotHeight - stringSize.Height) / 2.0f;
                e.Graphics.DrawString(msg, SystemFonts.DefaultFont, Brushes.Black, fx, fy + i * timeSlotHeight + dowHeight);
            }
        }

        private void _drawEmptyCell(Graphics g, Rectangle r, float padding = 1.0f) {
            g.FillRectangle(Brushes.Black, r);
            g.FillRectangle(Brushes.Aqua, r.X + padding, r.Y + padding, r.Width - 2 * padding, r.Height - 2 * padding);
        }
    }
}
