using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using poshsecframework.Strings;

namespace poshsecframework.Interface
{
    public partial class frmScan : Form
    {
        #region Private Variables
        String[] ips = null;
        String selIP = null;
        #endregion

        #region Public Methods
        public frmScan()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Events
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbIPs.Text.Trim() != "")
            {
                selIP = cmbIPs.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(StringValue.SelectIPScan);
            }
        }
        #endregion

        #region Public Properties
        public String[] IPs
        {
            set 
            { 
                ips = value;
                if (ips != null && ips.Length > 0)
                {
                    cmbIPs.Items.Clear();
                    foreach (String ip in ips)
                    {
                        cmbIPs.Items.Add(ip);
                    }
                    cmbIPs.SelectedIndex = 0;
                }                
            }
        }

        public String SelectedIP
        {
            get { return selIP; }
        }
        #endregion

    }
}
