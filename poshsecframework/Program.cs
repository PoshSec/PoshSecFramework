using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

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

            SynchronizationContext.SetSynchronizationContext(
                new WindowsFormsSynchronizationContext());
            var psf = new PoshSecFramework();
            psf.ExitRequested += Program_ExitRequested;
            Task programStart = psf.StartAsync();
            HandleAnyTaskExceptions(programStart);

            Application.Run();
        }

        private static async void HandleAnyTaskExceptions(Task task)
        {
            try
            {
                await Task.Yield();
                await task;
            }
            catch (Exception e)
            {
                MessageBox.Show("Unhandled Exception" + Environment.NewLine + e.Message + Environment.NewLine + "Stack Trace: " + Environment.NewLine + e.StackTrace, "Unhandled Exception. Program Will Halt.");
                Application.Exit();
            }
        }

        private static void Program_ExitRequested(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
    }
}