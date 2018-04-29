using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DSUScheduleBuilder.Main_Menu
{
    using Network;
    using Models;

    public partial class UserSettings : UserControl, IResetable
    {
        public UserSettings()
        {
            InitializeComponent();
        }

        public void ResetToDefault()
        {
            CurrPasswordTxt.Text = "";
            PasswordTxt.Text = "";
            ConfirmPasswordTxt.Text = "";
        }

        private void UpdatePasswordBtn_Click(object sender, EventArgs e)
        {
            if (ConfirmPasswordTxt.Text != PasswordTxt.Text)
            {
                MessageBox.Show("Passwords should match", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            HttpRequester.Default.ChangePassword(CurrPasswordTxt.Text, PasswordTxt.Text, (SuccessResponse succ) =>
            {
                if (succ.errorCode != null)
                {
                    MessageBox.Show("Error " + succ.errorCode + " : " + succ.errorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (succ.success == 1)
                {
                    MessageBox.Show("Succesfully change password.", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                }

                ResetToDefault();
                return false;
            });
        }
    }
}
