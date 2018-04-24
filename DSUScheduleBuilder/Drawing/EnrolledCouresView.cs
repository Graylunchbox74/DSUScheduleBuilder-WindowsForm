using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSUScheduleBuilder.Drawing
{
    using System.Drawing;
    using Models;
    using Network;

    public class EnrolledCouresView : CourseList<Course>
    {
        private Rectangle returnButtonRect;
        private Rectangle dropButtonRect;

        public EnrolledCouresView() : base()
        {
        }

        public override void SetCourses(List<Course> cs)
        {
            base.SetCourses(cs);

            int bx;
            bx = (this.Size.Width / 2 - 96) / 2;
            returnButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight - 48, 96, 48);
            bx += this.Size.Width / 2;
            dropButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight - 48, 96, 48);
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
            
            textSize = g.MeasureString("Drop", font);
            g.DrawString("Drop", font, Brushes.White
                , dropButtonRect.X + (dropButtonRect.Width - textSize.Width) / 2
                , dropButtonRect.Y + (dropButtonRect.Height - textSize.Height) / 2);
        }

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
                        switch(succ.errorCode)
                        {
                            default:
                                MessageBox.Show("Error " + succ.errorCode + " : " + succ.errorMessage);
                                break;
                        }

                        return false;
                    }

                    if (succ?.success == 1)
                    {
                        MessageBox.Show("Successfully dropped course");
                    }

                    return true;
                });
            }
        }
    }
}
