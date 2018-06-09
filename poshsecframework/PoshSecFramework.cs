using poshsecframework.Strings;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using poshsecframework.Interface;

namespace poshsecframework
{
    public class PoshSecFramework
    {
        private frmMain _mainForm;
        private SplashScreen _splashScreen;

        public event EventHandler<EventArgs> ExitRequested;

        public PoshSecFramework()
        {
        }

        private void _mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            OnExitRequested(EventArgs.Empty);
        }

        private void OnExitRequested(EventArgs e)
        {
            ExitRequested?.Invoke(this, e);

            if (_splashScreen != null)
            {
                _splashScreen.Close();
                _splashScreen.Dispose();
                _splashScreen = null;
            }
        }

        public async Task StartAsync()
        {

            _splashScreen = new SplashScreen();

            _mainForm = new frmMain();
            _mainForm.FormClosed += _mainForm_FormClosed;
            _mainForm.StatusChange += MainFormStatusChange;
            _mainForm.ShowSplashScreen += MainFormShowSplashScreen;
            _mainForm.HideSplashScreen += MainFormHideSplashScreen;

            await _mainForm.InitializeAsync();

            _mainForm.Show();
        }

        private void MainFormShowSplashScreen(object sender, EventArgs e)
        {
            if (!_splashScreen.Visible)
                _splashScreen.Show(_mainForm);
        }

        private void MainFormStatusChange(object sender, StatusChangeEventArgs e)
        {
            _splashScreen?.SetStatus(e.Message);
        }

        private void MainFormHideSplashScreen(object sender, EventArgs e)
        {
            if (_splashScreen.Visible)
                _splashScreen.Hide();
        }
    }

}
