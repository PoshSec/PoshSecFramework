using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.Management.Automation;
using System.Net;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System.Net.NetworkInformation;
using PoshSec.Framework.Comparers;
using PoshSec.Framework.Enums;
using PoshSec.Framework.Interface;
using PoshSec.Framework.Properties;
using PoshSec.Framework.PShell;
using PoshSec.Framework.Strings;
using PoshSec.Framework.Utility;
using Timer = System.Timers.Timer;

namespace PoshSec.Framework
{
    public partial class frmMain : Form
    {
        private Collection<PSObject> _commands;
        private readonly Networks _networks = new Networks();

        private readonly NetworkBrowser _scnr = new NetworkBrowser();
        private frmStartup _spashScreen;
        private int _mincurpos = 6;
        private readonly Collection<string> _cmdhist = new Collection<string>();
        private int _cmdhistidx = -1;
        private pshell _psf;
        private bool _cancelscan;
        private bool _restart;
        private bool _shown;
        private readonly bool _cont = true;
        private readonly Schedule _schedule = new Schedule(1000);
        private string _loaderrors = "";
        private readonly Collection<string> _enabledmods = new Collection<string>();
        private const int Updatefrequency = 12; // in hours
        private readonly Collection<ListViewItem> _alerts = new Collection<ListViewItem>();
        private Syslog _slog;
        private readonly ListViewColumnSorter _lvwSorter = new ListViewColumnSorter();
        private bool _closing;
        private FormWindowState _lastwindowstate = FormWindowState.Normal;
        private Timer _ghChecker;

        private enum LibraryImages
        {
            Function,
            Cmdlet,
            Command,
            Alias
        }

        private enum ScheduleColumns
        {
            ScriptName = 0,
            Parameters,
            Schedule,
            RunAs,
            LastRun,
            Message
        }

        #region Form
        public frmMain()
        {
            InitializeComponent();

            _lvwNetworkNodes.ListViewItemSorter = _lvwSorter;
            this.Enabled = false;
            if (IsRootDrive())
            {
                _cont = false;
                if (MessageBox.Show(StringValue.RootDrive, "Running in root drive!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    _cont = true;
                }
            }
            if (_cont)
            {
                _spashScreen = new Interface.frmStartup();
                _spashScreen.Show();
                _spashScreen.Refresh();
            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            if (_cont)
            {
                _spashScreen.SetStatus("Initializing, please wait...");
                _scnr.ScanComplete += scnr_ScanComplete;
                _scnr.ScanCancelled += scnr_ScanCancelled;
                _scnr.ScanUpdate += scnr_ScanUpdate;
                _schedule.ItemUpdated += schedule_ItemUpdated;
                _schedule.ScriptInvoked += schedule_ScriptInvoked;
                _schedule.ScheduleRemoved += schedule_ScheduleRemoved;

                _spashScreen.SetStatus("Checking Settings, please wait...");
                CheckSettings();
                if (Settings.Default.FirstTime)
                {
                    _restart = true;
                    _spashScreen.Hide();
                    FirstTimeSetup();
                }
                if (_restart)
                {
                    Application.Restart();
                    this.Close();
                }
                else
                {
                    Initialize();
                    _spashScreen.Show();
                    _spashScreen.SetStatus("Loading Networks, please wait...");
                    LoadNetworks();
                    GetAlerts();
                }
                if (_loaderrors != "")
                {
                    DisplayOutput(StringValue.ImportError + Environment.NewLine + _loaderrors, null, false, false, false, true);
                }
                _shown = true;
                _spashScreen.Close();
                _spashScreen.Dispose();
                _spashScreen = null;
                this.Enabled = true;
                this.Focus();
                _schedule.Start();
            }
            else
            {
                this.Close();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closing = true;
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
                            } while ((thd.ThreadState != ThreadState.Aborted) && times <= 5);
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        _closing = false;
                    }
                }
                SaveSystems();
                SaveAlerts();
                Properties.Settings.Default.Save();
            }
            catch (Exception)
            {
                e.Cancel = false;
                _closing = true;
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = Properties.Settings.Default.ShowInTaskbar;
                nimain.Visible = !this.ShowInTaskbar;
            }
            else
            {
                _lastwindowstate = this.WindowState;
                this.ShowInTaskbar = true;
                nimain.Visible = false;
            }
        }
        #endregion

        #region Private Methods

        private void Initialize()
        {
            if (_spashScreen != null) { _spashScreen.SetStatus("Initializing PowerShell, please wait..."); }
            _psf = new PShell.pshell(this);
            _psf.ImportPSModules(_enabledmods);
            _psf.ParentForm = this;
            _commands = _psf.GetCommand();

            if (_spashScreen != null) { _spashScreen.SetStatus("Looking for modules, please wait..."); }
            CheckPendingModules();

            BuildModuleFilter();
            if (_spashScreen != null) { _spashScreen.SetStatus("Checking for updates, please wait..."); }

            // Set up GitHub Timer for Checking for Updates
            _ghChecker = new System.Timers.Timer();
            _ghChecker.Interval = 3600000; //One Hour
            _ghChecker.Elapsed += ghChecker_Elapsed;
            _ghChecker.Enabled = true;

            CheckLastModified();
            moduleFilterComboBox.SelectedIndex = 0;

            _scnr.ParentForm = this;
            if (_psf.LoadErrors != "")
            {
                _loaderrors += _psf.LoadErrors;
            }
            txtPShellOutput.Text = StringValue.psf;
            _mincurpos = txtPShellOutput.Text.Length;
            txtPShellOutput.SelectionStart = _mincurpos;
            if (_spashScreen != null) { _spashScreen.SetStatus("Loading script library, please wait..."); }
            GetLibrary();
            if (_spashScreen != null) { _spashScreen.SetStatus("Getting commands, please wait..."); }
            LoadCommands(_commands);
            if (_spashScreen != null) { _spashScreen.SetStatus("Loading schedule library, please wait..."); }
            LoadSchedule();
            InitSyslog();
        }

        private bool IsRootDrive()
        {
            bool rtn = false;
            DirectoryInfo dinfo = new DirectoryInfo(Application.StartupPath);
            if (dinfo.Parent == null)
            {
                rtn = true;
            }
            dinfo = null;
            return rtn;
        }

        void ghChecker_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CheckLastModified();
        }

        private void InitSyslog()
        {
            try
            {
                if (Properties.Settings.Default.UseSyslog && Properties.Settings.Default.SyslogServer != "")
                {
                    if (_slog == null)
                    {
                        _slog = new Syslog(new IPEndPoint(System.Net.IPAddress.Parse(Properties.Settings.Default.SyslogServer), Properties.Settings.Default.SyslogPort));
                    }
                }
                else
                {
                    if (_slog != null)
                    {
                        _slog.Close();
                        _slog = null;
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
                _restart = false;
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
                        _alerts.Add(lvwItm);
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

        private void LoadNetworks()
        {
            _networks.Clear();            
            _networks.Add(new LocalNetwork());

            //Add(StringValue.LocalNetwork, NetworkType.Local);

            try
            {
                //Get Domain Name
                var hostForest = Forest.GetCurrentForest();
                var domains = hostForest.Domains;

                foreach (Domain domain in domains)
                {
                    //var node = new DomainNetworkTreeNode(domain.Name);
                    //var rootnode = tvwNetworks.Nodes[0];
                    //rootnode.Nodes.Add(node);
                    _networks.Add(new DomainNetwork(domain.Name));
                }
            }
            catch
            {
                //fail silently because it's not on A/D   
            }

            tvwNetworks.Load(_networks);

            // TODO: load nodes of the currently selected Network
            if (Settings.Default.Systems != null && Settings.Default.Systems.Count > 0)
            {
                _lvwNetworkNodes.Items.Clear();
                foreach (var system in Settings.Default.Systems)
                {
                    var systemparts = system.Split('|');
                    if (systemparts.Length == _lvwNetworkNodes.Columns.Count)
                    {
                        var lvwItm = new ListViewItem { Text = systemparts[0] };
                        for (var idx = 1; idx < systemparts.Length; idx++) lvwItm.SubItems.Add(systemparts[idx]);
                        lvwItm.ImageIndex = 2;
                        _lvwNetworkNodes.Items.Add(lvwItm);
                    }
                }

                UpdateSystemCount();
            }
            else
            {
                try
                {
                    //Add Local IP/Host to Local Network
                    _lvwNetworkNodes.Items.Clear();
                    var localHost = Dns.GetHostName();
                    var localIPs = _scnr.GetIP(localHost).Split(',');
                    foreach (var localIP in localIPs)
                    {
                        // TODO: Replace with strongly typed NetworkNodeListViewItem
                        var lvwItm = new ListViewItem {Text = localHost};
                        lvwItm.SubItems.Add(localIP);
                        lvwItm.SubItems.Add(_scnr.GetMyMac(localIP));
                        lvwItm.SubItems.Add("");
                        lvwItm.SubItems.Add(StringValue.Up);
                        lvwItm.SubItems.Add(StringValue.NotInstalled);
                        lvwItm.SubItems.Add("0");
                        lvwItm.SubItems.Add(DateTime.Now.ToString(StringValue.TimeFormat));

                        lvwItm.ImageIndex = 2;
                        _lvwNetworkNodes.Items.Add(lvwItm);
                    }

                    tvwNetworks.Nodes[0].Expand();
                    UpdateSystemCount();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + Environment.NewLine + e.StackTrace);
                }
            }

            if (tvwNetworks.Count > 0) tvwNetworks.SelectedNode = tvwNetworks.Nodes[0].Nodes[0];
        }

        private void Scan()
        {
            if (tvwNetworks.SelectedNode?.Tag is NetworkType type)
            {
                this.UseWaitCursor = true;
                switch (type)
                {
                    case NetworkType.Local:
                        ScanbyIP();
                        break;
                    case NetworkType.Domain:
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
            _scnr.ParentForm = this;
            _cancelscan = false;
            UseWaitCursor = true;
            btnScan.Enabled = false;
            mnuScan.Enabled = false;
            var domain = tvwNetworks.SelectedNode.Text;
            var thd = new Thread(_scnr.ScanActiveDirectory);
            _scnr.Domain = domain;
            thd.Start();
        }

        private void ScanbyIP()
        {
            btnCancelScan.Enabled = true;
            _scnr.ParentForm = this;
            _cancelscan = false;
            this.UseWaitCursor = true;
            btnScan.Enabled = false;
            mnuScan.Enabled = false;
            Thread thd = new Thread(_scnr.ScanbyIP);
            thd.Start();
        }

        private void scnr_ScanComplete(object sender, ScanEventArgs e)
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
                if (rslts.Count > 0 && !_cancelscan)
                {
                    _lvwNetworkNodes.Items.Clear();
                    SetProgress(0, rslts.Count);
                    _lvwNetworkNodes.BeginUpdate();
                    foreach (Object system in rslts)
                    {
                        if (system != null)
                        {
                            if (system is string)
                            {
                                string sys = (string)system;
                                if (sys != null && sys != "")
                                {
                                    ListViewItem lvwItm = new ListViewItem();
                                    String[] ipinfo = system.ToString().Split('|');

                                    var systemListViewItem = new NetworkNodeListViewItem(ipinfo[2])
                                    {
                                        IpAddress = ipinfo[1],
                                        MacAddress = _scnr.GetMac(ipinfo[1]),
                                        Description = "",
                                        Status = StringValue.Up,
                                        ClientInstalled = StringValue.NotInstalled,
                                        Alerts = 0,
                                        LastScanned = DateTime.Now
                                    };

                                    SetStatus("Adding " + ipinfo[2] + ", please wait...");

                                    _lvwNetworkNodes.Add(systemListViewItem);

                                    pbStatus.Value += 1;
                                }
                            }
                            else
                            {
                                DirectoryEntry sys = (DirectoryEntry)system;
                                String ipadr = _scnr.GetIP(sys.Name.Replace("CN=", ""));
                                String[] ips = ipadr.Split(',');
                                if (ips != null && ips.Length > 0)
                                {
                                    foreach (String ip in ips)
                                    {
                                        ListViewItem lvwItm = new ListViewItem();
                                        SetStatus("Adding " + sys.Name.Replace("CN=", "") + ", please wait...");
                                        lvwItm.Text = sys.Name.Replace("CN=", "").ToString();

                                        lvwItm.SubItems.Add(ip);
                                        string macaddr = _scnr.GetMac(ip);
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
                                        _lvwNetworkNodes.Items.Add(lvwItm);
                                    }
                                }
                                pbStatus.Value += 1;
                            }
                            _lvwNetworkNodes.Refresh();
                        }
                    }
                    _lvwNetworkNodes.EndUpdate();
                }
                rslts = null;
                _lvwNetworkNodes.Sorting = SortOrder.Ascending;
                _lvwNetworkNodes.Sort();
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
            if (_lvwNetworkNodes.Items.Count > 0 && Properties.Settings.Default.SaveSystems)
            {
                if (Properties.Settings.Default.Systems == null)
                {
                    Properties.Settings.Default["Systems"] = new System.Collections.Specialized.StringCollection();
                }
                ((System.Collections.Specialized.StringCollection)Properties.Settings.Default["Systems"]).Clear();
                foreach (ListViewItem lvw in _lvwNetworkNodes.Items)
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

        void scnr_ScanUpdate(object sender, ScanEventArgs e)
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
                scriptparams = _psf.CheckForParams(lvw.Tag.ToString());

                if (!_psf.ParamSelectionCancelled)
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
                        _schedule.ScheduleItems.Add(sitm);
                        if (_schedule.Save())
                        {
                            LoadSchedule();
                        }
                        else
                        {
                            MessageBox.Show("Error saving schedule: " + _schedule.LastException.Message);
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
            switch (schedtime.Frequency)
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
                case 1:
                case 21:
                case 31:
                    rtn = "st";
                    break;
                case 2:
                case 22:
                    rtn = "nd";
                    break;
                case 3:
                case 23:
                    rtn = "rd";
                    break;
            }
            return rtn;
        }

        private void ShowOptions()
        {
            if (lvwActiveScripts.Items.Count == 0)
            {
                _schedule.Pause();
                frmSettings frm = new frmSettings();
                System.Windows.Forms.DialogResult rslt = frm.ShowDialog();
                bool restart = frm.Restart;
                frm.Dispose();
                frm = null;
                if (rslt == System.Windows.Forms.DialogResult.OK)
                {
                    _psf.Close();
                    if (restart)
                    {
                        if (MessageBox.Show(StringValue.RestartRequired, "Restart?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            Application.Restart();
                            this.Close();
                        }
                        else
                        {
                            _psf.Open();
                        }
                    }
                    else
                    {
                        _psf.Open();
                        Initialize();
                    }
                }
                _schedule.Resume();
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
                    _schedule.Remove((int)lvw.Tag);
                }
                _schedule.Save();
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
                txtPShellOutput.AppendText(StringValue.psf);
                _mincurpos = txtPShellOutput.Text.Length;
                txtPShellOutput.SelectionStart = _mincurpos;
                txtPShellOutput.Select();
                txtPShellOutput.ReadOnly = false;
                LogOutput(Environment.NewLine + output + StringValue.psf);
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
                if (!_closing)
                {
                    this.Invoke(del);
                }
            }
            else
            {
                if ((txtPShellOutput.Text.Length + output.Length + StringValue.psf.Length) > txtPShellOutput.MaxLength)
                {
                    txtPShellOutput.Text = txtPShellOutput.Text.Substring(output.Length + 500, txtPShellOutput.Text.Length - (output.Length + 500));
                }
                txtPShellOutput.AppendText(output);
                txtPShellOutput.AppendText(StringValue.psf);
                _mincurpos = txtPShellOutput.Text.Length;
                txtPShellOutput.SelectionStart = _mincurpos;
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
                LogOutput(Environment.NewLine + output + StringValue.psf);
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
                    _alerts.Add(lvwitm);
                    lvwAlerts_Update();
                    lvwitm.EnsureVisible();
                    string alert = String.Format(StringValue.AlertFormat, lvwitm.SubItems[0].Text, lvwitm.SubItems[1].Text, lvwitm.SubItems[2].Text, lvwitm.SubItems[3].Text).Replace("\\r\\n", Environment.NewLine);
                    alert += Environment.NewLine;
                    LogAlert(alert);
                    if (Properties.Settings.Default.UseSyslog)
                    {
                        if (_slog != null)
                        {
                            _slog.SendMessage(alerttype, scriptname, message);
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
                return (Collection<PSObject>)this.Invoke((Func<Collection<PSObject>>)delegate
               {
                   return GetCheckedHosts();
               });
            }
            else
            {
                Collection<PSObject> hosts = new Collection<PSObject>();
                ListView.CheckedListViewItemCollection lvwitms = _lvwNetworkNodes.CheckedItems;
                if (lvwitms != null && lvwitms.Count > 0)
                {
                    foreach (ListViewItem lvw in lvwitms)
                    {
                        PSObject pobj = new PSObject();
                        int idx = -1;
                        foreach (ColumnHeader col in _lvwNetworkNodes.Columns)
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
                ListView.ListViewItemCollection lvwitms = _lvwNetworkNodes.Items;
                if (lvwitms != null && lvwitms.Count > 0)
                {
                    foreach (ListViewItem lvw in lvwitms)
                    {
                        PSObject pobj = new PSObject();
                        int idx = -1;
                        foreach (ColumnHeader col in _lvwNetworkNodes.Columns)
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
                ListView.ListViewItemCollection lvwitms = _lvwNetworkNodes.Items;
                if (lvwitms != null && lvwitms.Count > 0)
                {
                    foreach (ListViewItem lvw in lvwitms)
                    {
                        PSObject pobj = new PSObject();
                        int idx = -1;
                        foreach (ColumnHeader col in _lvwNetworkNodes.Columns)
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
                    _cmdhist.Add(cmd);
                }
                _cmdhistidx = _cmdhist.Count;
                switch (cmd.ToUpper())
                {
                    case StringValue.CLS:
                    case StringValue.Clear:
                        txtPShellOutput.Text = StringValue.psf;
                        txtPShellOutput.SelectionStart = txtPShellOutput.Text.Length;
                        _mincurpos = txtPShellOutput.Text.Length;
                        break;
                    case StringValue.AptGetUpdate:
                        txtPShellOutput.AppendText(Environment.NewLine + StringValue.psf);
                        txtPShellOutput.SelectionStart = txtPShellOutput.Text.Length;
                        _mincurpos = txtPShellOutput.Text.Length;
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
                            _mincurpos = txtPShellOutput.Text.Length;
                        }
                        break;
                    case StringValue.Exit:
                        this.Close();
                        break;
                    default:
                        txtPShellOutput.AppendText(Environment.NewLine);
                        _mincurpos = txtPShellOutput.Text.Length;
                        txtPShellOutput.ReadOnly = true;
                        _psf.Run(cmd, true, false);
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
            foreach (ListViewItem lvw in _alerts)
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
            if (_schedule.Load())
            {
                if (_schedule.ScheduleItems != null && _schedule.ScheduleItems.Count > 0)
                {
                    lvwSchedule.Items.Clear();
                    lvwSchedule.BeginUpdate();
                    foreach (Utility.ScheduleItem sitm in _schedule.ScheduleItems)
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
                    _schedule.Start();
                }
            }
            else
            {
                if (_schedule.LastException != null)
                {
                    MessageBox.Show("Error loading schedule: " + _schedule.LastException.Message);
                }
            }
        }

        private void CheckSettings()
        {
            //Ensure we have settings and that if it's .\ to change to application path.
            String scrpath = Settings.Default.ScriptPath;
            String modpath = Settings.Default.ModulePath;
            String schpath = Settings.Default.ScheduleFile;
            String ghapikey = Settings.Default.GithubAPIKey;
            String outputlog = Settings.Default.OutputLogFile;
            String alertlog = Settings.Default.AlertLogFile;
            if (scrpath.StartsWith(".") || scrpath.Trim() == "")
            {
                Settings.Default["ScriptPath"] = Path.Combine(Application.StartupPath, scrpath).Replace("\\.\\", "\\");
            }
            if (modpath.StartsWith(".") || modpath.Trim() == "")
            {
                Settings.Default["ModulePath"] = Path.Combine(Application.StartupPath, modpath).Replace("\\.\\", "\\");
            }
            if (schpath.StartsWith(".") || modpath.Trim() == "")
            {
                Settings.Default["ScheduleFile"] = Path.Combine(Application.StartupPath, schpath).Replace("\\.\\", "\\");
            }
            if (outputlog.StartsWith("."))
            {
                Settings.Default["OutputLogFile"] = Path.Combine(Application.StartupPath, outputlog).Replace("\\.\\", "\\");
            }
            if (alertlog.StartsWith("."))
            {
                Settings.Default["AlertLogFile"] = Path.Combine(Application.StartupPath, alertlog).Replace("\\.\\", "\\");
            }
            if (ghapikey.Contains("\\"))
            {
                //Used to be Framework File path which is not needed.
                ghapikey = "";
                Settings.Default["GithubAPIKey"] = "";
            }
            Settings.Default.Save();
            Settings.Default.Reload();
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
                    if (filcontents.Trim() != "")
                    {
                        String[] movetos = filcontents.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                        if (movetos != null && movetos.Length > 0)
                        {
                            foreach (String moveto in movetos)
                            {
                                if (moveto.Trim() != "")
                                {
                                    String[] paths = moveto.Split(new string[] { ">>" }, StringSplitOptions.None);
                                    if (paths != null && paths.Length == 2)
                                    {
                                        String newfolder = Path.Combine(Properties.Settings.Default.ModulePath, paths[0]);
                                        String target = Path.Combine(Properties.Settings.Default.ModulePath, paths[1]);
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
                                        if (Directory.Exists(Path.Combine(Properties.Settings.Default.ModulePath, moveto)))
                                        {
                                            try
                                            {
                                                Directory.Delete(Path.Combine(Properties.Settings.Default.ModulePath, moveto), true);
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
            moduleFilterComboBox.Items.Clear();
            moduleFilterComboBox.Items.Add("All");
            _enabledmods.Clear();

            var modules = _commands.Select(pso => pso.BaseObject).OfType<CommandInfo>().Select(cmd => cmd.ModuleName).Distinct();
            foreach (var module in modules)
            {
                moduleFilterComboBox.Items.Add(module);
            }

            //string modpath = Properties.Settings.Default.ModulePath;
            //if (Directory.Exists(modpath))
            //{
            //    try
            //    {
            //        String[] modfolders = Directory.GetDirectories(modpath, "*", SearchOption.TopDirectoryOnly);
            //        if (modfolders != null && modfolders.Length > 0)
            //        {
            //            foreach (String modfolder in modfolders)
            //            {
            //                DirectoryInfo dirinfo = new DirectoryInfo(modfolder);
            //                String[] modpsms = Directory.GetFiles(modfolder, "*.psd1", SearchOption.TopDirectoryOnly);
            //                if (modpsms != null & modpsms.Length > 0)
            //                {
            //                    foreach (String modpsm in modpsms)
            //                    {
            //                        FileInfo psminfo = new FileInfo(modpsm);
            //                        enabledmods.Add(psminfo.FullName);
            //                        moduleFilterComboBox.Items.Add(psminfo.Name.Replace(psminfo.Extension, ""));
            //                    }
            //                }                            
            //            }
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        DisplayError(e);
            //    }                
            //}
        }

        private void CheckLastModified()
        {
            DateTime last;
            DateTime.TryParse(Properties.Settings.Default.LastModuleCheck, out last);
            bool update = true;
            if (last.Year > 1)
            {
                if (DateTime.Now.Subtract(last).TotalHours < Updatefrequency)
                {
                    update = false;
                }
            }
            if (update)
            {
                string err = "";
                System.Collections.Specialized.StringCollection mods = Properties.Settings.Default.Modules;
                Web.GithubClient ghc = new Web.GithubClient();
                String psflastmodified = ghc.GetLastModified("PoshSec", "PoshSecFramework", "master", Properties.Settings.Default.LastModuleCheck);
                DateTime psflast;
                DateTime modlast;
                DateTime.TryParse(psflastmodified, out psflast);
                DateTime.TryParse(Properties.Settings.Default.LastModuleCheck, out modlast);
                if (psflast > modlast)
                {
                    AddAlert("PoshSec Framework has been updated! Please update your version. Last update: " + psflastmodified, PShell.psmethods.PSAlert.AlertType.Critical, "Github API");
                }
                if (ghc.Errors.Count > 0)
                {
                    err += String.Join(Environment.NewLine, ghc.Errors.ToArray());
                }

                Properties.Settings.Default["LastModuleCheck"] = DateTime.Now.ToString();
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
                if (mods != null && mods.Count > 0)
                {
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
                ghc = null;
            }
        }

        private void LoadCommands(Collection<PSObject> commands)
        {
            try
            {
                if (commands != null)
                {
                    List<String> accmds = new List<String>();
                    lvwCommands.Items.Clear();
                    lvwCommands.BeginUpdate();
                    foreach (PSObject command in commands)
                    {
                        ListViewItem lvw = null;
                        switch (command.BaseObject.GetType().Name)
                        {
                            case "AliasInfo":
                                AliasInfo ai = (AliasInfo)command.BaseObject;
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
                                FunctionInfo fi = (FunctionInfo)command.BaseObject;
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
                                CmdletInfo cmi = (CmdletInfo)command.BaseObject;
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
                                Console.WriteLine(command.BaseObject.GetType().Name);
                                break;
                        }
                        // Add to listview if All or passes through filter
                        if (lvw != null && (moduleFilterComboBox.Text == "All" || moduleFilterComboBox.Text.ToLower() == lvw.SubItems[1].Text.ToLower()))
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
                String scriptroot = Settings.Default["ScriptPath"].ToString();
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
            tslSystemCount.Text = _lvwNetworkNodes.Items.Count.ToString() + " Systems";
        }
        #endregion

        #endregion

        #region Public Methods

        #region Interface
        public System.Windows.Forms.DialogResult ShowParams(psparamtype parm)
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
                            if (_schedule.ScheduleItems.Count > idx)
                            {
                                if ((int)lvw.Tag == _schedule.ScheduleItems[idx].Index)
                                {
                                    sched = _schedule.ScheduleItems[idx];
                                    found = true;
                                }
                            }
                        }
                        idx++;
                    } while (idx < lvwSchedule.Items.Count && !found);
                    if (sched != null)
                    {
                        sched.LastRunTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
                        _schedule.Save();
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
            switch (Settings.Default.ScriptDefaultAction)
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
            if (e.Column == _lvwSorter.SortColumn)
            {
                if (_lvwSorter.Order == SortOrder.Ascending)
                {
                    _lvwSorter.Order = SortOrder.Descending;
                }
                else
                {
                    _lvwSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                _lvwSorter.SortColumn = e.Column;
                _lvwSorter.Order = SortOrder.Ascending;
            }
            _lvwNetworkNodes.Sort();
        }

        private void lvwCommands_DoubleClick(object sender, EventArgs e)
        {
            if (lvwCommands.SelectedItems.Count > 0 && !txtPShellOutput.ReadOnly)
            {
                ListViewItem lvw = lvwCommands.SelectedItems[0];
                txtPShellOutput.ReadOnly = true;
                _psf.Run(lvw.Text, true);
            }
            else if (txtPShellOutput.ReadOnly)
            {
                txtPShellOutput.AppendText(Environment.NewLine + StringValue.CommandRunning + Environment.NewLine + Environment.NewLine);
                txtPShellOutput.AppendText(StringValue.psf);
                _mincurpos = txtPShellOutput.Text.Length;
                txtPShellOutput.SelectionStart = _mincurpos;
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

        #region Networks

        private void tvwNetworks_AfterSelect(object sender, TreeViewEventArgs e)
        {
            btnRemoveNetwork.Enabled = e.Node != tvwNetworks.Nodes[0] & !(e.Node is LocalNetworkTreeNode);

            // TODO: Refresh systems (NetworkNodes)
        }

        private void btnAddNetwork_Click(object sender, EventArgs e)
        {
            using (var frm = new frmAddNetwork())
            {
                if (frm.ShowDialog() != DialogResult.OK) return;
                var name = frm.NetworkName;
                if (_networks.IsValid(name))
                {
                    tvwNetworks.Add(name, NetworkType.Domain);
                    _networks.Add(new DomainNetwork(name));
                }
                else
                    MessageBox.Show(StringValue.InvalidNetworkName);
            }
        }

        private void btnRemoveNetwork_Click(object sender, EventArgs e)
        {
            if (tvwNetworks.SelectedNode != null && tvwNetworks.SelectedNode.Parent != null)
            {
                if (MessageBox.Show(StringValue.ConfirmNetworkDelete, "Confirm Delete", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    tvwNetworks.SelectedNode.Remove();
                    var name = tvwNetworks.SelectedNode.Name;
                    _networks.Remove(_networks.SingleOrDefault(n => n.Name == name));
                }
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
                    if (txtPShellOutput.SelectionStart <= _mincurpos)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;
                case Keys.Delete:
                    if (txtPShellOutput.SelectionStart < _mincurpos)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;
                case Keys.Left:
                    if (txtPShellOutput.SelectionStart <= _mincurpos)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        txtPShellOutput.SelectionStart = _mincurpos;
                    }
                    break;
                case Keys.Home:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    txtPShellOutput.SelectionStart = _mincurpos;
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
                        String cmd = txtPShellOutput.Text.Substring(_mincurpos, txtPShellOutput.Text.Length - _mincurpos);

                        LogOutput(cmd);
                        if (cmd.Trim() != "")
                        {
                            ProcessCommand(cmd);
                        }
                        else
                        {
                            DisplayOutput("\r\n");
                        }
                    }
                    break;
                case Keys.ControlKey:
                case Keys.Alt:
                    e.SuppressKeyPress = false;
                    e.Handled = false;
                    break;
                case Keys.Up:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    if (_cmdhist != null && _cmdhist.Count > 0)
                    {
                        if (_cmdhistidx <= _cmdhist.Count && _cmdhistidx > 0)
                        {
                            _cmdhistidx -= 1;
                            txtPShellOutput.Text = txtPShellOutput.Text.Substring(0, _mincurpos);
                            txtPShellOutput.AppendText(_cmdhist[_cmdhistidx]);
                            txtPShellOutput.SelectionStart = txtPShellOutput.Text.Length;
                        }
                    }
                    break;
                case Keys.Down:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    if (_cmdhist != null && _cmdhist.Count > 0)
                    {
                        if (_cmdhistidx >= 0 && _cmdhistidx < _cmdhist.Count - 1)
                        {
                            _cmdhistidx += 1;
                            txtPShellOutput.Text = txtPShellOutput.Text.Substring(0, _mincurpos);
                            txtPShellOutput.AppendText(_cmdhist[_cmdhistidx]);
                            txtPShellOutput.SelectionStart = txtPShellOutput.Text.Length;

                        }
                        else
                        {
                            txtPShellOutput.Text = txtPShellOutput.Text.Substring(0, _mincurpos);
                            txtPShellOutput.SelectionStart = txtPShellOutput.Text.Length;
                            _cmdhistidx = _cmdhist.Count;
                        }
                    }
                    break;
                case Keys.L:
                case Keys.C:
                case Keys.X:
                case Keys.V:
                    if (e.Control)
                    {
                        switch (e.KeyCode)
                        {
                            case Keys.L:
                                //Ctrl+L for CLS!
                                e.Handled = true;
                                e.SuppressKeyPress = true;
                                txtPShellOutput.Text = StringValue.psf;
                                _mincurpos = txtPShellOutput.Text.Length;
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
                        if (txtPShellOutput.SelectionStart < _mincurpos)
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
                    if (txtPShellOutput.SelectionStart < _mincurpos)
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
            _commands = _psf.GetCommand();
            LoadCommands(_commands);
        }

        private void btnShowAliases_Click(object sender, EventArgs e)
        {
            btnShowAliases.Checked = !btnShowAliases.Checked;
            LoadCommands(_commands);
        }

        private void btnShowFunctions_Click(object sender, EventArgs e)
        {
            btnShowFunctions.Checked = !btnShowFunctions.Checked;
            LoadCommands(_commands);
        }

        private void btnShowCmdlets_Click(object sender, EventArgs e)
        {
            btnShowCmdlets.Checked = !btnShowCmdlets.Checked;
            LoadCommands(_commands);
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
            this.WindowState = _lastwindowstate;
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
                foreach (ListViewItem lvw in lvwAlerts.SelectedItems)
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
                _alerts.Clear();
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
                        _alerts.Remove(lvw);
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
            _cancelscan = true;
        }

        private void btnRefreshNetworks_Click(object sender, EventArgs e)
        {
            LoadNetworks();
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
            using (var frm = new SystemForm())
            {
                if (frm.ShowDialog() != DialogResult.OK) return;
                var systemName = frm.SystemName;

                if (_lvwNetworkNodes.IsValid(systemName))
                {
                    var ipAddress = frm.IpAddress;
                    var description = frm.Description;
                    var status = "Unknown";
                    if (IPAddress.TryParse(ipAddress, out var ip))
                    {
                        var ping = new Ping();
                        var reply = ping.Send(ip);
                        if (reply?.Status == IPStatus.Success)
                            status = StringValue.Up;
                    }
                    _lvwNetworkNodes.Add(new NetworkNodeListViewItem(systemName)
                    {
                        IpAddress = ipAddress,
                        MacAddress = _scnr.GetMac(ipAddress),
                        Status = status,
                        Description = description
                    });
                }
                else
                    MessageBox.Show(StringValue.InvalidSystemName);
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
                _psf.Run(ghcmd, true, false, true);
                tcMain.SelectedTab = tbpPowerShell;
            }
        }

        private void mnuScriptGetHelp_Click(object sender, EventArgs e)
        {
            if (lvwScripts.SelectedItems.Count > 0)
            {
                ListViewItem lvw = lvwScripts.SelectedItems[0];
                String ghcmd = StringValue.GetHelpFull.Replace("{0}", "\"" + lvw.Tag + "\"");
                txtPShellOutput.AppendText(ghcmd + Environment.NewLine);
                txtPShellOutput.ReadOnly = true;
                _psf.Run(ghcmd, true, false, true);
                tcMain.SelectedTab = tbpPowerShell;
            }
        }

        private void btnRemoveSystem_Click(object sender, EventArgs e)
        {
            if (_lvwNetworkNodes.SelectedItems.Count > 0)
            {
                if (MessageBox.Show(StringValue.ConfirmRemoveSystem, "Confirm", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    _lvwNetworkNodes.BeginUpdate();
                    while (_lvwNetworkNodes.SelectedItems.Count > 0)
                    {
                        ListViewItem lvw = _lvwNetworkNodes.SelectedItems[0];
                        _lvwNetworkNodes.Items.Remove(lvw);
                    }
                    _lvwNetworkNodes.EndUpdate();
                    SaveSystems();
                    UpdateSystemCount();
                }
            }
        }
        #endregion

        #region ComboBox Events
        private void cmbLibraryTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_shown)
            {
                LoadCommands(_commands);
            }

        }
        #endregion

        #region Tab Pages
        private void tbpAlerts_TextChanged(object sender, EventArgs e)
        {
            int alertcnt = lvwAlerts.Items.Count;
            if (alertcnt > 0)
            {
                nimain.Icon = Properties.Resources.psficon_alert;
                this.Icon = Properties.Resources.psficon_alert;
                nimain.Text = StringValue.psftitle + " - Alerts (" + alertcnt.ToString() + ")";
                if (alertcnt > 1)
                {
                    nimain.BalloonTipText = String.Format(StringValue.AlertsBalloon, alertcnt.ToString());
                }
                else
                {
                    nimain.BalloonTipText = StringValue.AlertBalloon;
                }
                nimain.ShowBalloonTip(5);
            }
            else
            {
                nimain.Icon = Properties.Resources.psficon_ico;
                this.Icon = Properties.Resources.psficon_ico;
                nimain.Text = StringValue.psftitle;
                nimain.BalloonTipText = "";
            }
        }
        #endregion

        #region Tab Control
        private void tcMain_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tbpPowerShell)
            {
                txtPShellOutput.Select();
                txtPShellOutput.DrawCaret();
            }
        }
        #endregion

        #region NotifyIcon
        private void nimain_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = _lastwindowstate;
        }
        #endregion

        #endregion

        #region Public Properties
        public bool CancelIPScan
        {
            get { return _cancelscan; }
        }
        #endregion

    }
}