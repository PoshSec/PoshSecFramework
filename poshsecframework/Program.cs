using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;

namespace poshsecframework
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var program = new PoshSecFramework();
            program.ExitRequested += Program_ExitRequested;
            program.Start();

            Application.Run();
        }

        private static void Program_ExitRequested(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
    }
}