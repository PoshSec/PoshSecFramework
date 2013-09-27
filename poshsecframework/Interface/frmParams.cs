using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace psframework.Interface
{
    public partial class frmParams : Form
    {
        public frmParams()
        {
            InitializeComponent();
        }

        public void SetParameters(PShell.psparamtype p)
        {
            pgParams.SelectedObject = p;
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
                MessageBox.Show("There are required paramaters that are missing values. Please fill in all of the required parameters before proceeding.");
            }
        }
    }
}
