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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using poshsecframework.Strings;
using poshsecframework.Enums;

namespace poshsecframework
{
    public partial class frmMain : Form
    {
        #region Private Variables 
        Network.NetworkBrowser scnr = new Network.NetworkBrowser();
        Interface.frmStartup stat = null;
        private int mincurpos = 6;
        private Collection<String> cmdhist = new Collection<string>();
        private int cmdhistidx = -1;
        private PShell.pshell psf;
        private bool cancelscan = false;
        private bool restart = false;
        private bool shown = false;
        private Utility.Schedule schedule = new Utility.Schedule(1000);
        private string loaderrors = "";
        private Collection<String> enabledmods = new Collection<string>();
        private int updatefrequency = 12; // in hours
        private Collection<ListViewItem> alerts = new Collection<ListViewItem>();
        private Network.Syslog slog = null;
        private Comparers.ListViewColumnSorter lvwSorter = new Comparers.ListViewColumnSorter();
        private bool closing = false;
        private FormWindowState lastwindowstate = FormWindowState.Normal;

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
            lvwSystems.ListViewItemSorter = lvwSorter;
            this.Enabled = false;
            stat = new Interface.frmStartup();
            stat.Show();
            stat.Refresh();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {            
            stat.SetStatus("Initializing, please wait...");
            scnr.ScanComplete += scnr_ScanComplete;
            scnr.ScanCancelled += scnr_ScanCancelled;
            scnr.ScanUpdate += scnr_ScanUpdate;
            schedule.ItemUpdated += schedule_ItemUpdated;
            schedule.ScriptInvoked += schedule_ScriptInvoked;
            schedule.ScheduleRemoved += schedule_ScheduleRemoved;

            stat.SetStatus("Checking Settings, please wait...");
            CheckSettings();
            if (poshsecframework.Properties.Settings.Default.FirstTime)
            {
                restart = true;
                stat.Hide();
                FirstTimeSetup();
            }
            if (restart)
            {
                Application.Restart();
                this.Close();
            }
            else
            {
                Initialize();
                stat.Show();
                stat.SetStatus("Loading Networks, please wait...");
                GetNetworks();
                GetAlerts();
            }
            if (loaderrors != "")
            {
                DisplayOutput(StringValue.ImportError + Environment.NewLine + loaderrors, null, false, false, false, true);
            }
            shown = true;
            stat.Close();
            stat.Dispose();
            stat = null;
            this.Enabled = true;
            this.Focus();
            schedule.Start();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            closing = true;
            try
            {
                if (lvwActiveScripts.Items.Count > 0)
                {
                    if (MessageBox.Show(StringValue.ActiveScriptsRunning, "Active Scripts", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        foreach (ListViewItem lvw in lvwActiveScripts.Items)
                        {
                            int times = 0;
                            Thread thd = (Thread)lvw.Tag;
                            thd.Abort();
                            do
                            {
                                System.Threading.Thread.Sleep(1000);
                                times++;
                            } while ((thd.ThreadState != ThreadState.Aborted) && times <=5);
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        closing = false;
                    }
                }
                SaveSystems();
                SaveAlerts();
                Properties.Settings.Default.Save();
            }
            catch (Exception)
            {
                e.Cancel = false;
                closing = true;
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                nimain.Visible = true;
            }
            else
            {
                lastwindowstate = this.WindowState;
                this.ShowInTaskbar = true;
                nimain.Visible = false;
            }
        }
        #endregion

        #region Private Methods

        private void Initialize()
        {
            if (stat != null) { stat.SetStatus("Looking for modules, please wait..."); }
            CheckPendingModules();            
            BuildModuleFilter();
            if (stat != null) { stat.SetStatus("Checking for updates, please wait..."); }
            CheckLastModified();
            cmbLibraryTypes.SelectedIndex = 0;
            if (stat != null) { stat.SetStatus("Initializing PowerShell, please wait..."); }
            psf = new PShell.pshell(this);
            psf.ImportPSModules(enabledmods);
            psf.ParentForm = this;
            scnr.ParentForm = this; 
            if (psf.LoadErrors != "")
            {
                loaderrors += psf.LoadErrors;
            }
            txtPShellOutput.Text = StringValue.psf;
            mincurpos = txtPShellOutput.Text.Length;
            txtPShellOutput.SelectionStart = mincurpos;
            if (stat != null) { stat.SetStatus("Loading script library, please wait..."); }
            GetLibrary();
            if (stat != null) { stat.SetStatus("Getting commands, please wait..."); }
            GetCommand();
            if (stat != null) { stat.SetStatus("Loading schedule library, please wait..."); }
            LoadSchedule();
            InitSyslog();
        }

        private void InitSyslog()
        {
            try
            {
                if (Properties.Settings.Default.UseSyslog && Properties.Settings.Default.SyslogServer != "")
                {
                    if(slog == null)
                    {
                        slog = new Network.Syslog(new IPEndPoint(System.Net.IPAddress.Parse(Properties.Settings.Default.SyslogServer), Properties.Settings.Default.SyslogPort));
                    }                
                }
                else
                {
                    if(slog != null)
                    {
                        slog.Close();
                        slog = null;
                    }
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        private void LogOutput(String text)
        {
            if (Properties.Settings.Default.LogOutput)
            {
                StreamWriter wtr = null;
                try
                {
                    if (!File.Exists(Properties.Settings.Default.OutputLogFile))
                    {
                        DirectoryInfo dirinfo = new DirectoryInfo(Properties.Settings.Default.OutputLogFile);
                        if (!Directory.Exists(dirinfo.Parent.FullName))
                        {
                            Directory.CreateDirectory(dirinfo.Parent.FullName);
                        }
                        wtr = File.CreateText(Properties.Settings.Default.OutputLogFile);
                        wtr.Write(StringValue.psf);
                    }
                    else
                    {
                        wtr = File.AppendText(Properties.Settings.Default.OutputLogFile);
                    }
                    wtr.Write(text);
                    wtr.Flush();
                    wtr.Close();
                }
                catch (Exception e)
                {
                    AddAlert("Unable to log output to file: " + e.Message, PShell.psmethods.PSAlert.AlertType.Error, "Logging");
                }
            }
        }

        private void LogAlert(String text)
        {
            if (Properties.Settings.Default.LogAlerts)
            {
                try
                {
                    if (!File.Exists(Properties.Settings.Default.AlertLogFile))
                    {
                        DirectoryInfo dirinfo = new DirectoryInfo(Properties.Settings.Default.AlertLogFile);
                        if (!Directory.Exists(dirinfo.Parent.FullName))
                        {
                            Directory.CreateDirectory(dirinfo.Parent.FullName);
                        }
                    }
                    StreamWriter wtr = File.AppendText(Properties.Settings.Default.AlertLogFile);
                    wtr.Write(text);
                    wtr.Flush();
                    wtr.Close();
                }
                catch (Exception e)
                {
                    AddAlert("Unable to log alerts to file: " + e.Message, PShell.psmethods.PSAlert.AlertType.Error, "Logging");
                }
            }
        }

        private void FirstTimeSetup()
        {
            Interface.frmFirstTime frm = new Interface.frmFirstTime(this);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                restart = false;
            }
            frm.Dispose();
            frm = null;
        }

        private void GetAlerts()
        {
            if (Properties.Settings.Default.Alerts != null && Properties.Settings.Default.Alerts.Count > 0)
            {
                foreach (String system in Properties.Settings.Default.Alerts)
                {
                    String[] alertparts = system.Split('|');
                    if (alertparts.Length == lvwAlerts.Columns.Count)
                    {
                        ListViewItem lvwItm = new ListViewItem();
                        lvwItm.Text = alertparts[0];
                        for (int idx = 1; idx < alertparts.Length; idx++)
                        {
                            lvwItm.SubItems.Add(alertparts[idx]);
                        }
                        PShell.psmethods.PSAlert alert = new PShell.psmethods.PSAlert(this);
                        lvwItm.ImageIndex = (int)alert.GetAlertTypeFromString(alertparts[0]);
                        alert = null;
                        lvwAlerts.Items.Add(lvwItm);
                        alerts.Add(lvwItm);
                    }
                }
                lvwAlerts_Update();
                Properties.Settings.Default["Alerts"] = new System.Collections.Specialized.StringCollection();
                ((System.Collections.Specialized.StringCollection)Properties.Settings.Default["Alerts"]).Clear();
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
            }
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

            if (Properties.Settings.Default.Systems != null && Properties.Settings.Default.Systems.Count > 0)
            {
                lvwSystems.Items.Clear();
                foreach (String system in Properties.Settings.Default.Systems)
                {
                    String[] systemparts = system.Split('|');
                    if (systemparts.Length == lvwSystems.Columns.Count)
                    {
                        ListViewItem lvwItm = new ListViewItem();
                        lvwItm.Text = systemparts[0];
                        for(int idx = 1; idx < systemparts.Length; idx++)
                        {
                            lvwItm.SubItems.Add(systemparts[idx]);
                        }
                        lvwItm.ImageIndex = 2;
                        lvwSystems.Items.Add(lvwItm);
                    }
                }
                UpdateSystemCount();
            }
            else
            {
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
                        lvwItm.SubItems.Add("");
                        lvwItm.SubItems.Add(StringValue.Up);
                        lvwItm.SubItems.Add(StringValue.NotInstalled);
                        lvwItm.SubItems.Add("0");
                        lvwItm.SubItems.Add(DateTime.Now.ToString(StringValue.TimeFormat));

                        lvwItm.ImageIndex = 2;
                        lvwSystems.Items.Add(lvwItm);
                    }
                    tvwNetworks.Nodes[0].Expand();
                    UpdateSystemCount();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + Environment.NewLine + e.StackTrace);
                }
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
            btnCancelScan.Enabled = true;
            scnr.ParentForm = this;
            cancelscan = false;
            this.UseWaitCursor = true;
            btnScan.Enabled = false;
            mnuScan.Enabled = false;
            String domain = tvwNetworks.SelectedNode.Text;
            Thread thd = new Thread(scnr.ScanActiveDirectory);
            scnr.Domain = domain;
            thd.Start();
        }

        private void ScanbyIP()
        {            
            btnCancelScan.Enabled = true;
            scnr.ParentForm = this;
            cancelscan = false;
            this.UseWaitCursor = true;
            btnScan.Enabled = false;
            mnuScan.Enabled = false;
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
                    foreach (Object system in rslts)
                    {
                        if (system != null)
                        {
                            if (system.GetType() == typeof(String))
                            {
                                string sys = (string)system;
                                if (sys != null && sys != "")
                                {
                                    ListViewItem lvwItm = new ListViewItem();
                                    String[] ipinfo = system.ToString().Split('|');
                                    SetStatus("Adding " + ipinfo[2] + ", please wait...");

                                    lvwItm.Text = ipinfo[2];
                                    lvwItm.SubItems.Add(ipinfo[1]);
                                    lvwItm.SubItems.Add(scnr.GetMac(ipinfo[1]));
                                    lvwItm.SubItems.Add("");
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
                            else
                            {
                                DirectoryEntry sys = (DirectoryEntry)system;
                                String ipadr = scnr.GetIP(sys.Name.Replace("CN=", ""));
                                String[] ips = ipadr.Split(',');
                                if(ips != null && ips.Length > 0)
                                {
                                    foreach(String ip in ips)
                                    {
                                        ListViewItem lvwItm = new ListViewItem();
                                        SetStatus("Adding " + sys.Name.Replace("CN=", "") + ", please wait...");
                                        lvwItm.Text = sys.Name.Replace("CN=", "").ToString();

                                        lvwItm.SubItems.Add(ip);
                                        string macaddr = scnr.GetMac(ip);
                                        lvwItm.SubItems.Add(macaddr);
                                        lvwItm.SubItems.Add((string)sys.Properties["description"].Value ?? "");
                                        bool isup = false;
                                        if (ipadr != StringValue.UnknownHost && macaddr != StringValue.BlankMAC)
                                        {
                                            isup = true;
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
                                    }
                                }
                                pbStatus.Value += 1;
                            }
                        }                        
                    }
                    lvwSystems.EndUpdate();
                }
                rslts = null;
                lvwSystems.Sorting = SortOrder.Ascending;
                lvwSystems.Sort();
                SaveSystems();                
                btnCancelScan.Enabled = false;
                btnScan.Enabled = true;
                mnuScan.Enabled = true;
                this.UseWaitCursor = false;
                HideProgress();
                UpdateSystemCount();
                lblStatus.Text = StringValue.Ready;
            }            
        }

        private void SaveSystems()
        {
            if (lvwSystems.Items.Count > 0 && Properties.Settings.Default.SaveSystems)
            {
                if (Properties.Settings.Default.Systems == null)
                {
                    Properties.Settings.Default["Systems"] = new System.Collections.Specialized.StringCollection();
                }
                ((System.Collections.Specialized.StringCollection)Properties.Settings.Default["Systems"]).Clear();
                foreach (ListViewItem lvw in lvwSystems.Items)
                {
                    String system = "";
                    foreach (ListViewItem.ListViewSubItem lvwsub in lvw.SubItems)
                    {
                        system += lvwsub.Text + "|";
                    }
                    if (system != "")
                    {
                        system = system.Substring(0, system.Length - 1);
                        ((System.Collections.Specialized.StringCollection)Properties.Settings.Default["Systems"]).Add(system);
                    }
                }
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
            }
        }

        private void SaveAlerts()
        {
            if (lvwAlerts.Items.Count > 0)
            {
                if (Properties.Settings.Default.Alerts == null)
                {
                    Properties.Settings.Default["Alerts"] = new System.Collections.Specialized.StringCollection();
                }
                ((System.Collections.Specialized.StringCollection)Properties.Settings.Default["Alerts"]).Clear();
                foreach (ListViewItem lvw in lvwAlerts.Items)
                {
                    String system = "";
                    foreach (ListViewItem.ListViewSubItem lvwsub in lvw.SubItems)
                    {
                        system += lvwsub.Text + "|";
                    }
                    if (system != "")
                    {
                        system = system.Substring(0, system.Length - 1);
                        ((System.Collections.Specialized.StringCollection)Properties.Settings.Default["Alerts"]).Add(system);
                    }
                }
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
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
                btnScan.Enabled = true;
                UpdateSystemCount();
                lblStatus.Text = StringValue.Ready;
            }            
        }

        void scnr_ScanUpdate(object sender, Network.ScanEventArgs e)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    scnr_ScanUpdate(sender, e);
                };
                this.Invoke(del);
            }
            else
            {
                SetProgress(e.CurrentIndex, e.MaxIndex);
                SetStatus(e.Status);
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
                int idx = 0;
                bool found = false;
                ListViewItem lvw = null;
                if (lvwSchedule.Items.Count > 0)
                {
                    do
                    {
                        lvw = lvwSchedule.Items[idx];
                        if (lvw.Tag != null)
                        {
                            if ((int)lvw.Tag == e.Schedule.Index)
                            {
                                found = true;
                            }
                        }
                        idx++;
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

        private void schedule_ScheduleRemoved(object sender, Utility.ScheduleEventArgs e)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    schedule_ScheduleRemoved(sender, e);
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
                        lvwSchedule.Items.Remove(lvw);
                    }
                }
            }
        }

        private void ScheduleScript()
        {
            try
            {
                List<PShell.psparameter> scriptparams;
                ListViewItem lvw = lvwScripts.SelectedItems[0];
                scriptparams = psf.CheckForParams(lvw.Tag.ToString());

                if (!psf.ParamSelectionCancelled)
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
            if (schedtime.StartDate != null && schedtime.StartDate.ToString() != "")
            {
                rtn = "Starting " + schedtime.StartDate.ToString("MM/dd/yyyy") + " ";
            }
            switch(schedtime.Frequency)
            {
                case Enums.EnumValues.TimeFrequency.Daily:
                    rtn += schedtime.Frequency.ToString();
                    break;
                case EnumValues.TimeFrequency.Once:
                    rtn += "Once ";
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

        private void ShowOptions()
        {
            if (lvwActiveScripts.Items.Count == 0)
            {
                schedule.Pause();
                psf.Close();
                poshsecframework.Interface.frmSettings frm = new poshsecframework.Interface.frmSettings();
                System.Windows.Forms.DialogResult rslt = frm.ShowDialog();
                bool restart = frm.Restart;
                frm.Dispose();
                frm = null;
                if (rslt == System.Windows.Forms.DialogResult.OK)
                {
                    if (restart)
                    {
                        if (MessageBox.Show(StringValue.RestartRequired, "Restart?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            Application.Restart();
                            this.Close();
                        }
                        else
                        {
                            psf.Open();
                        }
                    }
                    else
                    {
                        psf.Open();
                        Initialize();
                    }
                }
                else
                {
                    psf.Open();
                }
                schedule.Resume();
            }
            else
            {
                MessageBox.Show(StringValue.SettingScriptsRunning);
            }
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
                PShell.pshell ps = new PShell.pshell(this);
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
                PShell.pshell ps = new PShell.pshell(this);
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

        public void DisplayOutput(String output)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    DisplayOutput(output);
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
                LogOutput(output);
            }
        }

        public void DisplayOutput(String output, ListViewItem lvw, bool clicked, bool cancelled = false, bool scroll = false, bool showtab = false)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    DisplayOutput(output, lvw, clicked, cancelled, scroll);
                };
                if (!closing)
                {
                    this.Invoke(del);
                }                
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
                if (showtab)
                {
                    tcMain.SelectedTab = tbpPowerShell;
                }
                RemoveActiveScript(lvw);
                LogOutput(output + Environment.NewLine + StringValue.psf);
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
                    lvwitm.ToolTipText = message;
                    lvwitm.SubItems.Add(DateTime.Now.ToString(StringValue.TimeFormat));
                    lvwitm.SubItems.Add(scriptname);
                    if (AlertFilterActive(alerttype))
                    {
                        lvwAlerts.Items.Add(lvwitm);
                    }                    
                    alerts.Add(lvwitm);
                    lvwAlerts_Update();
                    lvwitm.EnsureVisible();
                    string alert = String.Format(StringValue.AlertFormat, lvwitm.SubItems[0].Text, lvwitm.SubItems[1].Text, lvwitm.SubItems[2].Text, lvwitm.SubItems[3].Text).Replace("\\r\\n", Environment.NewLine);
                    alert += Environment.NewLine;
                    LogAlert(alert);
                    if (Properties.Settings.Default.UseSyslog)
                    {
                        if (slog != null)
                        {
                            slog.SendMessage(alerttype, scriptname, message);    
                        }
                    }                    
                }
                catch (Exception e)
                {
                    DisplayError(e);
                }                
            }
        }

        public void UpdateProgress(String progress, ListViewItem lvw)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    UpdateProgress(progress, lvw);
                };
                this.Invoke(del);
            }
            else
            {
                try
                {
                    lvw.SubItems[2].Text = progress;
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

        public System.Object[] GetHostsAsObject()
        {
            if (this.InvokeRequired)
            {
                return (System.Object[])this.Invoke((Func<System.Object[]>)delegate
                {
                    return GetHostsAsObject();
                });
            }
            else
            {
                List<PSObject> hosts = new List<PSObject>();
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
                return hosts.ToArray();
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
                            this.UseWaitCursor = true;
                            Initialize();
                            this.UseWaitCursor = false;
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

        private bool AlertFilterActive(PShell.psmethods.PSAlert.AlertType atype)
        {
            bool rtn = false;
            switch (atype)
            {
                case PShell.psmethods.PSAlert.AlertType.Information:
                    if (Utility.AlertFilter.Informational)
                    {
                        rtn = true;
                    }
                    break;
                case PShell.psmethods.PSAlert.AlertType.Error:
                    if (Utility.AlertFilter.Error)
                    {
                        rtn = true;
                    }
                    break;
                case PShell.psmethods.PSAlert.AlertType.Warning:
                    if (Utility.AlertFilter.Warning)
                    {
                        rtn = true;
                    }
                    break;
                case PShell.psmethods.PSAlert.AlertType.Severe:
                    if (Utility.AlertFilter.Severe)
                    {
                        rtn = true;
                    }
                    break;
                case PShell.psmethods.PSAlert.AlertType.Critical:
                    if (Utility.AlertFilter.Critical)
                    {
                        rtn = true;
                    }
                    break;
            }
            return rtn;
        }

        private void FilterAlerts()
        {
            lvwAlerts.Items.Clear();
            foreach (ListViewItem lvw in alerts)
            {                
                if (AlertFilterActive((PShell.psmethods.PSAlert.AlertType)lvw.ImageIndex))
                {
                    lvwAlerts.Items.Add(lvw);
                }
            }
            lvwAlerts_Update();
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
                    schedule.Start();
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

        private bool ValidNetwork(String networkname)
        {
            bool rtn = true;
            int idx = 0;
            TreeNode rootnode = tvwNetworks.Nodes[0];
            while (idx < rootnode.Nodes.Count && rtn)
            {
                TreeNode node = rootnode.Nodes[idx];
                if (node.Text == networkname)
                {
                    rtn = false;
                }
                idx++;
            }
            return rtn;
        }

        private void CheckSettings()
        {
            //Ensure we have settings and that if it's .\ to change to application path.
            String scrpath = poshsecframework.Properties.Settings.Default.ScriptPath;
            String modpath = poshsecframework.Properties.Settings.Default.ModulePath;
            String schpath = poshsecframework.Properties.Settings.Default.ScheduleFile;
            String ghapikey = poshsecframework.Properties.Settings.Default.GithubAPIKey;
            String outputlog = poshsecframework.Properties.Settings.Default.OutputLogFile;
            String alertlog = poshsecframework.Properties.Settings.Default.AlertLogFile;
            if (scrpath.StartsWith(".") || scrpath.Trim() == "")
            {
                poshsecframework.Properties.Settings.Default["ScriptPath"] = Path.Combine(Application.StartupPath, scrpath).Replace("\\.\\", "\\");
            }
            if (modpath.StartsWith(".") || modpath.Trim() == "")
            {
                poshsecframework.Properties.Settings.Default["ModulePath"] = Path.Combine(Application.StartupPath, modpath).Replace("\\.\\", "\\");
            }
            if (schpath.StartsWith(".") || modpath.Trim() == "")
            {
                poshsecframework.Properties.Settings.Default["ScheduleFile"] = Path.Combine(Application.StartupPath, schpath).Replace("\\.\\", "\\");
            }
            if (outputlog.StartsWith("."))
            {
                poshsecframework.Properties.Settings.Default["OutputLogFile"] = Path.Combine(Application.StartupPath, outputlog).Replace("\\.\\", "\\");
            }
            if (alertlog.StartsWith("."))
            {
                poshsecframework.Properties.Settings.Default["AlertLogFile"] = Path.Combine(Application.StartupPath, alertlog).Replace("\\.\\", "\\");
            }
            if(ghapikey.Contains("\\"))
            {
                //Used to be Framework File path which is not needed.
                ghapikey = "";
                poshsecframework.Properties.Settings.Default["GithubAPIKey"] = "";
            }
            poshsecframework.Properties.Settings.Default.Save();
            poshsecframework.Properties.Settings.Default.Reload();
        }

        private void CheckPendingModules()
        {
            try
            { 
                if (File.Exists(Path.Combine(Properties.Settings.Default.ModulePath, StringValue.ModRestartFilename)))
                { 
                    StreamReader rdr = File.OpenText(Path.Combine(Properties.Settings.Default.ModulePath, StringValue.ModRestartFilename));
                    String filcontents = rdr.ReadToEnd();
                    rdr.Close();
                    if(filcontents.Trim() != "")
                    {
                        String[] movetos = filcontents.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                        if(movetos != null && movetos.Length > 0)
                        {
                            foreach(String moveto in movetos)
                            {
                                if (moveto.Trim() != "")
                                {
                                    String[] paths = moveto.Split(new string[] { ">>" }, StringSplitOptions.None);
                                    if (paths != null && paths.Length == 2)
                                    {
                                        String newfolder = paths[0];
                                        String target = paths[1];
                                        DirectoryInfo di = new DirectoryInfo(newfolder);
                                        if (Directory.Exists(target))
                                        {
                                            Directory.Delete(target, true);
                                        }
                                        di.MoveTo(target);
                                    }
                                    else
                                    { 
                                        //Just delete the path listed.
                                        if (Directory.Exists(moveto))
                                        {
                                            try
                                            {
                                                Directory.Delete(moveto, true);
                                            }
                                            catch (Exception e)
                                            {
                                                AddAlert(e.Message, PShell.psmethods.PSAlert.AlertType.Error, StringValue.psftitle);
                                            }
                                        }
                                    }
                                }                                
                            }
                        }
                    }
                    File.Delete(Path.Combine(Properties.Settings.Default.ModulePath, StringValue.ModRestartFilename));
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
            }
        }

        private void BuildModuleFilter()
        {
            cmbLibraryTypes.Items.Clear();
            cmbLibraryTypes.Items.Add("All");
            enabledmods.Clear();
            String modpath = poshsecframework.Properties.Settings.Default.ModulePath;
            if (Directory.Exists(modpath))
            {
                try
                {
                    String[] modfolders = Directory.GetDirectories(modpath, "*", SearchOption.TopDirectoryOnly);
                    if (modfolders != null && modfolders.Length > 0)
                    {
                        foreach (String modfolder in modfolders)
                        {
                            DirectoryInfo dirinfo = new DirectoryInfo(modfolder);
                            String[] modpsms = Directory.GetFiles(modfolder, "*.psd1", SearchOption.TopDirectoryOnly);
                            if (modpsms != null & modpsms.Length > 0)
                            {
                                foreach (String modpsm in modpsms)
                                {
                                    FileInfo psminfo = new FileInfo(modpsm);
                                    enabledmods.Add(psminfo.FullName);
                                    cmbLibraryTypes.Items.Add(psminfo.Name.Replace(psminfo.Extension, ""));
                                }
                            }                            
                        }
                    }
                }
                catch (Exception e)
                {
                    DisplayError(e);
                }                
            }
        }

        private void CheckLastModified()
        {
            DateTime last;
            DateTime.TryParse(Properties.Settings.Default.LastModuleCheck, out last);
            bool update = true;
            if (last.Year > 1)
            {
                if (DateTime.Now.Subtract(last).Hours < updatefrequency)
                {
                    update = false;
                }
            }
            if (update)
            {
                Properties.Settings.Default["LastModuleCheck"] = DateTime.Now.ToString();
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
                string err = "";
                System.Collections.Specialized.StringCollection mods = Properties.Settings.Default.Modules;
                if (mods != null && mods.Count > 0)
                {
                    Web.GithubClient ghc = new Web.GithubClient();
                    foreach (string mod in mods)
                    {
                        String[] modparts = mod.Split('|');
                        if (modparts != null && modparts.Length >= 3 && modparts.Length <= 4)
                        {
                            try
                            {
                                String location = modparts[1];
                                String[] locparts = location.Split('/');
                                if (locparts != null && locparts.Length == 2)
                                {
                                    String RepoOwner = locparts[0];
                                    String Repository = modparts[0];
                                    String branch = modparts[2];
                                    String lastmodified = modparts[3];
                                    String repolastmodified = ghc.GetLastModified(RepoOwner, Repository, branch, lastmodified);
                                    if (lastmodified != repolastmodified)
                                    {
                                        AddAlert(Repository + " has a new update in branch " + branch + ". Last update: " + repolastmodified, PShell.psmethods.PSAlert.AlertType.Warning, "Github API");
                                    }
                                    if (ghc.Errors.Count > 0)
                                    {
                                        err += String.Join(Environment.NewLine, ghc.Errors.ToArray());
                                    }
                                }
                                else
                                {
                                    err += "Invalid location in module.";
                                }
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message);
                            }
                        }
                    }
                    ghc = null;
                }
                if (err != "")
                {
                    DisplayError(new Exception(err));
                }
            }            
        }

        private void GetCommand()
        {
            try
            {
                Collection<PSObject> rslt = psf.GetCommand();
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
                lvwScripts_SelectedIndexChanged(null, null);
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

        #region " Update System Count "
        private void UpdateSystemCount()
        {
            tslSystemCount.Text = lvwSystems.Items.Count.ToString() + " Systems";
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
            ShowOptions();
        }

        private void mnuDeleteScheduleItem_Click(object sender, EventArgs e)
        {
            DeleteScheduleItems();
        }

        private void mnuScheduleItemRunNow_Click(object sender, EventArgs e)
        {
            if (lvwSchedule.SelectedItems.Count > 0)
            {
                ListViewItem lvw = lvwSchedule.SelectedItems[0];
                if (lvw != null)
                {
                    Utility.ScheduleItem sched = null;
                    bool found = false;
                    int idx = 0;
                    do
                    {
                        if (lvw.Tag != null)
                        {
                            if(schedule.ScheduleItems.Count > idx) 
                            {
                                if ((int)lvw.Tag == schedule.ScheduleItems[idx].Index)
                                {
                                    sched = schedule.ScheduleItems[idx];
                                    found = true;
                                }
                            }                            
                        }
                        idx++;
                    } while (idx < lvwSchedule.Items.Count && !found);                                        
                    if (sched != null)
                    {
                        sched.LastRunTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
                        schedule.Save();
                        schedule_ScriptInvoked(null, new Utility.ScheduleEventArgs(sched));
                        schedule_ItemUpdated(null, new Utility.ScheduleEventArgs(sched));
                    }
                }
            }
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

        private void lvwSystems_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwSorter.SortColumn)
            {
                if (lvwSorter.Order == SortOrder.Ascending)
                {
                    lvwSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                lvwSorter.SortColumn = e.Column;
                lvwSorter.Order = SortOrder.Ascending;
            }
            lvwSystems.Sort();
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

        #region TreeNode
        private void tvwNetworks_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent != null && e.Node.Text != StringValue.LocalNetwork)
            {
                btnRemoveNetwork.Enabled = true;
            }
            else
            {
                btnRemoveNetwork.Enabled = false;
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
                        LogOutput(cmd);
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
                    else
                    {
                        if (txtPShellOutput.SelectionStart < mincurpos)
                        {
                            txtPShellOutput.SelectionStart = txtPShellOutput.Text.Length;
                            txtPShellOutput.ScrollToCaret();
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
        private void btnAlert_Information_CheckedChanged(object sender, EventArgs e)
        {
            Utility.AlertFilter.Informational = btnAlert_Information.Checked;
            FilterAlerts();
        }

        private void btnAlert_Error_CheckedChanged(object sender, EventArgs e)
        {
            Utility.AlertFilter.Error = btnAlert_Error.Checked;
            FilterAlerts();
        }

        private void btnAlert_Warning_CheckedChanged(object sender, EventArgs e)
        {
            Utility.AlertFilter.Warning = btnAlert_Warning.Checked;
            FilterAlerts();
        }

        private void btnAlert_Severe_CheckedChanged(object sender, EventArgs e)
        {
            Utility.AlertFilter.Severe = btnAlert_Severe.Checked;
            FilterAlerts();
        }

        private void btnAlert_Critical_CheckedChanged(object sender, EventArgs e)
        {
            Utility.AlertFilter.Critical = btnAlert_Critical.Checked;
            FilterAlerts();
        }

        private void btnAlert_Information_Click(object sender, EventArgs e)
        {
            btnAlert_Information.Checked = !btnAlert_Information.Checked;
        }

        private void btnAlert_Error_Click(object sender, EventArgs e)
        {
            btnAlert_Error.Checked = !btnAlert_Error.Checked;
        }

        private void btnAlert_Warning_Click(object sender, EventArgs e)
        {
            btnAlert_Warning.Checked = !btnAlert_Warning.Checked;
        }

        private void btnAlert_Severe_Click(object sender, EventArgs e)
        {
            btnAlert_Severe.Checked = !btnAlert_Severe.Checked;
        }

        private void btnAlert_Critical_Click(object sender, EventArgs e)
        {
            btnAlert_Critical.Checked = !btnAlert_Critical.Checked;
        }

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

        private void cmnuAlerts_Opening(object sender, CancelEventArgs e)
        {
            if (lvwAlerts.SelectedItems.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void cmnuRestore_Click(object sender, EventArgs e)
        {
            this.WindowState = lastwindowstate;
        }

        private void cmnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void cmnuPSFConsole_Opening(object sender, CancelEventArgs e)
        {
            cmbtnCopy.Enabled = false;
            cmbtnPaste.Enabled = false;

            if (txtPShellOutput.SelectedText.Length > 0)
            {
                cmbtnCopy.Enabled = true;
            }

            String cliptxt = Clipboard.GetText(TextDataFormat.Text);
            if (cliptxt != null && cliptxt != "")
            {
                cmbtnPaste.Enabled = true;
            }
        }

        private void cmbtnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtPShellOutput.SelectedText, TextDataFormat.Text);
        }

        private void cmbtnPaste_Click(object sender, EventArgs e)
        {
            //This gets rid of any formating and nontext data.
            Clipboard.SetText(Clipboard.GetText(TextDataFormat.Text));
            txtPShellOutput.Paste();
        }

        private void cmbtnCopyMessage_Click(object sender, EventArgs e)
        {
            string messages = "";
            if (lvwAlerts.SelectedItems.Count > 0)
            { 
                foreach(ListViewItem lvw in lvwAlerts.SelectedItems)
                {
                    messages += lvw.SubItems[1].Text + Environment.NewLine;
                }
                Clipboard.SetText(messages, TextDataFormat.Text);
            }
        }

        private void cmbtnCopyAlert_Click(object sender, EventArgs e)
        {
            string alerts = "";
            if (lvwAlerts.SelectedItems.Count > 0)
            {
                foreach (ListViewItem lvw in lvwAlerts.SelectedItems)
                {
                    string alert = String.Format(StringValue.AlertFormat, lvw.SubItems[0].Text, lvw.SubItems[1].Text, lvw.SubItems[2].Text, lvw.SubItems[3].Text).Replace("\\r\\n", Environment.NewLine);
                    alerts += alert + Environment.NewLine;
                }
                Clipboard.SetText(alerts, TextDataFormat.Text);
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
                alerts.Clear();
                lvwAlerts_Update();
            }
        }

        private void btnAlert_MarkResolved_Click(object sender, EventArgs e)
        {
            if (lvwAlerts.SelectedItems.Count > 0)
            {
                if (MessageBox.Show(StringValue.ClearAlert, "Confirm", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (ListViewItem lvw in lvwAlerts.SelectedItems)
                    {
                        lvwAlerts.Items.Remove(lvw);
                        alerts.Remove(lvw);
                    }
                    lvwAlerts_Update();
                }
            }
            
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            Scan();
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

        private void btnAddSystem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(StringValue.NotImplemented);           
        }

        private void btnAddNetwork_Click(object sender, EventArgs e)
        {
            Interface.frmAddNetwork frm = new Interface.frmAddNetwork();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ValidNetwork(frm.NetworkName))
                {
                    TreeNode node = new TreeNode();
                    node.Text = frm.NetworkName;
                    node.SelectedImageIndex = 3;
                    node.ImageIndex = 3;
                    node.Tag = SystemType.Domain;
                    tvwNetworks.Nodes[0].Nodes.Add(node);
                }
                else
                {
                    MessageBox.Show(StringValue.InvalidNetworkName);
                }
            }
            frm.Dispose();
            frm = null;
        }

        private void btnRemoveNetwork_Click(object sender, EventArgs e)
        {
            if (tvwNetworks.SelectedNode != null && tvwNetworks.SelectedNode.Parent != null)
            {
                if (MessageBox.Show(StringValue.ConfirmNetworkDelete, "Confirm Delete", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    tvwNetworks.SelectedNode.Remove();
                }
            }
            
        }

        private void btnLaunchPShellCmd_Click(object sender, EventArgs e)
        {
            ShellOpenCommand("powershell.exe");
        }

        private void btnLaunchCmd_Click(object sender, EventArgs e)
        {
            ShellOpenCommand("cmd.exe");
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            ShowOptions();
        }

        private void btnExportSystems_Click(object sender, EventArgs e)
        {
            try
            {
                System.Object[] hosts = GetHostsAsObject();
                String expfilter = StringValue.ExportFormats;
                SaveFileDialog dlgExport = new SaveFileDialog();
                dlgExport.Filter = expfilter;
                dlgExport.CheckFileExists = false;
                dlgExport.CheckPathExists = true;
                dlgExport.Title = "Export As...";
                if (dlgExport.ShowDialog() == DialogResult.OK)
                {
                    Utility.ExportObject exobj = new Utility.ExportObject();
                    switch ((EnumValues.FilterType)dlgExport.FilterIndex)
                    {
                        case EnumValues.FilterType.XML:
                            exobj.XML(hosts, dlgExport.FileName);
                            break;
                        case EnumValues.FilterType.CSV:
                            exobj.CSV(hosts, dlgExport.FileName);
                            break;
                        case EnumValues.FilterType.TXT:
                            exobj.TXT(hosts, dlgExport.FileName);
                            break;
                    }
                    exobj = null;
                }
                dlgExport.Dispose();
                dlgExport = null;
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
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
            if (shown)
            {
                GetCommand();
            }
            
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

        private void nimain_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = lastwindowstate;
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