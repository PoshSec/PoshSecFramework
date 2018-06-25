using System;
using System.Windows.Forms;
using PoshSec.Framework.Strings;

namespace PoshSec.Framework
{
    public partial class frmScan : Form
    {
        public frmScan()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbIPs.Text.Trim() != "")
            {
                SelectedIP = cmbIPs.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(StringValue.SelectIPScan);
            }
        }

        private string[] ips;

        public string[] IPs
        {
            set
            {
                ips = value;
                if (ips != null && ips.Length > 0)
                {
                    cmbIPs.Items.Clear();
                    foreach (var ip in ips) cmbIPs.Items.Add(ip);
                    cmbIPs.SelectedIndex = 0;
                }
            }
        }

        public string SelectedIP { get; private set; }
    }
}