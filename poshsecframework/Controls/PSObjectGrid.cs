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

        private enum FilterType
        { 
            XML = 1,
            CSV,
            TXT
        }
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
            String expfilter = "Extensible Markup Language (*.xml)|*.xml|Comma Separate Values (*.csv)|*.csv|Tabbed Delimited (*.txt)|*.txt";
            SaveFileDialog dlgExport = new SaveFileDialog();
            dlgExport.Filter = expfilter;
            dlgExport.CheckFileExists = false;
            dlgExport.CheckPathExists = true;
            dlgExport.Title = "Export As...";
            if (dlgExport.ShowDialog() == DialogResult.OK)
            {
                ExportObject((FilterType)dlgExport.FilterIndex, dlgExport.FileName);
            }
            dlgExport.Dispose();
            dlgExport = null;
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
                        if (prop != null)
                        {
                            if (sidx == 0)
                            {
                                lvw.Text = (prop.Value ?? "").ToString();
                            }
                            else
                            {
                                lvw.SubItems.Add((prop.Value ?? "").ToString());
                            }
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

        private void ExportObject(FilterType type, String filename)
        {
            if (psobj != null && psobj.Count > 0)
            {
                Utility.ExportObject exobj = new Utility.ExportObject();
                switch (type)
                { 
                    case FilterType.XML:
                        exobj.XML(psobj, filename);
                        break;
                    case FilterType.CSV:
                        exobj.CSV(psobj, filename);
                        break;
                    case FilterType.TXT:
                        exobj.TXT(psobj, filename);
                        break;
                }
                exobj = null;
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
