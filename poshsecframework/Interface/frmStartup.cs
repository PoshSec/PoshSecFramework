using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PoshSec.Framework.Interface
{
    public partial class frmStartup : Form
    {
        public frmStartup()
        {
            InitializeComponent();
        }

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
                lblStatus.Refresh();
            }
        }
    }
}
