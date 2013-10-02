using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Management.Automation;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace psframework
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
        #endregion

        #region Form
        public frmMain()
        {
            InitializeComponent();
            Initialize();
            GetNetworks();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (lvwActiveScripts.Items.Count > 0)
                {
                    if (MessageBox.Show("You have active scripts running. If you exit, all running scripts will be terminated. Are you sure you want to exit?", "Active Scripts", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
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
            txtPShellOutput.Text = "psf > ";
            mincurpos = txtPShellOutput.Text.Length;
            txtPShellOutput.SelectionStart = mincurpos;
            scnr.ParentForm = this;
            cmbLibraryTypes.SelectedIndex = 1;
            psf.ParentForm = this;
            GetLibrary();
            GetCommand();
        }

        #region Network
        private void GetNetworks()
        {
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
                String localHost = Dns.GetHostName();
                String localIP = scnr.GetIP(localHost);

                ListViewItem lvwItm = new ListViewItem();

                lvwItm.Text = localHost;
                lvwItm.SubItems.Add(localIP);
                lvwItm.SubItems.Add("00-00-00-00-00-00");
                lvwItm.SubItems.Add("Up");
                lvwItm.SubItems.Add("Not Installed");
                lvwItm.SubItems.Add("0");
                lvwItm.SubItems.Add(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));

                lvwItm.ImageIndex = 2;
                lvwSystems.Items.Add(lvwItm);
                lvwSystems.Refresh();

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
            lvwSystems.Items.Clear();
            if (tvwNetworks.SelectedNode != null && tvwNetworks.SelectedNode.Tag != null)
            {
                SystemType typ = (SystemType)Enum.Parse(typeof(SystemType), tvwNetworks.SelectedNode.Tag.ToString());
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
                MessageBox.Show("Please select a network first.");
            }
        }

        private void ScanAD()
        {
            String domain = tvwNetworks.SelectedNode.Text;
            ArrayList rslts = scnr.ScanActiveDirectory(domain);
            if (rslts.Count > 0)
            {
                foreach (DirectoryEntry system in rslts)
                {
                    ListViewItem lvwItm = new ListViewItem();
                    lvwItm.Text = system.Name.ToString();

                    String ipadr = scnr.GetIP(system.Name);
                    lvwItm.SubItems.Add(ipadr);
                    lvwItm.SubItems.Add("00-00-00-00-00-00");
                    bool isup = false;
                    if (ipadr != "0.0.0.0 (unknown host)")
                    {
                        isup = scnr.Ping(system.Name, 1, 500);
                    }
                    if (isup)
                    {
                        lvwItm.SubItems.Add("Up");
                    }
                    else
                    {
                        lvwItm.SubItems.Add("Down");
                    }
                    lvwItm.SubItems.Add("Not Installed");
                    lvwItm.SubItems.Add("0");
                    lvwItm.SubItems.Add(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));

                    lvwItm.ImageIndex = 2;
                    lvwSystems.Items.Add(lvwItm);
                    lvwSystems.Refresh();
                    Application.DoEvents();
                }
            }

            rslts = null;
        }

        private void ScanbyIP()
        {
            lvwSystems.Items.Clear();
            btnCancelScan.Enabled = true;
            scnr.ParentForm = this;
            cancelscan = false;
            ArrayList rslts = scnr.ScanbyIP();
            if (rslts.Count > 0 && !cancelscan)
            {
                SetProgress(0, rslts.Count);
                foreach (String system in rslts)
                {
                    ListViewItem lvwItm = new ListViewItem();
                    
                    SetStatus("Adding " + system + ", please wait...");
                    
                    lvwItm.Text = scnr.GetHostname(system);
                    lvwItm.SubItems.Add(system);
                    lvwItm.SubItems.Add("00-00-00-00-00-00");
                    lvwItm.SubItems.Add("Up");
                    lvwItm.SubItems.Add("Not Installed");
                    lvwItm.SubItems.Add("0");
                    lvwItm.SubItems.Add(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));

                    lvwItm.ImageIndex = 2;
                    lvwSystems.Items.Add(lvwItm);
                    lvwSystems.Refresh();

                    pbStatus.Value += 1;
                    Application.DoEvents();
                }
            }

            rslts = null;
            HideProgress();
            btnCancelScan.Enabled = true;
            lblStatus.Text = "Ready";            
        }

        private void RunScript()
        {
            if (lvwScripts.SelectedItems.Count > 0)
            {
                ListViewItem lvw = lvwScripts.SelectedItems[0];
                //This needs to be a separate runspace.
                PShell.pshell ps = new PShell.pshell();
                ps.ParentForm = this;
                ps.Run(lvw.Text);
                ps = null;
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
                        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(script);
                        psi.UseShellExecute = true;
                        psi.Verb = "open";
                        System.Diagnostics.Process prc = new System.Diagnostics.Process();
                        prc.StartInfo = psi;
                        prc.Start();
                        prc = null;
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
            lblStatus.Text = message;
            Application.DoEvents();
        }
        #endregion

        #region ProgressBar
        public void SetProgress(int Value, int Maximum)
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

        public void HideProgress()
        {
            pbStatus.Visible = false;
        }
        #endregion

        #region "PowerShell"
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
                if ((txtPShellOutput.Text.Length + output.Length + (Environment.NewLine + "psf > ").Length) > txtPShellOutput.MaxLength)
                {
                    txtPShellOutput.Text = txtPShellOutput.Text.Substring(output.Length + 500, txtPShellOutput.Text.Length - (output.Length + 500));
                }
                txtPShellOutput.AppendText(output);
                txtPShellOutput.AppendText(Environment.NewLine + "psf > ");
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
                if (lvw != null)
                {
                    lvw.Remove();
                    tbpScripts.Text = "Active Scripts (" + lvwActiveScripts.Items.Count.ToString() + ")";
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
                    lvwitm.SubItems.Add(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));
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
                tbpScripts.Text = "Active Scripts (" + lvwActiveScripts.Items.Count.ToString() + ")";
            }
        }

        private void LaunchWinUpdate()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("wuapp");
                psi.UseShellExecute = true;
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
                cmdhist.Add(cmd);
                cmdhistidx = cmdhist.Count;
                switch (cmd.ToUpper())
                { 
                    case "CLS": case "CLEAR":
                        txtPShellOutput.Text = "psf > ";
                        txtPShellOutput.SelectionStart = txtPShellOutput.Text.Length;
                        mincurpos = txtPShellOutput.Text.Length;
                        break;
                    case "APT-GET UPDATE":
                        txtPShellOutput.AppendText(Environment.NewLine + "psf > ");
                        txtPShellOutput.SelectionStart = txtPShellOutput.Text.Length;
                        mincurpos = txtPShellOutput.Text.Length;
                        LaunchWinUpdate();
                        break; 
                    case "RELOAD":
                        if (lvwActiveScripts.Items.Count == 0)
                        {
                            Initialize();
                        }
                        else 
                        {
                            txtPShellOutput.AppendText(Environment.NewLine + "Can not reload the framework because there are scripts running. Please stop all scripts before issuing the reload command again." + Environment.NewLine);
                            txtPShellOutput.AppendText(Environment.NewLine + "psf > ");
                            mincurpos = txtPShellOutput.Text.Length;
                        }
                        break;
                    case "EXIT":
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

        private void CheckSettings()
        {
            //Ensure we have settings and that if it's .\ to change to application path.
            String scrpath = poshsecframework.Properties.Settings.Default.ScriptPath;
            String frwpath = poshsecframework.Properties.Settings.Default.FrameworkPath;
            String modpath = poshsecframework.Properties.Settings.Default.ModulePath;
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
                String scriptroot = poshsecframework.Properties.Settings.Default["ScriptPath"].ToString(); ; // Get this variable from Settings.
                if (Directory.Exists(scriptroot))
                {
                    String[] scpaths = Directory.GetFiles(scriptroot, "*.ps1", SearchOption.TopDirectoryOnly);
                    if (scpaths != null)
                    {
                        lvwScripts.BeginUpdate();
                        lvwScripts.Items.Clear();
                        foreach (String scpath in scpaths)
                        {
                            ListViewItem lvw = new ListViewItem();
                            lvw.Text = new FileInfo(scpath).Name;
                            lvw.ImageIndex = 4;
                            lvw.Tag = scpath;
                            lvwScripts.Items.Add(lvw);
                        }
                        lvwScripts.EndUpdate();
                    }
                }
                else
                { 
                    AddAlert("Unable to find the Script Path. Check your settings", PShell.psmethods.PSAlert.AlertType.Error, "PoshSec Framework");
                }
            }
            catch (Exception e)
            {
                DisplayError(e);
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
            MessageBox.Show("Not implemented yet. Soon!");
        }

        private void mnuPSFWiki_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet. Soon!");
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
                MessageBox.Show("You can not change the settings while scripts or commands are running. Please stop any commands or scripts and then try again.");
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
                txtPShellOutput.AppendText(Environment.NewLine + "A command is already running. Please wait, or cancel the command and try again." + Environment.NewLine + Environment.NewLine);
                txtPShellOutput.AppendText("psf > ");
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
            }
            else
            {
                btnViewScript.Enabled = false;
                btnRunScript.Enabled = false;
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
                                txtPShellOutput.Text = "psf > ";
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
            txtPShellOutput.DrawCaret();
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
                MessageBox.Show("Please select an active script.");
            }
        }

        private void btnClearAlerts_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear all of the alerts?", "Confirm", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                lvwAlerts.Items.Clear();
                lvwAlerts_Update();
            }
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
            MessageBox.Show("Not implemented yet. Soon!");
        }

        private void btnAddSystem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet. Soon!");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            cancelscan = true;
        }

        private void mnuCmdGetHelp_Click(object sender, EventArgs e)
        {
            if (lvwCommands.SelectedItems.Count > 0)
            {
                ListViewItem lvw = lvwCommands.SelectedItems[0];
                String ghcmd = "Get-Help " + lvw.Text + " -full | Out-String";
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
                String ghcmd = "Get-Help \"" + Path.Combine(poshsecframework.Properties.Settings.Default.ScriptPath, lvw.Text) + "\" -full | Out-String";
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