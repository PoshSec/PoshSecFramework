using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace psframework.PShell
{
    class psvariables
    {
        public class PSRoot : PSVariable
        {
            private String psroot = poshsecframework.Properties.Settings.Default.ScriptPath;

            public PSRoot(string name): base(name) { }

            public override Object Value
            {
                get { return psroot; }
            }
        }

        public class PSModRoot : PSVariable
        {
            private String psmodroot = poshsecframework.Properties.Settings.Default.ModulePath;

            public PSModRoot(string name) : base(name) { }

            public override Object Value
            {
                get { return psmodroot; }
            }
        }

        public class PSFramework : PSVariable
        {
            private String psf = poshsecframework.Properties.Settings.Default.FrameworkPath;

            public PSFramework(string name) : base(name) { }

            public override Object Value
            {
                get { return psf; }
            }
        }

        public class PSExec : PSVariable
        {
            private String psexec = poshsecframework.Properties.Settings.Default.PSExecPath;

            public PSExec(string name) : base(name) { }

            public override Object Value
            {
                get { return psexec; }
            }
        }
    }

}
