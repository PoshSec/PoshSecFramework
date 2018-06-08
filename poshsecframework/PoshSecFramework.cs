using System;
using System.Windows.Forms;

namespace poshsecframework
{
    public class PoshSecFramework
    {
        private readonly frmMain _mainForm;

        public PoshSecFramework()
        {
            _mainForm = new frmMain();
            _mainForm.FormClosed += _mainForm_FormClosed;
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

        public void Start()
        {
            try
            {
                _mainForm.Show();
            }
            catch (Exception e)
            {
                MessageBox.Show("Unhandled Exception" + Environment.NewLine + e.Message + Environment.NewLine + "Stack Trace: " + Environment.NewLine + e.StackTrace, "Unhandled Exception. Program Will Halt.");
                OnExitRequested(EventArgs.Empty);
            }
        }
    }
}