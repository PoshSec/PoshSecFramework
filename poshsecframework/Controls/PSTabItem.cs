using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Windows.Forms;

namespace poshsecframework.Controls
{
    class PSTabItem : TabPage
    {
        #region Private Variables
        private ToolStrip tbTools = new ToolStrip();
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        public PSTabItem()
        {
            tbTools.Dock = DockStyle.Top;
            tbTools.GripStyle = ToolStripGripStyle.Hidden;
            ToolStripButton tbClose = new ToolStripButton();
            tbClose.Text = "Close";
            tbClose.Image = Properties.Resources.tab_close_3;
            tbClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            tbClose.ToolTipText = "Close Tab";
            tbClose.Alignment = ToolStripItemAlignment.Right;
            tbClose.Click += new EventHandler(CloseTab);
            tbTools.Items.Add(tbClose);
            this.Controls.Add(tbTools);
        }

        public void AddGrid(System.Object[] CustomObject)
        {
            PSObjectGrid pgrid = new PSObjectGrid(CustomObject);
            pgrid.ParentStrip = tbTools;
            this.Controls.Add(pgrid);
            this.Controls.SetChildIndex(pgrid, 0);
        }
        #endregion

        #region Private Events
        private void CloseTab(object sender, EventArgs e)
        {
            if (this.Parent.GetType() == typeof(TabControl))
            {
                TabControl parent = (TabControl)this.Parent;
                if (parent != null)
                {
                    parent.TabPages.Remove(this);
                }
            }                        
        }
        #endregion
    }
}
