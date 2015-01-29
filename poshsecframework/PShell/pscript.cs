using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using poshsecframework.Strings;

namespace poshsecframework.PShell
{
    class pscript
    {
        #region " Private Variables "
        private RunspaceConfiguration rspaceconfig;
        private Runspace rspace;
        private psfhost host;
        private psfhostinterface hostinterface;
        private String scriptcommand;
        private bool iscommand = false;
        private bool clicked = true;
        private bool scheduled = false;
        private List<psparameter> scriptparams = new List<psparameter>();
        private StringBuilder rslts = new StringBuilder();
        private psexception psexec = new psexception();
        private bool cancel = false;
        private frmMain frm = null;
        private ListViewItem scriptlvw;
        private psvariables.PSRoot PSRoot;
        private psvariables.PSModRoot PSModRoot;
        private psvariables.PSFramework PSFramework;
        private psvariables.PSExec PSExec;
        private psmethods.PSMessageBox PSMessageBox;
        private psmethods.PSAlert PSAlert;
        private psmethods.PSStatus PSStatus;
        private psmethods.PSHosts PSHosts;
        private psmethods.PSTab PSTab;
        private String loaderrors = "";
        #endregion

        #region " Public Events "
        public EventHandler<pseventargs> ScriptCompleted;
        #endregion

        #region " Private Methods "
        private void InitializeScript()
        {
            rspaceconfig = RunspaceConfiguration.Create();
            host = new psfhost(frm);
            hostinterface = (psfhostinterface)host.UI;
            hostinterface.WriteProgressUpdate += WriteProgressUpdate;
            hostinterface.WriteUpdate += WriteUpdate;
            rspace = RunspaceFactory.CreateRunspace(host, rspaceconfig);
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
                PSExec = new psvariables.PSExec("PSExec");
                PSMessageBox = new psmethods.PSMessageBox();
                PSTab = new psmethods.PSTab(frm);
                PSHosts = new psmethods.PSHosts(frm);
                rspace.SessionStateProxy.PSVariable.Set(PSRoot);
                rspace.SessionStateProxy.PSVariable.Set(PSModRoot);
                rspace.SessionStateProxy.PSVariable.Set(PSFramework);
                rspace.SessionStateProxy.PSVariable.Set(PSExec);
                rspace.SessionStateProxy.SetVariable("PSMessageBox", PSMessageBox);
                rspace.SessionStateProxy.SetVariable("PSAlert", PSAlert);
                rspace.SessionStateProxy.SetVariable("PSStatus", PSStatus);
                rspace.SessionStateProxy.SetVariable("PSHosts", PSHosts);
                rspace.SessionStateProxy.SetVariable("PSTab", PSTab);
            }
        }

        public void Close()
        {
            rspace.Close();
            do
            {
                System.Threading.Thread.Sleep(100);
            } while (rspace.RunspaceStateInfo.State != RunspaceState.Closed);
            rspace.Dispose();
            rspace = null;
            GC.Collect();
        }

        public void ImportPSModules(Collection<String> enabledmods)
        {
            if (enabledmods != null & enabledmods.Count() > 0)
            {
                Pipeline pline = rspace.CreatePipeline();
                String script = "";
                String namecheck = "";
                if (!Properties.Settings.Default.NameChecking)
                {
                    namecheck = " -DisableNameChecking";
                }
                foreach (String mod in enabledmods)
                {
                    script += "Import-Module \"" + mod + "\"" + namecheck + "\r\n";
                }
                pline.Commands.AddScript(script + StringValue.WriteError);
                Collection<PSObject> rslt = pline.Invoke();
                HandleWarningsErrors(pline.Error);
                pline.Dispose();
                pline = null;
                if (rslt != null && rslt.Count > 0)
                {
                    foreach (PSObject po in rslt)
                    {
                        if (po != null)
                        {
                            rslts.AppendLine(po.ToString());
                        }
                    }
                    loaderrors += rslts.ToString();
                }
            }           
        }

        private void InvokeCommand(string command)
        {
            Collection<PSObject> rslt = null;
            Pipeline pline = rspace.CreatePipeline();
            pline.Commands.AddScript(command);           
            try
            {
                rslt = pline.Invoke();
            }
            catch (Exception e)
            {
                rslts.AppendLine(e.Message);
            }
            finally
            {
                HandleWarningsErrors(pline.Error);
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
                GC.Collect();
            }            
        }
        #endregion

        #region " Public Methods "
        public pscript(frmMain ParentForm)
        {            
            try
            {
                frm = ParentForm;
                InitializeScript();
            }
            catch (Exception e)
            {
                //Base Exception Handler
                OnScriptComplete(new pseventargs(StringValue.UnhandledException + Environment.NewLine + e.Message + Environment.NewLine + "Stack Trace:" + Environment.NewLine + e.StackTrace, null, false));
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
            rslts.Clear();
            Collection<PSObject> rslt = null;
            String scrpt = "";
            Pipeline pline = rspace.CreatePipeline();
            scrpt = StringValue.GetCommand;
            pline.Commands.AddScript(scrpt);
            try
            {
                rslt = pline.Invoke();
            }
            catch (Exception e)
            {
                rslts.AppendLine(e.Message);
            }
            finally
            {
                HandleWarningsErrors(pline.Error);
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
                GC.Collect();
            }                        
            return rslt;
        }

        public bool UnblockFiles(string FolderPath)
        {
            bool rtn = true;
            rslts.Clear();

            if (Directory.Exists(FolderPath))
            {
                string[] files = null;
                try
                {
                    files = Directory.GetFiles(FolderPath, "*.*", SearchOption.AllDirectories);
                }
                catch (Exception e)
                {
                    rslts.AppendLine(e.Message);
                }
                if (files != null && files.Count() > 0)
                {
                    string script = "";
                    foreach (string file in files)
                    {
                        script += "Unblock-File -path \"" + file + "\"\r\n";
                    }
                    InvokeCommand(script);
                    if (rslts.ToString().Trim() != "")
                    {
                        rtn = false;
                    }
                }
            }
            else
            {
                rslts.AppendLine("The path " + FolderPath + " does not exist.");
                rtn = false;
            }
            return rtn;
        }

        public bool SetExecutionPolicy()
        {
            bool rtn = false;
            InvokeCommand(StringValue.SetExecutionPolicy);
            if (rslts.ToString().Trim() == "")
            {
                rtn = true;
            }
            return rtn;
        }

        public bool UpdateHelp()
        {
            bool rtn = false;
            InvokeCommand(StringValue.UpdateHelp);
            if (rslts.ToString().Trim() == "")
            {
                rtn = true;
            }
            return rtn;
        }

        public void RunScript()
        {
            cancel = false;
            rslts.Clear();
            InitializeSessionVars();
            PSAlert.ScriptName = scriptcommand.Replace(poshsecframework.Properties.Settings.Default.ScriptPath, "");
            PSTab.ScriptName = scriptcommand.Replace(poshsecframework.Properties.Settings.Default.ScriptPath, "");
            Pipeline pline = null;
            bool cancelled = false;
            try
            {
                if (clicked && !scheduled)
                {
                    //Only run this if the user double clicked a script or command.
                    //If they typed the command then they should have passed params.
                    //If it was scheduled and there were params, they should be passed.
                    scriptparams = CheckForParams(scriptcommand);
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
                        String cmdscript = scriptcommand + cmdparams;
                        if (clicked)
                        {
                            rslts.AppendLine(scriptcommand + cmdparams);
                        }
                        pline.Commands.AddScript(cmdscript);
                        pline.Commands.Add(StringValue.OutString);
                    }
                    else
                    {
                        rslts.AppendLine("Running script: " + scriptcommand.Replace(poshsecframework.Properties.Settings.Default.ScriptPath, ""));
                        pline.Commands.Add(pscmd);
                    }
                    Collection<PSObject> rslt = null;
                    try
                    {
                        rslt = pline.Invoke();
                    }
                    catch (System.Threading.ThreadAbortException)
                    {
                        if (pline != null)
                        {
                            HandleWarningsErrors(pline.Error);
                        }
                        cancelled = true;
                        if (iscommand)
                        {
                            rslts.AppendLine(StringValue.CommandCancelled);
                        }
                        else
                        {
                            rslts.AppendLine(StringValue.ScriptCancelled);
                        }
                    }
                    catch (Exception pex)
                    {
                        rslts.AppendLine(psexec.psexceptionhandler(pex, iscommand, pline.Commands));
                    }
                    HandleWarningsErrors(pline.Error);
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
                    rslts.AppendLine(StringValue.ScriptCancelled);
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
                if (pline != null)
                {
                    pline.Stop();
                    pline.Dispose();
                    pline = null;
                }
                GC.Collect();
            }
            catch (Exception e)
            {
                if (pline != null)
                {
                    HandleWarningsErrors(pline.Error);
                }                
                rslts.AppendLine(psexec.psexceptionhandler(e, iscommand));
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

        public List<psparameter> CheckForParams(String scriptcommand)
        {
            cancel = false;
            List<psparameter> parms = null;
            psparamtype parm = new psparamtype();

            Pipeline pline = rspace.CreatePipeline();

            String scrpt = "";
            if (iscommand)
            {
                scrpt = StringValue.GetHelpFull.Replace("{0}", scriptcommand);
            }
            else
            {
                scrpt = StringValue.GetHelpFull.Replace("{0}", "\"" + scriptcommand + "\"");
            }
            pline.Commands.AddScript(scrpt);
            pline.Commands.Add(StringValue.OutString);

            Collection<PSObject> rslt = pline.Invoke();
            HandleWarningsErrors(pline.Error);
            if (rslt != null)
            {
                if (rslt[0].ToString().Contains("PARAMETERS"))
                {
                    String[] lines = rslt[0].ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    if (lines != null)
                    {
                        int idx = 0;
                        bool found = false;
                        List<String> fileparams = GetEditorParams(rslt[0].ToString(), "psfilename");
                        List<String> hostparams = GetEditorParams(rslt[0].ToString(), "pshosts");                        
                        do
                        {
                            String line = lines[idx];
                            if (line == "PARAMETERS")
                            {
                                found = true;
                            }
                            idx++;
                        } while (found == false && idx < lines.Length);

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
                                    if (fileparams.Contains(prm.Name) || prm.Name.ToLower() == "file" || prm.Name.ToLower() == "filename")
                                    {
                                        prm.IsFileName = true;
                                    }
                                    if (hostparams.Contains(prm.Name))
                                    {
                                        prm.IsHostList = true;
                                    }
                                    if (prm.Name.ToLower() == "file" || prm.Name.ToLower() == "filename")
                                    {
                                        prm.IsFileName = true;
                                    }
                                    if (prm.Name.ToLower() == "credential")
                                    {
                                        prm.IsCredential = true;
                                    }
                                    parm.Properties.Add(prm);
                                }
                                idx++;
                            } while (line.Substring(0, 1) == " " && idx < lines.Length);
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
        #endregion

        #region " Private Methods "
        private void HandleWarningsErrors(PipelineReader<object> pipelineerrors)
        {
            foreach (string warning in hostinterface.Warnings)
            {
                PSAlert.Add(warning, psmethods.PSAlert.AlertType.Warning);
            }
            if (pipelineerrors.Count > 0)
            {
                Collection<object> errors = pipelineerrors.ReadToEnd();
                foreach (object error in errors)
                {
                    PSAlert.Add(error.ToString(), psmethods.PSAlert.AlertType.Error);
                }
            }
            hostinterface.ClearWarnings();
        }

        private void WriteProgressUpdate(object sender, Events.WriteProgressEventArgs e)
        {
            PSStatus.Update(e.ProgressRecord.StatusDescription);
            PSStatus.WriteProgress(e.ProgressRecord.PercentComplete.ToString() + "%");
        }

        private void WriteUpdate(object sender, Events.WriteEventArgs e)
        {
            rslts.AppendLine(e.Message);
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
                case "<psobject>":
                    rtn = typeof(PSObject);
                    break;
                case "[<switchparameter>]":
                    rtn = typeof(bool);
                    break;
                default:
                    rtn = typeof(Object);
                    break;
            }
            return rtn;
        }

        private List<String> GetEditorParams(String helptext, String identifier)
        {
            List<String> rtn = new List<string>();
            identifier += "=";
            if (helptext.Contains(identifier))
            {
                int fnidx = helptext.IndexOf(identifier);
                int fnendidx = helptext.IndexOf(" ", fnidx);
                String fnparms = helptext.Substring(fnidx, fnendidx - fnidx);
                fnparms = fnparms.Replace("\r\n", "").Replace(identifier, "");
                if (fnparms.Trim() != "")
                {
                    String[] prms = fnparms.Split(',');
                    if (prms != null && prms.Length > 0)
                    {
                        foreach (String prm in prms)
                        {
                            rtn.Add(prm);
                        }
                    }
                }
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

        public bool IsScheduled
        {
            set { this.scheduled = value; }
        }

        public bool Clicked
        {
            set { this.clicked = value; }
        }

        public List<psparameter> Parameters
        {
            set { this.scriptparams = value; }
            get { return this.scriptparams; }
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

        public bool ParamSelectionCancelled
        {
            get { return cancel; }
        }

        public String LoadErrors
        {
            get { return loaderrors; }
        }
        #endregion
    }
}
