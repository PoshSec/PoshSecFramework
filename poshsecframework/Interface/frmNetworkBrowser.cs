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
using poshsecframework.Strings;

namespace poshsecframework.Interface
{
    public partial class frmNetworkBrowser : Form
    {
        #region Private Variables
        private Network.NetworkBrowser scnr = new Network.NetworkBrowser();
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
        }
        #endregion
        
        #region Private Events
        void scnr_ScanComplete(object sender, Network.ScanEventArgs e)
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
                    foreach (String system in rslts)
                    {
                        if (system != null && system != "")
                        {
                            ListViewItem lvwItm = new ListViewItem();
                            String[] ipinfo = system.Split('|');
                            lvwItm.Text = ipinfo[2];
                            lvwItm.SubItems.Add(ipinfo[1]);
                            lvwItm.SubItems.Add(scnr.GetMac(ipinfo[1]));
                            lvwItm.SubItems.Add(StringValue.Up);

                            lvwItm.ImageIndex = 2;
                            lvwSystems.Items.Add(lvwItm);
                            lvwSystems.Refresh();
                        }
                    }
                    lvwSystems.EndUpdate();
                }
                rslts = null;
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

        private void OnScanADComplete(ArrayList rslts)
        { 
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    OnScanADComplete(rslts);
                };
                this.Invoke(del);
            }
            else
            {
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

                        lvwItm.ImageIndex = 0;
                        lvwSystems.Items.Add(lvwItm);
                        lvwSystems.Refresh();
                        Application.DoEvents();
                    }
                    lvwSystems.EndUpdate();
                }
                rslts = null;
                EnableControls();
            }
        }

        private void ScanAD()
        {
            scnr.Domain = domain;
            scnr.ScanActiveDirectory();

            ArrayList rslts = scnr.Systems;
            OnScanADComplete(rslts);
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
