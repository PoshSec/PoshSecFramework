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
                exp.Click += new EventHandler(Save);
                parstrip.Items.Add(exp);
            }
        }

        private void Save(object sender, EventArgs e)
        {
            SaveFileDialog sdlg = new SaveFileDialog();
            sdlg.CheckPathExists = true;
            sdlg.Title = "Save As...";
            sdlg.Filter = "Text Document (*.txt)|*.txt|All Documents (*.*)|*.*";
            if (sdlg.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter wtr = System.IO.File.CreateText(sdlg.FileName);
                wtr.Write(this.Text);
                wtr.Flush();
                wtr.Close();
                MessageBox.Show(Strings.StringValue.FileSavedSuccessfully);
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
