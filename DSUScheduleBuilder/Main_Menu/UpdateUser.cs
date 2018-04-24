using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSUScheduleBuilder.Main_Menu
{
    public partial class UpdateUser : UserControl
    {
        public UpdateUser()
        {
            InitializeComponent();
        }

        private void EnrolledCourseView_Click(object sender, EventArgs e)
        {
            EnrolledCourseView.OnClickEvent(e);
        }
    }
}
