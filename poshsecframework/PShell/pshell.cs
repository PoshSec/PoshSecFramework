using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        #endregion

        #region " Public Methods "
        public pshell()
        {
            try
            {
                pspath = poshsecframework.Properties.Settings.Default["ScriptPath"].ToString();
                ps = new pscript();
                ps.ScriptCompleted += new EventHandler<pseventargs>(ScriptCompleted);
            }
            catch (Exception e)
            { 
                //Base Exception Handler
                MessageBox.Show(StringValue.UnhandledException + Environment.NewLine + e.Message + Environment.NewLine + "Stack Trace:" + Environment.NewLine + e.StackTrace);
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
            frm.DisplayOutput(rslts.Results, rslts.ScriptListView, clicked, rslts.Cancelled, scroll);
        }
        #endregion

        #region " Public Properties "
        public frmMain ParentForm
        {
            set { frm = value; }
        }
        #endregion
    }
}
