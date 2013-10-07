using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Windows.Forms;

namespace poshsecframework.Controls
{
    class PSObjectGrid : ListView
    {
        #region Private Variables
        private ToolStrip parstrip = null;
        Collection<PSObject> psobj = null;
        #endregion

        #region Public Methods
        public PSObjectGrid(Collection<PSObject> CustomObject)
        {
            this.Dock = DockStyle.Fill;
            this.View = System.Windows.Forms.View.Details;
            psobj = CustomObject;
            BuildGrid();
        }

        public void Export(object sender, EventArgs e)
        {
            MessageBox.Show("Export button clicked. Not implemented yet.");
        }
        #endregion

        #region Private Methods
        private void AddButtons()
        {
            if (parstrip != null)
            {
                ToolStripButton exp = new ToolStripButton();
                exp.Text = "Export";
                exp.Image = Properties.Resources.table_save;
                exp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                exp.ToolTipText = "Export Grid Data";
                exp.Alignment = ToolStripItemAlignment.Left;
                exp.Click += new EventHandler(Export);
                parstrip.Items.Add(exp);
            }
        }

        private void BuildGrid()
        {
            if (psobj != null && psobj.Count > 0)
            {
                CreateHeaders(psobj[0]);
                foreach (PSObject pobj in psobj)
                {
                    ListViewItem lvw = new ListViewItem();
                    int sidx = -1;
                    foreach (PSNoteProperty prop in pobj.Properties)
                    {
                        sidx++;
                        if (sidx == 0)
                        {
                            lvw.Text = prop.Value.ToString();
                        }
                        else
                        {
                            lvw.SubItems.Add(prop.Value.ToString());
                        }
                    }
                    this.Items.Add(lvw);                    
                }
            }
        }

        private void CreateHeaders(PSObject pobj)
        {
            foreach (PSNoteProperty prop in pobj.Properties)
            {
                ColumnHeader col = new ColumnHeader();
                col.Text = prop.Name;
                col.Width = -2;
                this.Columns.Add(col);
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
