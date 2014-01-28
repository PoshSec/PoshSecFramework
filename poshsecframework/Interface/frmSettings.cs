using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using poshsecframework.Strings;

namespace poshsecframework.Interface
{
    public partial class frmSettings : Form
    {
        private FolderBrowserDialog dlgFolder = new FolderBrowserDialog();
        private OpenFileDialog dlgFile = new OpenFileDialog();
        private bool restart = false;

        public frmSettings()
        {
            InitializeComponent();
            cmbScriptDefAction.SelectedIndex = 0;
            LoadSettings();
        }

        private void LoadSettings()
        {
            txtScriptDirectory.Text = Properties.Settings.Default.ScriptPath;
            txtGithubAPIKey.Text = Properties.Settings.Default.GithubAPIKey;
            txtModuleDirectory.Text = Properties.Settings.Default.ModulePath;
            txtPSExecPath.Text = Properties.Settings.Default.PSExecPath;
            txtSchFile.Text = Properties.Settings.Default.ScheduleFile;
            cmbScriptDefAction.SelectedIndex = Properties.Settings.Default.ScriptDefaultAction;
            bool firsttime = Properties.Settings.Default.FirstTime;
            if (firsttime)
            {
                cmbFirstTime.SelectedIndex = 0;
            }
            else
            {
                cmbFirstTime.SelectedIndex = 1;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private bool Save()
        {
            bool rtn = true;
            if (Directory.Exists(txtScriptDirectory.Text) && Directory.Exists(txtModuleDirectory.Text))
            {
                Properties.Settings.Default["ScriptPath"] = txtScriptDirectory.Text;
                Properties.Settings.Default["ModulePath"] = txtModuleDirectory.Text;
                Properties.Settings.Default["ScriptDefaultAction"] = cmbScriptDefAction.SelectedIndex;
                Properties.Settings.Default["PSExecPath"] = txtPSExecPath.Text;
                Properties.Settings.Default["ScheduleFile"] = txtSchFile.Text;
                Properties.Settings.Default["GithubAPIKey"] = txtGithubAPIKey.Text;
                bool firsttime = false;
                if (cmbFirstTime.SelectedIndex == 0)
                {
                    firsttime = true;
                }
                Properties.Settings.Default["FirstTime"] = firsttime;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();

            }
            else if (!Directory.Exists(txtScriptDirectory.Text))
            {
                MessageBox.Show(StringValue.ScriptPathError);
                rtn = false;
            }
            else if (!Directory.Exists(txtModuleDirectory.Text))
            {
                MessageBox.Show(StringValue.ModulePathError);
                rtn = false;
            }
            return rtn;
        }

        private void btnBrowseScript_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtScriptDirectory.Text))
            {
                dlgFolder.SelectedPath = txtScriptDirectory.Text;
            }
            dlgFolder.Description = "Select the Script Directory.";
            if (dlgFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtScriptDirectory.Text = dlgFolder.SelectedPath;
            }
        }

        private void btnBrowseModule_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtModuleDirectory.Text))
            {
                dlgFolder.SelectedPath = txtModuleDirectory.Text;
            }
            dlgFolder.Description = "Select the Module Directory.";
            if (dlgFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtModuleDirectory.Text = dlgFolder.SelectedPath;
            }
        }

        private void btnBrowsePSExec_Click(object sender, EventArgs e)
        {
            dlgFile.Title = "Select the PSExec File.";
            dlgFile.FileName = "*psexec.exe";
            dlgFile.CheckFileExists = true;
            if (File.Exists(txtPSExecPath.Text))
            {
                dlgFile.InitialDirectory = new FileInfo(txtPSExecPath.Text).Directory.ToString();
            }                        
            if (dlgFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtPSExecPath.Text = dlgFile.FileName;
            }
        }

        private void btnBrowseSchFile_Click(object sender, EventArgs e)
        {
            dlgFile.Title = "Set Schedule File";
            dlgFile.FileName = "schedule.xml";
            dlgFile.Filter = "Extensible Markup Language (*.xml)|*.xml";
            dlgFile.CheckFileExists = false;
            if (File.Exists(txtSchFile.Text))
            {
                dlgFile.InitialDirectory = new FileInfo(txtSchFile.Text).Directory.ToString();
            }
            if (dlgFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtSchFile.Text = dlgFile.FileName;
            }
        }

        private void btnAddModule_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                Interface.frmRepository frm = new frmRepository();
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    restart = frm.Restart;
                }
                frm.Dispose();
                frm = null;
                if (restart)
                {
                    lblRestartRequired.Visible = true;
                }
            }            
        }

        public bool Restart
        {
            get { return restart; }
        }

        private void btnGithubHelp_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(StringValue.RateLimitURL);
                psi.UseShellExecute = true;
                psi.Verb = "open";
                System.Diagnostics.Process prc = new System.Diagnostics.Process();
                prc.StartInfo = psi;
                prc.Start();
                prc = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
