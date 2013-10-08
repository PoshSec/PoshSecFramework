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
        System.Object[] psobj = null;

        private enum FilterType
        { 
            XML = 1,
            CSV,
            TXT
        }
        #endregion

        #region Public Methods

        public PSObjectGrid(System.Object[] CustomObject)
        {
            psobj = CustomObject;
            Init();
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
        private void Init()
        {
            this.Dock = DockStyle.Fill;
            this.View = System.Windows.Forms.View.Details;
            this.FullRowSelect = true;
            BuildGrid();
        }

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
            if (psobj != null && psobj.Count() > 0)
            {
                CreateHeaders(psobj[0]);
                if (psobj[0].GetType() == typeof(PSObject))
                {
                    foreach (PSObject pobj in psobj)
                    {
                        ListViewItem lvw = new ListViewItem();
                        int sidx = -1;
                        foreach (PSPropertyInfo prop in pobj.Properties)
                        {
                            sidx++;
                            if (prop != null)
                            {
                                if (sidx == 0)
                                {
                                    try
                                    {
                                        lvw.Text = (prop.Value ?? "").ToString();
                                    }
                                    catch (GetValueException)
                                    {
                                        lvw.Text = "";
                                    }

                                }
                                else
                                {
                                    try
                                    {
                                        lvw.SubItems.Add((prop.Value ?? "").ToString());
                                    }
                                    catch (GetValueInvocationException)
                                    {
                                        lvw.SubItems.Add("");
                                    }

                                }
                            }
                        }
                        this.Items.Add(lvw);
                    }
                }                
            }            
        }

        private void CreateHeaders(Object pobj)
        {
            if (pobj.GetType() == typeof(PSObject))
            {
                PSObject p = (PSObject)pobj;
                foreach (PSPropertyInfo prop in p.Properties)
                {
                    ColumnHeader col = new ColumnHeader();
                    col.Text = prop.Name;
                    col.Width = -2;
                    this.Columns.Add(col);
                }
            }            
        }

        private void ExportObject(FilterType type, String filename)
        {
            if (psobj != null && psobj.Count() > 0)
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
