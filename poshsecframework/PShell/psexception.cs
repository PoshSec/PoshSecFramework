using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation.Runspaces;

namespace poshsecframework.PShell
{
    class psexception
    {
        #region " Public Methods "
        public String psexceptionhandler(Exception e, bool iscommand = true, CommandCollection commands = null)
        {
            String rtn = "";
            if (iscommand)
            {
                rtn += Environment.NewLine;
            }
            else
            {
                rtn += "Script Failed to Run!" + Environment.NewLine + Environment.NewLine;
            }
            switch (e.GetType().Name)
            { 
                case "PSSecurityException":
                    rtn += PSSecurityException((System.Management.Automation.PSSecurityException)e);
                    break;
                case "CmdletInvocationException":
                    rtn += CmdletInvocationException((System.Management.Automation.CmdletInvocationException)e);
                    break;
                case "ParameterBindingException":
                    rtn += ParameterBindingException((System.Management.Automation.ParameterBindingException)e);
                    break;
                case "ParameterBindingValidationException":
                    rtn += ParameterBindingValidationException((System.Management.Automation.ParameterBindingException)e);
                    break;
                case "CommandNotFoundException":
                    rtn += CommandNotFoundException((System.Management.Automation.CommandNotFoundException)e);
                    break;
                default:
                    rtn += PSException(e);
                    break;
            }
            return rtn;
        }
        #endregion

        #region
        private String PSException(Exception e)
        {
            String rtn = "";
            rtn += "Exception Type: " + e.GetType().ToString() + Environment.NewLine;
            rtn += "Message:" + Environment.NewLine;
            rtn += e.Message.ToString() + Environment.NewLine;
            rtn += "Stack Trace:" + Environment.NewLine;
            rtn += e.StackTrace.ToString();
            return rtn;
        }

        private String CommandNotFoundException(System.Management.Automation.CommandNotFoundException e)
        {
            String rtn = e.Message.ToString();
            return rtn;
        }

        private String PSSecurityException(System.Management.Automation.PSSecurityException e)
        {
            String rtn = "";
            rtn += "You need to run the following command from a PowerShell prompt with administrative privileges:" + Environment.NewLine + Environment.NewLine;
            rtn += "Set-ExecutionPolicy RemoteSigned" + Environment.NewLine + Environment.NewLine;
            rtn += "When asked: " + Environment.NewLine + "Do you want to change the execution policy?" + Environment.NewLine + "[Y] Yes  [N] No  [S] Suspend  [?] Help (default is \"Y\"):" + Environment.NewLine + Environment.NewLine + "Type Y and press enter." + Environment.NewLine + Environment.NewLine;
            rtn += "Error Message:" + Environment.NewLine;
            rtn += e.Message.ToString();
            return rtn;
        }

        private String CmdletInvocationException(System.Management.Automation.CmdletInvocationException e, CommandCollection commands = null)
        {
            String rtn = "";
            rtn += "There was an error in your script or command. Please see the error message below." + Environment.NewLine + Environment.NewLine;
            rtn += "Error Message:" + Environment.NewLine;
            rtn += e.Message.ToString() + Environment.NewLine;
            rtn += e.ErrorRecord.ScriptStackTrace.Replace(Strings.StringValue.ScriptBlockNoFile, "");
            if (commands != null)
            {
                rtn += "Commands: " + Environment.NewLine;
                foreach (Command cmd in commands)
                {
                    rtn += cmd.CommandText.ToString() + Environment.NewLine;
                }
            }
            return rtn;
        }

        private String ParameterBindingException(System.Management.Automation.ParameterBindingException e)
        {
            String rtn = "";
            rtn += "Missing Parameters!" + Environment.NewLine + Environment.NewLine;
            rtn += "Please check the command or script file and ensure that the parameters are defined properly." + Environment.NewLine + Environment.NewLine;
            rtn += "Error Message:" + Environment.NewLine;
            rtn += e.Message.ToString();
            return rtn;
        }

        private String ParameterBindingValidationException(System.Management.Automation.ParameterBindingException e)
        {
            String rtn = "";
            rtn += "Invalid parameter value!" + Environment.NewLine + Environment.NewLine;
            rtn += "You entered an invalid parameter value. Please see the error message below." + Environment.NewLine + Environment.NewLine;
            rtn += "Error Message:" + Environment.NewLine;
            rtn += e.Message.ToString();
            return rtn;
        }
        #endregion
    }
}
