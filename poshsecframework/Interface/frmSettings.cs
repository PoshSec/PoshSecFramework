using System;
using System.Collections.Generic;
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

        public frmSettings()
        {
            InitializeComponent();
            cmbScriptDefAction.SelectedIndex = 0;
            LoadSettings();
        }

        private void LoadSettings()
        {
            txtScriptDirectory.Text = Properties.Settings.Default.ScriptPath;
            txtFrameworkFile.Text = Properties.Settings.Default.FrameworkPath;
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
            if (Directory.Exists(txtScriptDirectory.Text) && File.Exists(txtFrameworkFile.Text) && Directory.Exists(txtModuleDirectory.Text))
            {
                Properties.Settings.Default["ScriptPath"] = txtScriptDirectory.Text;
                Properties.Settings.Default["FrameworkPath"] = txtFrameworkFile.Text;
                Properties.Settings.Default["ModulePath"] = txtModuleDirectory.Text;
                Properties.Settings.Default["ScriptDefaultAction"] = cmbScriptDefAction.SelectedIndex;
                Properties.Settings.Default["PSExecPath"] = txtPSExecPath.Text;
                Properties.Settings.Default["ScheduleFile"] = txtSchFile.Text;
                bool firsttime = false;
                if(cmbFirstTime.SelectedIndex == 0) 
                {
                    firsttime = true;
                }
                Properties.Settings.Default["FirstTime"] = firsttime;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else if (!Directory.Exists(txtScriptDirectory.Text))
            {
                MessageBox.Show(StringValue.ScriptPathError);
            }
            else if (!File.Exists(txtFrameworkFile.Text))
            {
                MessageBox.Show(StringValue.FrameworkFileError);
            }
            else if (!Directory.Exists(txtModuleDirectory.Text))
            {
                MessageBox.Show(StringValue.ModulePathError);
            }
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

        private void btnBrowseFramework_Click(object sender, EventArgs e)
        {
            dlgFile.CheckFileExists = true;
            if (File.Exists(txtFrameworkFile.Text))
            {
                dlgFile.FileName = txtFrameworkFile.Text;
                dlgFile.InitialDirectory = new FileInfo(txtFrameworkFile.Text).Directory.ToString();
            }
            dlgFile.Title = "Select the Framework File.";
            if (dlgFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFrameworkFile.Text = dlgFile.FileName;
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
    }
}
