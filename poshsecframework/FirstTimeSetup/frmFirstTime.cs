using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        private readonly string[] _message =
        {
            StringValue.FTCheckSettings,
            StringValue.FTInitialDownload,
            StringValue.FTUnblockFiles,
            StringValue.FTUpdateHelp,
            StringValue.FTSetExecutionPolicy
        };


        private readonly string[] _errors =
        {
            StringValue.StepSuccessDescription,
            StringValue.StepSuccessDescription,
            StringValue.StepSuccessDescription,
            StringValue.StepSuccessDescription,
            StringValue.StepSuccessDescription
        };

        private bool _showerrors;
        private readonly frmMain _parentform;

        public frmFirstTime(frmMain parent)
        {
            _parentform = parent;
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
            btnContinue.Text = "Start";
            btnContinue.Click += StartClick;
        }

        private void lvwSteps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwSteps.SelectedIndices.Count > 0)
            {
                if (_showerrors)
                {
                    btnFix.Enabled = false;
                    if (lvwSteps.SelectedIndices[0] < _errors.Length)
                    {
                        txtDescription.Text = _errors[lvwSteps.SelectedIndices[0]];
                        if (lvwSteps.SelectedItems[0].ImageIndex == 1)
                            btnFix.Enabled = true;
                    }
                }
                else
                {
                    if (lvwSteps.SelectedIndices[0] < _message.Length)
                        txtDescription.Text = _message[lvwSteps.SelectedIndices[0]];
                }
            }
            else
            {
                txtDescription.Text = _showerrors
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

        private async void StartClick(object sender, EventArgs e)
        {

            lvwSteps.SelectedItems.Clear();
            txtDescription.Text = StringValue.StepCompleteDescription;
            if (lvwSteps.CheckedIndices.Count > 0)
            {
                var cancellationTokenSource = new CancellationTokenSource();
                void CancelClick(object control, EventArgs e2)
                {
                    cancellationTokenSource.Cancel();
                }

                btnContinue.Enabled = false;
                btnContinue.Click -= StartClick;
                btnContinue.Text = "Cancel";
                btnContinue.Click += CancelClick;
                btnContinue.Enabled = true;

                _showerrors = true;
                pnlFix.Visible = true;
                btnFix.Enabled = false;
                btnDont.Enabled = false;
                var steps = PrepListBox();
                var tasks = new List<Task>();
                foreach (var step in steps)
                {
                    lvwSteps.Items[step].SubItems[1].Text = StringValue.StepRunning;
                    lvwSteps.Items[step].ImageIndex = 3;
                    lvwSteps.Update();
                    var task = Task<bool>.Factory
                        .StartNew(() => PerformStep(step),
                            cancellationTokenSource.Token,
                            TaskCreationOptions.LongRunning,
                            TaskScheduler.Default)
                        .ContinueWith(t =>
                        {
                            var success = t.Result;
                            UpdateStepStatus(step, success);
                        }, cancellationTokenSource.Token);
                    tasks.Add(task);
                    if (cancellationTokenSource.IsCancellationRequested)
                        break;
                }
                try
                {
                    await Task.WhenAll(tasks).ContinueWith(t =>
                    {
                        Settings.Default["FirstTime"] = false;
                    }, cancellationTokenSource.Token);
                }
                catch (TaskCanceledException)
                {
                    Settings.Default["FirstTime"] = true;
                }
                finally
                {
                    Settings.Default.Save();
                    Settings.Default.Reload();

                    void FinishClick(object control, EventArgs e2)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }

                    btnContinue.Enabled = false;
                    btnContinue.Click -= CancelClick;
                    btnContinue.Text = "Finish";
                    btnContinue.Click += FinishClick;
                    btnContinue.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show(StringValue.MustSelectStep);
            }
        }

        private void UpdateStepStatus(int step, bool success)
        {
            if (lvwSteps.InvokeRequired)
            {
                MethodInvoker del = delegate { UpdateStepStatus(step, success); };
                lvwSteps.Invoke(del);
            }
            else if (success)
            {
                lvwSteps.Items[step].ImageIndex = 0;
                lvwSteps.Items[step].SubItems[1].Text = StringValue.StepSuccess;
            }
            else
            {
                lvwSteps.Items[step].ImageIndex = 1;
                lvwSteps.Items[step].SubItems[1].Text = StringValue.StepFailed;
            }
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            if (lvwSteps.SelectedItems.Count > 0)
            {
                var idx = lvwSteps.SelectedIndices[0];
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

        private bool TestPSEnvironment()
        {
            var rtn = true;
            try
            {
                var ps = new pscript(_parentform);
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
            _errors[index] = StringValue.StepSuccessDescription;
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
                if (PerformStep((int)Steps.Set_Execution_Policy))
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
                if (PerformStep((int)Steps.Unblock_Files))
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
                    _errors[index] = StringValue.StepIgnoredDescription;
                }
            }
            lvwSteps.CheckBoxes = false;
            lvwSteps.SmallImageList = imgList;
            return indexes;
        }

        private bool PerformStep(int step)
        {
            var success = false;
            switch ((Steps)step)
            {
                case Steps.Check_Settings:
                    success = CheckSettings();
                    break;
                case Steps.InitialDownload:
                    success = InitialDownload();
                    break;
                case Steps.Set_Execution_Policy:
                    success = SetExecutionPolicy();
                    break;
                case Steps.Unblock_Files:
                    success = UnblockFiles();
                    break;
                case Steps.Update_Help:
                    success = UpdateHelp();
                    break;
            }
            return success;
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
                _errors[(int)Steps.Check_Settings] = err;
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
                _errors[(int)Steps.InitialDownload] = err;
            return rtn;
        }

        private bool UnblockFiles()
        {
            var rtn = true;
            var err = "";
            var ps = new pscript(_parentform);
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
                _errors[(int)Steps.Unblock_Files] = err + StringValue.TNUnblockFile;
            return rtn;
        }

        private bool SetExecutionPolicy()
        {
            var rtn = false;
            var ps = new pscript(_parentform);
            rtn = ps.SetExecutionPolicy();
            if (!rtn)
                _errors[(int)Steps.Set_Execution_Policy] = ps.Results + StringValue.TNSetExecutionPolicy;
            ps.Dispose();
            ps = null;
            return rtn;
        }

        private bool UpdateHelp()
        {
            using (var ps = new pscript(_parentform))
            {
                var rtn = ps.UpdateHelp();
                if (!rtn)
                    _errors[(int)Steps.Update_Help] = ps.Results + StringValue.TNUpdateHelp;
                return rtn;
            }
        }

    }
}

