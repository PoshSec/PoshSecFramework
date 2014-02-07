using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation.Host;
using System.Text;
using System.Threading.Tasks;

namespace poshsecframework.PShell
{
    class psfhostinterface : PSHostUserInterface
    {
        private Collection<String> warnings = new Collection<string>();
        public EventHandler<Events.WriteProgressEventArgs> WriteProgressUpdate;

        public void ClearWarnings()
        {
            warnings.Clear();
        }

        public override void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value)
        {
            throw new NotImplementedException();
        }

        public override void WriteDebugLine(string message)
        {
            throw new NotImplementedException();
        }

        public override void WriteLine()
        {
            base.WriteLine();
        }

        public override void Write(string value)
        {
            throw new NotImplementedException();
        }

        public override void WriteErrorLine(string value)
        {
            return;
        }

        public override void WriteLine(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value)
        {
            base.WriteLine(foregroundColor, backgroundColor, value);
        }

        public override void WriteLine(string value)
        {
            throw new NotImplementedException();
        }

        public override void WriteProgress(long sourceId, System.Management.Automation.ProgressRecord record)
        {
            OnWriteProgress(new Events.WriteProgressEventArgs(record));
        }

        private void OnWriteProgress(Events.WriteProgressEventArgs e)
        {
            EventHandler<Events.WriteProgressEventArgs> handler = WriteProgressUpdate;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public override void WriteVerboseLine(string message)
        {
            throw new NotImplementedException();
        }

        public override void WriteWarningLine(string message)
        {
            warnings.Add(message);
        }

        public override Dictionary<string, System.Management.Automation.PSObject> Prompt(string caption, string message, System.Collections.ObjectModel.Collection<FieldDescription> descriptions)
        {
            throw new NotImplementedException();
        }

        public override int PromptForChoice(string caption, string message, System.Collections.ObjectModel.Collection<ChoiceDescription> choices, int defaultChoice)
        {
            throw new NotImplementedException();
        }

        public override System.Management.Automation.PSCredential PromptForCredential(string caption, string message, string userName, string targetName)
        {
            throw new NotImplementedException();
        }

        public override System.Management.Automation.PSCredential PromptForCredential(string caption, string message, string userName, string targetName, System.Management.Automation.PSCredentialTypes allowedCredentialTypes, System.Management.Automation.PSCredentialUIOptions options)
        {
            throw new NotImplementedException();
        }

        public override PSHostRawUserInterface RawUI
        {
            get { return null; }
        }

        public override string ReadLine()
        {
            throw new NotImplementedException();
        }

        public override System.Security.SecureString ReadLineAsSecureString()
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Collection<String> Warnings
        {
            get { return warnings; }
        }
    }
}
