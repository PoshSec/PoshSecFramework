using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using PoshSec.Framework.PShell;
using PoshSec.Framework.Strings;

namespace PoshSec.Framework.Controls
{
    public class PSAlertList : ListView
    {
        #region Private Variables
        private ToolStrip parstrip = null;
        private const int SEVERITY_WIDTH = 100;
        private const int MESSAGE_WIDTH = 535;
        private const int TIMESTAMP_WIDTH = 135;
        private const int SCRIPT_WIDTH = 150;
        private string scriptname = "";
        private ImageList imgListAlerts;
        private System.ComponentModel.IContainer components;
        private Syslog slog = null;
        private ToolStripLabel lblalertcount = new ToolStripLabel(String.Format(StringValue.AlertLabelFormat, "Alerts", 0));
        private int alertcount = 0;
        PSTabItem parent = null;
        private string tablabel = "";
        #endregion

        #region Public Methods
        public PSAlertList(String ScriptName, PSTabItem Parent)
        {
            scriptname = ScriptName;
            InitializeComponent();
            Init();
            parent = Parent;
            tablabel = Parent.Text;
        }

        public void Add(String message, int alerttype)
        {
            if (alerttype >= (int)PShell.psmethods.PSAlert.AlertType.Information && alerttype <= (int)PShell.psmethods.PSAlert.AlertType.Critical)
            {
                Add(message, (PShell.psmethods.PSAlert.AlertType)alerttype);
            }
        }
        #endregion

        #region Private Methods
        private void Init()
        {
            this.Dock = DockStyle.Fill;
            AddColumns();
            AddButtons();
        }

        private void AddColumns()
        {
            ColumnHeader chsev = new ColumnHeader();
            chsev.Text = "Severity";
            chsev.Width = SEVERITY_WIDTH;
            this.Columns.Add(chsev);
            ColumnHeader chmsg = new ColumnHeader();
            chmsg.Text = "Message";
            chmsg.Width = MESSAGE_WIDTH;
            this.Columns.Add(chmsg);
            ColumnHeader chtmst = new ColumnHeader();
            chtmst.Text = "Timestamp";
            chtmst.Width = TIMESTAMP_WIDTH;
            this.Columns.Add(chtmst);
            ColumnHeader chscpt = new ColumnHeader();
            chscpt.Text = "Script";
            chscpt.Width = SCRIPT_WIDTH;
            this.Columns.Add(chscpt);
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
                parstrip.Items.Add(lblalertcount);
            }
        }

        private void Add(String message, psmethods.PSAlert.AlertType alerttype)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    Add(message, alerttype);
                };
                this.Invoke(del);
            }
            else
            {
                alertcount++;
                lblalertcount.Text = String.Format(StringValue.AlertLabelFormat, "Alerts", alertcount);
                parent.Text = string.Format(StringValue.AlertLabelFormat, tablabel, alertcount);
                ListViewItem lvwitm = new ListViewItem();
                lvwitm.Text = alerttype.ToString();
                lvwitm.ImageIndex = (int)alerttype;
                lvwitm.SubItems.Add(message);
                lvwitm.ToolTipText = message;
                lvwitm.SubItems.Add(DateTime.Now.ToString(StringValue.TimeFormat));
                lvwitm.SubItems.Add(scriptname);
                this.Items.Add(lvwitm);
                this.Update();
                lvwitm.EnsureVisible();
                string alert = String.Format(StringValue.AlertFormat, lvwitm.SubItems[0].Text, lvwitm.SubItems[1].Text, lvwitm.SubItems[2].Text, lvwitm.SubItems[3].Text).Replace("\\r\\n", Environment.NewLine);
                alert += Environment.NewLine;
                LogAlert(alert);
                if (Properties.Settings.Default.UseSyslog)
                {
                    if (slog == null)
                    {
                        slog = new Syslog(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(Properties.Settings.Default.SyslogServer), Properties.Settings.Default.SyslogPort));
                    }
                    slog.SendMessage(alerttype, scriptname, message);
                    slog.Close();
                    slog = null;
                }
            }
            
        }

        private void LogAlert(String text)
        {
            if (Properties.Settings.Default.LogAlerts)
            {
                if (!File.Exists(Properties.Settings.Default.AlertLogFile))
                {
                    DirectoryInfo dirinfo = new DirectoryInfo(Properties.Settings.Default.AlertLogFile);
                    if (!Directory.Exists(dirinfo.Parent.FullName))
                    {
                        Directory.CreateDirectory(dirinfo.Parent.FullName);
                    }
                }
                StreamWriter wtr = File.AppendText(Properties.Settings.Default.AlertLogFile);
                wtr.Write(text);
                wtr.Flush();
                wtr.Close();
            }
        }

        private void Save(object sender, EventArgs e)
        {
            MessageBox.Show("Save not implemented yet.");
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSAlertList));
            this.imgListAlerts = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imgListAlerts
            // 
            this.imgListAlerts.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListAlerts.ImageStream")));
            this.imgListAlerts.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListAlerts.Images.SetKeyName(0, "dialog-information-3.png");
            this.imgListAlerts.Images.SetKeyName(1, "dialog-error-4.png");
            this.imgListAlerts.Images.SetKeyName(2, "dialog-warning-3.png");
            this.imgListAlerts.Images.SetKeyName(3, "dialog-warning-2.png");
            this.imgListAlerts.Images.SetKeyName(4, "exclamation.png");
            // 
            // PSAlertList
            // 
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FullRowSelect = true;
            this.HideSelection = false;
            this.SmallImageList = this.imgListAlerts;
            this.View = System.Windows.Forms.View.Details;
            this.ResumeLayout(false);

        }
    }
}
