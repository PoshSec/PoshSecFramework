using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoshSec.Framework.Interface
{
    public partial class frmAddNetwork : Form
    {
        private string networkname = "";

        public frmAddNetwork()
        {
            InitializeComponent();
        }

        public string NetworkName
        {
            get { return networkname; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CheckName();
        }

        private void CheckName()
        {
            if (txtNetworkName.Text.Trim() != "" && txtNetworkName.Text.Trim() != Strings.StringValue.LocalNetwork)
            {
                networkname = txtNetworkName.Text.Trim();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(Strings.StringValue.InvalidNetworkName);
            }
        }

        private void txtNetworkName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                CheckName();
            }
        }
    }
}
