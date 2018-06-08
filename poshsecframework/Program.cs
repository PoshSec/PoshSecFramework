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
            SetDefaultWebProxy();

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

        public static void SetDefaultWebProxy()
        {
            var proxyPreference = Properties.Settings.Default.ProxyPreference;
            switch (proxyPreference)
            {
                case ProxyPreference.System:
                    WebRequest.DefaultWebProxy = WebRequest.GetSystemWebProxy();
                    break;
                case ProxyPreference.None:
                    WebRequest.DefaultWebProxy = null;
                    break;
                case ProxyPreference.Manual:
                    var host = Properties.Settings.Default.ProxyHost;
                    var port = Properties.Settings.Default.ProxyPort;
                    if (!string.IsNullOrWhiteSpace(host) && port > 0)
                        WebRequest.DefaultWebProxy = new WebProxy(host, port);
                    else if (!string.IsNullOrWhiteSpace(host))
                        WebRequest.DefaultWebProxy = new WebProxy(host);
                    else
                        WebRequest.DefaultWebProxy = new WebProxy();
                    break;
                default:
                    break;
            }
        }
    }
}