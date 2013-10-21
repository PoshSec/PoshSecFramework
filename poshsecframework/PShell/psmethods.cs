using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Windows.Forms;

namespace poshsecframework.PShell
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

        public class PSHosts
        {
            private frmMain frm = null;

            public PSHosts(frmMain ParentForm)
            {
                frm = ParentForm;
            }

            public Collection<PSObject> GetHosts(bool AllHosts = false)
            {
                if (AllHosts)
                {
                    return frm.GetHosts();
                }
                else
                {
                    return frm.GetCheckedHosts();
                }
            }
        }

        public class PSTab
        {
            private frmMain frm = null;
            
            public PSTab(frmMain ParentForm)
            {
                frm = ParentForm;
            }

            public void AddObjectGrid(System.Object[] CustomObject, String TabTitle)
            {
                poshsecframework.Controls.PSTabItem ptitm = new poshsecframework.Controls.PSTabItem();
                ptitm.Text = TabTitle;
                ptitm.AddGrid(CustomObject);
                frm.AddTabPage(ptitm);
            }
        }
    }
}
