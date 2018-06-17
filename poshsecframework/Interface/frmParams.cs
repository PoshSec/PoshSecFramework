using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PoshSec.Framework.Strings;

namespace PoshSec.Framework.Interface
{
    public partial class frmParams : Form
    {
        private DialogResult _frmresult = DialogResult.Cancel;

        public frmParams()
        {
            InitializeComponent();
        }

        public void ShowFromThread()
        {
            _frmresult = this.ShowDialog();
        }

        public void SetParameters(PShell.psparamtype p)
        {
            pgParams.SelectedObject = p;
        }

        public DialogResult FormCloseResult
        {
            get { return _frmresult; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool hasrequired = true;
            PShell.psparamtype p = (PShell.psparamtype)pgParams.SelectedObject;
            foreach (PShell.psparameter parm in p.Properties)
            {
                if (parm.Category == "Required" && parm.Value == null && parm.DefaultValue == null)
                {
                    hasrequired = false;
                }
            }

            if (hasrequired)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(StringValue.RequireParams);
            }
        }
    }
}
