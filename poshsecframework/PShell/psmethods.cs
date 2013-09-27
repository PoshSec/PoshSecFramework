using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace psframework.PShell
{
    public class psmethods
    {
        public class PSMessageBox
        {
            public void Show(String message, String title = "Script Message")
            {
                System.Windows.Forms.MessageBox.Show(message, title);
            }
        }

        public class PSAlert
        {
            private String scriptname = "";
            private frmMain frm = null;

            public enum AlertType
            { 
                Information,
                Error,
                Warning,
                Severe,
                Critical
            }

            public PSAlert(frmMain ParentForm)
            {
                frm = ParentForm;
            }

            public void Add(String message, AlertType alerttype)
            {
                frm.AddAlert(message, alerttype, scriptname);
            }

            public String ScriptName
            {
                get { return scriptname; }
                set { scriptname = value; }
            }
        }

        public class PSStatus
        {
            private frmMain frm = null;
            private ListViewItem lvw = null;

            public PSStatus(frmMain ParentForm, ListViewItem StatusListViewItem)
            {
                frm = ParentForm;
                lvw = StatusListViewItem;
            }

            public void Update(String StatusMessage)
            {
                frm.UpdateStatus(StatusMessage, lvw);
            }
        }
    }
}
