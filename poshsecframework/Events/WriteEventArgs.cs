using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshSec.Framework.Events
{
    class WriteEventArgs: EventArgs
    {
        String message = "";

        public WriteEventArgs(String Message)
        {
            message = Message;
        }

        public String Message
        {
            get { return message; }
        }
    }
}
