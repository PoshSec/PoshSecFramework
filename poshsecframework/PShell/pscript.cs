using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace psframework.PShell
{
    class pscript
    {
        #region " Private Variables "
        private RunspaceConfiguration rspaceconfig;
        private Runspace rspace;
        private String scriptcommand;
        private bool iscommand = false;
        private bool clicked = true;
        private List<psparameter> scriptparams;
        private StringBuilder rslts = new StringBuilder();
        private psexception psexec = new psexception();
        private bool cancel = false;
        private frmMain frm = null;
        private ListViewItem scriptlvw;
        private psvariables.PSRoot PSRoot;
        private psvariables.PSModRoot PSModRoot;
        private psvariables.PSFramework PSFramework;
        private psmethods.PSMessageBox PSMessageBox;
        private psmethods.PSAlert PSAlert;
        private psmethods.PSStatus PSStatus;
        private psmethods.PSHosts PSHosts;
        private psmethods.PSTab PSTab;
        #endregion

        #region " Public Events "
        public EventHandler<pseventargs> ScriptCompleted;
        #endregion

        #region " Private Methods "
        private void InitializeScript()
        {
            rspaceconfig = RunspaceConfiguration.Create();
            rspace = RunspaceFactory.CreateRunspace(rspaceconfig);
            rspace.Open();
            InitializeSessionVars();
        }

        private void InitializeSessionVars()
        {
            if (rspace.RunspaceAvailability == RunspaceAvailability.Available)
            {
                PSAlert = new psmethods.PSAlert(frm);
                PSRoot = new psvariables.PSRoot("PSRoot");
                PSStatus = new psmethods.PSStatus(frm, scriptlvw);
                PSModRoot = new psvariables.PSModRoot("PSModRoot");
                PSFramework = new psvariables.PSFramework("PSFramework");
                PSMessageBox = new psmethods.PSMessageBox();
                PSTab = new psmethods.PSTab(frm);
                PSHosts = new psmethods.PSHosts(frm);
                rspace.SessionStateProxy.PSVariable.Set(PSRoot);
                rspace.SessionStateProxy.PSVariable.Set(PSModRoot);
                rspace.SessionStateProxy.PSVariable.Set(PSFramework);
                rspace.SessionStateProxy.SetVariable("PSMessageBox", PSMessageBox);
                rspace.SessionStateProxy.SetVariable("PSAlert", PSAlert);
                rspace.SessionStateProxy.SetVariable("PSStatus", PSStatus);
                rspace.SessionStateProxy.SetVariable("PSHosts", PSHosts);
                rspace.SessionStateProxy.SetVariable("PSTab", PSTab);
            }
        }
        #endregion

        #region " Public Methods "
        public void Test()
        {
            OnScriptComplete(new pseventargs("It worked!", null, false));
        }

        public pscript()
        {            
            try
            {
                InitializeScript();
            }
            catch (Exception e)
            {
                //Base Exception Handler
                OnScriptComplete(new pseventargs("Unhandled exception in script function." + Environment.NewLine + e.Message + Environment.NewLine + "Stack Trace:" + Environment.NewLine + e.StackTrace, null, false));
            } 
        }

        public void Dispose()
        {
            rspace.Close();
            rspace.Dispose();
            rspace = null;
        }

        public Collection<PSObject> GetCommand()
        {
            Collection<PSObject> rslt = null;
            String scrpt = "";
            Pipeline pline = rspace.CreatePipeline();
            if (System.IO.File.Exists(poshsecframework.Properties.Settings.Default.FrameworkPath))
            {                
                scrpt = "Import-Module \"$PSFramework\"" + Environment.NewLine;
            }
            else
            {
                frm.AddAlert("Can not locate the Framework file. Check your settings.", psmethods.PSAlert.AlertType.Error, "PoshSec Framework");
            }
            scrpt += "Get-Command";
            pline.Commands.AddScript(scrpt);
            rslt = pline.Invoke();
            pline.Dispose();
            GC.Collect();            
            return rslt;
        }
        
        public void RunScript()
        {
            InitializeSessionVars();
            PSAlert.ScriptName = scriptcommand.Replace(poshsecframework.Properties.Settings.Default.ScriptPath, "");
            if (scriptparams != null)
            {
                scriptparams.Clear();
            }
            Pipeline pline = null;
            bool cancelled = false;
            try
            {
                if (clicked)
                {
                    //Only run this if the user double clicked a script or command.
                    //If they typed the command then they should have passed params.
                    scriptparams = CheckForParams(rspace, scriptcommand);
                }                
                if (!cancel)
                {
                    Command pscmd = new Command(scriptcommand);
                    String cmdparams = "";
                    if (scriptparams != null)
                    {                        
                        foreach (psparameter param in scriptparams)
                        {
                            CommandParameter prm = new CommandParameter(param.Name, param.Value ?? param.DefaultValue);
                            pscmd.Parameters.Add(prm);
                            cmdparams += " -" + param.Name + " " + param.Value;
                        }
                    }

                    pline = rspace.CreatePipeline();
                    if (iscommand)
                    {
                        String cmdscript = "Import-Module \"$PSFramework\"" + Environment.NewLine + scriptcommand + cmdparams;
                        if (clicked)
                        {
                            rslts.AppendLine(scriptcommand + cmdparams);
                        }
                        pline.Commands.AddScript(cmdscript);
                        pline.Commands.Add("Out-String");
                    }
                    else
                    {
                        rslts.AppendLine("Running script: " + scriptcommand.Replace(poshsecframework.Properties.Settings.Default.ScriptPath, ""));                        
                        pline.Commands.Add(pscmd);
                    }                    
                    Collection<PSObject> rslt = pline.Invoke();
                    pline.Dispose();
                    pline = null;
                    if (rslt != null)
                    {
                        foreach (PSObject po in rslt)
                        {
                            if (po != null)
                            {
                                rslts.AppendLine(po.ToString());
                            }
                        }
                    }
                }
                else
                {
                    rslts.AppendLine("Script cancelled by user.");
                }
            }
            catch (ThreadAbortException thde)
            {
                if (pline != null)
                {
                    pline.Stop();
                    pline.Dispose();
                    pline = null;
                }
                GC.Collect();
                cancelled = true;
                if (iscommand)
                {
                    rslts.AppendLine("Command cancelled by user." + Environment.NewLine + thde.Message);
                }
                else
                {
                    rslts.AppendLine("Script cancelled by user." + Environment.NewLine + thde.Message);
                }                
            }
            catch (Exception e)
            {
                rslts.AppendLine(psexec.psexceptionhandler(e,iscommand));
            }
            finally
            {
                if (pline != null)
                {
                    pline.Dispose();
                }
                pline = null;
                GC.Collect();
                OnScriptComplete(new pseventargs(rslts.ToString(), scriptlvw, cancelled));
                rslts.Clear();
            }            
        }
        #endregion

        #region " Private Methods "
        private List<psparameter>CheckForParams(Runspace rspace, String scriptcommand)
        {
            cancel = false;
            List<psparameter> parms = null;
            psparamtype parm = new psparamtype();

            Pipeline pline = rspace.CreatePipeline();

            String scrpt = "Get-Help ";
            if (iscommand)
            {
                scrpt = "Import-Module \"$PSFramework\"" + Environment.NewLine + scrpt + scriptcommand + " -full";
            }
            else
            {
                scrpt += "\"" + scriptcommand + "\" -full";
            }
            pline.Commands.AddScript(scrpt);
            pline.Commands.Add("Out-String");

            Collection<PSObject> rslt = pline.Invoke();
            if (rslt != null)
            {
                if (rslt[0].ToString().Contains("PARAMETERS"))
                {
                    String[] lines = rslt[0].ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    if (lines != null)
                    {
                        int idx = 0;
                        bool found = false;
                        do
                        {
                            String line = lines[idx];
                            if(line == "PARAMETERS")
                            {
                                found = true;
                            }
                            idx++;
                        }while( found == false && idx < lines.Length);

                        if (found)
                        {
                            String line = "";
                            do
                            {
                                line = lines[idx];
                                if (line.Trim() != "" && line.Trim().Substring(0, 1) == "-")
                                {
                                    psparameter prm = new psparameter();
                                    String param = line.Trim().Substring(1, line.Trim().Length - 1);
                                    String[] paramparts = param.Split(' ');
                                    prm.Name = paramparts[0].Trim();
                                    if (paramparts.Length == 2)
                                    {
                                        prm.Type = GetTypeFromString(paramparts[1]);
                                    }
                                    else
                                    {
                                        prm.Type = typeof(int);
                                    }
                                    idx++;
                                    line = lines[idx];
                                    prm.Description = line.Trim();
                                    idx += 2;
                                    line = lines[idx];
                                    if (line.Contains("true"))
                                    { 
                                        prm.Category = "Required";
                                    }
                                    else
                                    {
                                        prm.Category = "Optional";
                                    }
                                    idx += 2;
                                    line = lines[idx];
                                    if (line.Contains("Default value"))
                                    {
                                        String defval = line.Replace("Default value", "").Trim();
                                        if (defval != "")
                                        {
                                            if (defval.ToLower() == "true" || defval.ToLower() == "false")
                                            {
                                                prm.DefaultValue = bool.Parse(defval);
                                            }
                                            else
                                            {
                                                prm.DefaultValue = defval;
                                            }
                                        }
                                    }                                    
                                    parm.Properties.Add(prm);
                                }
                                idx++;
                            }while(line.Substring(0,1) == " " && idx < lines.Length);
                        }
                    }
                }
            }
            pline.Stop();
            pline.Dispose();
            pline = null;
            GC.Collect();
            if (parm.Properties.Count > 0)
            {
                if (frm.ShowParams(parm) == System.Windows.Forms.DialogResult.OK)
                {
                    parms = parm.Properties;
                }
                else
                {
                    cancel = true;
                }
            }            
            return parms;
        }

        private Type GetTypeFromString(String typename)
        {
            Type rtn = null;
            switch (typename.ToLower())
            { 
                case "<string>":
                    rtn = typeof(string);
                    break;
                case "<boolean>":
                    rtn = typeof(Boolean);
                    break;
                case "<int32>":
                    rtn = typeof(int);
                    break;
                default:
                    rtn = typeof(Object);
                    break;
            }
            return rtn;
        }

        private void OnScriptComplete(pseventargs e)
        {
            EventHandler<pseventargs> handler = ScriptCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        #region " Public Properties "
        public String Script
        {
            set { this.scriptcommand = value;  }
        }

        public bool IsCommand
        {
            set { this.iscommand = value; }
        }

        public bool Clicked
        {
            set { this.clicked = value; }
        }

        public List<psparameter> Parameters
        {
            set { this.scriptparams = value; }
        }

        public String Results
        {
            get { return this.rslts.ToString(); }
        }

        public frmMain ParentForm
        {
            set { frm = value; }
        }

        public ListViewItem ScriptListView
        {
            set { scriptlvw = value; }
        }
        #endregion
    }
}
