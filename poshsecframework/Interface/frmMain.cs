using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Globalization;
using System.Management.Automation;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using poshsecframework.Strings;

namespace poshsecframework
{
    public partial class frmMain : Form
    {
        #region Private Variables 
        Network.NetworkBrowser scnr = new Network.NetworkBrowser();
        private int mincurpos = 6;
        private Collection<String> cmdhist = new Collection<string>();
        private int cmdhistidx = -1;
        private PShell.pshell psf;
        private bool cancelscan = false;
        private bool restart = false;
        private Utility.Schedule schedule = new Utility.Schedule(1000);

        enum SystemType
        { 
            Local = 1,
            Domain
        }

        enum LibraryImages
        { 
            Function,
            Cmdlet,
            Command,
            Alias
        }

        enum ScheduleColumns
        { 
            ScriptName = 0,
            Parameters,
            Schedule,
            RunAs,
            LastRun,
            Message
        }
        #endregion

        #region Form
        public frmMain()
        {
            InitializeComponent();
            scnr.ScanComplete += scnr_ScanComplete;
            scnr.ScanCancelled += scnr_ScanCancelled;
            schedule.ItemUpdated += schedule_ItemUpdated;
            schedule.ScriptInvoked += schedule_ScriptInvoked;

            Initialize();
            if (poshsecframework.Properties.Settings.Default.FirstTime)
            {
                restart = true;
                FirstTimeSetup();                
            }
            if (!restart)
            {
                GetNetworks();
            }            
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            if (restart)
            {
                Application.Restart();
                this.Close();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (lvwActiveScripts.Items.Count > 0)
                {
                    if (MessageBox.Show(StringValue.ActiveScriptsRunning, "Active Scripts", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        foreach (ListViewItem lvw in lvwActiveScripts.Items)
                        {
                            Thread thd = (Thread)lvw.Tag;
                            thd.Abort();
                            do
                            {
                                Application.DoEvents();
                            } while (thd.ThreadState != ThreadState.Aborted);
                        }
                        this.Close();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception)
            { 
                //just exit.
                this.Close();
            }
        }
        #endregion

        #region Private Methods

        private void Initialize()
        {
            CheckSettings();
            psf = new PShell.pshell();
            txtPShellOutput.Text = StringValue.psf;
            mincurpos = txtPShellOutput.Text.Length;
            txtPShellOutput.SelectionStart = mincurpos;
            scnr.ParentForm = this;
            cmbLibraryTypes.SelectedIndex = 1;
            psf.ParentForm = this;
            GetLibrary();
            GetCommand();
            LoadSchedule();  
        }

        private void FirstTimeSetup()
        {
            Interface.frmFirstTime frm = new Interface.frmFirstTime();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                restart = false;
            }
            frm.Dispose();
            frm = null;
        }

        #region Network
        private void GetNetworks()
        {
            tvwNetworks.Nodes[0].Nodes.Clear();
            TreeNode lnode = new TreeNode();
            lnode.Text = StringValue.LocalNetwork;
            lnode.ImageIndex = 3;
            lnode.SelectedImageIndex = 3;
            lnode.Tag = 1;
            tvwNetworks.Nodes[0].Nodes.Add(lnode);

            try
            {
                //Get Domain Name
                Forest hostForest = Forest.GetCurrentForest();
                DomainCollection domains = hostForest.Domains;
                
                foreach (Domain domain in domains)
                {
                    TreeNode node = new TreeNode();
                    node.Text = domain.Name;
                    node.SelectedImageIndex = 3;
                    node.ImageIndex = 3;
                    node.Tag = SystemType.Domain;
                    TreeNode rootnode = tvwNetworks.Nodes[0];
                    rootnode.Nodes.Add(node);
                }
            }
            catch
            {
                //fail silently because it's not on A/D   
            }

            try
            {
                //Add Local IP/Host to Local Network
                lvwSystems.Items.Clear();
                String localHost = Dns.GetHostName();
                String[] localIPs = scnr.GetIP(localHost).Split(',');
                foreach (String localIP in localIPs)
                {
                    ListViewItem lvwItm = new ListViewItem();

                    lvwItm.Text = localHost;
                    lvwItm.SubItems.Add(localIP);
                    lvwItm.SubItems.Add(scnr.GetMyMac(localIP));
                    lvwItm.SubItems.Add(StringValue.Up);
                    lvwItm.SubItems.Add(StringValue.NotInstalled);
                    lvwItm.SubItems.Add("0");
                    lvwItm.SubItems.Add(DateTime.Now.ToString(StringValue.TimeFormat));

                    lvwItm.ImageIndex = 2;
                    lvwSystems.Items.Add(lvwItm);
                    lvwSystems.Refresh();
                }
                tvwNetworks.Nodes[0].Expand();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + Environment.NewLine + e.StackTrace);
            }
            if (tvwNetworks.Nodes[0].Nodes.Count > 0)
            {
                tvwNetworks.SelectedNode = tvwNetworks.Nodes[0].Nodes[0];
            }
        }

        private void Scan()
        {
            if (tvwNetworks.SelectedNode != null && tvwNetworks.SelectedNode.Tag != null)
            {
                SystemType typ = (SystemType)Enum.Parse(typeof(SystemType), tvwNetworks.SelectedNode.Tag.ToString());
                this.UseWaitCursor = true;
                switch (typ)
                {
                    case SystemType.Local:
                        ScanbyIP();
                        break;
                    case SystemType.Domain:
                        ScanAD();
                        break;
                }
            }
            else
            {
                MessageBox.Show(StringValue.SelectNetwork);
            }
        }

        private void ScanAD()
        {
            String domain = tvwNetworks.SelectedNode.Text;
            ArrayList rslts = scnr.ScanActiveDirectory(domain);
            if (rslts.Count > 0)
            {
                lvwSystems.Items.Clear();
                lvwSystems.BeginUpdate();
                foreach (DirectoryEntry system in rslts)
                {
                    ListViewItem lvwItm = new ListViewItem();
                    lvwItm.Text = system.Name.ToString();

                    String ipadr = scnr.GetIP(system.Name);
                    lvwItm.SubItems.Add(ipadr);
                    lvwItm.SubItems.Add(scnr.GetMac(ipadr));
                    bool isup = false;
                    if (ipadr != StringValue.UnknownHost)
                    {
                        isup = scnr.Ping(system.Name, 1, 500);
                    }
                    if (isup)
                    {
                        lvwItm.SubItems.Add(StringValue.Up);
                    }
                    else
                    {
                        lvwItm.SubItems.Add(StringValue.Down);
                    }
                    lvwItm.SubItems.Add(StringValue.NotInstalled);
                    lvwItm.SubItems.Add("0");
                    lvwItm.SubItems.Add(DateTime.Now.ToString(StringValue.TimeFormat));

                    lvwItm.ImageIndex = 2;
                    lvwSystems.Items.Add(lvwItm);
                    lvwSystems.Refresh();
                    Application.DoEvents();
                }
                lvwSystems.EndUpdate();
            }
            rslts = null;
            this.UseWaitCursor = false;
        }

        private void ScanbyIP()
        {            
            btnCancelScan.Enabled = true;
            scnr.ParentForm = this;
            cancelscan = false;
            Thread thd = new Thread(scnr.ScanbyIP);
            thd.Start();
        }

        private void scnr_ScanComplete(object sender, poshsecframework.Network.ScanEventArgs e)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    scnr_ScanComplete(sender, e);
                };
                this.Invoke(del);
            }
            else
            {
                ArrayList rslts = e.Systems;
                if (rslts.Count > 0 && !cancelscan)
                {
                    lvwSystems.Items.Clear();
                    SetProgress(0, rslts.Count);
                    lvwSystems.BeginUpdate();
                    foreach (String system in rslts)
                    {
                        if (system != null && system != "")
                        {
                            ListViewItem lvwItm = new ListViewItem();

                            SetStatus("Adding " + system + ", please wait...");

                            String[] ipinfo = system.Split('|');
                            lvwItm.Text = ipinfo[2];
                            lvwItm.SubItems.Add(ipinfo[1]);
                            lvwItm.SubItems.Add(scnr.GetMac(ipinfo[1]));
                            lvwItm.SubItems.Add(StringValue.Up);
                            lvwItm.SubItems.Add(StringValue.NotInstalled);
                            lvwItm.SubItems.Add("0");
                            lvwItm.SubItems.Add(DateTime.Now.ToString(StringValue.TimeFormat));

                            lvwItm.ImageIndex = 2;
                            lvwSystems.Items.Add(lvwItm);
                            lvwSystems.Refresh();

                            pbStatus.Value += 1;
                        }
                    }
                    lvwSystems.EndUpdate();
                }
                rslts = null;
                HideProgress();
                btnCancelScan.Enabled = false;
                this.UseWaitCursor = false;
                lblStatus.Text = StringValue.Ready;
            }            
        }

        private void scnr_ScanCancelled(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    scnr_ScanCancelled(sender, e);
                };
                this.Invoke(del);
            }
            else
            {
                HideProgress();
                btnCancelScan.Enabled = false;
                this.UseWaitCursor = false;
                lblStatus.Text = StringValue.Ready;
            }            
        }

        private void schedule_ItemUpdated(object sender, Utility.ScheduleEventArgs e)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    schedule_ItemUpdated(sender, e);
                };
                this.Invoke(del);
            }
            else
            {
                int idx = -1;
                bool found = false;
                ListViewItem lvw = null;
                if (lvwSchedule.Items.Count > 0)
                {
                    do
                    {
                        idx++;
                        lvw = lvwSchedule.Items[idx];
                        if (lvw.Tag != null)
                        {
                            if ((int)lvw.Tag == e.Schedule.Index)
                            {
                                found = true;
                            }
                        }                        
                    } while (idx < lvwSchedule.Items.Count && !found);
                    if (found && lvw != null)
                    {
                        lvw.SubItems[(int)ScheduleColumns.LastRun].Text = e.Schedule.LastRunTime;
                    }
                }                
            }
        }

        private void schedule_ScriptInvoked(object sender, Utility.ScheduleEventArgs e)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    schedule_ScriptInvoked(sender, e);
                };
                this.Invoke(del);
            }
            else
            {
                RunScript(e.Schedule);
            }
        }

        private void ScheduleScript()
        {
            try
            {
                List<PShell.psparameter> scriptparams;
                ListViewItem lvw = lvwScripts.SelectedItems[0];
                PShell.pscript psc = new PShell.pscript();
                psc.ParentForm = this;
                scriptparams = psc.CheckForParams(lvw.Tag.ToString());

                if (!psc.ParamSelectionCancelled)
                {
                    Utility.ScheduleItem sitm = new Utility.ScheduleItem();
                    sitm.ScriptName = lvw.Text;
                    sitm.ScriptPath = lvw.Tag.ToString();
                    sitm.RunAs = Enums.EnumValues.RunAs.CurrentUser;
                    if (scriptparams != null && scriptparams.Count > 0)
                    {
                        foreach (PShell.psparameter prm in scriptparams)
                        {
                            sitm.Parameters.Properties.Add(prm);
                        }
                    }
                    Interface.frmSchedule sched = new Interface.frmSchedule();
                    if (sched.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        sitm.ScheduledTime = sched.ScheduledTime;
                        sitm.Index = GetScheduleIndex();
                        schedule.ScheduleItems.Add(sitm);
                        if (schedule.Save())
                        {
                            LoadSchedule();
                        }
                        else
                        {
                            MessageBox.Show("Error saving schedule: " + schedule.LastException.Message);
                        }
                    }
                    sched = null;
                }                
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        private int GetScheduleIndex()
        {
            int idx = 0;
            if (lvwSchedule.Items.Count > 0)
            {
                foreach (ListViewItem itm in lvwSchedule.Items)
                {
                    if ((int)itm.Tag > idx)
                    { 
                        idx = (int)itm.Tag;
                    }
                }
            }
            idx++;
            return idx;
        }

        private String GetScheduleText(Utility.ScheduleTime schedtime)
        { 
            String rtn = "";
            switch(schedtime.Frequency)
            {
                case Enums.EnumValues.TimeFrequency.Daily:
                    rtn = schedtime.Frequency.ToString();
                    break;
                case Enums.EnumValues.TimeFrequency.Weekly:
                    String daystr = "Every ";
                    if (schedtime.DaysofWeek.Count > 0)
                    {
                        foreach (DayOfWeek dayidx in schedtime.DaysofWeek)
                        {
                            daystr += dayidx.ToString() + ", ";
                        }
                        daystr = daystr.Substring(0, daystr.Length - 2);
                    }
                    rtn += daystr;
                    break;
                case Enums.EnumValues.TimeFrequency.Monthly:
                    String monstr = "Every ";
                    if (schedtime.Months.Count > 0)
                    {
                        foreach (int monidx in schedtime.Months)
                        {
                            monstr += DateTimeFormatInfo.CurrentInfo.GetMonthName(monidx) + ", ";
                        }
                        monstr = monstr.Substring(0, monstr.Length - 2);
                    }
                    monstr += " on the ";
                    if (schedtime.Dates.Count > 0)
                    {
                        foreach (int day in schedtime.Dates)
                        {
                            if (day == 32)
                            {
                                monstr += "Last Day, ";
                            }
                            else
                            {
                                monstr += day.ToString() + GetOrdinalSuffix(day) + ", ";
                            }
                        }
                        monstr = monstr.Substring(0, monstr.Length - 2);
                    }
                    rtn += monstr;
                    break;
            }
            rtn += " at " + schedtime.StartTime.ToString("hh:mm tt");
            return rtn;
        }

        private String GetOrdinalSuffix(int day)
        {
            String rtn = "th";
            switch (day)
            { 
                case 1: case 21: case 31:
                    rtn = "st";
                    break;
                case 2: case 22:
                    rtn = "nd";
                    break;
                case 3: case 23:
                    rtn = "rd";
                    break;
            }
            return rtn;
        }

        private void DeleteScheduleItems()
        {
            if (MessageBox.Show(StringValue.ConfirmScheduleDelete, "Confirm Delete", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (ListViewItem lvw in lvwSchedule.SelectedItems)
                {
                    schedule.Remove((int)lvw.Tag);
                }
                schedule.Save();
                int idx = 0;
                do
                {
                    if (lvwSchedule.Items[idx].Selected)
                    {
                        lvwSchedule.Items.RemoveAt(idx);
                    }
                    else
                    {
                        idx++;
                    }
                } while (idx < lvwSchedule.Items.Count);
            }
        }

        private void RunScript()
        {
            if (lvwScripts.SelectedItems.Count > 0)
            {
                ListViewItem lvw = lvwScripts.SelectedItems[0];
                //This needs to be a separate runspace.
                PShell.pshell ps = new PShell.pshell();
                ps.ParentForm = this;
                if (lvw.Group.Header != null && lvw.Group.Header != "General")
                {
                    ps.Run(Path.Combine(lvw.Group.Header, lvw.Text));
                }
                else
                {
                    ps.Run(lvw.Text);
                }                
                ps = null;
            }
        }

        private void RunScript(Utility.ScheduleItem sched)
        {
            if (sched != null)
            {
                PShell.pshell ps = new PShell.pshell();
                ps.ParentForm = this;
                ps.Run(sched);
            }
        }

        private void ViewScript()
        {
            try
            {
                if (lvwScripts.SelectedItems.Count > 0)
                {
                    ListViewItem lvw = lvwScripts.SelectedItems[0];
                    String script = (String)lvw.Tag;
                    if (File.Exists(script))
                    {
                        ShellOpenCommand(script);
                    }
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }
        #endregion

        #region Status
        public void SetStatus(String message)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    SetStatus(message);
                };
                this.Invoke(del);
            }
            else
            {
                lblStatus.Text = message;
            }
        }
        #endregion

        #region ProgressBar
        public void SetProgress(int Value, int Maximum)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    SetProgress(Value, Maximum);
                };
                this.Invoke(del);
            }
            else
            {
                pbStatus.Visible = true;
                if (pbStatus.Maximum != Maximum)
                {
                    pbStatus.Maximum = Maximum;
                }
                if (Value <= Maximum)
                {
                    pbStatus.Value = Value;
                }
            }            
        }

        public void HideProgress()
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    HideProgress();
                };
                this.Invoke(del);
            }
            else
            {
                pbStatus.Visible = false;
            }            
        }
        #endregion

        #region "PowerShell"
        public void RemoveActiveScript(ListViewItem lvw)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    RemoveActiveScript(lvw);
                };
                this.Invoke(del);
            }
            else
            {
                if (lvw != null)
                {
                    lvw.Remove();
                    tbpScripts.Text = StringValue.ActiveScripts + " (" + lvwActiveScripts.Items.Count.ToString() + ")";
                }
            }
        }
        public void DisplayOutput(String output, ListViewItem lvw, bool clicked, bool cancelled = false, bool scroll = false)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    DisplayOutput(output, lvw, clicked, cancelled, scroll);
                };
                this.Invoke(del);
            }
            else
            {
                if ((txtPShellOutput.Text.Length + output.Length + (Environment.NewLine + StringValue.psf).Length) > txtPShellOutput.MaxLength)
                {
                    txtPShellOutput.Text = txtPShellOutput.Text.Substring(output.Length + 500, txtPShellOutput.Text.Length - (output.Length + 500));
                }
                txtPShellOutput.AppendText(output);
                txtPShellOutput.AppendText(Environment.NewLine + StringValue.psf);
                mincurpos = txtPShellOutput.Text.Length;
                txtPShellOutput.SelectionStart = mincurpos;
                if (clicked || cancelled || scroll)
                {
                    //Not sure why this happens, but if you type the command the scroll to caret isn't needed.
                    //If you initiate a script or command by double clicking, or you abort the thread, you do.
                    txtPShellOutput.ScrollToCaret();
                }
                txtPShellOutput.Select();
                txtPShellOutput.ReadOnly = false;
                RemoveActiveScript(lvw);                
            }            
        }

        public void AddTabPage(TabPage NewTabPage)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    AddTabPage(NewTabPage);
                };
                this.Invoke(del);
            }
            else
            {
                try
                {
                    tcMain.TabPages.Add(NewTabPage);
                }
                catch (Exception e)
                {
                    DisplayError(e);
                }
            }            
        }

        public void AddAlert(String message, PShell.psmethods.PSAlert.AlertType alerttype, String scriptname)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    AddAlert(message, alerttype, scriptname);
                };
                this.Invoke(del);
            }
            else 
            {
                try
                {
                    ListViewItem lvwitm = new ListViewItem();
                    lvwitm.Text = alerttype.ToString();
                    lvwitm.ImageIndex = (int)alerttype;
                    lvwitm.SubItems.Add(message);
                    lvwitm.SubItems.Add(DateTime.Now.ToString(StringValue.TimeFormat));
                    lvwitm.SubItems.Add(scriptname);                    
                    lvwAlerts.Items.Add(lvwitm);                    
                    lvwAlerts_Update();
                    lvwitm.EnsureVisible();
                }
                catch (Exception e)
                {
                    DisplayError(e);
                }                
            }
        }

        public void UpdateStatus(String message, ListViewItem lvw)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    UpdateStatus(message, lvw);
                };
                this.Invoke(del);
            }
            else
            {
                try
                {
                    lvw.SubItems[1].Text = message;
                    lvwScripts.Refresh();
                }
                catch (Exception e)
                {
                    DisplayError(e);
                }
            }
        }

        public Collection<PSObject> GetCheckedHosts()
        {            
            if (this.InvokeRequired)
            {
                return (Collection<PSObject>)this.Invoke((Func<Collection<PSObject>>) delegate 
                {
                    return GetCheckedHosts();
                });
            }
            else
            {
                Collection<PSObject> hosts = new Collection<PSObject>();
                ListView.CheckedListViewItemCollection lvwitms = lvwSystems.CheckedItems;
                if (lvwitms != null && lvwitms.Count > 0)
                {
                    foreach (ListViewItem lvw in lvwitms)
                    {
                        PSObject pobj = new PSObject();
                        int idx = -1;
                        foreach (ColumnHeader col in lvwSystems.Columns)
                        {
                            idx++;
                            pobj.Properties.Add(new PSNoteProperty(col.Text.Replace(" ", "_"), lvw.SubItems[idx].Text));
                        }                        
                        hosts.Add(pobj);
                    }
                }
                return hosts;
            }
        }

        public Collection<PSObject> GetHosts()
        {
            if (this.InvokeRequired)
            {
                return (Collection<PSObject>)this.Invoke((Func<Collection<PSObject>>)delegate
                {
                    return GetHosts();
                });
            }
            else
            {
                Collection<PSObject> hosts = new Collection<PSObject>();
                ListView.ListViewItemCollection lvwitms = lvwSystems.Items;
                if (lvwitms != null && lvwitms.Count > 0)
                {
                    foreach (ListViewItem lvw in lvwitms)
                    {
                        PSObject pobj = new PSObject();
                        int idx = -1;
                        foreach (ColumnHeader col in lvwSystems.Columns)
                        {
                            idx++;
                            pobj.Properties.Add(new PSNoteProperty(col.Text.Replace(" ", "_"), lvw.SubItems[idx].Text));
                        }
                        hosts.Add(pobj);
                    }
                }
                return hosts;
            }
        }

        public void AddActiveScript(ListViewItem lvw)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    AddActiveScript(lvw);
                };
                this.Invoke(del);
            }
            else
            {
                lvwActiveScripts.Items.Add(lvw);
                tbpScripts.Text = StringValue.ActiveScripts + "(" + lvwActiveScripts.Items.Count.ToString() + ")";
            }
        }

        private void ShellOpenCommand(String cmd)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(cmd);
                psi.UseShellExecute = true;
                psi.Verb = "open";
                System.Diagnostics.Process prc = new System.Diagnostics.Process();
                prc.StartInfo = psi;
                prc.Start();
                prc = null;
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        private void ProcessCommand(String cmd)
        {
            try
            {
                if (cmd.Trim() != "")
                {
                    cmdhist.Add(cmd);
                }                
                cmdhistidx = cmdhist.Count;
                switch (cmd.ToUpper())
                { 
                    case StringValue.CLS: case StringValue.Clear:
                        txtPShellOutput.Text = StringValue.psf;
                        txtPShellOutput.SelectionStart = txtPShellOutput.Text.Length;
                        mincurpos = txtPShellOutput.Text.Length;
                        break;
                    case StringValue.AptGetUpdate:
                        txtPShellOutput.AppendText(Environment.NewLine + StringValue.psf);
                        txtPShellOutput.SelectionStart = txtPShellOutput.Text.Length;
                        mincurpos = txtPShellOutput.Text.Length;
                        ShellOpenCommand("wuapp");
                        break; 
                    case StringValue.Reload:
                        if (lvwActiveScripts.Items.Count == 0)
                        {
                            Initialize();
                        }
                        else 
                        {
                            txtPShellOutput.AppendText(Environment.NewLine + StringValue.ReloadScriptsRunning + Environment.NewLine);
                            txtPShellOutput.AppendText(Environment.NewLine + StringValue.psf);
                            mincurpos = txtPShellOutput.Text.Length;
                        }
                        break;
                    case StringValue.Exit:
                        this.Close();
                        break;
                    default:
                        txtPShellOutput.AppendText(Environment.NewLine);
                        mincurpos = txtPShellOutput.Text.Length;
                        txtPShellOutput.ReadOnly = true;
                        psf.Run(cmd, true, false);
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("ProcessCommand Unhandled Exception: " + e.Message + Environment.NewLine + "Stack Trace:" + Environment.NewLine);
            }
        }

        private void LoadSchedule()
        {
            if (schedule.Load())
            {
                if (schedule.ScheduleItems != null && schedule.ScheduleItems.Count > 0)
                {
                    lvwSchedule.Items.Clear();
                    lvwSchedule.BeginUpdate();
                    foreach (Utility.ScheduleItem sitm in schedule.ScheduleItems)
                    {
                        ListViewItem lvw = new ListViewItem();
                        lvw.ImageIndex = 5;
                        lvw.Text = sitm.ScriptName;
                        lvw.Tag = sitm.Index;
                        String parms = "";
                        if (sitm.Parameters.Properties != null && sitm.Parameters.Properties.Count > 0)
                        {
                            foreach (PShell.psparameter prop in sitm.Parameters.Properties)
                            {
                                parms += prop.Name + "=" + (prop.Value ?? prop.DefaultValue ?? "").ToString() + ", ";
                            }
                            parms = parms.Substring(0, parms.Length - 2);
                        }
                        lvw.SubItems.Add(parms);
                        if (sitm.ScheduledTime != null)
                        {
                            lvw.SubItems.Add(GetScheduleText(sitm.ScheduledTime));
                        }
                        else
                        {
                            lvw.SubItems.Add("");
                        }
                        lvw.SubItems.Add(sitm.RunAs.ToString());
                        lvw.SubItems.Add(sitm.LastRunTime);
                        lvw.SubItems.Add(sitm.Message);
                        lvwSchedule.Items.Add(lvw);
                    }
                    lvwSchedule.EndUpdate();
                }
            }
            else
            {
                if (schedule.LastException != null)
                {
                    MessageBox.Show("Error loading schedule: " + schedule.LastException.Message);
                }                
            }
        }

        private void CheckSettings()
        {
            //Ensure we have settings and that if it's .\ to change to application path.
            String scrpath = poshsecframework.Properties.Settings.Default.ScriptPath;
            String frwpath = poshsecframework.Properties.Settings.Default.FrameworkPath;
            String modpath = poshsecframework.Properties.Settings.Default.ModulePath;
            String schpath = poshsecframework.Properties.Settings.Default.ScheduleFile;
            if (scrpath.StartsWith(".") || scrpath.Trim() == "")
            {
                poshsecframework.Properties.Settings.Default["ScriptPath"] = Path.Combine(Application.StartupPath, scrpath).Replace("\\.\\", "\\");
            }
            if (frwpath.StartsWith(".") || frwpath.Trim() == "")
            {
                poshsecframework.Properties.Settings.Default["FrameworkPath"] = Path.Combine(Application.StartupPath, frwpath).Replace("\\.\\", "\\");
            }
            if (modpath.StartsWith(".") || modpath.Trim() == "")
            {
                poshsecframework.Properties.Settings.Default["ModulePath"] = Path.Combine(Application.StartupPath, modpath).Replace("\\.\\", "\\");
            }
            if (schpath.StartsWith(".") || modpath.Trim() == "")
            {
                poshsecframework.Properties.Settings.Default["ScheduleFile"] = Path.Combine(Application.StartupPath, schpath).Replace("\\.\\", "\\");
            }
            poshsecframework.Properties.Settings.Default.Save();
            poshsecframework.Properties.Settings.Default.Reload();
        }

        private void GetCommand()
        {
            try
            {
                PShell.pscript ps = new PShell.pscript();
                ps.ParentForm = this;
                Collection<PSObject> rslt = ps.GetCommand();
                ps = null;
                if (rslt != null)
                {
                    List<String> accmds = new List<String>();
                    lvwCommands.Items.Clear();
                    lvwCommands.BeginUpdate();
                    foreach (PSObject po in rslt)
                    {
                        ListViewItem lvw = null;
                        switch (po.BaseObject.GetType().Name)
                        {
                            case "AliasInfo":
                                AliasInfo ai = (AliasInfo)po.BaseObject;
                                if (btnShowAliases.Checked)
                                {
                                    lvw = new ListViewItem();
                                    lvw.Text = ai.Name;
                                    lvw.ToolTipText = ai.Name;
                                    lvw.SubItems.Add(ai.ModuleName);
                                    lvw.ImageIndex = (int)LibraryImages.Alias;
                                    accmds.Add(ai.Name);
                                }
                                break;
                            case "FunctionInfo":
                                FunctionInfo fi = (FunctionInfo)po.BaseObject;
                                if (btnShowFunctions.Checked)
                                {
                                    lvw = new ListViewItem();
                                    lvw.Text = fi.Name;
                                    lvw.ToolTipText = fi.Name;
                                    lvw.SubItems.Add(fi.ModuleName);
                                    lvw.ImageIndex = (int)LibraryImages.Function;
                                    accmds.Add(fi.Name);
                                }
                                break;
                            case "CmdletInfo":
                                CmdletInfo cmi = (CmdletInfo)po.BaseObject;
                                if (btnShowCmdlets.Checked)
                                {
                                    lvw = new ListViewItem();
                                    lvw.Text = cmi.Name;
                                    lvw.ToolTipText = cmi.Name;
                                    lvw.SubItems.Add(cmi.ModuleName);
                                    lvw.ImageIndex = (int)LibraryImages.Cmdlet;
                                    accmds.Add(cmi.Name);
                                }
                                break;
                            default:
                                Console.WriteLine(po.BaseObject.GetType().Name);
                                break;
                        }
                        if (lvw != null && (cmbLibraryTypes.Text == "All" || cmbLibraryTypes.Text.ToLower() == lvw.SubItems[1].Text.ToLower()))
                        {
                            lvwCommands.Items.Add(lvw);
                        }
                        else
                        {
                            lvw = null;
                        }
                    }
                    lvwCommands.EndUpdate();
                    accmds.Sort();
                    txtPShellOutput.AutoCompleteCommands = accmds;
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        private void GetLibrary()
        {
            try
            {
                lvwScripts.BeginUpdate();
                lvwScripts.Items.Clear();
                String scriptroot = poshsecframework.Properties.Settings.Default["ScriptPath"].ToString();
                if (Directory.Exists(scriptroot))
                {
                    AddLibraryItem(scriptroot);
                }
                else
                { 
                    AddAlert(StringValue.ScriptPathError, PShell.psmethods.PSAlert.AlertType.Error, StringValue.psftitle);
                }
                lvwScripts.EndUpdate();
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        private void AddLibraryItem(String scriptroot, String group = "General")
        {
            String[] scpaths = Directory.GetFiles(scriptroot, "*.ps1", SearchOption.TopDirectoryOnly);
            if (scpaths != null)
            {
                ListViewGroup lvwg = new ListViewGroup(group);
                lvwScripts.Groups.Add(lvwg);
                foreach (String scpath in scpaths)
                {
                    ListViewItem lvw = new ListViewItem();
                    lvw.Text = new FileInfo(scpath).Name;
                    lvw.ImageIndex = 4;
                    lvw.Tag = scpath;
                    lvw.Group = lvwg;
                    lvwScripts.Items.Add(lvw);
                }
            }
            String[] folders = Directory.GetDirectories(scriptroot);
            if (folders != null && folders.Length > 0)
            {
                foreach (String folder in folders)
                { 
                    DirectoryInfo diri = new DirectoryInfo(folder);
                    AddLibraryItem(folder, diri.Name);
                    diri = null;
                }
            }
        }
        #endregion

        #region " Display Error "
        private void DisplayError(Exception e)
        {
            DisplayOutput(Environment.NewLine + "Unhandled exception." + Environment.NewLine + e.Message + Environment.NewLine + "Stack Trace:" + Environment.NewLine + e.StackTrace, null, true);
            tcMain.SelectedTab = tbpPowerShell;
        }
        #endregion

        #endregion

        #region Public Methods

        #region Interface
        public System.Windows.Forms.DialogResult ShowParams(poshsecframework.PShell.psparamtype parm)
        {
            if (this.InvokeRequired)
            {
                return (System.Windows.Forms.DialogResult)this.Invoke((Func<System.Windows.Forms.DialogResult>)delegate
                {
                    return ShowParams(parm);
                });
            }
            else
            {
                DialogResult rslt = System.Windows.Forms.DialogResult.Cancel;
                Interface.frmParams frmi = new Interface.frmParams();
                frmi.SetParameters(parm);
                rslt = frmi.ShowDialog(this);
                return rslt;
            }
        }
        #endregion

        #endregion

        #region Private Events

        #region Menu Clicks
        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void mnuScan_Click(object sender, EventArgs e)
        {
            Scan();
        }

        private void mnuCheckforUpdates_Click(object sender, EventArgs e)
        {
            try
            {
                String wurl = StringValue.UpdateURI;
                ShellOpenCommand(wurl);
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void mnuPSFWiki_Click(object sender, EventArgs e)
        {
            try
            {
                String wurl = StringValue.WikiURI;
                ShellOpenCommand(wurl);
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void mnuOptions_Click(object sender, EventArgs e)
        {
            if (lvwActiveScripts.Items.Count == 0)
            {
                poshsecframework.Interface.frmSettings frm = new poshsecframework.Interface.frmSettings();
                System.Windows.Forms.DialogResult rslt = frm.ShowDialog();
                frm.Dispose();
                frm = null;
                if (rslt == System.Windows.Forms.DialogResult.OK)
                {
                    Initialize();
                };
            }
            else
            {
                MessageBox.Show(StringValue.SettingScriptsRunning);
            }
        }

        private void mnuDeleteScheduleItem_Click(object sender, EventArgs e)
        {
            DeleteScheduleItems();
        }
        #endregion 

        #region List View
        private void lvwAlerts_Update()
        {
            tbpAlerts.Text = "Alerts (" + lvwAlerts.Items.Count.ToString() + ")";
        }

        private void lvwScripts_DoubleClick(object sender, EventArgs e)
        {
            switch (poshsecframework.Properties.Settings.Default.ScriptDefaultAction)
            {
                case 0:
                    RunScript();
                    break;
                case 1:
                    ViewScript();
                    break;
            }
        }

        private void lvwCommands_DoubleClick(object sender, EventArgs e)
        {
            if (lvwCommands.SelectedItems.Count > 0 && !txtPShellOutput.ReadOnly)
            {
                ListViewItem lvw = lvwCommands.SelectedItems[0];
                txtPShellOutput.ReadOnly = true;
                psf.Run(lvw.Text, true);
            }
            else if (txtPShellOutput.ReadOnly)
            {
                txtPShellOutput.AppendText(Environment.NewLine + StringValue.CommandRunning + Environment.NewLine + Environment.NewLine);
                txtPShellOutput.AppendText(StringValue.psf);
                mincurpos = txtPShellOutput.Text.Length;
                txtPShellOutput.SelectionStart = mincurpos;
                tcMain.SelectedTab = tbpPowerShell;
            } 
        }

        private void lvwScripts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwScripts.SelectedItems.Count > 0)
            {
                btnViewScript.Enabled = true;
                btnRunScript.Enabled = true;
                btnSchedScript.Enabled = true;
            }
            else
            {
                btnViewScript.Enabled = false;
                btnRunScript.Enabled = false;
                btnSchedScript.Enabled = false;
            }
        }

        private void lvwSchedule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lvwSchedule.SelectedItems.Count > 0)
            {
                DeleteScheduleItems();
            }
        }
        #endregion

        #region TextBox
        private void txtPShellOutput_KeyDown(object sender, KeyEventArgs e)
        {
            //This code is required to emulate a powershell command prompt.
            //otherwise it's just a textbox.
            switch (e.KeyCode)
            {
                case Keys.Back:
                    if (txtPShellOutput.SelectionStart <= mincurpos)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;
                case Keys.Delete:
                    if (txtPShellOutput.SelectionStart < mincurpos)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;
                case Keys.Left:
                    if (txtPShellOutput.SelectionStart <= mincurpos)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        txtPShellOutput.SelectionStart = mincurpos;                    
                    }
                    break;
                case Keys.Home:
                    e.Handled = true;
                    e.SuppressKeyPress = true;                   
                    txtPShellOutput.SelectionStart = mincurpos;
                    txtPShellOutput.ScrollToCaret();
                    break;
                case Keys.End:
                    e.Handled = true;
                    e.SuppressKeyPress = true;                   
                    txtPShellOutput.SelectionStart = txtPShellOutput.Text.Length;
                    txtPShellOutput.ScrollToCaret();
                    break;
                case Keys.Enter:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    if (!txtPShellOutput.ReadOnly)
                    {
                        String cmd = txtPShellOutput.Text.Substring(mincurpos, txtPShellOutput.Text.Length - mincurpos);
                        ProcessCommand(cmd);
                    }
                    break;
                case Keys.ControlKey: case Keys.Alt:
                    e.SuppressKeyPress = false;
                    e.Handled = false;
                    break;
                case Keys.Up:                    
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    if (cmdhist != null && cmdhist.Count > 0)
                    {
                        if (cmdhistidx <= cmdhist.Count && cmdhistidx > 0)
                        {
                            cmdhistidx -= 1;
                            txtPShellOutput.Text = txtPShellOutput.Text.Substring(0, mincurpos);
                            txtPShellOutput.AppendText(cmdhist[cmdhistidx]);
                            txtPShellOutput.SelectionStart = txtPShellOutput.Text.Length;
                        }
                    }
                    break;
                case Keys.Down:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    if (cmdhist != null && cmdhist.Count > 0)
                    {                       
                        if (cmdhistidx >= 0 && cmdhistidx < cmdhist.Count - 1)
                        {
                            cmdhistidx += 1;
                            txtPShellOutput.Text = txtPShellOutput.Text.Substring(0, mincurpos);
                            txtPShellOutput.AppendText(cmdhist[cmdhistidx]);
                            txtPShellOutput.SelectionStart = txtPShellOutput.Text.Length;
                            
                        }
                        else 
                        {
                            txtPShellOutput.Text = txtPShellOutput.Text.Substring(0, mincurpos);
                            txtPShellOutput.SelectionStart = txtPShellOutput.Text.Length;
                            cmdhistidx = cmdhist.Count;
                        }
                    }
                    break;
                case Keys.L: case Keys.C: case Keys.X: case Keys.V:
                    if (e.Control)
                    {
                        switch (e.KeyCode)
                        { 
                            case Keys.L:
                                //Ctrl+L for CLS!
                                e.Handled = true;
                                e.SuppressKeyPress = true;
                                txtPShellOutput.Text = StringValue.psf;
                                mincurpos = txtPShellOutput.Text.Length;
                                txtPShellOutput.SelectionStart = txtPShellOutput.Text.Length;
                                txtPShellOutput.ScrollToCaret();
                                break;
                            case Keys.V:
                                txtPShellOutput.SelectionStart = txtPShellOutput.Text.Length;
                                e.Handled = false;
                                e.SuppressKeyPress = false;
                                break;
                            default:
                                e.Handled = false;
                                e.SuppressKeyPress = false;
                                break;
                        }                        
                    }
                    break;
                case Keys.Tab:
                    e.Handled = true;
                    break;
                default:
                    if (txtPShellOutput.SelectionStart < mincurpos)
                    {
                        txtPShellOutput.SelectionStart = txtPShellOutput.Text.Length;
                        txtPShellOutput.ScrollToCaret();
                    }
                    break;
            }
        }
        #endregion

        #region Button Clicks
        private void btnLibraryRefresh_Click(object sender, EventArgs e)
        {
            GetCommand();
        }

        private void btnShowAliases_Click(object sender, EventArgs e)
        {
            btnShowAliases.Checked = !btnShowAliases.Checked;
            GetCommand();
        }

        private void btnShowFunctions_Click(object sender, EventArgs e)
        {
            btnShowFunctions.Checked = !btnShowFunctions.Checked;
            GetCommand();
        }

        private void btnShowCmdlets_Click(object sender, EventArgs e)
        {
            btnShowCmdlets.Checked = !btnShowCmdlets.Checked;
            GetCommand();
        }

        private void cmnuActiveScripts_Opening(object sender, CancelEventArgs e)
        {
            if (lvwActiveScripts.SelectedItems.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void cmnuScheduleCommands_Opening(object sender, CancelEventArgs e)
        {
            if (lvwSchedule.Items.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void cmnuScripts_Opening(object sender, CancelEventArgs e)
        {
            if (lvwScripts.SelectedItems.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void cmbtnCancelScript_Click(object sender, EventArgs e)
        {
            if (lvwActiveScripts.SelectedItems.Count > 0)
            {
                ListViewItem lvw = lvwActiveScripts.SelectedItems[0];
                Thread thd = (Thread)lvw.Tag;
                thd.Abort();
                lvw.SubItems[1].Text = "Aborting... ThreadState = " + thd.ThreadState.ToString();
            }
            else
            {
                MessageBox.Show(StringValue.SelectActiveScript);
            }
        }

        private void btnClearAlerts_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(StringValue.ClearAlerts, "Confirm", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                lvwAlerts.Items.Clear();
                lvwAlerts_Update();
            }
        }

        private void btnCancelScan_Click(object sender, EventArgs e)
        {
            cancelscan = true;
        }

        private void btnRefreshNetworks_Click(object sender, EventArgs e)
        {
            GetNetworks();
        }

        private void btnRefreshScripts_Click(object sender, EventArgs e)
        {
            GetLibrary();
        }

        private void btnViewScript_Click(object sender, EventArgs e)
        {
            ViewScript();
        }

        private void btnRunScript_Click(object sender, EventArgs e)
        {
            RunScript();
        }

        private void btnSchedScript_Click(object sender, EventArgs e)
        {
            ScheduleScript();
        }

        private void cmbtnSchedScript_Click(object sender, EventArgs e)
        {
            ScheduleScript();
        }

        private void cmbtnRunScript_Click(object sender, EventArgs e)
        {
            RunScript();
        }

        private void cmbtnViewScript_Click(object sender, EventArgs e)
        {
            ViewScript();
        }

        private void btnAddNetwork_Click(object sender, EventArgs e)
        {
            MessageBox.Show(StringValue.NotImplemented);
        }

        private void btnAddSystem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(StringValue.NotImplemented);           
        }

        private void mnuCmdGetHelp_Click(object sender, EventArgs e)
        {
            if (lvwCommands.SelectedItems.Count > 0)
            {
                ListViewItem lvw = lvwCommands.SelectedItems[0];
                String ghcmd = StringValue.GetHelpFull.Replace("{0}", lvw.Text);
                txtPShellOutput.AppendText(ghcmd + Environment.NewLine);
                txtPShellOutput.ReadOnly = true;
                psf.Run(ghcmd, true, false, true);
                tcMain.SelectedTab = tbpPowerShell;
            }
        }

        private void mnuScriptGetHelp_Click(object sender, EventArgs e)
        {
            if (lvwScripts.SelectedItems.Count > 0)
            {
                ListViewItem lvw = lvwScripts.SelectedItems[0];
                String ghcmd = StringValue.GetHelpFull.Replace("{0}","\"" + lvw.Tag + "\"");
                txtPShellOutput.AppendText(ghcmd + Environment.NewLine);
                txtPShellOutput.ReadOnly = true;
                psf.Run(ghcmd, true, false, true);
                tcMain.SelectedTab = tbpPowerShell;
            }
        }
        #endregion

        #region ComboBox Events
        private void cmbLibraryTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCommand();
        }
        #endregion

        private void tcMain_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tbpPowerShell)
            {
                txtPShellOutput.Select();
                txtPShellOutput.DrawCaret();
            }
        }

        #endregion

        #region Public Properties
        public bool CancelIPScan
        {
            get { return cancelscan; }
        }
        #endregion
    }
}