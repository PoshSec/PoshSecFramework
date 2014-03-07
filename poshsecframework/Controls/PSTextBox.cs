using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace poshsecframework.Controls
{
    class PSTextBox : TextBox
    {
        #region Private Variables
        private ToolStrip parstrip = null;
        #endregion

        #region Public Methods
        public PSTextBox()
        {
            Init();
        }
        #endregion

        #region Private Methods
        private void Init()
        {
            this.Dock = DockStyle.Fill;
            this.Multiline = true;
            this.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Font = new System.Drawing.Font("Lucida Console", (float)9.75);
            AddButtons();
        }

        private void AddButtons()
        {
            if (parstrip != null)
            {
                ToolStripButton exp = new ToolStripButton();
                exp.Text = "Save";
                exp.Image = Properties.Resources.table_save;
                exp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                exp.ToolTipText = "Save";
                exp.Alignment = ToolStripItemAlignment.Left;
                //exp.Click += new EventHandler(Save);
                parstrip.Items.Add(exp);
            }
        }
        #endregion

        #region Public Properties
        public ToolStrip ParentStrip
        {
            set
            {
                parstrip = value;
                AddButtons();
            }
        }
        #endregion
    }
}
