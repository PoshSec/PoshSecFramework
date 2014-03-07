using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
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

            public AlertType GetAlertTypeFromString(String alerttype)
            {
                AlertType rtn = AlertType.Information;
                bool found = false;
                int idx = 0;
                do
                {
                    if (((AlertType)idx).ToString() == alerttype)
                    {
                        found = true;
                        rtn = (AlertType)idx;
                    }
                    idx++;
                } while (!found && idx <= (int)AlertType.Critical);
                return rtn;
            }

            public void Add(String message, AlertType alerttype)
            {
                if (frm != null)
                {
                    frm.AddAlert(message, alerttype, scriptname);
                }                
            }

            public void Add(String message, int alerttype)
            {
                if (alerttype >= (int)AlertType.Information && alerttype <= (int)AlertType.Critical)
                {
                    Add(message, (AlertType)alerttype);
                }
                else
                {
                    if (frm != null)
                    {
                        String msg = Strings.StringValue.InvalidAlertType;
                        for (int idx = 0; idx <= (int)AlertType.Critical; idx++)
                        {
                            msg += "[" + idx.ToString() + "] " + ((AlertType)idx).ToString() + ", ";
                        }
                        msg = msg.Substring(0, msg.Length - 2);
                        frm.DisplayOutput(msg);
                    }
                }
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
                if (frm != null && lvw != null)
                {
                    frm.UpdateStatus(StatusMessage, lvw);
                }                
            }

            public void WriteProgress(String Progress)
            {
                if (frm != null && lvw != null)
                {
                    frm.UpdateProgress(Progress, lvw);
                }
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
                if (frm != null)
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
                else
                {
                    return null;
                }
            }

            public String GetHostsCsv(bool AllHosts = false)
            {
                if (frm != null)
                {
                    string rtncsv = "";
                    Collection<PSObject> hosts = null;
                    if (AllHosts)
                    {
                        hosts = frm.GetHosts();
                    }
                    else
                    {
                        hosts = frm.GetCheckedHosts();
                    }
                    if (hosts != null)
                    {
                        List<String> csvhosts = new List<string>();
                        foreach (PSObject host in hosts)
                        {
                            csvhosts.Add(host.Properties["Name"].Value.ToString());
                        }
                        rtncsv = string.Join(",", csvhosts.ToArray());
                    }
                    return rtncsv;
                }
                else
                {
                    return "";
                }
            }

            public List<String> GetHostsList(bool AllHosts = false)
            {
                if (frm != null)
                {
                    List<String> rtn = new List<string>();
                    Collection<PSObject> hosts = null;
                    if (AllHosts)
                    {
                        hosts = frm.GetHosts();
                    }
                    else
                    {
                        hosts = frm.GetCheckedHosts();
                    }
                    if (hosts != null)
                    {
                        foreach (PSObject host in hosts)
                        {
                            rtn.Add(host.Properties["Name"].Value.ToString());
                        }
                    }
                    return rtn;
                }
                else
                {
                    return null;
                }
            }

            public PSObject DeserializeHosts(String serializedhosts)
            {
                PSObject hosts = (PSObject)PSSerializer.Deserialize(serializedhosts);
                return hosts;
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
                if (frm != null)
                {
                    poshsecframework.Controls.PSTabItem ptitm = new poshsecframework.Controls.PSTabItem();
                    ptitm.Text = TabTitle;
                    ptitm.AddGrid(CustomObject);
                    frm.AddTabPage(ptitm);
                }
                else
                {
                    throw new Exception("Parent Form is not set in PSTab.");
                }
            }

            public void AddText(String Text, String TabTitle)
            {
                poshsecframework.Controls.PSTabItem ptitm = new poshsecframework.Controls.PSTabItem();
                ptitm.Text = TabTitle;
                ptitm.AddText(Text);
                frm.AddTabPage(ptitm);
            }
        }
    }
}
