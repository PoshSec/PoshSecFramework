using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using poshsecframework.Strings;

namespace poshsecframework.PShell
{
    class pshell
    {
        #region " Private Variables "
        private String pspath;
        private frmMain frm;
        private pscript ps;
        private bool clicked;
        private bool scroll;
        private bool scheduled = false;
        private string loaderrors = "";
        private bool paramcancelled = false;
        #endregion

        #region " Public Methods "
        public pshell(frmMain ParentForm)
        {
            try
            {
                pspath = poshsecframework.Properties.Settings.Default["ScriptPath"].ToString();
                ps = new pscript();
                frm = ParentForm;
                ps.ParentForm = frm;
                ps.ScriptCompleted += new EventHandler<pseventargs>(ScriptCompleted);
                if (ps.LoadErrors != "")
                {
                    loaderrors = ps.LoadErrors;
                }
            }
            catch (Exception e)
            { 
                //Base Exception Handler
                MessageBox.Show(StringValue.UnhandledException + Environment.NewLine + e.Message + Environment.NewLine + "Stack Trace:" + Environment.NewLine + e.StackTrace);
            }            
        }

        public void ImportPSModules(Collection<String> enabledmods)
        {        
            ps.ImportPSModules(enabledmods);
        }

        public Collection<PSObject> GetCommand()
        {
            return ps.GetCommand();
        }

        public List<psparameter> CheckForParams(String scriptcommand)
        {
            paramcancelled = false;
            List<psparameter> parms = ps.CheckForParams(scriptcommand);
            paramcancelled = ps.ParamSelectionCancelled;
            return parms;
        }

        public void Run(Utility.ScheduleItem sched)
        {
            clicked = false;
            scroll = false;
            scheduled = true;
            if (File.Exists(sched.ScriptPath))
            {
                try
                {
                    ListViewItem lvw = new ListViewItem();
                    lvw.Text = "Scheduled Script: " + sched.ScriptName;
                    lvw.SubItems.Add("Running...");
                    lvw.ImageIndex = 4;

                    ps.ParentForm = frm;
                    ps.Script = sched.ScriptPath;
                    ps.IsCommand = false;
                    ps.Clicked = false;
                    ps.IsScheduled = true;
                    ps.ScriptListView = lvw;
                    ps.Parameters = sched.Parameters.Properties;

                    Thread thd = new Thread(ps.RunScript);
                    thd.SetApartmentState(ApartmentState.STA);
                    lvw.Tag = thd;

                    frm.AddActiveScript(lvw);
                    thd.Start();
                }
                catch (Exception e)
                {
                    MessageBox.Show(StringValue.UnhandledException + Environment.NewLine + e.Message + Environment.NewLine + "Stack Trace:" + Environment.NewLine + e.StackTrace);
                }
            }
        }
        
        public void Run(string ScriptCommand, bool IsCommand = false, bool Clicked = true, bool Scroll = false)
        {
            clicked = Clicked;
            scroll = Scroll;
            String spath = "";
            if (!IsCommand)
            {
                spath = Path.Combine(pspath, ScriptCommand);
            } 
            if(IsCommand || File.Exists(spath))
            {
                try
                {
                    ListViewItem lvw = new ListViewItem();
                    lvw.Text = ScriptCommand;
                    lvw.SubItems.Add("Running...");
                    lvw.ImageIndex = 4;

                    ps.ParentForm = frm;
                    if (IsCommand)
                    {
                        ps.Script = ScriptCommand;
                    }
                    else
                    {
                        ps.Script = spath;
                    }
                    ps.IsCommand = IsCommand;
                    ps.Clicked = clicked;
                    ps.ScriptListView = lvw;
                    ps.Parameters.Clear();
                    
                    Thread thd = new Thread(ps.RunScript);
                    thd.SetApartmentState(ApartmentState.STA);
                    lvw.Tag = thd;

                    frm.AddActiveScript(lvw);
                    thd.Start();
                }
                catch (Exception e)
                {
                    MessageBox.Show(StringValue.UnhandledException + Environment.NewLine + e.Message + Environment.NewLine + "Stack Trace:" + Environment.NewLine + e.StackTrace);
                }
                              
            }
        }
        #endregion

        #region " Private Methods "
        private void ScriptCompleted(object sender, EventArgs e)
        {
            pseventargs rslts = (pseventargs)e;
            if (!scheduled)
            {                
                frm.DisplayOutput(rslts.Results, rslts.ScriptListView, clicked, rslts.Cancelled, scroll);
            }
            else
            {
                frm.RemoveActiveScript(rslts.ScriptListView);
            }
        }
        #endregion

        #region " Public Properties "
        public frmMain ParentForm
        {
            set { frm = value; }
        }

        public string LoadErrors
        {
            get { return loaderrors; }
        }

        public bool ParamSelectionCancelled
        {
            get { return paramcancelled; }
        }
        #endregion
    }
}
