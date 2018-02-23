using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DSUScheduleBuilder {
    public class WeekView : Control {
        public WeekView() {

        }

        protected override void OnPaint(PaintEventArgs e) {
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(0, 0, 100, 100));
            e.Graphics.DrawString("Hello world!", SystemFonts.MessageBoxFont, Brushes.White, 0, 0);
        }
    }
}
