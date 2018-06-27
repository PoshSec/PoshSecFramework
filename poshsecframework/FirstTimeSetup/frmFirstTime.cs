using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PoshSec.Framework.Interface;
using PoshSec.Framework.Properties;
using PoshSec.Framework.PShell;
using PoshSec.Framework.Strings;
using PoshSec.Framework.Web;

namespace PoshSec.Framework.FirstTimeSetup
{
    public partial class frmFirstTime : Form
    {
        private readonly string[] Message =
        {
            StringValue.FTCheckSettings,
            StringValue.FTInitialDownload,
            StringValue.FTUnblockFiles,
            StringValue.FTUpdateHelp,
            StringValue.FTSetExecutionPolicy
        };


        private readonly string[] Errors =
        {
            StringValue.StepSuccessDescription,
            StringValue.StepSuccessDescription,
            StringValue.StepSuccessDescription,
            StringValue.StepSuccessDescription,
            StringValue.StepSuccessDescription
        };

        private bool showerrors;
        private int contcount;
        private readonly frmMain parentform;

        public frmFirstTime(frmMain Parent)
        {
            parentform = Parent;
            InitializeComponent();
            if (TestPSEnvironment())
            {
                var stepvals = Enum.GetValues(typeof(Steps));
                foreach (Steps step in stepvals)
                {
                    var lvw = new ListViewItem
                    {
                        Text = step.ToString().Replace("_", " "),
                        ImageIndex = -1
                    };
                    lvw.SubItems.Add("");
                    lvw.Checked = true;
                    lvwSteps.Items.Add(lvw);
                }
            }
            else
            {
                MessageBox.Show(StringValue.PSRequirements);
            }
        }

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
                            btnFix.Enabled = true;
                    }
                }
                else
                {
                    if (lvwSteps.SelectedIndices[0] < Message.Length)
                        txtDescription.Text = Message[lvwSteps.SelectedIndices[0]];
                }
            }
            else
            {
                txtDescription.Text = showerrors 
                    ? StringValue.StepCompleteDescription 
                    : StringValue.StepSelectDescription;
            }
        }

        private void btnDont_Click(object sender, EventArgs e)
        {
            Settings.Default["FirstTime"] = false;
            Settings.Default.Save();
            Settings.Default.Reload();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            contcount++;
            if (contcount == 1)
            {
                lvwSteps.SelectedItems.Clear();
                txtDescription.Text = StringValue.StepCompleteDescription;
                if (lvwSteps.CheckedIndices.Count > 0)
                {
                    showerrors = true;
                    pnlFix.Visible = true;
                    btnFix.Enabled = false;
                    btnDont.Enabled = false;
                    btnContinue.Enabled = false;
                    var indexes = PrepListBox();
                    foreach (var idx in indexes)
                    {
                        lvwSteps.Items[idx].SubItems[1].Text = StringValue.StepRunning;
                        lvwSteps.Items[idx].ImageIndex = 3;
                        lvwSteps.Update();
                        var rslt = PerformStep(idx);
                        if (rslt)
                        {
                            lvwSteps.Items[idx].ImageIndex = 0;
                            lvwSteps.Items[idx].SubItems[1].Text = StringValue.StepSuccess;
                        }
                        else
                        {
                            lvwSteps.Items[idx].ImageIndex = 1;
                            lvwSteps.Items[idx].SubItems[1].Text = StringValue.StepFailed;
                        }
                    }
                    Settings.Default["FirstTime"] = false;
                    Settings.Default.Save();
                    Settings.Default.Reload();
                    btnContinue.Enabled = true;
                }
                else
                {
                    MessageBox.Show(StringValue.MustSelectStep);
                }
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            if (lvwSteps.SelectedItems.Count > 0)
            {
                var idx = lvwSteps.SelectedIndices[0];
                switch ((Steps) idx)
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

        private bool TestPSEnvironment()
        {
            var rtn = true;
            try
            {
                var ps = new pscript(parentform);
            }
            catch (Exception)
            {
                // This will never happen because pscript() handles all exceptions
                rtn = false;
            }
            return rtn;
        }

        private void SetStepFixed(int index)
        {
            lvwSteps.Items[index].ImageIndex = 0;
            lvwSteps.Items[index].SubItems[1].Text = StringValue.StepSuccess;
            Errors[index] = StringValue.StepSuccessDescription;
            lvwSteps.SelectedItems.Clear();
        }

        private void FixSettings(int idx)
        {
            var frm = new frmSettings();
            var rslt = frm.ShowDialog();
            frm.Dispose();
            if (rslt == DialogResult.OK)
                SetStepFixed(idx);
        }

        private void FixExecutionPolicy(int idx)
        {
            if (MessageBox.Show(StringValue.RunAsAdministratorError, Steps.Set_Execution_Policy.ToString(),
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lvwSteps.Items[idx].SubItems[1].Text = StringValue.StepRunning;
                lvwSteps.Items[idx].ImageIndex = 3;
                lvwSteps.Update();
                if (PerformStep((int) Steps.Set_Execution_Policy))
                {
                    SetStepFixed(idx);
                }
                else
                {
                    lvwSteps.Items[idx].ImageIndex = 1;
                    lvwSteps.Items[idx].SubItems[1].Text = StringValue.StepFailed;
                }
            }
        }

        private void FixUnblockFiles(int idx)
        {
            if (MessageBox.Show(StringValue.RunAsAdministratorError, Steps.Unblock_Files.ToString(),
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lvwSteps.Items[idx].SubItems[1].Text = StringValue.StepRunning;
                lvwSteps.Items[idx].ImageIndex = 3;
                lvwSteps.Update();
                if (PerformStep((int) Steps.Unblock_Files))
                {
                    SetStepFixed(idx);
                }
                else
                {
                    lvwSteps.Items[idx].ImageIndex = 1;
                    lvwSteps.Items[idx].SubItems[1].Text = StringValue.StepFailed;
                }
            }
        }

        private List<int> PrepListBox()
        {
            var indexes = new List<int>();
            var index = -1;
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
                    lvw.SubItems[1].Text = StringValue.StepIgnored;
                    Errors[index] = StringValue.StepIgnoredDescription;
                }
            }
            lvwSteps.CheckBoxes = false;
            lvwSteps.SmallImageList = imgList;
            return indexes;
        }

        private bool PerformStep(int idx)
        {
            var rtn = false;
            switch ((Steps) idx)
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
            var rtn = true;
            var err = "";
            if (!Directory.Exists(Settings.Default.ScriptPath))
                try
                {
                    Directory.CreateDirectory(Settings.Default.ScriptPath);
                }
                catch
                {
                    err += "Script Path " + Settings.Default.ScriptPath + " does not exist.\r\n";
                    rtn = false;
                }
            if (!Directory.Exists(Settings.Default.ModulePath))
                try
                {
                    Directory.CreateDirectory(Settings.Default.ModulePath);
                }
                catch
                {
                    err += "Module Path " + Settings.Default.ModulePath + " does not exist.\r\n";
                    rtn = false;
                }
            if (!rtn)
                Errors[(int) Steps.Check_Settings] = err;
            return rtn;
        }

        private bool InitialDownload()
        {
            var rtn = true;
            var err = "";
            var mods = Settings.Default.Modules;
            var newmods = new StringCollection();
            if (mods != null && mods.Count > 0)
            {
                foreach (var mod in mods)
                {
                    var modparts = mod.Split('|');
                    if (modparts.Length >= 3 && modparts.Length <= 4)
                        try
                        {
                            var ghc = new GithubClient();
                            var location = modparts[1];
                            var locparts = location.Split('/');
                            if (locparts.Length == 2)
                            {
                                var RepoOwner = locparts[0];
                                var Repository = modparts[0];
                                var branch = modparts[2];
                                ghc.GetArchive(RepoOwner, Repository, branch, Settings.Default.ModulePath);
                                ghc.GetLastModified(RepoOwner, Repository, branch, "");
                                newmods.Add(mod + ghc.LastModified);
                                if (ghc.Errors.Count > 0)
                                {
                                    rtn = rtn && false;
                                    err += string.Join(Environment.NewLine, ghc.Errors.ToArray());
                                }
                                if (new DirectoryInfo(Settings.Default.ScriptPath)
                                        .GetFiles("*", SearchOption.AllDirectories).Any())
                                {
                                    if (MessageBox.Show(StringValue.ConfirmScriptDelete, "Existing Scripts",
                                            MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        ghc.GetPSFScripts(Settings.Default.ScriptPath);
                                }
                                else
                                {
                                    ghc.GetPSFScripts(Settings.Default.ScriptPath);
                                }
                                if (ghc.Errors.Count > 0)
                                {
                                    rtn = false;
                                    err += string.Join(Environment.NewLine, ghc.Errors.ToArray());
                                }
                            }
                            else
                            {
                                rtn = false;
                                err += "Invalid location in module.";
                            }
                            ghc = null;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                }
                if (newmods.Count == mods.Count)
                {
                    Settings.Default["Modules"] = newmods;
                    Settings.Default["LastModuleCheck"] = DateTime.Now.ToString();
                    Settings.Default.Save();
                    Settings.Default.Reload();
                }
            }
            if (!rtn)
                Errors[(int) Steps.InitialDownload] = err;
            return rtn;
        }

        private bool UnblockFiles()
        {
            var rtn = true;
            var err = "";
            var ps = new pscript(parentform);
            if (!ps.UnblockFiles(Settings.Default.ScriptPath))
            {
                err = ps.Results;
                rtn = false;
            }
            if (!ps.UnblockFiles(Settings.Default.ModulePath))
            {
                err += "\r\n" + ps.Results;
                rtn = false;
            }
            if (!ps.UnblockFiles(Application.StartupPath))
            {
                err += "\r\n" + ps.Results;
                rtn = false;
            }
            ps.Dispose();
            if (!rtn)
                Errors[(int) Steps.Unblock_Files] = err + StringValue.TNUnblockFile;
            return rtn;
        }

        private bool SetExecutionPolicy()
        {
            var rtn = false;
            var ps = new pscript(parentform);
            rtn = ps.SetExecutionPolicy();
            if (!rtn)
                Errors[(int) Steps.Set_Execution_Policy] = ps.Results + StringValue.TNSetExecutionPolicy;
            ps.Dispose();
            ps = null;
            return rtn;
        }

        private bool UpdateHelp()
        {
            var rtn = false;
            var ps = new pscript(parentform);
            rtn = ps.UpdateHelp();
            if (!rtn)
                Errors[(int) Steps.Update_Help] = ps.Results + StringValue.TNUpdateHelp;
            ps.Dispose();
            ps = null;
            return rtn;
        }
    }
}
