using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Host;
using System.Text;
using System.Threading.Tasks;

namespace poshsecframework.PShell
{
    class psfhost : PSHost
    {
        private psfhostinterface psfi = new psfhostinterface();
        private Guid gid = Guid.NewGuid();
        private System.Globalization.CultureInfo originalCultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
        private System.Globalization.CultureInfo originalUICultureInfo = System.Threading.Thread.CurrentThread.CurrentUICulture;        

        public psfhost()
        {

        }

        public override void EnterNestedPrompt()
        {
            System.Windows.Forms.MessageBox.Show("Enter nested prompt");
            //throw new NotImplementedException();
        }

        public override void ExitNestedPrompt()
        {
            System.Windows.Forms.MessageBox.Show("Exit nested prompt");
            //throw new NotImplementedException();
        }

        public override void NotifyBeginApplication()
        {
            return;
        }

        public override void NotifyEndApplication()
        {
            return;
        }

        public override void SetShouldExit(int exitCode)
        {
            
        }

        public override PSHostUserInterface UI
        {
            get { return psfi; }
        }

        public override System.Globalization.CultureInfo CurrentCulture
        {
            get { return originalCultureInfo; }
        }

        public override System.Globalization.CultureInfo CurrentUICulture
        {
            get { return originalUICultureInfo; }
        }

        public override Guid InstanceId
        {
            get { return gid; }
        }

        public override string Name
        {
            get { return "PoshSec Framework"; }
        }

        public override System.Management.Automation.PSObject PrivateData
        {
            get
            {
                return base.PrivateData;
            }
        }

        public override System.Version Version
        {
            get { return new System.Version(0, 2, 0, 0); }
        }

    }
}
