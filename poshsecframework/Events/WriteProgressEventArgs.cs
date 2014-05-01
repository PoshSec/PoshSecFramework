using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poshsecframework.Events
{
    class WriteProgressEventArgs : EventArgs
    {
        System.Management.Automation.ProgressRecord record = null;

        public WriteProgressEventArgs(System.Management.Automation.ProgressRecord Record)
        {
            record = Record;
        }

        public System.Management.Automation.ProgressRecord ProgressRecord
        {
            get { return record; }
        }
    }
}
