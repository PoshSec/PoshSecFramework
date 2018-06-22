using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PoshSec.Framework.Strings;

namespace PoshSec.Framework.Interface
{
    public partial class frmNetworkBrowser : Form
    {
        #region Private Variables
        private NetworkBrowser scnr = new NetworkBrowser();
        private Collection<PSObject> hosts = new Collection<PSObject>();
        private String domain = "";
        #endregion

        #region Public Methods
        public frmNetworkBrowser()
        {
            InitializeComponent();
            scnr.ScanComplete += scnr_ScanComplete;
            scnr.ScanCancelled += scnr_ScanCancelled;
            GetNetworks();
            ListSystems();
        }
        #endregion
        
        #region Private Events
        void scnr_ScanComplete(object sender, NetworkScanCompleteEventArgs e)
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
                if (rslts.Count > 0)
                {
                    lvwSystems.Items.Clear();
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

                                    lvwItm.Text = ipinfo[2];
                                    lvwItm.SubItems.Add(ipinfo[1]);
                                    lvwItm.SubItems.Add(scnr.GetMac(ipinfo[1]));
                                    lvwItm.SubItems.Add(StringValue.Up);

                                    lvwItm.ImageIndex = 2;
                                    lvwSystems.Items.Add(lvwItm);
                                    lvwSystems.Refresh();
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
                                        lvwItm.Text = sys.Name.Replace("CN=", "").ToString();

                                        lvwItm.SubItems.Add(ip);
                                        string macaddr = scnr.GetMac(ip);
                                        lvwItm.SubItems.Add(macaddr);
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

                                        lvwItm.ImageIndex = 2;
                                        lvwSystems.Items.Add(lvwItm);
                                        lvwSystems.Refresh();
                                    }
                                }
                            }
                        }                        
                    }
                    lvwSystems.EndUpdate();
                    lvwSystems.Sort();
                }
                EnableControls();
            }
        }

        void scnr_ScanCancelled(object sender, EventArgs e)
        {
            EnableControls();
        }

        private void cmbNetworks_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbNetworks.Enabled = false;
            btnRefresh.Enabled = false;
            btnOK.Enabled = false;
            btnCancel.Enabled = false;
            if (cmbNetworks.SelectedIndex == 0)
            {
                ScanbyIP();
            }
            else
            {
                domain = cmbNetworks.Text;
                lvwSystems.UseWaitCursor = true;
                Thread thd = new Thread(ScanAD);
                thd.Start();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lvwSystems.CheckedItems.Count > 0)
            {
                ListView.CheckedListViewItemCollection lvwitms = lvwSystems.CheckedItems;
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
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(StringValue.SelectSystems);
            }
        }
        #endregion

        #region Private Methods
        private void GetNetworks()
        {
            cmbNetworks.Items.Clear();
            cmbNetworks.Items.Add(StringValue.LocalNetwork);
            try
            {
                //Get Domain Name
                Forest hostForest = Forest.GetCurrentForest();
                DomainCollection domains = hostForest.Domains;

                foreach (Domain domain in domains)
                {
                    cmbNetworks.Items.Add(domain.Name);
                }
            }
            catch
            {
                //fail silently because it's not on A/D   
            }            
        }

        private void ListSystems()
        {
            if (Properties.Settings.Default.Systems != null && Properties.Settings.Default.Systems.Count > 0)
            {
                lvwSystems.Items.Clear();
                foreach (String system in Properties.Settings.Default.Systems)
                {
                    String[] systemparts = system.Split('|');
                    ListViewItem lvwItm = new ListViewItem();
                    lvwItm.Text = systemparts[0];
                    lvwItm.SubItems.Add(systemparts[1]);
                    lvwItm.SubItems.Add(systemparts[2]);
                    lvwItm.SubItems.Add(systemparts[4]);
                    lvwItm.ImageIndex = 2;
                    lvwSystems.Items.Add(lvwItm);
                }
            }
        }

        private void EnableControls()
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    EnableControls();
                };
                this.Invoke(del);
            }
            else
            {
                lvwSystems.UseWaitCursor = false;
                cmbNetworks.Enabled = true;
                btnRefresh.Enabled = true;
                btnOK.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        private void ScanbyIP()
        {
            lvwSystems.UseWaitCursor = true;
            scnr.ShowStatus = false;
            Thread thd = new Thread(scnr.ScanbyIP);
            thd.Start();
        }

        private void ScanAD()
        {
            scnr.Domain = domain;
            scnr.ScanActiveDirectory();
        }
        #endregion

        #region Public Property
        public Collection<PSObject> SelectedHosts
        {
            get { return hosts; }
        }

        public String SerializedHosts
        {
            get { return PSSerializer.Serialize(hosts); }
        }
        #endregion
    }
}
