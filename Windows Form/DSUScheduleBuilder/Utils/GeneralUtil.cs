using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSUScheduleBuilder.Utils
{
    using Network;

    public static class GeneralUtil
    {
        public static void ShowError(Errorable e)
        {
            MessageBox.Show("Error " + e.errorCode + " : " + e.errorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
