using poshsecframework.Strings;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace poshsecframework
{
    public class PoshSecFramework
    {
        private frmMain _mainForm;

        public PoshSecFramework()
        {
        }

        public event EventHandler<EventArgs> ExitRequested;

        private void _mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            OnExitRequested(EventArgs.Empty);
        }

        private void OnExitRequested(EventArgs e)
        {
            ExitRequested?.Invoke(this, e);
        }

        public async Task StartAsync()
        {
            _mainForm = new frmMain();
            _mainForm.FormClosed += _mainForm_FormClosed;

            await _mainForm.InitializeAsync();
            _mainForm.Show();
        }
    }

}
