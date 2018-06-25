using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using PoshSec.Framework.Properties;

namespace PoshSec.Framework.PShell
{
    class psvariables
    {
        public class PSRoot : PSVariable
        {
            private String psroot = Settings.Default.ScriptPath;

            public PSRoot(string name): base(name) { }

            public override Object Value
            {
                get { return psroot; }
            }
        }

        public class PSModRoot : PSVariable
        {
            private String psmodroot = Settings.Default.ModulePath;

            public PSModRoot(string name) : base(name) { }

            public override Object Value
            {
                get { return psmodroot; }
            }
        }

        public class PSFramework : PSVariable
        {
            private String psf = Settings.Default.GithubAPIKey;

            public PSFramework(string name) : base(name) { }

            public override Object Value
            {
                get { return psf; }
            }
        }

        public class PSExec : PSVariable
        {
            private String psexec = Settings.Default.PSExecPath;

            public PSExec(string name) : base(name) { }

            public override Object Value
            {
                get { return psexec; }
            }
        }

        public class PSScheduleFile : PSVariable
        {
            private String schedulefile = Settings.Default.ScheduleFile;

            public PSScheduleFile(string name) : base(name) { }

            public override Object Value
            {
                get { return schedulefile; }
            }
        }
    }

}
