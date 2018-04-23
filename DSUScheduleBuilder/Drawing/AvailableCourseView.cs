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

    public class AvailableCourseView : CourseList<AvailableCourse>
    {
        private Rectangle returnButtonRect;
        private Rectangle enrollButtonRect;

        public AvailableCourseView()
        {
        }

        public override void SetCourses(List<AvailableCourse> cs)
        {
            base.SetCourses(cs);

            int bx;
            bx = (this.Size.Width / 2 - 96) / 2;
            returnButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight - 48, 96, 48);
            bx += this.Size.Width / 2;
            enrollButtonRect = new Rectangle(bx, this.Size.Height - this.bottomBarHeight - 48, 96, 48);
        }

        protected override void drawSelectedCourseExtra(Graphics g)
        {
            string text = selectedCourse.Description;
            Font font = new Font(FontFamily.GenericSansSerif, 14);
            SizeF textSize = g.MeasureString(text, font);

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

        #region Click Methods
        protected override void HandleClick(int mx, int my)
        {
            switch(state)
            {
                case CourseListState.ClassList:
                    clickClassList(mx, my);
                    break;
                case CourseListState.SpecificClass:
                    clickSpecificClass(mx, my);
                    break;
                default:
                    break;
            }
            
            this.Refresh();
        }

        private void clickClassList(int mx, int my)
        {
            if (!CheckBottomBarClick(mx, my))
            {
                if (my < this.Size.Height - this.bottomBarHeight)
                {
                    int index = (my / cellHeight) + currPage * 5;
                    selectedCourse = courses[index];
                    state = CourseListState.SpecificClass;
                }
            }
        }

        private void clickSpecificClass(int mx, int my)
        {
            if (returnButtonRect.Contains(mx, my))
            {
                state = CourseListState.ClassList;
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
