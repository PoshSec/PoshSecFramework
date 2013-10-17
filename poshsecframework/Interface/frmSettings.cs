using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            cmbScriptDefAction.SelectedIndex = Properties.Settings.Default.ScriptDefaultAction;
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
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else if (!Directory.Exists(txtScriptDirectory.Text))
            {
                MessageBox.Show("The specified script directory does not exist. Please check the path.");
            }
            else if (!File.Exists(txtFrameworkFile.Text))
            {
                MessageBox.Show("The specified Framework file does not exist. Please check the path.");
            }
            else if (!Directory.Exists(txtModuleDirectory.Text))
            {
                MessageBox.Show("The specified module directory does not exist. Please check the path.");
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
            if (File.Exists(txtPSExecPath.Text))
            {
                dlgFile.InitialDirectory = new FileInfo(txtPSExecPath.Text).Directory.ToString();
            }                        
            if (dlgFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtPSExecPath.Text = dlgFile.FileName;
            }
        }
    }
}
