using System;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PoshSec.Framework.Properties;
using PoshSec.Framework.Strings;

namespace PoshSec.Framework.Interface
{
    public partial class frmNetworkBrowser : Form
    {
        private readonly Networks _networks = new Networks();
        private readonly Collection<PSObject> _hosts = new Collection<PSObject>();


        public frmNetworkBrowser()
        {
            InitializeComponent();
            LoadNetworks();
            ListSystems();
        }

        private void NetworkBrowserScanComplete(object sender, NetworkScanCompleteEventArgs e)
        {
            if (InvokeRequired)
            {
                MethodInvoker del = delegate { NetworkBrowserScanComplete(sender, e); };
                Invoke(del);
            }
            else
            {

                var systems = e.Network.Nodes;
                _lvwSystems.Load(systems);
                _lvwSystems.Sorting = SortOrder.Ascending;
                _lvwSystems.Sort();

                EnableControls();
            }
        }

        private void NetworkBrowserScanCancelled(object sender, EventArgs e)
        {
            EnableControls();
        }

        private void cmbNetworks_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbNetworks.Enabled = false;
            btnRefresh.Enabled = false;
            btnOK.Enabled = false;
            btnCancel.Enabled = false;
            var selectedItem = cmbNetworks.SelectedItem;
            Scan((Network)selectedItem);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_lvwSystems.CheckedItems.Count > 0)
            {
                var lvwitms = _lvwSystems.CheckedItems;
                foreach (ListViewItem lvw in lvwitms)
                {
                    var pobj = new PSObject();
                    var idx = -1;
                    foreach (ColumnHeader col in _lvwSystems.Columns)
                    {
                        idx++;
                        pobj.Properties.Add(new PSNoteProperty(col.Text.Replace(" ", "_"), lvw.SubItems[idx].Text));
                    }

                    _hosts.Add(pobj);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(StringValue.SelectSystems);
            }
        }

        private void LoadNetworks()
        {
            cmbNetworks.Items.Clear();
            cmbNetworks.Items.Add(StringValue.LocalNetwork);
            try
            {
                //Get Domain Name
                var hostForest = Forest.GetCurrentForest();
                var domains = hostForest.Domains;

                foreach (Domain domain in domains)
                {
                    var network = new DomainNetwork(domain.Name);
                    cmbNetworks.Items.Add(domain);
                }
            }
            catch
            {
                //fail silently because it's not on A/D   
            }
        }

        private void ListSystems()
        {
            if (Settings.Default.Systems != null && Settings.Default.Systems.Count > 0)
            {
                _lvwSystems.Items.Clear();
                foreach (var system in Settings.Default.Systems)
                {
                    var systemparts = system.Split('|');
                    var lvwItm = new ListViewItem();
                    lvwItm.Text = systemparts[0];
                    lvwItm.SubItems.Add(systemparts[1]);
                    lvwItm.SubItems.Add(systemparts[2]);
                    lvwItm.SubItems.Add(systemparts[4]);
                    lvwItm.ImageIndex = 2;
                    _lvwSystems.Items.Add(lvwItm);
                }
            }
        }

        private void EnableControls()
        {
            if (InvokeRequired)
            {
                MethodInvoker del = delegate { EnableControls(); };
                Invoke(del);
            }
            else
            {
                _lvwSystems.UseWaitCursor = false;
                cmbNetworks.Enabled = true;
                btnRefresh.Enabled = true;
                btnOK.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        private void Scan(Network network)
        {
            _lvwSystems.UseWaitCursor = true;

            Task.Run(() =>
            {
                var networkBrowser = new NetworkBrowser(network);
                networkBrowser.NetworkScanComplete += NetworkBrowserScanComplete;
                networkBrowser.NetworkScanCancelled += NetworkBrowserScanCancelled;
                networkBrowser.Scan();
                networkBrowser.NetworkScanComplete -= NetworkBrowserScanComplete;
                networkBrowser.NetworkScanCancelled -= NetworkBrowserScanCancelled;
            });
        }

        public string SerializedHosts => PSSerializer.Serialize(_hosts);
    }
}