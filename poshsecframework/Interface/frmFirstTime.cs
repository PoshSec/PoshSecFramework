using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace poshsecframework.Interface
{
    public partial class frmFirstTime : Form
    {
        #region Private Variables
        private string[] Message = new string[] {
            Strings.StringValue.FTCheckSettings,
            Strings.StringValue.FTInitialDownload,
            Strings.StringValue.FTUnblockFiles,
            Strings.StringValue.FTUpdateHelp,
            Strings.StringValue.FTSetExecutionPolicy
        };

        private enum Steps
        { 
            Check_Settings = 0,
            InitialDownload,
            Unblock_Files,
            Update_Help,
            Set_Execution_Policy
        }

        private string[] Errors = new string[] {
            Strings.StringValue.StepSuccessDescription,
            Strings.StringValue.StepSuccessDescription,
            Strings.StringValue.StepSuccessDescription,
            Strings.StringValue.StepSuccessDescription,
            Strings.StringValue.StepSuccessDescription
        };

        private bool showerrors = false;
        private int contcount = 0;
        #endregion

        #region Public Methods 
        public frmFirstTime()
        {
            InitializeComponent();
            Array stepvals = Enum.GetValues(typeof(Steps));
            foreach (Steps step in stepvals)
            {
                ListViewItem lvw = new ListViewItem();
                lvw.Text = step.ToString().Replace("_", " ");
                lvw.ImageIndex = -1;
                lvw.SubItems.Add("");
                lvw.Checked = true;
                lvwSteps.Items.Add(lvw);
            }
        }
        #endregion

        #region Private Events
        private void lvwSteps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwSteps.SelectedIndices.Count > 0)
            {
                if (showerrors)
                {
                    btnFix.Enabled = false;
                    if (lvwSteps.SelectedIndices[0] < Errors.Length)
                    {
                        txtDescription.Text = Errors[lvwSteps.SelectedIndices[0]];
                        if (lvwSteps.SelectedItems[0].ImageIndex == 1)
                        {
                            btnFix.Enabled = true;
                        }
                    }
                }
                else
                {
                    if (lvwSteps.SelectedIndices[0] < Message.Length)
                    {
                        txtDescription.Text = Message[lvwSteps.SelectedIndices[0]];
                    }
                }
            }
            else
            {
                if (showerrors)
                {
                    txtDescription.Text = Strings.StringValue.StepCompleteDescription;
                }
                else
                {
                    txtDescription.Text = Strings.StringValue.StepSelectDescription;
                }
            }
        }

        private void btnDont_Click(object sender, EventArgs e)
        {
            poshsecframework.Properties.Settings.Default["FirstTime"] = false;
            poshsecframework.Properties.Settings.Default.Save();
            poshsecframework.Properties.Settings.Default.Reload();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            contcount++;
            if (contcount == 1)
            {
                lvwSteps.SelectedItems.Clear();
                txtDescription.Text = Strings.StringValue.StepCompleteDescription;
                if (lvwSteps.CheckedIndices.Count > 0)
                {
                    showerrors = true;
                    pnlFix.Visible = true;
                    btnFix.Enabled = false;
                    btnDont.Enabled = false;
                    btnContinue.Enabled = false;
                    List<int> indexes = PrepListBox();
                    foreach (int idx in indexes)
                    {
                        lvwSteps.Items[idx].SubItems[1].Text = Strings.StringValue.StepRunning;
                        lvwSteps.Items[idx].ImageIndex = 3;
                        lvwSteps.Update();
                        bool rslt = PerformStep(idx);
                        if (rslt)
                        {
                            lvwSteps.Items[idx].ImageIndex = 0;
                            lvwSteps.Items[idx].SubItems[1].Text = Strings.StringValue.StepSuccess;
                        }
                        else
                        {
                            lvwSteps.Items[idx].ImageIndex = 1;
                            lvwSteps.Items[idx].SubItems[1].Text = Strings.StringValue.StepFailed;
                        }
                    }
                    poshsecframework.Properties.Settings.Default["FirstTime"] = false;
                    poshsecframework.Properties.Settings.Default.Save();
                    poshsecframework.Properties.Settings.Default.Reload();
                    btnContinue.Enabled = true;
                }
                else
                {
                    MessageBox.Show(Strings.StringValue.MustSelectStep);
                }
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            if (lvwSteps.SelectedItems.Count > 0)
            {
                int idx = lvwSteps.SelectedIndices[0];
                switch ((Steps)idx)
                { 
                    case Steps.Check_Settings:
                        FixSettings(idx);
                        break;
                    case Steps.Set_Execution_Policy:
                        FixExecutionPolicy(idx);
                        break;
                    case Steps.Unblock_Files:
                        FixUnblockFiles(idx);
                        break;
                }
            }
        }
        #endregion

        #region Private Methods
        private void SetStepFixed(int index)
        {
            lvwSteps.Items[index].ImageIndex = 0;
            lvwSteps.Items[index].SubItems[1].Text = Strings.StringValue.StepSuccess;
            Errors[index] = Strings.StringValue.StepSuccessDescription;
            lvwSteps.SelectedItems.Clear();
        }

        private void FixSettings(int idx)
        {
            poshsecframework.Interface.frmSettings frm = new poshsecframework.Interface.frmSettings();
            System.Windows.Forms.DialogResult rslt = frm.ShowDialog();
            frm.Dispose();
            frm = null;
            if (rslt == System.Windows.Forms.DialogResult.OK)
            {
                SetStepFixed(idx);
            };
        }

        private void FixExecutionPolicy(int idx)
        {
            if (MessageBox.Show(Strings.StringValue.RunAsAdministratorError, Steps.Set_Execution_Policy.ToString(), MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                lvwSteps.Items[idx].SubItems[1].Text = Strings.StringValue.StepRunning;
                lvwSteps.Items[idx].ImageIndex = 3;
                lvwSteps.Update();
                if (PerformStep((int)Steps.Set_Execution_Policy))
                {
                    SetStepFixed(idx);
                }
                else
                {
                    lvwSteps.Items[idx].ImageIndex = 1;
                    lvwSteps.Items[idx].SubItems[1].Text = Strings.StringValue.StepFailed;
                }
            }
        }

        private void FixUnblockFiles(int idx)
        {
            if (MessageBox.Show(Strings.StringValue.RunAsAdministratorError, Steps.Unblock_Files.ToString(), MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                lvwSteps.Items[idx].SubItems[1].Text = Strings.StringValue.StepRunning;
                lvwSteps.Items[idx].ImageIndex = 3;
                lvwSteps.Update();
                if (PerformStep((int)Steps.Unblock_Files))
                {
                    SetStepFixed(idx);
                }
                else
                {
                    lvwSteps.Items[idx].ImageIndex = 1;
                    lvwSteps.Items[idx].SubItems[1].Text = Strings.StringValue.StepFailed;
                }
            }
        }

        private List<int> PrepListBox()
        {
            List<int> indexes = new List<int>();
            int index = -1;
            foreach (ListViewItem lvw in lvwSteps.Items)
            {
                index++;
                if (lvw.Checked)
                {
                    indexes.Add(index);
                }
                else
                {
                    lvw.ImageIndex = 2;
                    lvw.SubItems[1].Text = Strings.StringValue.StepIgnored;
                    Errors[index] = Strings.StringValue.StepIgnoredDescription;
                }
            }
            lvwSteps.CheckBoxes = false;
            lvwSteps.SmallImageList = imgList;
            return indexes;
        }

        private bool PerformStep(int idx)
        {
            bool rtn = false;
            switch ((Steps)idx)
            { 
                case Steps.Check_Settings:
                    rtn = CheckSettings();
                    break;
                case Steps.InitialDownload:
                    rtn = InitialDownload();
                    break;
                case Steps.Set_Execution_Policy:
                    rtn = SetExecutionPolicy();
                    break;
                case Steps.Unblock_Files:
                    rtn = UnblockFiles();
                    break;
                case Steps.Update_Help:
                    rtn = UpdateHelp();
                    break;
            }
            return rtn;
        }

        private bool CheckSettings()
        {
            bool rtn = true;
            string err = "";
            if (!Directory.Exists(Properties.Settings.Default.ScriptPath))
            {
                try
                {
                    Directory.CreateDirectory(Properties.Settings.Default.ScriptPath);
                }
                catch
                {
                    err += "Script Path " + Properties.Settings.Default.ScriptPath + " does not exist.\r\n";
                    rtn = rtn && false;
                }                
            }
            if (!Directory.Exists(Properties.Settings.Default.ModulePath)) 
            {
                try
                {
                    Directory.CreateDirectory(Properties.Settings.Default.ModulePath);
                }
                catch
                {
                    err += "Module Path " + Properties.Settings.Default.ModulePath + " does not exist.\r\n";
                    rtn = rtn && false;
                }                
            }
            if (!rtn)
            {
                Errors[(int)Steps.Check_Settings] = err;
            }
            return rtn;
        }

        private bool InitialDownload()
        {
            bool rtn = true;
            string err = "";
            System.Collections.Specialized.StringCollection mods = Properties.Settings.Default.Modules;
            if (mods != null && mods.Count > 0)
            {
                foreach (string mod in mods)
                {
                    String[] modparts = mod.Split('|');
                    if (modparts != null && modparts.Length >= 3 && modparts.Length <= 4)
                    {
                        try
                        {
                            Web.GithubClient ghc = new Web.GithubClient();
                            String location = modparts[1];
                            String[] locparts = location.Split('/');
                            if (locparts != null && locparts.Length == 2)
                            {
                                String RepoOwner = locparts[0];
                                String Repository = modparts[0];
                                String branch = modparts[2];
                                ghc.GetArchive(RepoOwner, Repository, branch, Properties.Settings.Default.ModulePath);
                                if (ghc.Errors.Count > 0)
                                {
                                    rtn = rtn && false;
                                    err += String.Join(Environment.NewLine, ghc.Errors.ToArray());
                                }
                                ghc.GetPSFScripts(Properties.Settings.Default.ScriptPath);
                                if (ghc.Errors.Count > 0)
                                {
                                    rtn = rtn && false;
                                    err += String.Join(Environment.NewLine, ghc.Errors.ToArray());
                                }
                            }
                            else
                            {
                                rtn = rtn && false;
                                err += "Invalid location in module.";
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }
                }
            }
            if (!rtn)
            {
                Errors[(int)Steps.Check_Settings] = err;
            }
            return rtn;
        }

        private bool UnblockFiles()
        {
            bool rtn = true;
            string err = "";
            PShell.pscript ps = new PShell.pscript(null);
            if (!ps.UnblockFiles(poshsecframework.Properties.Settings.Default.ScriptPath))
            {
                err = ps.Results;
                rtn = rtn && false;
            }
            if (!ps.UnblockFiles(poshsecframework.Properties.Settings.Default.ModulePath))
            {
                err += "\r\n" + ps.Results;
                rtn = rtn && false;
            }
            if (!ps.UnblockFiles(Application.StartupPath))
            {
                err += "\r\n" + ps.Results;
                rtn = rtn && false;
            }
            ps.Dispose();
            ps = null;
            if (!rtn)
            {
                Errors[(int)Steps.Unblock_Files] = err + Strings.StringValue.TNUnblockFile;
            }
            return rtn;
        }

        private bool SetExecutionPolicy()
        {
            bool rtn = false;
            PShell.pscript ps = new PShell.pscript(null);
            rtn = ps.SetExecutionPolicy();
            if (!rtn)
            {
                Errors[(int)Steps.Set_Execution_Policy] = ps.Results + Strings.StringValue.TNSetExecutionPolicy;
            }
            ps.Dispose();
            ps = null;
            return rtn;
        }

        private bool UpdateHelp()
        {
            bool rtn = false;
            PShell.pscript ps = new PShell.pscript(null);
            rtn = ps.UpdateHelp();
            if (!rtn)
            {
                Errors[(int)Steps.Update_Help] = ps.Results + Strings.StringValue.TNUpdateHelp;
            }
            ps.Dispose();
            ps = null;
            return rtn;
        }
        #endregion
    }
}
