using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DSUScheduleBuilder
{
    using Network;
    using Models;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new HttpRequester("http://208.107.52.130:4200");
            //new HttpRequester("http://localhost:4200");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
