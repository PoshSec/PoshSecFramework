using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;

namespace poshsecframework
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
            }
            catch (Exception e)
            { 
                //Safety Net
                //This is a global exception handler.
                MessageBox.Show("Unhandled Exception" + Environment.NewLine + e.Message + Environment.NewLine + "Stack Trace: " + Environment.NewLine + e.StackTrace, "Unhandled Exception. Program Will Halt.");
                Application.Exit();
            }
        }
    }
}