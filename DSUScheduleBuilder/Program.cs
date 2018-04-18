using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DSUScheduleBuilder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Network.HttpRequester.getUser("test1");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}
